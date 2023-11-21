namespace MMEdit
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public interface IObservableReadOnlyCollection<T>: IReadOnlyCollection<T>, INotifyCollectionChanged
    {
    }
}
