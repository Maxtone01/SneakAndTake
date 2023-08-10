using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInInventory : MonoBehaviour
{
    public ItemSO _itemSO;

    public void SetItemSO(ItemSO item)
    { 
        this._itemSO = item;
    }

    public ItemSO GetItemSO()
    {
        return this._itemSO;
    }
}
