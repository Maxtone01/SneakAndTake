using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory
{
    public class InventoryUI : MonoBehaviour, IPointerClickHandler
    {
        public GameObject inventoryPrefab;
        public InventoryObject inventory;

        Dictionary<InventorySlot, GameObject> inventoryObjects = new Dictionary<InventorySlot, GameObject>();

        private void Start()
        {

        }

        private void Update()
        {
        }

        public void UpdateUI()
        {
            AddItemOrQuantity();
        }

        private void AddItemOrQuantity()
        {
            for (int i = 0; i < inventory.container.Count; i++)
            {
                if (inventoryObjects.ContainsKey(inventory.container[i]))
                {
                    inventoryObjects[inventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
                }
                else
                {
                    AddItemToInventoryUI(i);
                }
            }
        }

        private void AddItemToInventoryUI(int i)
        {
            var obj = Instantiate(inventoryPrefab, this.transform);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.container[i].item._sprite;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
            obj.transform.GetChild(0).GetComponentInChildren<ItemInInventory>().SetItemSO(inventory.container[i].item);
            inventoryObjects.Add(inventory.container[i], obj);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ItemInInventory item = eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemInInventory>();
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (item == null)
                {
                    return;
                }
                Debug.Log(item.GetItemSO()._itemName);
            }
        }
    }

}