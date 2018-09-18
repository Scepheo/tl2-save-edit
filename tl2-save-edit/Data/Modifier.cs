namespace Tl2SaveEdit.Data
{
    public class Modifier
    {
        public ModifierType Type { get; internal set; }
        public string Name { get; internal set; }
        public string ExtraName { get; internal set; }
        public string ExtraExtraName { get; internal set; }
        public long Id { get; internal set; }
        public float[] Parameters { get; internal set; }
        public byte[] Unknown1 { get; internal set; }
        public int StatParameter1 { get; internal set; }
        public int StatParameter2 { get; internal set; }
        public byte[] Unknown2 { get; internal set; }
        public float StatParameter3 { get; internal set; }
        public byte[] Unknown3 { get; internal set; }
        public float ValueParameter { get; internal set; }
        public byte[] Unknown4 { get; internal set; }
        public string EndName { get; internal set; }
    }
}
