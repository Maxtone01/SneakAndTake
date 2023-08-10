using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class DialogueEditor : EditorWindow
{
    Dialogue currentDialogue = null;
    private Vector2 _scrollPos;
    [NonSerialized]
    GUIStyle nodeStyle;
    [NonSerialized]
    GUIStyle playerStyle;
    [NonSerialized]
    DialogueNode draggingNode = null;
    [NonSerialized]
    Vector2 draggingOffset;
    [NonSerialized]
    DialogueNode createNode = null;
    [NonSerialized]
    DialogueNode deleteNode = null;
    [NonSerialized]
    DialogueNode linkNode = null;
    [NonSerialized]
    bool draggingCanvas = false;
    [NonSerialized]
    Vector2 draggingCanvasOffset;
    const float canvasSize = 6000f;
    const float bgSize = 60f;

    [MenuItem("Window/Dialog Editor")]
    public static void ShowEditorWindow()
    {
        GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
    }


    [OnOpenAsset(1)]
    public static bool OnOpenDialogue(int instanceId, int line)
    {
        Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceId) as Dialogue;
        if (dialogue != null)
        {
            ShowEditorWindow();
            return true;
        }
        return false;
    }

    private void OnGUI()
    {
        if (currentDialogue != null)
        {
            ProcessEvents();

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

            Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);
            Texture2D bgTexture = Resources.Load("background") as Texture2D;
            Rect texCoords = new Rect(0, 0, canvasSize / bgSize, canvasSize / bgSize);
            GUI.DrawTextureWithTexCoords(canvas, bgTexture, texCoords);

            foreach (DialogueNode node in currentDialogue.GetAllNodes())
            {
                OnGUIChanged(node);
            }

            foreach (DialogueNode node in currentDialogue.GetAllNodes())
            {
                DrawConnections(node);
            }

            if (createNode != null)
            {
                currentDialogue.CreateNode(createNode);
                createNode = null;
            }

            EditorGUILayout.EndScrollView();

            if (deleteNode != null)
            {
                currentDialogue.DeleteNode(deleteNode);
                createNode = null;
            }
        }
    }

    private void OnEnable()
    {
        Selection.selectionChanged += OnSelectionChanged;

        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
        nodeStyle.padding = new RectOffset(12, 12, 12, 12);
        nodeStyle.border = new RectOffset(12, 12, 12, 12);

        playerStyle = new GUIStyle();
        playerStyle.normal.background = EditorGUIUtility.Load("node2") as Texture2D;
        playerStyle.padding = new RectOffset(12, 12, 12, 12);
        playerStyle.border = new RectOffset(12, 12, 12, 12);
    }

    private void OnSelectionChanged()
    {
        Dialogue newDialogue = Selection.activeObject as Dialogue;
        if (newDialogue != null)
        {
            currentDialogue = newDialogue;
            Repaint();
        }
    }

    private void DrawConnections(DialogueNode node)
    {
        Vector3 startPos = new Vector2(node.GetRect().xMax, node.GetRect().center.y);
        foreach (DialogueNode child in currentDialogue.GetAllNodeChildren(node))
        {
            Vector3 endPos = new Vector2(child.GetRect().xMin, child.GetRect().center.y);
            Vector3 offsetPoint = endPos - startPos;

            offsetPoint.y = 0;
            offsetPoint.x *= 0.8f;

            Handles.DrawBezier(
                startPos, endPos, startPos + offsetPoint, endPos - offsetPoint, 
                Color.green, null, 6f);
        }
    }

    private void ProcessEvents()
    {
        if (Event.current.type == EventType.MouseDown && draggingNode == null)
        {
            draggingNode = GetNodePoint(Event.current.mousePosition + _scrollPos);
            if (draggingNode != null)
            {
                draggingOffset = draggingNode.GetRect().position - Event.current.mousePosition;
                Selection.activeObject = draggingNode;
            }
            else
            {
                draggingCanvas = true;
                draggingCanvasOffset = Event.current.mousePosition + _scrollPos;
                Selection.activeObject = currentDialogue;
            }
        }

        else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
        {
            draggingNode.SetPosition(Event.current.mousePosition + draggingOffset);

            GUI.changed = true;
        }

        else if (Event.current.type == EventType.MouseDrag && draggingCanvas)
        {
            _scrollPos = draggingCanvasOffset - Event.current.mousePosition;
            GUI.changed = true;
        }

        else if (Event.current.type == EventType.MouseUp && draggingNode != null)
        {
            draggingNode = null;
        }

        else if (Event.current.type == EventType.MouseUp && draggingCanvas)
        {
            draggingCanvas = false;
        }
    }

    private DialogueNode GetNodePoint(Vector2 point)
    {
        DialogueNode foundNode = null;
        foreach (DialogueNode node in currentDialogue.GetAllNodes())
        {
            if (node.GetRect().Contains(point))
            {
                foundNode = node;
            }
        }
        return foundNode;
    }

    private void OnGUIChanged(DialogueNode node)
    {
        GUIStyle style = nodeStyle;
        if (node.IsPlayer())
        {
            style = playerStyle;
        }
        GUILayout.BeginArea(node.GetRect(), style);

        EditorGUILayout.LabelField("Node id: " + node.name);
        node.SetText(EditorGUILayout.TextField(node.GetText()));

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("+"))
        {
            createNode = node;
        }

        LinkingButtons(node);

        if (GUILayout.Button("-"))
        {
            deleteNode = node;
        }

        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    private void LinkingButtons(DialogueNode node)
    {
        if (linkNode == null)
        {
            if (GUILayout.Button("Correct variant"))
            {
                linkNode = node;
            }
        }
        else if (linkNode == node)
        {
            if (GUILayout.Button("Cancel"))
            {
                linkNode = null;
            }
        }
        else if (linkNode.GetVariants().Contains(node.name))
        {
            if (GUILayout.Button("Unlink"))
            {
                linkNode.RemoveVariant(node.name);
                linkNode = null;
            }
        }
        else
        {
            if (GUILayout.Button("Link"))
            {
                Undo.RecordObject(currentDialogue, "Add link");
                linkNode.AddVariant(node.name);
                linkNode = null;
            }
        }
    }
}
