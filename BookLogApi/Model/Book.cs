using System.Reflection.Metadata.Ecma335;

namespace BookLogApi.Model
{
    public class Book //private properties as best you can later.
    {
public Guid Id { get; set; }
public string Title { get; set; }
public string SubTitle { get; set; }
public string Author { get; set; }
public string Publisher { get; set; }
public string Genre { get; set; }
public string Subject { get; set; }
public string Description { get; set; }
//public string? Image { get; set; }
public byte Rating { get; set; }

public string? Image { get; set; }

public Book()
{

}
public Book( string title, string subTitle, string author, string publisher, string genre, string subject, string description, byte rating,string image)
{
    Id = Guid.NewGuid();
    Title = title;
    SubTitle = subTitle;
    Author = author;
    Publisher = publisher;
    Genre = genre;
    Subject = subject;
    Description = description;
    Rating = rating;
    Image = image;
}

public void AlterBook(Book bookToChangeTo)
{

    Title = bookToChangeTo.Title;
    SubTitle = bookToChangeTo.SubTitle;
    Author = bookToChangeTo.Author;
    Publisher = bookToChangeTo.Publisher;
    Genre = bookToChangeTo.Genre;
    Subject = bookToChangeTo.Subject;
    Description = bookToChangeTo.Description;
    Rating = bookToChangeTo.Rating;
    Image = bookToChangeTo.Image;
}
    }

}
