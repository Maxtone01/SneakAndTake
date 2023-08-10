using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public void Walk(float speed, Animator animator, bool isGrabbed)
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
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
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        animator.SetFloat("State", 8);
    }

    public void CrouchWalk(float speed, Animator animator)
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        animator.SetFloat("State", 4);
    }

    public void CrouchIdle(Animator animator)
    {
        animator.SetFloat("State", 5);
    }
}
