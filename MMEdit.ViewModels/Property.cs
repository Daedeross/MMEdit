namespace MMEdit.ViewModels
{
    using ReactiveUI;
    using System;

    public class Property : ViewModelBase, IProperty
    {
        private Action<object> _setter;

        public Type Type { get; private set; }

        private string m_Name;
        public string Name
        {
            get => m_Name;
            set => this.RaiseAndSetIfChanged(ref m_Name, value);
        }

        protected object m_InitialValue;
        protected object m_Value;
        public virtual object Value
        {
            get => m_Value;
            set
            {
                this.RaiseAndSetIfChanged(ref m_Value, value);
                IsChanged = !Equals(m_Value, m_InitialValue);
            }
        }

        private bool m_IsChanged;
        public bool IsChanged
        {
            get => m_IsChanged;
            protected set => this.RaiseAndSetIfChanged(ref m_IsChanged, value);
        }

        public void Commit()
        {
            m_InitialValue = Value;
            IsChanged = false;

            _setter(m_Value);
        }

        public Property(Type type, string name, object value, Action<object> setter)
        {
            m_Name = name;
            Type = type;
            m_InitialValue = value;
            m_Value = value;
            _setter = setter;
        }
    }
}
