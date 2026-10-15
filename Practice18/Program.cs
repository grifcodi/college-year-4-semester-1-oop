
List<LibraryItem> libraryItems = new List<LibraryItem>()
{
    new Book { Title = "1984", Author = "George Orwell"},
    new Ebook { Title = "The Plague", Author = "Albert Camus"},
};

foreach (LibraryItem libraryItem in libraryItems)
{
    libraryItem.GetInfo();
    if (libraryItem is IDownloadable downloadable)
    {
        downloadable.Download();
    }
}


public abstract class LibraryItem
{
    public string Title { get; set; }
    public string Author { get; set; }
    public abstract void GetInfo();

}

public interface IDownloadable
{
    void Download();
}

public class Book: LibraryItem
{
    public override void GetInfo()
    {
        Console.WriteLine($"This is a book titled '{this.Title}' by {this.Author}");
    }
}

public class Ebook: LibraryItem, IDownloadable
{
    public override void GetInfo()
    {
        Console.WriteLine($"This a digital copy of a book '{this.Title}' by {this.Author}");
    }

    public void Download()
    {
        Console.WriteLine("Ebook downloaded!");
    }
}

