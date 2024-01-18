using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public void Walk(float speed, Animator animator, bool isGrabbed, Vector2 inputDir, Transform cam)
        {
            if (inputDir.y == -1)
            {
                transform.Translate(speed * Time.deltaTime * -transform.forward.normalized, Space.World);
            }
            if (inputDir.y == 1)
            {
                transform.Translate(speed * Time.deltaTime * transform.forward, Space.World);
            }
            if (inputDir.x == 1)
            {
                transform.Translate(speed * Time.deltaTime * transform.right.normalized, Space.World);
            }
            if (inputDir.x == -1)
            {
                transform.Translate(speed * Time.deltaTime * -transform.right, Space.World);
            }
            if (inputDir.x >= 0.7f & inputDir.y >= 0.7f)
            {
                transform.Translate(speed * Time.deltaTime * (transform.forward + transform.right).normalized, Space.World);
            }
            if (inputDir.x <= -0.7f & inputDir.y >= 0.7f)
            {
                transform.Translate(speed * Time.deltaTime * (transform.forward - transform.right).normalized, Space.World);
            }
            if (inputDir.x <= -0.7f & inputDir.y <= -0.7f)
            {
                transform.Translate(speed * Time.deltaTime * (-transform.forward - transform.right).normalized, Space.World);
            }
            if (inputDir.x >= 0.7f & inputDir.y <= -0.7f)
            {
                transform.Translate(speed * Time.deltaTime * (-transform.forward + transform.right).normalized, Space.World);
            }
            if (!isGrabbed)
            {
                animator.SetFloat("State", 3);
            }
            else
            {
                animator.SetFloat("State", 4);
            }
        }

        public void Run(float speed, Animator animator)
        {
            transform.Translate(transform.forward * (speed * Time.deltaTime), Space.World);
            animator.SetFloat("State", 8);
        }

        public void CrouchWalk(float speed, Animator animator)
        {
            transform.Translate(transform.forward * (speed * Time.deltaTime), Space.World);
            animator.SetFloat("State", 4);
        }

        public void CrouchIdle(Animator animator)
        {
            animator.SetFloat("State", 5);
        }
    }
}