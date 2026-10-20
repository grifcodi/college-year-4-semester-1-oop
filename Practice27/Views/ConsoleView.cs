using System;
using System.Collections.Generic;
using System.Text;

using Practice27.Models;

namespace Practice27.Views
{
    internal class ConsoleView
    {
        public void ShowMenu()
        {
            Console.WriteLine("\n--- Book Catalog ---");
            Console.WriteLine("1. Add book");
            Console.WriteLine("2. Show catalog");
            Console.WriteLine("3. Find book");
            Console.WriteLine("4. Delete book");
            Console.WriteLine("5. Exit");
            Console.Write("Choose action (1-5):");
        }

        public string ReadInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        public void DisplayMessage(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void DisplayBooks(IReadOnlyList<Book> books)
        {
            if (books.Count == 0)
            {
                DisplayMessage("No entries", ConsoleColor.Yellow);
                return;
            }

            Console.WriteLine("\nBook catalog:");
            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine($"{i + 1}. '{books[i].Title}' - {books[i].Author}");
            }
        }
    }
}
