using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterConversant : MonoBehaviour
{
    #region Variables
    PlayerActions dialogueController;
    [SerializeField] 
    Dialogue dialogue = null;
    [SerializeField]
    private float viewRadius;
    [SerializeField]
    string characterName;
    public LayerMask _playerMask;
    [SerializeField] Animator _animator;
    #endregion

    public void StartDialogue(PlayerActions dialogueController)
    {
        dialogueController.GetComponent<ConversantController>().StartDialogue(this, dialogue);
    }

    public void EndDialogue(PlayerActions dialogueController)
    {
        dialogueController.GetComponent<ConversantController>().QuitDialogue();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            dialogueController = collider.gameObject.GetComponent<PlayerActions>();
            StartDialogue(dialogueController);
            _animator.SetFloat("State", 0);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            EndDialogue(dialogueController);
            dialogueController = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, viewRadius);
    }

    public void TestExitAction()
    {
        Debug.Log("Exit!");
    }

    public void TestEnterAction()
    {
        Debug.Log("Enter!!");
    }

    public string GetName()
    {
        return characterName;
    }
}
