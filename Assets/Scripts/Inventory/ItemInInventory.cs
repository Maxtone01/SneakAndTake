using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInInventory : MonoBehaviour
{
    public ItemSO _itemSO;

    public void SetItemSO(ItemSO item)
    { 
        _itemSO = item;
    }

    public ItemSO GetItemSO()
    {
        return _itemSO;
    }
}
