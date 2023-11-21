using ReactiveUI;
using System.Collections.Generic;
using System;

namespace MMEdit.ViewModels
{
    public static class ViewModelExtensions
    {
        /// <summary>
        /// RaiseAndSetIfChanged fully implements a Setter for a read-write
        /// property on a ReactiveObject, using CallerMemberName to raise the notification
        /// and the ref to the backing field to set the property.
        /// </summary>
        /// <typeparam name="TObj">The type of the This.</typeparam>
        /// <typeparam name="TRet">The type of the return value.</typeparam>
        /// <param name="reactiveObject">The <see cref="ReactiveObject"/> raising the notification.</param>
        /// <param name="backingField">A Reference to the backing field for this
        /// property.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="propertyName">The name of the property, usually
        /// automatically provided through the CallerMemberName attribute.</param>
        /// <returns>The newly set value, normally discarded.</returns>
        public static TRet RaiseAndSetIfChanged2<TObj, TRet>(
            this TObj reactiveObject,
            ref TRet backingField,
            TRet newValue,
            params string[] propertyNames)
            where TObj : IViewModel, IReactiveObject
        {
            if (propertyNames is null)
            {
                throw new ArgumentNullException(nameof(propertyNames));
            }

            if (propertyNames.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(propertyNames));
            }

            if (EqualityComparer<TRet>.Default.Equals(backingField, newValue))
            {
                return newValue;
            }

            foreach (var name in propertyNames)
            {
                reactiveObject.RaisePropertyChanging(name);
            }
            backingField = newValue;
            foreach (var name in propertyNames)
            {
                reactiveObject.RaisePropertyChanged(name);
            }

            return newValue;
        }
    }
}
