﻿using BooksLib.Events;
using BooksLib.Models;
using BooksLib.Services;
using GenericViewModels.Services;
using GenericViewModels.ViewModels;
using System;
using System.Threading.Tasks;

namespace BooksLib.ViewModels
{
    public class BooksViewModel : MasterDetailViewModel<BookItemViewModel, Book>
    {
        private readonly IItemsService<Book> _booksService;
        private readonly INavigationService _navigationService;

        public BooksViewModel(IItemsService<Book> booksService, ISelectedItemService<Book> selectedBookService, INavigationService navigationService)
            : base(booksService, selectedBookService)
        {
            _booksService = booksService ?? throw new ArgumentNullException(nameof(booksService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            EventAggregator<NavigationInfoEvent>.Instance.Event += (sender, e) =>
            {
                _navigationService.UseNavigation = e.UseNavigation;
            };

            PropertyChanged += async (sender, e) =>
            {
                if (_navigationService.UseNavigation && e.PropertyName == nameof(SelectedItem) && _navigationService.CurrentPage == PageNames.BooksPage)
                {
                    await _navigationService.NavigateToAsync(PageNames.BookDetailPage);
                }
            };
        }

        protected override Task OnAddCoreAsync()
        {
            var newBook = new Book();
            Items.Add(newBook);
            SelectedItem = newBook;
            return base.OnRefreshCoreAsync();
        }

        protected override BookItemViewModel ToViewModel(Book item) => new BookItemViewModel(item, _booksService);
    }
}
