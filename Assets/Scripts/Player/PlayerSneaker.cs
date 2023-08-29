using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSneaker : PlayerGeneral
{
    private ISkill _turnToBox;

    private void Start()
    {
        _turnToBox = GetComponent<ISkill>();
    }

    private void Update()
    {
        PlayerClassAction();
        Inputs();
        SpeedSetup();
    }

    protected override void PlayerClassAction()
    {
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

    public void SetGrabState(bool grab)
    {
        canGrab = grab;
    }

    public void SetPlaceState(bool place)
    {
        canPlace = place;
    }
}
