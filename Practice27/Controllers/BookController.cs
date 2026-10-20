using System;
using System.Collections.Generic;
using System.Text;


namespace Practice27.Controllers
{
    using Practice27.Models;
    using Practice27.Views;
    internal class BookController(BookModel model, ConsoleView view)
    {
        public void Run()
        {
            bool isRunning = true;
            while(isRunning)
            {
                view.ShowMenu();
                string choice = view.ReadInput(string.Empty);

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        ShowCatalog();
                        break;
                    case "3":
                        SearchBook();
                        break;
                    case "4":
                        DeleteBook();
                        break;
                    case "5":
                        isRunning = false;
                        view.DisplayMessage("Program finished.", ConsoleColor.Cyan);
                        break;
                    default:
                        view.DisplayMessage("Error: invalid choice! Choose option from 1 to 5.", ConsoleColor.Red);
                        break;
                }
            }
        }

        private void AddBook()
        {
            string title = view.ReadInput("Enter book title: ");
            if (string.IsNullOrWhiteSpace(title))
            {
                view.DisplayMessage("Error: Book title cannot be empty!", ConsoleColor.Red);
                return;
            }

            string author = view.ReadInput("Enter book author: ");
            if (string.IsNullOrWhiteSpace(author))
            {
                view.DisplayMessage("Error: Book author cannot be empty!", ConsoleColor.Red);
                return;
            }

            model.AddBook(new Book(title, author));
            view.DisplayMessage($"Added: '{title}' - {author}", ConsoleColor.Green);
        }

        private void ShowCatalog()
        {
            var books = model.GetBooks();
            view.DisplayBooks(books);
        }

        private void SearchBook()
        {
            string query = view.ReadInput("Enter title or author to search: ");
            if (string.IsNullOrEmpty(query))
            {
                view.DisplayMessage("Error: search query cannot be empty!", ConsoleColor.Red);
                return;
            }

            var results = model.SearchBooks(query);
            view.DisplayMessage($"Search '{query}': found {results.Count} book(s)", ConsoleColor.Cyan);
            view.DisplayBooks(results);
        }

        private void DeleteBook()
        {
            string title = view.ReadInput("Enter book title to delete: ");
            if (string.IsNullOrWhiteSpace(title))
            {
                view.DisplayMessage("Error: Book author cannot be empty!", ConsoleColor.Red);
                return;
            }

            bool isDeleted = model.DeleteBookByTitle(title);
            if (isDeleted)
            {
                view.DisplayMessage($"Entry '{title}' succesfully deleted.", ConsoleColor.Green);
            }
            else
            {
                view.DisplayMessage($"Error: book titled '{title}' not found.", ConsoleColor.Red);
            }
        }
    }
}
