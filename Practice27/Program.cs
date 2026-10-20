using Practice27.Controllers;
using Practice27.Models;
using Practice27.Views;

var model = new BookModel();
var view = new ConsoleView();
var controller = new BookController(model, view);

controller.Run();