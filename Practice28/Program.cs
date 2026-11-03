using Practice28.Controllers;
using Practice28.Models;
using Practice28.Views;

Console.OutputEncoding = System.Text.Encoding.UTF8;

BookView bookView = new();

IBookRepository memoryRepo = new MemoryRepository();
BookService memoryService = new(memoryRepo);
BookController memoryController = new(memoryService, bookView);

IBookRepository consoleRepo = new ConsoleRepository();
BookService consoleService = new(consoleRepo);
BookController consoleController = new(consoleService, bookView);

memoryController.AddBook("Чистий код");
consoleController.AddBook("Патерни проєктування");
