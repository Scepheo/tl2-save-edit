using System.Collections.Generic;
using System.Linq;
using Tl2SaveEdit.Data;

namespace Tl2SaveEdit
{
    public class Inventory
    {
        public IReadOnlyList<Item> Items => _items;

        private Item[] _items;

        internal static Inventory FromItemList(ItemList itemList)
        {
            var items = itemList.Items.Select(Item.FromDataItem).ToArray();

            var inventory = new Inventory
            {
                _items = items,
            };

            return inventory;
        }

        internal ItemList ToItemList()
        {
            var items = _items.Select(item => item.ToDataItem()).ToArray();

            var itemList = new ItemList()
            {
                Items = items,
            };

            return itemList;
        }
    }
}
