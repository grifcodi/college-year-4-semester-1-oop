using System;
using System.Collections.Generic;
using System.Text;

namespace Practice28.Models
{
    internal interface IBookRepository
    {
        string Save(string bookName);
    }
}
