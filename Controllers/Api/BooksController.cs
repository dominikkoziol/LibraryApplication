using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        #region Private Members
        private ApplicationDbContext _context;
        private IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        #endregion

        public BooksController(ApplicationDbContext context, IMapper mapper, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        // GET /api/books
        [HttpGet]

        public IEnumerable<BookDto> GetBooks(string query = null)
        {
            var booksQuery = _bookRepository.GetBooks().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
                booksQuery = booksQuery.Where(b => b.Name.Contains(query));
            

  

            return booksQuery.ToList().Select(_mapper.Map<Book, BookDto>);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveBook(int id)
        {
            _bookRepository.DeleteBook(id);
            _bookRepository.Save();
            return Ok();
        }

    }
}
