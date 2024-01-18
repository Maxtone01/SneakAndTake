namespace Assets.Scripts.Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        public int id;
        public ItemSO item;
        public int amount;
        public InventorySlot(int id, ItemSO item, int amount)
        {
            this.id = id;
            this.item = item;
            this.amount = amount;
        }

        internal void AddAmount(int amountValue)
        {
            amount += amountValue;
        }
    }
}
