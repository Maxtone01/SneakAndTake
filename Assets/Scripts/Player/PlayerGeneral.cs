using UnityEngine;
using Assets.Scripts.Inventory;
using Cinemachine;
using Assets.Scripts.Interfaces;
using System;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerActions), typeof(PlayerMovement))]
    public abstract class PlayerGeneral : MonoBehaviour
    {
        #region Variables
        [SerializeField] protected private PlayerActions _playerActions;

        [SerializeField] protected private PlayerMovement _playerMovement;

        [SerializeField] protected QuestList questList;

        [SerializeField] protected GameObject tooltipGameObject;

        public InventoryObject inventory;

        public InventoryUI inventoryUI;

        public Transform cam;

        [SerializeField] public float _movementForce = 2;

        [SerializeField] private float turnSmoothVelocity;

        [SerializeField] private float turnSmoothTime = 0.01f;

        [SerializeField] private Quest quest;

        [SerializeField] protected private Animator _animator;

        [SerializeField] ConversantController characterConversant;

        private Vector2 input;

        private Vector2 inputDir;

        public float _speed;

        protected private bool isTooltipActive = false;

        protected private QuestTooltipUi _questTooltip;

        [NonSerialized] public bool canGrab;

        [NonSerialized] public bool isGrabbed;

        [NonSerialized] public bool canPlace;

        [SerializeField]
        private float _rotationSpeed;

        #endregion

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _playerActions = GetComponent<PlayerActions>();
            _playerMovement = GetComponent<PlayerMovement>();
            _questTooltip = tooltipGameObject.GetComponent<QuestTooltipUi>();
            _animator = GetComponent<Animator>();
        }

        protected private void SpeedSetup()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _speed = 15;
            }

            else
            {
                _speed = _movementForce * inputDir.magnitude;
            }

            switch (_speed)
            {
                case 0:
                    if (!Input.GetKey(KeyCode.LeftControl))
                    {
                        _playerActions.Idle(_animator, isGrabbed);
                    }
                    break;
                case 6:
                    if (!Input.GetKey(KeyCode.LeftControl))
                    {
                        _playerMovement.Walk(_speed, _animator, isGrabbed, inputDir.y, cam);
                    }
                    break;
                case 15:
                    _playerMovement.Run(_speed, _animator);
                    break;
            }
        }

        protected private void ModelRotationSetUp()
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            inputDir = input.normalized;

            if (inputDir != Vector2.zero)
            {
                Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                Camera.main.transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z);
                transform.rotation = newRotation;
            }
        }

        protected private void Inputs()
        {
            if (Input.GetKey(KeyCode.LeftControl) & _speed == 0)
            {
                Debug.Log("Crouch");
                _playerMovement.CrouchIdle(_animator);
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