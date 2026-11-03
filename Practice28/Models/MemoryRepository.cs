using System;
using System.Collections.Generic;
using System.Text;

namespace Practice28.Models
{
    internal class MemoryRepository : IBookRepository
    {
        private readonly List<string> _books = [];

        public string Save(string bookName)
        {
            _books.Add(bookName);
            return $"MemoryRepository: додано «{bookName}».\nКниг у пам’яті: {_books.Count}";
        }
    }
}

