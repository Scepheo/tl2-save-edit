namespace Tl2SaveEdit.Data
{
    public class ModIdList
    {
        public long[] Ids { get; set; }

        public override string ToString()
        {
            return $"{Ids.Length} mod ids";
        }
    }
}
