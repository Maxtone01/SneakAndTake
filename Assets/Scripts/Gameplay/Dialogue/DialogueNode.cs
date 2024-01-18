using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogueNode: ScriptableObject
{
    #region Variables
    [SerializeField]
    bool isPLayer = false;
    [SerializeField]
    private string dialogueText;
    [SerializeField]
    private List<string> variants = new List<string>();
    [SerializeField]
    private Rect rect = new(0, 0, 200, 100);
    [SerializeField]
    string onEnterDialogue;
    [SerializeField]
    string onExitDialogue;
    #endregion

    public Rect GetRect()
    {
        return rect;
    }

    public string GetText()
    { 
        return dialogueText;
    }

    public List<string> GetVariants()
    {
        return variants;
    }

    public string GetOnEnterDialogueAction()
    {
        return onEnterDialogue;
    }

    public string GetOnExitDialogueAction()
    {
        return onExitDialogue;
    }

    public bool GetIsPlayer()
    { 
        return isPLayer;
    }


#if UNITY_EDITOR
    public void SetPosition(Vector2 newPosition)
    {
        Undo.RecordObject(this, "Move dialogue node");
        rect.position = newPosition;
        EditorUtility.SetDirty(this);
    }

    public void SetText(string newText)
    {
        if (newText != dialogueText)
        {
            Undo.RecordObject(this, "Update text");
            dialogueText = newText;
            EditorUtility.SetDirty(this);
        }
    }

    public void AddVariant(string variantId)
    {
        Undo.RecordObject(this, "Link");
        variants.Add(variantId);
        EditorUtility.SetDirty(this);
    }

    public void RemoveVariant(string variantId)
    {
        Undo.RecordObject(this, "Unlink");
        variants.Remove(variantId);
        EditorUtility.SetDirty(this);
    }

    public bool IsPlayer()
    {
        return isPLayer;
    }

    public void SetPlayerNode(bool isPlayerNode)
    {
        Undo.RecordObject(this, "Change dialog actor");
        isPLayer = isPlayerNode;
        EditorUtility.SetDirty(this);
    }
#endif
}
