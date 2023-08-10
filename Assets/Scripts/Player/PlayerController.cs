using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Inventory;
using Cinemachine;
using Assets.Scripts.Interfaces;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;

    [SerializeField] private PlayerActions _playerActions;

    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] QuestList questList;

    [SerializeField] GameObject tooltipGameObject;

    [SerializeField] QuestCompletion qCompl;

    public InventoryObject inventory;

    public InventoryUI inventoryUI;

    public Transform cam;

    [SerializeField] public float _movementForce = 2;

    [SerializeField] private float turnSmoothVelocity = 0.2f;

    [SerializeField] private float turnSmoothTime = 0.1f;

    [SerializeField] private Quest quest;

    [SerializeField] private Animator _animator;

    [SerializeField] ConversantController characterConversant;

    [SerializeField] private Box _boxObserver;

    [SerializeField] private PlacableZone _placableObserver;

    private float number = 1;

    private Vector2 inputDir;

    public float _speed;

    private bool isTooltipActive = false;

    private QuestTooltipUi _questTooltip;

    private Vector2 input;

    [NonSerialized] public bool canGrab;

    [NonSerialized] public bool isGrabbed;

    [NonSerialized] public bool canPlace;

    private ISkill _turnToBox;

    [SerializeField]
    public GameObject _modelToSwap;


    #endregion

    private void Awake()
    {
        _boxObserver.onTriggerEnter += OnTriggerBox;
        _boxObserver.onTriggerExit += OnTriggerExitBox;
        _placableObserver.onTriggerEnter += OnPlacableEnter;
        _placableObserver.onTriggerExit += OnPlacableExit;
    }

    private void OnDestroy()
    {
        _boxObserver.onTriggerEnter -= OnTriggerBox;
        _boxObserver.onTriggerExit -= OnTriggerExitBox;
        _placableObserver.onTriggerEnter -= OnPlacableEnter;
        _placableObserver.onTriggerExit -= OnPlacableExit;
    }

    private void Start()
    {
        _playerActions = GetComponent<PlayerActions>();
        _playerMovement = GetComponent<PlayerMovement>();
        _questTooltip = tooltipGameObject.GetComponent<QuestTooltipUi>();
        _animator = GetComponent<Animator>();
        _turnToBox = GetComponent<ISkill>();
    }

    private void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 15;
        }

        else
        {
            _speed = _movementForce * inputDir.magnitude;
        }

        Inputs();

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
                    _playerMovement.Walk(_speed, _animator, isGrabbed);
                }
                break;
            case 15:
                _playerMovement.Run(_speed, _animator);
                break;
        }
    }

    private void Inputs()
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

        if (Input.GetMouseButtonDown(0))
        {
            _playerActions.SelectNextDialogue();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            _turnToBox.SkillAction();
        }

        if (Input.GetKey(KeyCode.E) & canGrab)
        {
            _playerActions.GrabItem(_animator);
            isGrabbed = true;
        }

        if (Input.GetKey(KeyCode.E) & canPlace)
        {
            _playerActions.PlaceItem(_animator);
            isGrabbed = false;
        }
    }

    private void OnTriggerBox(Box obj)
    {
        canGrab = true;
        _playerActions._grabbableItem = obj.gameObject;
    }
    private void OnTriggerExitBox(Box obj)
    {
        canGrab = false;
        _playerActions._grabbableItem = null;
    }

    private void OnPlacableEnter(PlacableZone obj)
    {
        canPlace = true;
        Debug.Log(obj.name);
    }

    private void OnPlacableExit(PlacableZone obj)
    {
        canPlace = false;
        Debug.Log(obj.name);
    }

    public void OpenQuestMenu()
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

    #region Observers
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Collectable")
    //    {
    //        inventory.AddItem(collision.gameObject.GetComponent<ItemController>()._itemSO, 1);
    //        inventoryUI.UpdateUI();
    //        Destroy(collision.gameObject);
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "HidableObject")
    //    {
    //        other.gameObject.GetComponentInChildren<Canvas>().enabled = true;
    //    }

    //    //if (other.gameObject.tag == "PlaceZone")
    //    //{
    //    //    canPlace = true;
    //    //}
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "HidableObject")
    //    {
    //        other.gameObject.GetComponentInChildren<Canvas>().enabled = false;
    //    }
    //    //if (other.gameObject.tag == "Grabbable")
    //    //{
    //    //    canGrab = false;
    //    //}
    //    if (other.gameObject.tag == "PlaceZone")
    //    {
    //        canPlace = true;
    //    }
    //}
    #endregion

}
