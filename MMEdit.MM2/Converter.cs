namespace MMEdit.MM2
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    public static class Converter
    {
        const uint CharacterSize = 130;
        const int MaxCharacterCount = 24;

        public static Character BinaryToCharacter(byte[] data)
        {
            var xp = data.GetUInt32(Offsets.Experience);
            return new Character
            {
                Name      = data.GetString(Offsets.Name),
                Town      = (Town)data.GetByte(Offsets.Town),
                Race      = (Race)data.GetByte(Offsets.Race),
                Gender    = (Gender)data.GetByte(Offsets.Gender),
                Alignment = (Alignment)data.GetByte(Offsets.Alignment),
                Class     = ByteToClass(data.GetByte(Offsets.Class)),

                ActualMight        = data.GetByte(Offsets.ActualMight),
                ActualIntelligence = data.GetByte(Offsets.ActualIntelligence),
                ActualPersonality  = data.GetByte(Offsets.ActualPersonality),
                ActualEndurence    = data.GetByte(Offsets.ActualEndurence),
                ActualSpeed        = data.GetByte(Offsets.ActualSpeed),
                ActualAccuracy     = data.GetByte(Offsets.ActualAccuracy),
                ActualLuck         = data.GetByte(Offsets.ActualLuck),
                ActualLevel        = data.GetByte(Offsets.ActualLevel),
                ActualSpellLevel   = data.GetByte(Offsets.ActualSpellLevel),

                TempMight        = data.GetByte(Offsets.TempMight),
                TempIntelligence = data.GetByte(Offsets.TempIntelligence),
                TempPersonality  = data.GetByte(Offsets.TempPersonality),
                TempEndurence    = data.GetByte(Offsets.TempEndurence),
                TempSpeed        = data.GetByte(Offsets.TempSpeed),
                TempAccuracy     = data.GetByte(Offsets.TempAccuracy),
                TempLuck         = data.GetByte(Offsets.TempLuck),
                TempLevel        = data.GetByte(Offsets.TempLevel),
                TempSpellLevel   = data.GetByte(Offsets.TempSpellLevel),

                Thievery  = data.GetByte(Offsets.Thievery),
                BonusAC   = data.GetByte(Offsets.BonusAC),
                TempAC    = data.GetByte(Offsets.TempAC),
                Age       = data.GetByte(Offsets.Age),
                Food      = data.GetByte(Offsets.Food),
                Condition = (Condition)data.GetByte(Offsets.Condition),

                EquipedIds      = data.SliceBytes(Offsets.EquipedIds),
                EquipedCharges  = data.SliceBytes(Offsets.EquipedCharges),
                EquipedBonuses  = data.SliceBytes(Offsets.EquipedBonuses),
                BackpackIds     = data.SliceBytes(Offsets.BackpackIds),
                BackpackCharges = data.SliceBytes(Offsets.BackpackCharges),
                BackpackBonuses = data.SliceBytes(Offsets.BackpackBonuses),

                CurrentSP = data.GetUInt16(Offsets.CurrentSP),
                MaxSP     = data.GetUInt16(Offsets.MaxSP),
                CurrentHP = data.GetUInt16(Offsets.CurrentHP),
                MaxHP     = data.GetUInt16(Offsets.MaxHP),
                TempMaxHP = data.GetUInt16(Offsets.TempMaxHP),
                Gems      = data.GetUInt16(Offsets.Gems),

                Experience = data.GetUInt32(Offsets.Experience),
                Gold       = data.GetUInt32(Offsets.Gold)
            };
        }

        public static Character BinaryToCharacter(ReadOnlySpan<byte> data)
        {
            if (data.Length < CharacterSize)
            {
                throw new ArgumentOutOfRangeException(nameof(data.Length));
            }

            return BinaryToCharacter(data.ToArray());
        }

        public static Character BinaryToCharacter(byte[] data, int start)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));
            if (start < 0) throw new ArgumentOutOfRangeException(nameof(start));
            if (start + CharacterSize > data.Length) throw new ArgumentOutOfRangeException(nameof(start));

            var bytes = new byte[CharacterSize];
            Array.Copy(data, start, bytes, 0, CharacterSize);

            return BinaryToCharacter(bytes);
        }

        public static Character ReadCharacter(Stream stream, int index)
        {
            if (index >= MaxCharacterCount || index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Index must be in the range 0..{MaxCharacterCount-1}");
            }
            stream.Position = index * CharacterSize;

            var data = new byte[CharacterSize];
            stream.Read(data, 0, data.Length);

            var character = BinaryToCharacter(data);
            character.Index = (byte)index;

            return character;
        }

        public static Character[] ReadCharacters(Stream stream)
        {
            var characterArray = new Character[MaxCharacterCount];
            for (int i = 0; i < MaxCharacterCount; i++)
            {
                characterArray[i] = ReadCharacter(stream, i);
            }

            return characterArray;
        }

        public static byte[] CharacterToBinary(Character character)
        {
            if (character is null) throw new ArgumentNullException(nameof(character));

            var data = new byte[CharacterSize];

            data.SetString(character.Name, Offsets.Name);
            data.SetByte((byte)character.Town, Offsets.Town);
            data.SetByte((byte)character.Race, Offsets.Race);
            data.SetByte((byte)character.Gender ,Offsets.Gender);
            data.SetByte((byte)character.Alignment ,Offsets.Alignment);

            data.SetByte(ClassToByte(character.Class), Offsets.Class);
            data.SetByte(character.ActualMight, Offsets.ActualMight);
            data.SetByte(character.ActualIntelligence, Offsets.ActualIntelligence);
            data.SetByte(character.ActualPersonality, Offsets.ActualPersonality);
            data.SetByte(character.ActualEndurence, Offsets.ActualEndurence);
            data.SetByte(character.ActualSpeed, Offsets.ActualSpeed);
            data.SetByte(character.ActualAccuracy, Offsets.ActualAccuracy);
            data.SetByte(character.ActualLuck, Offsets.ActualLuck);
            data.SetByte(character.ActualLevel, Offsets.ActualLevel);
            data.SetByte(character.ActualSpellLevel, Offsets.ActualSpellLevel);

            data.SetByte(character.TempMight, Offsets.TempMight);
            data.SetByte(character.TempIntelligence, Offsets.TempIntelligence);
            data.SetByte(character.TempPersonality, Offsets.TempPersonality);
            data.SetByte(character.TempEndurence, Offsets.TempEndurence);
            data.SetByte(character.TempSpeed, Offsets.TempSpeed);
            data.SetByte(character.TempAccuracy, Offsets.TempAccuracy);
            data.SetByte(character.TempLuck, Offsets.TempLuck);
            data.SetByte(character.TempLevel, Offsets.TempLevel);
            data.SetByte(character.TempSpellLevel, Offsets.TempSpellLevel);

            data.SetByte(character.Thievery, Offsets.Thievery);
            data.SetByte(character.BonusAC, Offsets.BonusAC);
            data.SetByte(character.TempAC, Offsets.TempAC);
            data.SetByte(character.Age, Offsets.Age);
            data.SetByte(character.Food, Offsets.Food);
            data.SetByte((byte)character.Condition, Offsets.Condition);

            data.UnSliceBytes(character.EquipedIds, Offsets.EquipedIds);
            data.UnSliceBytes(character.EquipedCharges, Offsets.EquipedCharges);
            data.UnSliceBytes(character.EquipedBonuses, Offsets.EquipedBonuses);
            data.UnSliceBytes(character.BackpackIds, Offsets.BackpackIds);
            data.UnSliceBytes(character.BackpackCharges, Offsets.BackpackCharges);
            data.UnSliceBytes(character.BackpackBonuses, Offsets.BackpackBonuses);

            data.SetUInt16(character.CurrentSP, Offsets.CurrentSP);
            data.SetUInt16(character.MaxSP, Offsets.MaxSP);
            data.SetUInt16(character.CurrentHP, Offsets.CurrentHP);
            data.SetUInt16(character.MaxHP, Offsets.MaxHP);
            data.SetUInt16(character.TempMaxHP, Offsets.TempMaxHP);
            data.SetUInt16(character.Gems, Offsets.Gems);

            data.SetUInt32(character.Experience, Offsets.Experience);
            data.SetUInt32(character.Gold, Offsets.Gold);

            return data;
        }

        private static Class ByteToClass(byte b) => b switch
        {
            0 => Class.Knight,
            1 => Class.Paladin,
            2 => Class.Archer,
            3 => Class.Cleric,
            4 => Class.Sorcerer,
            5 => Class.Robber,
            6 => Class.Ninja,
            7 => Class.Barbarian,
            _ => throw new ArgumentOutOfRangeException(nameof(b))
        };

        private static byte ClassToByte(Class c) => c switch
        {
            Class.Knight    => 0,
            Class.Paladin   => 1,
            Class.Archer    => 2,
            Class.Cleric    => 3,
            Class.Sorcerer  => 4,
            Class.Robber    => 5,
            Class.Ninja     => 6,
            Class.Barbarian => 7,
            _ => throw new ArgumentOutOfRangeException(nameof(c))
        };
    }
}
