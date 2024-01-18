using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue")]
public class Dialogue: ScriptableObject, ISerializationCallbackReceiver
{
    #region Variables
    [SerializeField]
    List<DialogueNode> dialogueNodes = new List<DialogueNode>();
    [SerializeField]
    Vector2 nodeOffset = new Vector2(300, 0);

    Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();
    #endregion

    private void OnValidate()
    {
        nodeLookup.Clear();

        foreach (DialogueNode node in GetAllNodes())
        {
            nodeLookup[node.name] = node;
        }
    }

    public IEnumerable<DialogueNode> GetAllNodeChildren(DialogueNode parentNode)
    {
        foreach (string childId in parentNode.GetVariants())
        {
            if (nodeLookup.ContainsKey(childId))
            {
                yield return nodeLookup[childId];
            }
        }
    }

    public IEnumerable<DialogueNode> GetNodeChildren(DialogueNode currentNode)
    {
        foreach (DialogueNode dialogVariant in GetAllNodeChildren(currentNode))
        {
            if (dialogVariant.IsPlayer())
            { 
                yield return dialogVariant;
            }
        }
    }

    public IEnumerable<DialogueNode> GetResponseChildren(DialogueNode currentNode)
    {
        foreach (DialogueNode dialogVariant in GetAllNodeChildren(currentNode))
        {
            if (!dialogVariant.IsPlayer())
            {
                yield return dialogVariant;
            }
        }
    }

    public IEnumerable<DialogueNode> GetAllNodes()
    {
        return dialogueNodes;
    }

    public DialogueNode GetRootNode()
    {
        return dialogueNodes[0];
    }

#if UNITY_EDITOR
    public void CreateNode(DialogueNode parent)
    {
        DialogueNode newNode = NodeCreating(parent);

        Undo.RegisterCreatedObjectUndo(newNode, "Undo created node");
        Undo.RecordObject(this, "Added dialog node");

        NodeAdding(newNode);
    }

    public void DeleteNode(DialogueNode node)
    {
        Undo.RecordObject(this, "Deleted dialog node");
        dialogueNodes.Remove(node);
        OnValidate();

        foreach (DialogueNode dialogueNode in GetAllNodes())
        {
            node.RemoveVariant(node.name);
        }

        Undo.DestroyObjectImmediate(node);
    }

    private void NodeAdding(DialogueNode newNode)
    {
        dialogueNodes.Add(newNode);
        OnValidate();
    }

    private DialogueNode NodeCreating(DialogueNode parent)
    {
        DialogueNode newNode = CreateInstance<DialogueNode>();
        newNode.name = Guid.NewGuid().ToString();
        if (parent != null)
        {
            parent.AddVariant(newNode.name);
            newNode.SetPlayerNode(!parent.IsPlayer());
            newNode.SetPosition(parent.GetRect().position + nodeOffset);
        }

        return newNode;
    }
#endif

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        if (dialogueNodes.Count == 0)
        {
            DialogueNode newNode = NodeCreating(null);
            NodeAdding(newNode);
        }

        if (AssetDatabase.GetAssetPath(this) != "")
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                if (AssetDatabase.GetAssetPath(node) == "")
                {
                    AssetDatabase.AddObjectToAsset(node, this);
                }
            }
        }
#endif
    }

    public void OnAfterDeserialize()
    {
    }
}
