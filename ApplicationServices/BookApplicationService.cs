using ApplicationInterfaces;
using BookReviews.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices
{
    public class BookApplicationService : IBookApplicationService
    {
        public IAPIClientService _iAPIClientService;

        public BookApplicationService(IAPIClientService iAPIClientService)
        {
            _iAPIClientService = iAPIClientService;
        }

        public async Task<BookDto?> AddBookAsync(AddBookDto addBookDto)
        {
            string apiUrl = "https://localhost:7292/api/books";

            var bookstr = Newtonsoft.Json.JsonConvert.SerializeObject(addBookDto);
            HttpContent content = new StringContent(bookstr, Encoding.UTF8, "application/json");

            var data = await _iAPIClientService.CallAPIPostAsync(apiUrl, content);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            var book = Newtonsoft.Json.JsonConvert.DeserializeObject<BookDto>(data);
            if (book == null)
            {
                return null;
            }
            return book;
        }

        public async Task<List<BookDto?>> GetAllActiveBooks()
        {
            string apiUrl = "https://localhost:7292/api/books";
            var data = await _iAPIClientService.CallAPIGetAsync(apiUrl);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            var books = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookDto>>(data);
            return books;
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            string apiUrl = "https://localhost:7292/api/books/" + id;
            var data = await _iAPIClientService.CallAPIGetAsync(apiUrl);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            var book = Newtonsoft.Json.JsonConvert.DeserializeObject<BookDto>(data);
            return book;
        }

        public async Task<BookDto?> SaveBookAsync(BookDto bookDto)
        {
            string apiUrl = "https://localhost:7292/api/books/update";

            var bookstr = Newtonsoft.Json.JsonConvert.SerializeObject(bookDto);
            HttpContent content = new StringContent(bookstr, Encoding.UTF8, "application/json");

            var data = await _iAPIClientService.CallAPIPostAsync(apiUrl, content);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            var book = Newtonsoft.Json.JsonConvert.DeserializeObject<BookDto>(data);
            if (book == null)
            {
                return null;
            }
            return book;
        }
    }
}
