using UnityEngine;
using Assets.Scripts.Inventory;
using System;
using Assets.Scripts.ClassSystem;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerActions), 
        typeof(PlayerMovement))]
    public abstract class PlayerGeneral : MonoBehaviour
    {
        #region Variables
        [SerializeField] private protected PlayerActions _playerActions;

        [SerializeField] private protected PlayerMovement _playerMovement;

        [SerializeField] private protected QuestList questList;

        [SerializeField] private protected GameObject tooltipGameObject;

        public Transform cam;

        [SerializeField] public float _movementForce = 2;

        [SerializeField] private protected float turnSmoothVelocity;

        [SerializeField] private protected float turnSmoothTime = 0.01f;

        [SerializeField] private protected Quest quest;

        [SerializeField] private protected Animator _animator;

        [SerializeField] private protected ConversantController characterConversant;

        [SerializeField] private protected DialogueUI dialoguePanel;

        private  Vector2 input;

        private Vector2 inputDir;

        public float _speed;

        private bool isTooltipActive = false;

        private QuestTooltipUi _questTooltip;

        [NonSerialized] public bool canGrab;

        [NonSerialized] protected bool isGrabbed;

        [NonSerialized] protected bool canPlace;

        [SerializeField]
        private float _rotationSpeed;

        public PlayerStats PlayerStats { get; private set; }

        public PlayerClass PlayerClass { get; private set; }

        #endregion

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _questTooltip = tooltipGameObject.GetComponent<QuestTooltipUi>();
        }

        public void SetUpPlayerStats(PlayerStats playerStats, PlayerClass playerClass)
        {
            PlayerStats = playerStats;
            PlayerClass = playerClass;
        }

        private protected void SpeedSetup()
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            inputDir = input.normalized;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _speed = 15;
            }

            else
            {
                _speed = _movementForce * inputDir.magnitude;
            }

            PerformMovementActions();
        }

        private void PerformMovementActions()
        {
            if (_speed == 0 & !Input.GetKey(KeyCode.LeftControl))
            {
                _playerActions.Idle(_animator, isGrabbed);
            }
            else if (_speed > 0 & !Input.GetKey(KeyCode.LeftControl))
            {
                _playerMovement.Walk(_speed, _animator, isGrabbed, inputDir, cam);
            }
            else
            {
                _playerMovement.Run(_speed, _animator);
            }
        }

        protected virtual void ModelRotation()
        {
            if (inputDir != Vector2.zero)
            {
                Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                Camera.main.transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z);
                transform.rotation = newRotation;
            }
        }

        private protected void PerformInputs()
        {
            if (Input.GetKey(KeyCode.LeftControl) & _speed == 0)
            {
                Debug.Log("Crouch");
                _playerMovement.CrouchIdle(_animator);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                dialoguePanel.conversantController.QuitDialogue();
            }

            if (Input.GetKey(KeyCode.LeftControl) & _speed > 0)
            {
                _playerMovement.CrouchWalk(_speed, _animator);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                OpenQuestMenu();
            }
        }

        private void OpenQuestMenu()
        {
            if (!isTooltipActive)
            {
                isTooltipActive = true;
                tooltipGameObject.SetActive(true);
                foreach (QuestStates questState in questList.GetQuests())
                {
                    _questTooltip.Setup(questState);
                }
            }
            else
            {
                isTooltipActive = false;
                tooltipGameObject.SetActive(false);
            }
        }

        protected abstract void PlayerClassAction();
    }
}