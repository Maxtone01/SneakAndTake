using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ClassSystem
{
    public class PlayerGuard : PlayerGeneral
    {

        public void Update()
        {
            Inputs();
            SpeedSetup();
            ModelRotationSetUp();
        }

        protected override void PlayerClassAction()
        {
            throw new System.NotImplementedException();
        }
    }
}