using System;
using System.Collections.Generic;
using System.Text;

namespace Practice27.Models
{
    internal class BookModel
    {
        private readonly List<Book> _catalog = [];

        public void AddBook(Book book)
        {
            _catalog.Add(book);
        }

        public IReadOnlyList<Book> GetBooks()
        {
            return _catalog.AsReadOnly();
        }

        public List<Book> SearchBooks(string query)
        {
            return [.. _catalog
                .Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            b.Author.Contains(query, StringComparison.OrdinalIgnoreCase))];
        }

        public bool DeleteBookByTitle(string title)
        {
            var book = _catalog.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (book == null)
            {
                return false;
            }
            _catalog.Remove(book);
            return true;
        }
    }
}
