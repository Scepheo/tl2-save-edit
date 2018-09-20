namespace Tl2SaveEdit.Data
{
    public class Modifier
    {
        public ModifierFlags Flags { get; set; }
        public string Name { get; set; }
        public string Graph { get; set; }
        public string Particles { get; set; }
        public long UnitThemeId { get; set; }
        public float[] Properties { get; set; }
        public StatName[] StatNames { get; set; }
        public int EffectType { get; set; }
        public ModifierDamageType DamageType { get; set; }
        public ModifierActivation Activation { get; set; }
        public int Level { get; set; }
        public float Duration { get; set; }
        public byte[] Unknown1 { get; set; }
        public float DisplayValue { get; set; }
        public ModifierSource Source { get; set; }
        public string Icon { get; set; }
    }
}
