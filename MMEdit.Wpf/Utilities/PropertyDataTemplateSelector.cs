namespace MMEdit.Wpf.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public class PropertyDataTemplateSelector : DataTemplateSelector
    {
        private readonly ISet<Type> _intLike = new HashSet<Type>
        {
            typeof(sbyte),
            typeof(byte),
            typeof(int),
            typeof(uint),
            typeof(short),
            typeof(ushort),
        };

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (container is FrameworkElement element)
            {
                if (item is IProperty property)
                {
                    if (_intLike.Contains(property.Type))
                    {
                        return element.FindResource($"{property.Type.Name}Template") as DataTemplate;
                    }
                    else if (property.Type == typeof(string))
                    {
                        return element.FindResource("stringTemplate") as DataTemplate;
                    }
                    else if (property is ICollectionProperty)
                    {
                        return element.FindResource("collectionTemplate") as DataTemplate;
                    }
                }
            }

            return null;
        }
    }
}
