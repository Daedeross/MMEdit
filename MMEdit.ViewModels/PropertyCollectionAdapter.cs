namespace MMEdit.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Factory class to create <see cref="IProperty"/> instances for most any object tree (see Remarks).
    /// Only walks through readable and writable properties.
    /// </summary>
    /// <remarks>
    /// Supported types are: Primitives, <see cref="string"/>, <see cref="Array"/>, <see cref="IList{T}"/>,
    /// and class/structs with properties that are one of the aforementioned types, or another class/struct.
    /// </remarks>
    public class PropertyCollectionAdapter: IPropertyAdapter
    {
        public static IPropertyAdapter Instance { get; private set; } = new PropertyCollectionAdapter();

        private readonly ConcurrentDictionary<Type, Func<Type, string, object?, Action<object>, object>> _constructorCache = new();
        private readonly ConcurrentDictionary<Type, Func<Type, string, object?, Action<object>, IProperty>> _cache = new();

        public IProperty CreateProperty(Type type, string name, object? value, Action<object> setter,
            params string[] blacklist)
        {
            var func = _cache.GetOrAdd(type, t => MakeLambda(t, blacklist));
            return func(type, name, value, setter);
        }

        public IProperty<T> CreateProperty<T>(string name, T value, Action<object> setter)
        {
            return (IProperty<T>)CreateProperty(typeof(T), name, value, setter);
        }

        internal Func<Type, string, object?, Action<object>, IProperty> MakeLambda(Type type, string[] blacklist)
        {
            if (type.IsPrimitive || type == typeof(string) || type == typeof(DateTime))
            {
                var constructor = _constructorCache.GetOrAdd(type, MakeConstructor);

                return (Type type, string name, object? value, Action<object> setter)
                    => (IProperty)constructor(type, name, value, setter);
            }
            else if (typeof(ICollection).IsAssignableFrom(type))
            {
                Type childType;
                if (type.IsArray)
                {
                    childType = type.GetElementType() ?? throw new InvalidCastException();

                    return (Type type, string name, object? value, Action<object> setter) =>
                    {
                        if (value is Array array)
                        {
                            var children = new List<IProperty>();

                            for (int i = 0; i < array.Length; i++)
                            {
                                Func<object> getItem = () => array.GetValue(i);
                                Action<object> setItem = (object value) => array.SetValue(value, i);

                                children.Add(CreateProperty(childType, $"{i:d2}", getItem(), setItem));
                            }

                            return new CollectionProperty(type, name, setter, children);
                        }
                        else throw new InvalidCastException(nameof(value));
                    };
                }
                else if (type.IsGenericType && typeof(IList<>).IsAssignableFrom(type.GetGenericTypeDefinition()))
                {
                    childType = type.GetGenericArguments()[0];

                    return (Type type, string name, object? value, Action<object> setter) =>
                    {
                        if (value is IList list)
                        {
                            var children = new List<IProperty>();

                            for (int i = 0; i < list.Count; i++)
                            {
                                Func<object> getItem = () => list[i];
                                Action<object> setItem = (object value) => list[i] = value;

                                children.Add(CreateProperty(childType, $"{i:d2}", getItem(), setItem));
                            }

                            return new CollectionProperty(type, name, setter, children);
                        }
                        else throw new InvalidCastException(nameof(value));
                    };
                }
                else
                {
                    throw new NotSupportedException("only generic array or list-like collections are not supported.");
                }
            }
            else
            {
                ICollection<string> blist = blacklist is null
                    ? new string[0]
                    : new HashSet<string>(blacklist);

                var properties = type
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(pi => pi.CanRead && pi.CanWrite)
                    .Where(pi => !blist.Contains(pi.Name))
                    .Select(pi =>
                    {
                        var getter = GenerateGetterLambda(type, pi);
                        var setter = GenerateSetterLambda(type, pi);

                        return (pi.PropertyType, pi.Name, getter, setter);
                    })
                    .ToList();

                return (Type type, string name, object? value, Action<object> setter) =>
                {
                    var children = properties
                        .Select(tuple => CreateProperty(tuple.PropertyType, tuple.Name, tuple.getter(value), (obj) => tuple.setter(value, obj)))
                        .ToList();

                    return new CollectionProperty(type, name, setter, children);
                };
            }
        }

        internal Func<Type, string, object?, Action<object>, object> MakeConstructor(Type typeParameter)
        {
            Type[] constructorParams = new Type[] { typeof(Type), typeof(string), typeParameter, typeof(Action<object>) };
            Type genericType = typeof(Property<>).MakeGenericType(typeParameter);
            ConstructorInfo constructorInfo = genericType.GetConstructor(constructorParams)
                ?? throw new InvalidOperationException("Missing constructor");

            constructorParams[2] = typeof(object);

            var paramArray = constructorParams
                .Select(t => Expression.Parameter(t))
                .ToArray();

            var constructorExpressions = new Expression[]
            {
                paramArray[0],
                paramArray[1],
                Expression.Convert(paramArray[2], typeParameter),
                paramArray[3]
            };

            return Expression.Lambda<Func<Type, string, object?, Action<object>, object>>(
                Expression.Convert(
                    Expression.New(constructorInfo, constructorExpressions),
                    typeof(object)
                ),
                paramArray)
                .Compile();
        }

        internal Func<object, object> GenerateGetterLambda(Type type, PropertyInfo propertyInfo)
        {
            if (propertyInfo.GetMethod is null)
            {
                throw new ArgumentException($"Property '{propertyInfo.Name}' must be a readable property.");
            }

            var param = Expression.Parameter(typeof(object));

            return Expression.Lambda<Func<object, object>>(
                Expression.Convert(
                    Expression.Property(
                        Expression.Convert(param, type),
                        propertyInfo.GetMethod),
                    typeof(object)
                ),
                param)
                .Compile();
        }

        internal Action<object, object> GenerateSetterLambda(Type type, PropertyInfo propertyInfo)
        {
            if (propertyInfo.SetMethod is null)
            {
                throw new ArgumentException($"Property '{propertyInfo.Name}' must be a writable property.");
            }

            var objParameter = Expression.Parameter(typeof(object));
            var valueParameter = Expression.Parameter(typeof(object));

            return Expression.Lambda<Action<object, object>>(
                    Expression.Call(
                        Expression.Convert(objParameter, type),
                        propertyInfo.SetMethod,
                        Expression.Convert(valueParameter, propertyInfo.PropertyType)),
                    objParameter, valueParameter)
                .Compile();
        }
    }
}
