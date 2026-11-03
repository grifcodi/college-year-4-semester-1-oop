using System;
using System.Collections.Generic;
using System.Text;

namespace Practice28.Models
{
    internal class BookService(IBookRepository bookRepository)
    {
        private readonly IBookRepository bookRepository = bookRepository;

        public string AddBook(string bookName)
        {
            return bookRepository.Save(bookName);
        }
    }
}
