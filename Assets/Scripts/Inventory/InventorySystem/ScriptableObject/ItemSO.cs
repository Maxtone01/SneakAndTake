using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{ 
    Equipment,
    Weapon,
}
public class ItemSO : ScriptableObject
{
    public int Id;
    public Sprite _sprite;
    public string _itemName;
    [TextArea(10, 20)]
    public string _itemDescription;
    public GameObject _itemPrefab;
    public ItemType _itemType;

}
