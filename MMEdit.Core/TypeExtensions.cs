namespace MMEdit
{
    using System;
    using System.Linq;

    public static class TypeExtensions
    {
        public static bool IsGenericType(this Type type, Type genericTypeDefinition)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (genericTypeDefinition == null) throw new ArgumentNullException(nameof(genericTypeDefinition));
            if (!genericTypeDefinition.IsGenericTypeDefinition) throw new ArgumentException($"{nameof(genericTypeDefinition)} is not a generic type definition.");

            return type.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == genericTypeDefinition);
        }

        public static bool ImplementsGenericType<TGeneric>(this object obj)
        {
            return IsGenericType(obj.GetType(), typeof(TGeneric));
        }
    }
}
