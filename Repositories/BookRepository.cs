using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using LibApp.Interfaces;
using LibApp.Models;
using LibApp.Data;

namespace LibApp.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Book> GetBooks()
        {
            
            return _context.Books.Include(b => b.Genre);
        }


        public Book GetBookById(int id)
        {
            return _context.Books.Find(id);
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
        }

        public void DeleteBook(int id)
        {
            _context.Books.Remove(GetBookById(id));
        }

        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
        }

        public void Save()
        {
            _context.SaveChanges();
        }



    }
}