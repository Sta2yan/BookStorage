using System;
using System.Collections.Generic;

namespace BookStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            Storage storage = new Storage();
            bool isActive = true;

            while (isActive)
            {
                Console.WriteLine($"{(int)MenuCommands.AddBook}. {MenuCommands.AddBook}" +
                                  $"\n{(int)MenuCommands.DeleteBook}. {MenuCommands.DeleteBook}" +
                                  $"\n{(int)MenuCommands.FindBooksByTitle}. {MenuCommands.FindBooksByTitle}" +
                                  $"\n{(int)MenuCommands.FindBooksByAuthor}. {MenuCommands.FindBooksByAuthor}" +
                                  $"\n{(int)MenuCommands.FindBooksByYearRelease}. {MenuCommands.FindBooksByYearRelease}" +
                                  $"\n{(int)MenuCommands.ShowAllBooks}. {MenuCommands.ShowAllBooks}" +
                                  $"\n{(int)MenuCommands.Exit}. {MenuCommands.Exit}");

                MenuCommands userInput = (MenuCommands)GetNumber(Console.ReadLine());

                switch (userInput)
                {
                    case MenuCommands.AddBook:
                        storage.AddBook();
                        break;
                    case MenuCommands.DeleteBook:
                        storage.DeleteBook();
                        break;
                    case MenuCommands.FindBooksByTitle:
                        storage.FindBooksByTitle();
                        break;
                    case MenuCommands.FindBooksByAuthor:
                        storage.FindBooksByAuthor();
                        break;
                    case MenuCommands.FindBooksByYearRelease:
                        storage.FindBooksByYearRelease();
                        break;
                    case MenuCommands.ShowAllBooks:
                        storage.ShowInfo();
                        break;
                    case MenuCommands.Exit:
                        isActive = false;
                        break;
                }

                Console.ReadKey(true);
                Console.Clear();
            }

            Console.WriteLine("Выход ...");
        }

        public static int GetNumber(string textNumber)
        {
            int number;

            while (int.TryParse(textNumber, out number) == false)
            {
                Console.WriteLine("Повторите попытку:");
                textNumber = Console.ReadLine();
            }

            return number;
        }
    }

    interface IShowInfo
    {
        public void ShowInfo();
    }

    enum MenuCommands
    {
        AddBook = 1,
        DeleteBook,
        FindBooksByTitle,
        FindBooksByAuthor,
        FindBooksByYearRelease,
        ShowAllBooks,
        Exit
    }

    class Storage : IShowInfo
    {
        private List<Book> _books = new List<Book>();
        
        public Storage(List<Book> books = null)
        {
            if (books == null)
                SetDefaultListBooks();
        }

        public void AddBook(Book book = null)
        {
            if (book == null)
            {
                book = GetNewBook();
            }

            _books.Add(book);
            Console.WriteLine("Книга добавлена в хранилище");
        }

        public void DeleteBook(int index = 0)
        {
            if (index == 0)
            {
                ShowInfo();
                Console.Write("\nВыберите книгу: ");
                index = GetIndexBook();
            }

            Console.WriteLine("Книга удалена:");
            _books[index].ShowInfo();
            _books.RemoveAt(index);
        }

        public void FindBooksByTitle(string title = null)
        {
            if (title == null)
            {
                Console.WriteLine("Введите название книги:");
                title = Console.ReadLine();
            }

            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].Title == title)
                {
                    _books[i].ShowInfo();
                }
            }
        }

        public void FindBooksByAuthor(string author = null)
        {
            if (author == null)
            {
                Console.WriteLine("Введите автора книги:");
                author = Console.ReadLine();
            }

            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].Author == author)
                {
                    _books[i].ShowInfo();
                }
            }
        }

        public void FindBooksByYearRelease(int yearRealse = 0)
        {
            if (yearRealse == 0)
            {
                Console.WriteLine("Введите год выпуска книги:");
                yearRealse = GetYearRelease();
            }

            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].YearRelease == yearRealse)
                {
                    _books[i].ShowInfo();
                }
            }
        }

        public void ShowInfo()
        {
            if (_books.Count > 0)
            {
                Console.WriteLine("Список всех книг:");

                for (int i = 0; i < _books.Count; i++)
                {
                    Console.Write(i + 1 + ". ");
                    _books[i].ShowInfo();
                }
            }
            else
            {
                Console.WriteLine("В хранилище нет книг");
            }
        }

        private int GetIndexBook()
        {
            int index;

            do
            {
                index = Program.GetNumber(Console.ReadLine()) - 1;
            } while (index < 0 || index >= _books.Count);

            return index;
        }

        private Book GetNewBook()
        {
            Console.WriteLine("Введите название:");
            string title = Console.ReadLine();
            Console.WriteLine("Введите автора:");
            string author = Console.ReadLine();
            Console.WriteLine("Введите год выпуска:");
            int yearRelease = GetYearRelease();
            
            Book book = new Book(title, author, yearRelease);

            return book;
        }

        private int GetYearRelease()
        {
            int number;

            do
            {
                number = Program.GetNumber(Console.ReadLine());
            } while (number <= 0);

            return number;
        }

        private void SetDefaultListBooks()
        {
            _books.Add(new Book("Гарри Поттер и философский камень", "Джоан Роулинг", 1997));
            _books.Add(new Book("Гарри Поттер и Тайная комната", "Джоан Роулинг", 1998));
            _books.Add(new Book("Гарри Поттер и узник Азкабана", "Джоан Роулинг", 1999));
            _books.Add(new Book("Гарри Поттер и Кубок огня", "Джоан Роулинг", 2000));
            _books.Add(new Book("Гарри Поттер и Орден Феникса", "Джоан Роулинг", 2003));
            _books.Add(new Book("Гарри Поттер и Принц-полукровка", "Джоан Роулинг", 2005));
            _books.Add(new Book("Гарри Поттер и Дары Смерти", "Джоан Роулинг", 2007));
            _books.Add(new Book("Самый богатый человек в Вавилоне", "Джордж Сэмюэль Клейсон", 1926));
            _books.Add(new Book("Стив Джобс. Тот, кто думал иначе", "Секачева", 2015));
        }
    }

    class Book : IShowInfo
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public int YearRelease { get; private set; }

        public Book(string title, string author, int yearRelease)
        {
            Title = title;
            Author = author;
            YearRelease = yearRelease;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Название - {Title} | Автор - {Author} | Год выпуска - {YearRelease}");
        }
    }
}