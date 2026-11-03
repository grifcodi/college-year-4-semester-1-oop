using System;
using System.Collections.Generic;
using System.Text;

namespace Practice28.Models
{
    internal class ConsoleRepository : IBookRepository
    {
        public string Save(string bookName)
        {
            return $"ConsoleRepository: збережено «{bookName}».";
        }
    }
}
