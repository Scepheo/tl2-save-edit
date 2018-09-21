using System;
using System.IO;

namespace Tl2SaveEdit.Data
{
    public class Passive
    {
        public int Flags { get; internal set; }
        public ShortString Name { get; internal set; }
        public byte[] Unknown1 { get; internal set; }

        internal int GetSize()
        {
            return 4
                + Name.GetSize()
                + Unknown1.Length;
        }
    }

    internal static class PassiveExtensions
    {
        public static Passive ReadPassive(this BinaryReader reader)
        {
            var passive = new Passive();

            // Embermage
            ////////////

            // Berserker
            ////////////
            // MELEE_DAMAGE_BONUS   = 80 = 0101_0000
            // Size = 51

            // Outlander
            ////////////
            // WANDERER_PASSIVE_??  = 85 = 0101_0101
            // Size = 63
            // WANDERER_CHARGE_RATE = 81 = 0101_0001
            // Size = 51

            // Engineer
            ///////////
            //                      = 81 = 0101_0001
            // Size = 43
            // MELEE_DAMAGE_BONUS   = 80 = 0101_0000
            // Size = 51
            passive.Flags = reader.ReadInt32();
            passive.Name = reader.ReadShortString();

            int size;

            var name = passive.Name.ToString();

            if (name == "MELEE_DAMAGE_BONUS")
            {
                size = 51;
            }
            else if (name == "")
            {
                size = 43;
            }
            else if (name == "WANDERER_CHARGE_RATE")
            {
                size = 51;
            }
            else if (name.StartsWith("WANDERER_PASSIVE_"))
            {
                size = 63;
            }
            else
            {
                throw new InvalidOperationException($"Unknown passive {passive.Name}");
            }

            passive.Unknown1 = reader.ReadBytes(size);

            return passive;
        }

        public static void WritePassive(this BinaryWriter writer, Passive passive)
        {
            writer.Write(passive.Flags);
            writer.WriteShortString(passive.Name);
            writer.Write(passive.Unknown1);
        }
    }
}
