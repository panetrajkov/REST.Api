using Class03.Homework.WebApiApp.Models;

namespace Class03.Homework.WebApiApp
{
    public static class StaticDb
    {
        public static List<Book> Books = new List<Book>
        {
            new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" },
            new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee" },
            new Book { Title = "The Lord of the Rings", Author = "J.R.R. Tolkien",  },
            new Book { Title = "1984", Author = "George Orwell" },
            new Book { Title = "Pride and Prejudice", Author = "Jane Austen" },
        };
    }
}