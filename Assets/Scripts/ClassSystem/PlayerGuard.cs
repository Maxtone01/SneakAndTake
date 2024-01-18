using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.ClassSystem
{
    public class PlayerGuard : PlayerGeneral
    {
        protected override void ModelRotation()
        {
            Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                cam.transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z);
            transform.rotation = newRotation;
        }

        protected override void PlayerClassAction()
        {
            throw new System.NotImplementedException();
        }
    }
}