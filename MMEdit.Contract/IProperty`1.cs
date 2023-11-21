namespace MMEdit
{
    public interface IProperty<T>
    {
         string Name { get; set; }

         T Value { get; set; }

         bool IsChanged { get; }

         void Commit();
    }
}
