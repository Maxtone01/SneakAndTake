using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public string savePath;
        private ItemsDatabase database;
        public List<InventorySlot> container = new();

        private void OnEnable()
        {
            database = (ItemsDatabase)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Inventory/InventorySystem/Items/Database.asset", typeof(ItemsDatabase));
        }

        public void AddItem(ItemSO item, int amount)
        {
            for (int i = 0; i < container.Count; i++)
            {
                if (container[i].item == item)
                {
                    container[i].AddAmount(amount);
                    return;
                }
            }
            container.Add(new InventorySlot(database.itemDict[item], item, amount));
        }

        public void SaveInventory()
        {
            IFormatter formatter =new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, container);
            stream.Close();
        }

        public void LoadInventory()
        {
            if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                file.Close();

                //IFormatter formatter = new BinaryFormatter();
                //Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
                //container = (container)
            }
        }


        public void OnAfterDeserialize()
        {
            for (int i = 0; i < container.Count; i++)
            {
                container[i].item = database.itemDictDeserialize[container[i].id];
            }
        }

        public void OnBeforeSerialize()
        {

        }
    }
}
