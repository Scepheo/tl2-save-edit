using System.IO;
using System.Linq;

namespace Tl2SaveEdit.Data
{
    public class ItemList
    {
        public Item[] Items { get; set; }

        internal int GetSize()
        {
            return 4 + Items.Sum(item => item.GetSize());
        }
    }

    internal static class ItemListExtensions
    {
        public static ItemList ReadItemList(this BinaryReader reader)
        {
            var length = reader.ReadInt32();
            var items = new Item[length];

            for (var i = 0; i < length; i++)
            {
                items[i] = reader.ReadItem();
            }

            return new ItemList
            {
                Items = items,
            };
        }

        public static void WriteItemList(this BinaryWriter writer, ItemList items)
        {
            writer.Write(items.Items.Length);

            foreach (var item in items.Items)
            {
                writer.WriteItem(item);
            }
        }
    }
}
