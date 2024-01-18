using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Database", menuName = "Inventory System/Items/Database")]
public class ItemsDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemSO[] items;

    public Dictionary<ItemSO, int> itemDict = new();
    public Dictionary<int, ItemSO> itemDictDeserialize = new();

    public void OnAfterDeserialize()
    {
        itemDict = new Dictionary<ItemSO, int>();

        for (int i = 0; i < items.Length; i++)
        {
            itemDict.Add(items[i], i);
            itemDictDeserialize.Add(i, items[i]);
        }
    }

    public void OnBeforeSerialize()
    {

    }
}
