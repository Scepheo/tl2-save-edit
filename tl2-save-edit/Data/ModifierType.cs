using System;

namespace Tl2SaveEdit.Data
{
    [Flags]
    public enum ModifierType
    {
        ExtraName      = 0x0000_0100,
        ExtraExtraName = 0x0000_1000,
        Id             = 0x0000_2000,
        Enchantment    = 0x0000_0400,
        Disabled       = 0x0020_0000,
        EndName        = 0x0002_0000,
    }
}
