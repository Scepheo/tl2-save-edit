using System.IO;
using System.Linq;

namespace Tl2SaveEdit.Data
{
    public class Modifier
    {
        public ModifierFlags Flags { get; set; }
        public ShortString Name { get; set; }
        public ShortString Graph { get; set; }
        public ShortString Particles { get; set; }
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
        public ByteString Icon { get; set; }

        internal int GetSize()
        {
            return 4 // Flags
                + Name.GetSize()
                + (Flags.HasFlag(ModifierFlags.HasGraph) ? Graph.GetSize() : 0)
                + (Flags.HasFlag(ModifierFlags.HasParticles) ? Particles.GetSize() : 0)
                + (Flags.HasFlag(ModifierFlags.HasUnitTheme) ? sizeof(long) : 0)
                + 1 // property count
                + Properties.Length * sizeof(float)
                + 2 // Stat name count
                + StatNames.Sum(statName => statName.GetSize())
                + 4 // Effect type
                + 4 // Damage type
                + 4 // Activation
                + 4 // Level
                + 4 // Duration
                + Unknown1.Length
                + 4 // Display value
                + 4 // Source
                + (Flags.HasFlag(ModifierFlags.HasIcon) ? Icon.GetSize() : 0);
        }
    }

    internal static class ModifierExtensions
    {
        public static Modifier ReadModifier(this BinaryReader reader)
        {
            var modifier = new Modifier();

            modifier.Flags = (ModifierFlags)reader.ReadInt32();
            modifier.Name = reader.ReadShortString();

            if (modifier.Flags.HasFlag(ModifierFlags.HasGraph))
            {
                modifier.Graph = reader.ReadShortString();
            }

            if (modifier.Flags.HasFlag(ModifierFlags.HasParticles))
            {
                modifier.Particles = reader.ReadShortString();
            }

            if (modifier.Flags.HasFlag(ModifierFlags.HasUnitTheme))
            {
                modifier.UnitThemeId = reader.ReadInt64();
            }

            var propertyCount = reader.ReadByte();
            modifier.Properties = new float[propertyCount];

            for (var i = 0; i < propertyCount; i++)
            {
                modifier.Properties[i] = reader.ReadSingle();
            }

            var statNameCount = reader.ReadInt16();
            modifier.StatNames = new StatName[statNameCount];

            for (var i = 0; i < statNameCount; i++)
            {
                modifier.StatNames[i] = reader.ReadStatName();
            }

            modifier.EffectType = reader.ReadInt32();
            modifier.DamageType = (ModifierDamageType)reader.ReadInt32();
            modifier.Activation = (ModifierActivation)reader.ReadInt32();
            modifier.Level = reader.ReadInt32();
            modifier.Duration = reader.ReadSingle();
            modifier.Unknown1 = reader.ReadBytes(4);
            modifier.DisplayValue = reader.ReadSingle();
            modifier.Source = (ModifierSource)reader.ReadInt32();

            if (modifier.Flags.HasFlag(ModifierFlags.HasIcon))
            {
                modifier.Icon = reader.ReadByteString();
            }

            return modifier;
        }

        public static void WriteModifier(this BinaryWriter writer, Modifier modifier)
        {
            writer.Write((int)modifier.Flags);
            writer.WriteShortString(modifier.Name);

            if (modifier.Flags.HasFlag(ModifierFlags.HasGraph))
            {
                writer.WriteShortString(modifier.Graph);
            }

            if (modifier.Flags.HasFlag(ModifierFlags.HasParticles))
            {
                writer.WriteShortString(modifier.Particles);
            }

            if (modifier.Flags.HasFlag(ModifierFlags.HasUnitTheme))
            {
                writer.Write(modifier.UnitThemeId);
            }

            writer.Write((byte)modifier.Properties.Length);

            foreach (var property in modifier.Properties)
            {
                writer.Write(property);
            }

            writer.Write((short)modifier.StatNames.Length);

            foreach (var statName in modifier.StatNames)
            {
                writer.WriteStatName(statName);
            }

            writer.Write(modifier.EffectType);
            writer.Write((int)modifier.DamageType);
            writer.Write((int)modifier.Activation);
            writer.Write(modifier.Level);
            writer.Write(modifier.Duration);
            writer.Write(modifier.Unknown1);
            writer.Write(modifier.DisplayValue);
            writer.Write((int)modifier.Source);

            if (modifier.Flags.HasFlag(ModifierFlags.HasIcon))
            {
                writer.WriteByteString(modifier.Icon);
            }
        }
    }
}
