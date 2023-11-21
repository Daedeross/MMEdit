namespace MMEdit
{
    public interface ICollectionProperty : IProperty
    {
        IObservableReadOnlyCollection<IProperty> Children { get; }

        bool CanMutate { get; }

        bool TryAdd(IProperty property);

        bool TryRemove(IProperty property);
    }
}
