namespace MMEdit.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Wrapper class arround <see cref="ObservableCollection{T}"/> that
    /// explicitly implements <see cref="IObservableReadOnlyCollection{T}"/>
    /// </summary>
    /// <typeparam name="T">The element type of the collection.</typeparam>
    public class ObservableCollection2<T> : ObservableCollection<T>, IObservableReadOnlyCollection<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollection2{T}"/>
        /// class.
        /// </summary>
        public ObservableCollection2() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollection2{T}"/>
        /// class that contains elements copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection from which the elements are copied.</param>
        /// <exception cref="System.ArgumentNullException">The collection parameter cannot be null.</exception>
        public ObservableCollection2(IEnumerable<T> collection) : base(collection) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollection2{T}"/>
        /// class that contains elements copied from the specified list.
        /// </summary>
        /// <param name="list">The list from which the elements are copied.</param>
        /// <exception cref="System.ArgumentNullException">The list parameter cannot be null.</exception>
        public ObservableCollection2(List<T> list) : base(list) { }
    }
}
