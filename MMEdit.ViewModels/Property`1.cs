namespace MMEdit.ViewModels
{
    using ReactiveUI;
    using System;

    public class Property<T> : Property, IProperty<T>
    {
        public override object Value
        {
            get => m_Value;
            set
            {
                if (value is T)
                {
                    this.RaiseAndSetIfChanged(ref m_Value, value);
                }
            }
        }

        T IProperty<T>.Value
        {
            get => (T)m_Value;
            set => this.RaiseAndSetIfChanged(ref m_Value, value);
        }

        public Property(Type type, string name, T value, Action<object> setter) : base(type, name, value, setter)
        {
        }
    }
}
