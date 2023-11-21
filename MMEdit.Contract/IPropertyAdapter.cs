namespace MMEdit
{
    using System;

    /// <summary>
    /// Interface for utility that creates IProperty instances for an object tree.
    /// </summary>
    public interface IPropertyAdapter
    {
        /// <summary>
        /// Creates an IProperty for a particular type, name, value, and setter.
        /// May be recursivedly called by child properties.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="valueCallback">A callback to return the initial value.</param>
        /// <param name="setter"></param>
        /// <returns><see cref="IProperty"/></returns>
        IProperty CreateProperty(Type type, string name, object value, Action<object> setter, params string[] blacklist);

        /// <summary>
        /// Generic typed method to create an <see cref="IProperty{T}"/>
        /// </summary>
        /// <typeparam name="T">The property type.</typeparam>
        /// <param name="name"></param>
        /// <param name="getter"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        IProperty<T> CreateProperty<T>(string name, T value, Action<object> setter);
    }
}
