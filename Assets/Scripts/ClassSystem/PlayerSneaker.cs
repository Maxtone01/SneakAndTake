using Assets.Scripts.Interfaces;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ClassSystem
{
    public class PlayerSneaker : PlayerGeneral
    {
        private ISkill _turnToBox;

        private void Awake()
        {
            _turnToBox = GetComponent<ISkill>();
        }

        private void Update()
        {
            if (GameContext.Instance.PauseManager.IsPaused)
            {
                return;
            }
            PlayerClassAction();
            PerformInputs();
            SpeedSetup();
            ModelRotation();
        }

        protected override void PlayerClassAction()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                _turnToBox.SkillAction(PlayerStats.Inteligence);
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
}