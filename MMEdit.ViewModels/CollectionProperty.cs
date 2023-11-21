#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
namespace MMEdit.ViewModels
{
    using ReactiveUI;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;

    public class CollectionProperty : ViewModelBase, ICollectionProperty
    {
        private Action<object> _setter;
        private int _initailCount;

        private readonly ObservableCollection2<IProperty> _children;
        public IObservableReadOnlyCollection<IProperty> Children => (IObservableReadOnlyCollection<IProperty>)_children;

        public Type Type { get; private set; }

        private bool m_CanMutate;
        public bool CanMutate
        {
            get => m_CanMutate;
            set => this.RaiseAndSetIfChanged(ref m_CanMutate, value);
        }

        public object Value
        {
            get => _children;
            set { }
        }

        private string m_Name;
        public string Name
        {
            get => m_Name;
            set => this.RaiseAndSetIfChanged(ref m_Name, value);
        }

        private bool m_IsChanged = false;
        public bool IsChanged
        {
            get => m_IsChanged;
            protected set => this.RaiseAndSetIfChanged(ref m_IsChanged, value);
        }

        public void Commit()
        {
            foreach (var child in _children)
            {
                child.Commit();
            }
            IsChanged = false;
            _initailCount = _children.Count;

            _setter(_children);
        }

        public bool TryAdd(IProperty property)
        {
            if (m_CanMutate)
            {
                _children.Add(property);
                this.RaisePropertyChanged(nameof(Value));
                return true;
            }

            return false;
        }

        public bool TryRemove(IProperty property)
        {
            if (m_CanMutate)
            {
                _children.Remove(property);
                this.RaisePropertyChanged(nameof(Value));
                return true;
            }

            return false;
        }

        public CollectionProperty(
            Type type,
            string name,
            Action<object> setter,
            ICollection<IProperty> children)
        {
            Type = type;

            m_Name = name;
            _setter = setter;
            if (type.IsArray)
            {
                m_CanMutate = false;
            }

            _children = new ObservableCollection2<IProperty>(children);
            _children.CollectionChanged += OnChildCollectionChanged;
        }

        private void OnChildCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is INotifyPropertyChanged x)
                    {
                        x.PropertyChanged += OnChildChanged;
                    }
                }
            }
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is INotifyPropertyChanged x)
                    {
                        x.PropertyChanged -= OnChildChanged;
                    }
                }
            }
        }

        private void OnChildChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.Equals(nameof(IProperty.IsChanged), e.PropertyName))
            {
                this.RaisePropertyChanged(nameof(Value));

                bool changed = false;
                if (_initailCount != _children.Count)
                {
                    changed = true;
                }
                else
                {
                    foreach (var child in _children)
                    {
                        if (child.IsChanged)
                        {
                            changed = true;
                            break;
                        }
                    }
                }

                IsChanged = changed;
            }
        }

        private bool disposedValue;

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _children.CollectionChanged -= OnChildCollectionChanged;
                    foreach (var item in _children)
                    {
                        ((INotifyPropertyChanged)item).PropertyChanged -= OnChildChanged;
                    }
                }

                disposedValue = true;
            }

            base.Dispose(disposing);
        }
    }
}

#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).