using System;

namespace MMEdit
{
    public interface IProperty
    {
        Type Type { get; }

        string Name { get; }

        object Value { get; set; }

        bool IsChanged { get; }

        void Commit();
    }
}
