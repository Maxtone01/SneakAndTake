using Assets.Scripts.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject _grabbableItem;
    [SerializeField] GameObject _grabbablePosition;
    [SerializeField] CompleteQuestOnItemCollect _completeQuest;
    [SerializeField] PlayerController _playerController;
    public InventoryObject inventory;

    float treshold = 0f;

    public void Idle(Animator animator, bool isGrabbed)
    {
        if (!Input.GetKey(KeyCode.LeftControl) & !Input.GetKey(KeyCode.E))
        {
            if (isGrabbed)
            {
                animator.SetFloat("State", 2);
                treshold = 2;
            }
            else 
            {
                animator.SetFloat("State", 0);
            }
        }
    }

    public void GrabItem(Animator animator)
    {
        if (treshold < 1)
        {
            treshold += Time.deltaTime * 1f;
            animator.SetFloat("State", treshold);
        }
        if (treshold > 1)
        {
            Destroy(_grabbableItem.GetComponent<Box>());
            _grabbableItem.transform.SetParent(_grabbablePosition.transform, true);
            _grabbableItem.transform.position = _grabbablePosition.transform.position;
            _playerController.canGrab = false;
            _completeQuest.CollectItem();
            treshold = 0;
        }
    }

    internal void PlaceItem(Animator animator)
    {
        if (treshold <= 2)
        {
            treshold -= Time.deltaTime * 1f;
            animator.SetFloat("State", treshold);
        }
        if (treshold < 1)
        {
            _grabbablePosition.transform.DetachChildren();
            treshold = 0;
            _completeQuest.StoreItem();
        }
    }
    public void SaveInventory()
    {
        Debug.Log("Saved!");
        inventory.SaveInventory();
    }

    public void LoadInventory()
    {
        Debug.Log("Loaded!");
        inventory.LoadInventory();
    }

    internal void SelectNextDialogue()
    {
        GameObject dialogueController = GameObject.FindGameObjectWithTag("Dialogue Panel");
        if (dialogueController == null)
        {
            return;
        }
        else
        {
            DialogueUI dialogue = dialogueController.GetComponent<DialogueUI>();
            dialogue.conversantController.SelectNextDialogueVariant();
        }
    }
}
