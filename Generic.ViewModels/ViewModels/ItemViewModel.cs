﻿namespace GenericViewModels.ViewModels
{
    /// <summary>
    /// base class for view-models that shows a single item
    /// </summary>
    /// <typeparam name="T">Item type for the view-model to display</typeparam>
    public abstract class ItemViewModel<T> : ViewModelBase, IItemViewModel<T>
    {
        public ItemViewModel() { }

        public ItemViewModel(T item) => Item = item;

        private T _item;
        public virtual T Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }
    }
}
