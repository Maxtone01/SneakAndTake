using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConversantController : MonoBehaviour
{
    #region Variables
    [SerializeField]
    string playerName;
    Dialogue dialogueTree;
    DialogueNode currentNode = null;
    CharacterConversant currentConversant = null;
    bool isChoosing = false;

    public event Action OnConversantUpdate;
    #endregion

    public void StartDialogue(CharacterConversant newConversant, Dialogue newDialogue)
    {
        currentConversant = newConversant;
        dialogueTree = newDialogue;
        currentNode = dialogueTree.GetRootNode();
        TriggerEnterAction();

        OnConversantUpdate();
        //GameManager.Instance.MouseState(States.GameStates.Enter_Dialogue);
    }

    public void SelectNextDialogueVariant()
    {
        int numResponses = dialogueTree.GetNodeChildren(currentNode).Count();
        if (numResponses > 0)
        {
            isChoosing = true;
            TriggerExitAction();
            OnConversantUpdate();
            return;
        }

        DialogueNode[] variant = dialogueTree.GetResponseChildren(currentNode).ToArray();
        if (variant.Length == 0)
        {
            QuitDialogue();
        }
        else 
        {
            TriggerExitAction();
            currentNode = variant[0];
            TriggerEnterAction();
        }

        OnConversantUpdate();
    }
    public void QuitDialogue()
    {
        dialogueTree = null;
        TriggerExitAction();
        currentNode = null;
        isChoosing = false;
        currentConversant = null;

        OnConversantUpdate();

        //GameManager.Instance.MouseState(States.GameStates.Exit_Dialogue);
    }

    public bool IsActive()
    { 
        return dialogueTree != null;
    }

    public bool IsChoosing()
    {
        return isChoosing;
    }

    public string GetText()
    {
        if (currentNode == null)
        {
            return "Dialogue is empty!";
        }

        return currentNode.GetText();
    }

    public IEnumerable<DialogueNode> GetChoiceVariants()
    {
        return dialogueTree.GetNodeChildren(currentNode);
    }

    public string GetCurrentConversantName()
    {
        if (currentNode.GetIsPlayer() | isChoosing)
        {
            return playerName;
        }
        else
        {
            return currentConversant.GetName();
        }
    }

    public void SelectChoice(DialogueNode choseNode)
    {
        currentNode = choseNode;
        TriggerEnterAction();
        isChoosing = false;
        SelectNextDialogueVariant();
    }

    public bool HasNext()
    {
        return dialogueTree.GetAllNodeChildren(currentNode).Count() > 0;
    }

    private void TriggerEnterAction()
    {
        if (currentNode != null)
        {
            TriggerAction(currentNode.GetOnEnterDialogueAction());
        }
    }

    private void TriggerExitAction()
    {
        if (currentNode != null)
        {
            TriggerAction(currentNode.GetOnExitDialogueAction());
        }
    }

    private void TriggerAction(string action)
    {
        if (action == " ")
        {
            return;
        }
        foreach (DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
        {
            trigger.TriggerAction(action);
        }
    }
}
