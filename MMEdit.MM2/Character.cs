namespace MMEdit.MM2
{
    using System;

    public class Character: ICloneable
    {
        public byte Index { get; set; }
        public string Name { get; set; } = "          ";
        public Town Town { get; set; }
        public Race Race { get; set; }
        public Gender Gender { get; set; }
        public Class Class { get; set; }
        public Alignment Alignment { get; set; }
        public Alignment CurrentAlignment { get; set; }
        public byte ActualMight { get; set; }
        public byte ActualIntelligence { get; set; }
        public byte ActualPersonality { get; set; }
        public byte ActualEndurence { get; set; }
        public byte ActualSpeed { get; set; }
        public byte ActualAccuracy { get; set; }
        public byte ActualLuck { get; set; }
        public byte ActualLevel { get; set; }
        public byte ActualSpellLevel { get; set; }
        public byte TempMight { get; set; }
        public byte TempIntelligence { get; set; }
        public byte TempPersonality { get; set; }
        public byte TempEndurence { get; set; }
        public byte TempSpeed { get; set; }
        public byte TempAccuracy { get; set; }
        public byte TempLuck { get; set; }
        public byte TempLevel { get; set; }
        public byte TempSpellLevel { get; set; }
        public byte Thievery { get; set; }
        public byte BonusAC { get; set; }
        public byte TempAC { get; set; }
        public byte Age { get; set; }
        public byte Food { get; set; }
        public Condition Condition { get; set; }
        public byte[] EquipedIds { get; set; } = new byte[6];
        public byte[] EquipedCharges { get; set; } = new byte[6];
        public byte[] EquipedBonuses { get; set; } = new byte[6];
        public byte[] BackpackIds { get; set; } = new byte[6];
        public byte[] BackpackCharges { get; set; } = new byte[6];
        public byte[] BackpackBonuses { get; set; } = new byte[6];
        public uint Experience { get; set; }
        public uint Gold { get; set; }
        public ushort Gems { get; set; }
        public ushort CurrentSP { get; set; }
        public ushort MaxSP { get; set; }
        public ushort CurrentHP { get; set; }
        public ushort MaxHP { get; set; }
        public ushort TempMaxHP { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
