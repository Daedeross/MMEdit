namespace MMEdit.MM2
{
    using System;

    public class MM2Character
    {
        public byte Index { get; set; }
        public string Name { get; set; } = "                    ";
        public MM2Race Race { get; set; }
        public MM2Gender Gender { get; set; }
        public MM2Class Class { get; set; }
        public MM2Alignment OriginalAlignment { get; set; }
        public MM2Alignment CurrentAlignment { get; set; }
        public ushort Experience { get; set; }
        public ushort Age { get; set; }
        public ushort RealLevel { get; set; }
        public ushort TemporaryLevel { get; set; }
        public ushort RealSpellLevel { get; set; }
        public ushort TemporarySpellLevel { get; set; }
        public ushort Gold { get; set; }
        public ushort Gems { get; set; }
        public ushort Food { get; set; }
    }
}
