using MMEdit.MM2;
using MMEdit.ViewModels;
using ReactiveUI;

namespace MMEdit.Wpf.ViewModels
{
    public class MM2CharacterViewModel : ViewModelBase
    {
        private Character _model;

        public MM2CharacterViewModel(Character model)
        {
            _model = (Character)model.Clone();

            m_Index = model.Index;
            m_Name = model.Name;
            m_Town = model.Town;
            m_Race = model.Race;
            m_Gender = model.Gender;
            m_Class = model.Class;
            m_Alignment = model.Alignment;
            m_CurrentAlignment = model.CurrentAlignment;
            m_ActualMight = model.ActualMight;
            m_ActualIntelligence = model.ActualIntelligence;
            m_ActualPersonality = model.ActualPersonality;
            m_ActualEndurence = model.ActualEndurence;
            m_ActualSpeed = model.ActualSpeed;
            m_ActualAccuracy = model.ActualAccuracy;
            m_ActualLuck = model.ActualLuck;
            m_ActualLevel = model.ActualLevel;
            m_ActualSpellLevel = model.ActualSpellLevel;
            m_TempMight = model.TempMight;
            m_TempIntelligence = model.TempIntelligence;
            m_TempPersonality = model.TempPersonality;
            m_TempEndurence = model.TempEndurence;
            m_TempSpeed = model.TempSpeed;
            m_TempAccuracy = model.TempAccuracy;
            m_TempLuck = model.TempLuck;
            m_TempLevel = model.TempLevel;
            m_TempSpellLevel = model.TempSpellLevel;
            m_Thievery = model.Thievery;
            m_BonusAC = model.BonusAC;
            m_TempAC = model.TempAC;
            m_Age = model.Age;
            m_Food = model.Food;
            m_Condition = model.Condition;
            m_Experience = model.Experience;
            m_Gold = model.Gold;
            m_Gems = model.Gems;
            m_CurrentSP = model.CurrentSP;
            m_MaxSP = model.MaxSP;
            m_CurrentHP = model.CurrentHP;
            m_MaxHP = model.MaxHP;
            m_TempMaxHP = model.TempMaxHP;

            var settter = (object obj) =>
            {
                if (obj is MM2.Character character)
                {
                    _model = character;
                }
            };
            var property = PropertyCollectionAdapter.Instance.CreateProperty(typeof(MM2.Character), "root", _model, settter);
            PropertyCollection = property;
        }

        public IProperty PropertyCollection { get; private set; }

        private byte m_Index;
        public byte Index
        {
            get => m_Index;
            set => this.RaiseAndSetIfChanged(ref m_Index, value);
        }

        private string m_Name;
        public string Name
        {
            get => m_Name;
            set => this.RaiseAndSetIfChanged(ref m_Name, value);
        }

        private Town m_Town;
        public Town Town
        {
            get => m_Town;
            set => this.RaiseAndSetIfChanged(ref m_Town, value);
        }

        private Race m_Race;
        public Race Race
        {
            get => m_Race;
            set => this.RaiseAndSetIfChanged(ref m_Race, value);
        }

        private Gender m_Gender;
        public Gender Gender
        {
            get => m_Gender;
            set => this.RaiseAndSetIfChanged(ref m_Gender, value);
        }

        private Class m_Class;
        public Class Class
        {
            get => m_Class;
            set => this.RaiseAndSetIfChanged(ref m_Class, value);
        }

        private Alignment m_Alignment;
        public Alignment Alignment
        {
            get => m_Alignment;
            set => this.RaiseAndSetIfChanged(ref m_Alignment, value);
        }

        private Alignment m_CurrentAlignment;
        public Alignment CurrentAlignment
        {
            get => m_CurrentAlignment;
            set => this.RaiseAndSetIfChanged(ref m_CurrentAlignment, value);
        }

        private byte m_ActualMight;
        public byte ActualMight
        {
            get => m_ActualMight;
            set => this.RaiseAndSetIfChanged(ref m_ActualMight, value);
        }

        private byte m_ActualIntelligence;
        public byte ActualIntelligence
        {
            get => m_ActualIntelligence;
            set => this.RaiseAndSetIfChanged(ref m_ActualIntelligence, value);
        }

        private byte m_ActualPersonality;
        public byte ActualPersonality
        {
            get => m_ActualPersonality;
            set => this.RaiseAndSetIfChanged(ref m_ActualPersonality, value);
        }

        private byte m_ActualEndurence;
        public byte ActualEndurence
        {
            get => m_ActualEndurence;
            set => this.RaiseAndSetIfChanged(ref m_ActualEndurence, value);
        }

        private byte m_ActualSpeed;
        public byte ActualSpeed
        {
            get => m_ActualSpeed;
            set => this.RaiseAndSetIfChanged(ref m_ActualSpeed, value);
        }

        private byte m_ActualAccuracy;
        public byte ActualAccuracy
        {
            get => m_ActualAccuracy;
            set => this.RaiseAndSetIfChanged(ref m_ActualAccuracy, value);
        }

        private byte m_ActualLuck;
        public byte ActualLuck
        {
            get => m_ActualLuck;
            set => this.RaiseAndSetIfChanged(ref m_ActualLuck, value);
        }

        private byte m_ActualLevel;
        public byte ActualLevel
        {
            get => m_ActualLevel;
            set => this.RaiseAndSetIfChanged(ref m_ActualLevel, value);
        }

        private byte m_ActualSpellLevel;
        public byte ActualSpellLevel
        {
            get => m_ActualSpellLevel;
            set => this.RaiseAndSetIfChanged(ref m_ActualSpellLevel, value);
        }

        private byte m_TempMight;
        public byte TempMight
        {
            get => m_TempMight;
            set => this.RaiseAndSetIfChanged(ref m_TempMight, value);
        }

        private byte m_TempIntelligence;
        public byte TempIntelligence
        {
            get => m_TempIntelligence;
            set => this.RaiseAndSetIfChanged(ref m_TempIntelligence, value);
        }

        private byte m_TempPersonality;
        public byte TempPersonality
        {
            get => m_TempPersonality;
            set => this.RaiseAndSetIfChanged(ref m_TempPersonality, value);
        }

        private byte m_TempEndurence;
        public byte TempEndurence
        {
            get => m_TempEndurence;
            set => this.RaiseAndSetIfChanged(ref m_TempEndurence, value);
        }

        private byte m_TempSpeed;
        public byte TempSpeed
        {
            get => m_TempSpeed;
            set => this.RaiseAndSetIfChanged(ref m_TempSpeed, value);
        }

        private byte m_TempAccuracy;
        public byte TempAccuracy
        {
            get => m_TempAccuracy;
            set => this.RaiseAndSetIfChanged(ref m_TempAccuracy, value);
        }

        private byte m_TempLuck;
        public byte TempLuck
        {
            get => m_TempLuck;
            set => this.RaiseAndSetIfChanged(ref m_TempLuck, value);
        }

        private byte m_TempLevel;
        public byte TempLevel
        {
            get => m_TempLevel;
            set => this.RaiseAndSetIfChanged(ref m_TempLevel, value);
        }

        private byte m_TempSpellLevel;
        public byte TempSpellLevel
        {
            get => m_TempSpellLevel;
            set => this.RaiseAndSetIfChanged(ref m_TempSpellLevel, value);
        }

        private byte m_Thievery;
        public byte Thievery
        {
            get => m_Thievery;
            set => this.RaiseAndSetIfChanged(ref m_Thievery, value);
        }

        private byte m_BonusAC;
        public byte BonusAC
        {
            get => m_BonusAC;
            set => this.RaiseAndSetIfChanged(ref m_BonusAC, value);
        }

        private byte m_TempAC;
        public byte TempAC
        {
            get => m_TempAC;
            set => this.RaiseAndSetIfChanged(ref m_TempAC, value);
        }

        private byte m_Age;
        public byte Age
        {
            get => m_Age;
            set => this.RaiseAndSetIfChanged(ref m_Age, value);
        }

        private byte m_Food;
        public byte Food
        {
            get => m_Food;
            set => this.RaiseAndSetIfChanged(ref m_Food, value);
        }

        private Condition m_Condition;
        public Condition Condition
        {
            get => m_Condition;
            set => this.RaiseAndSetIfChanged(ref m_Condition, value);
        }

        /* TODO: Equipment
        public byte[] EquipedIds
        {
         get;
         set;
        } = new byte[6];
        public byte[] EquipedCharges
        {
         get;
         set;
        } = new byte[6];
        public byte[] EquipedBonuses
        {
            get;
            set;
        } = new byte[6];
        public byte[] BackpackIds
        {
            get;
            set;
        } = new byte[6];
        public byte[] BackpackCharges
        {
            get;
            set;
        } = new byte[6];
        public byte[] BackpackBonuses
        {
            get;
            set;
        } = new byte[6];
        */

        private uint m_Experience;
        public uint Experience
        {
            get => m_Experience;
            set => this.RaiseAndSetIfChanged(ref m_Experience, value);
        }

        private uint m_Gold;
        public uint Gold
        {
            get => m_Gold;
            set => this.RaiseAndSetIfChanged(ref m_Gold, value);
        }

        private ushort m_Gems;
        public ushort Gems
        {
            get => m_Gems;
            set => this.RaiseAndSetIfChanged(ref m_Gems, value);
        }

        private ushort m_CurrentSP;
        public ushort CurrentSP
        {
            get => m_CurrentSP;
            set => this.RaiseAndSetIfChanged(ref m_CurrentSP, value);
        }

        private ushort m_MaxSP;
        public ushort MaxSP
        {
            get => m_MaxSP;
            set => this.RaiseAndSetIfChanged(ref m_MaxSP, value);
        }

        private ushort m_CurrentHP;
        public ushort CurrentHP
        {
            get => m_CurrentHP;
            set => this.RaiseAndSetIfChanged(ref m_CurrentHP, value);
        }

        private ushort m_MaxHP;
        public ushort MaxHP
        {
            get => m_MaxHP;
            set => this.RaiseAndSetIfChanged(ref m_MaxHP, value);
        }

        private ushort m_TempMaxHP;
        public ushort TempMaxHP
        {
            get => m_TempMaxHP;
            set => this.RaiseAndSetIfChanged(ref m_TempMaxHP, value);
        }
    }
}
