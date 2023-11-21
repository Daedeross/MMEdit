namespace MMEdit.MM2
{
    using System;
    using System.Linq;
    using System.Text;

    internal static class Offsets
    {
        public static DatumInfo Name               = new(0x0,  10);
        public static DatumInfo Town               = new(0x0A, 1);
        public static DatumInfo Gender             = new(0x0C, 1);
        public static DatumInfo Alignment          = new(0x0D, 1);
        public static DatumInfo Race               = new(0x0E, 1);
        public static DatumInfo Class              = new(0x0F, 1);
        public static DatumInfo ActualMight        = new(0x10, 1);
        public static DatumInfo ActualIntelligence = new(0x11, 1);
        public static DatumInfo ActualPersonality  = new(0x12, 1);
        public static DatumInfo ActualEndurence    = new(0x27, 1);
        public static DatumInfo ActualSpeed        = new(0x13, 1);
        public static DatumInfo ActualAccuracy     = new(0x14, 1);
        public static DatumInfo ActualLuck         = new(0x15, 1);
        public static DatumInfo ActualLevel        = new(0x16, 1);
        public static DatumInfo ActualSpellLevel   = new(0x17, 1);
        public static DatumInfo TempMight          = new(0x6B, 1);
        public static DatumInfo TempIntelligence   = new(0x6C, 1);
        public static DatumInfo TempPersonality    = new(0x6D, 1);
        public static DatumInfo TempEndurence      = new(0x73, 1);
        public static DatumInfo TempSpeed          = new(0x6E, 1);
        public static DatumInfo TempAccuracy       = new(0x6F, 1);
        public static DatumInfo TempLuck           = new(0x70, 1);
        public static DatumInfo TempLevel          = new(0x71, 1);
        public static DatumInfo TempSpellLevel     = new(0x72, 1);

        public static DatumInfo Thievery  = new(0x1E, 1);
        public static DatumInfo BonusAC   = new(0x1E, 1);
        public static DatumInfo TempAC    = new(0x24, 1);
        public static DatumInfo Age       = new(0x21, 1);
        public static DatumInfo Food      = new(0x25, 1);
        public static DatumInfo Condition = new(0x26, 1);

        public static DatumInfo[] EquipedIds      = Enumerable.Range(0, 6).Select(i => new DatumInfo(0x28 + i, 1)).ToArray();
        public static DatumInfo[] EquipedCharges  = Enumerable.Range(0, 6).Select(i => new DatumInfo(0x2E + i, 1)).ToArray();
        public static DatumInfo[] EquipedBonuses  = Enumerable.Range(0, 6).Select(i => new DatumInfo(0x34 + i, 1)).ToArray();
        public static DatumInfo[] BackpackIds     = Enumerable.Range(0, 6).Select(i => new DatumInfo(0x3A + i, 1)).ToArray();
        public static DatumInfo[] BackpackCharges = Enumerable.Range(0, 6).Select(i => new DatumInfo(0x40 + i, 1)).ToArray();
        public static DatumInfo[] BackpackBonuses = Enumerable.Range(0, 6).Select(i => new DatumInfo(0x46 + i, 1)).ToArray();

        public static DatumInfo Skills    = new(0x50, 1);

        public static DatumInfo CurrentSP  = new(0x58, 2);
        public static DatumInfo MaxSP      = new(0x5A, 2);
        public static DatumInfo CurrentHP  = new(0x5E, 2);
        public static DatumInfo MaxHP      = new(0x60, 2);
        public static DatumInfo TempMaxHP  = new(0x74, 2);
        public static DatumInfo Gems       = new(0x5C, 2);
        public static DatumInfo Experience = new(0x62, 4);
        public static DatumInfo Gold       = new(0x66, 4);

        

        static Offsets()
        {
            if (!BitConverter.IsLittleEndian)
            {
                throw new NotSupportedException("Only supported on little endian architectures.");
            }
        }
    }
}
