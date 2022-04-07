using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using LibApp.Interfaces;

namespace LibApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookRepository _bookRepository;

        public BooksController(ApplicationDbContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }

        public IActionResult Index()
        {
            var books = _bookRepository.GetBooks().ToList();

            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _bookRepository.GetBookById(id);

            return View(book);
        }

        public IActionResult Edit(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = _context.Genre.ToList()
            };

            return View("BookForm", viewModel);
        }

        public IActionResult New()
        {
            var viewModel = new BookFormViewModel
            {
                Genres = _context.Genre.ToList()
            };
            return View("BookForm", viewModel);
        }

        [HttpPost]
        public IActionResult Save(Book book)
        {
            if (book.Id == 0)
            {
                book.DateAdded = DateTime.Now;
                _bookRepository.AddBook(book);
            }
            else
            {
                var dbBook = _bookRepository.GetBookById(book.Id);
                    dbBook.Name = book.Name;
                    dbBook.AuthorName = book.AuthorName;
                    dbBook.GenreId = book.GenreId;
                    dbBook.ReleaseDate = book.ReleaseDate;
                    dbBook.DateAdded = book.DateAdded;
                    dbBook.NumberInStock = book.NumberInStock;
                _bookRepository.UpdateBook(dbBook);
            }

            try
            {
                _bookRepository.Save();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Books");
        }

        //[HttpGet]
        //[Route("api/books")]
        //public IList<Book> GetBooks()
        //{
        //    return _bookRepository.GetBooks().ToList();
        //}

    }
}
