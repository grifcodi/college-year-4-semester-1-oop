using Practice28.Models;
using Practice28.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Practice28.Controllers
{
    internal class BookController(BookService bookService, BookView bookView)
    {
        private readonly BookService bookService = bookService;
        private readonly BookView bookView = bookView;

        public void AddBook(string bookName)
        {
            string result = bookService.AddBook(bookName);

            bookView.DisplayMessage(result);
        }
    }
}
