using ApplicationInterfaces;
using BookReviews.Models.Dto;
using System.Text;

namespace ApplicationServices
{
    public class AuthorApplicationService : IAuthorApplicationService
    {
        public IAPIClientService _iAPIClientService;

        public AuthorApplicationService(IAPIClientService iAPIClientService)
        {
            _iAPIClientService = iAPIClientService;
        }

        public async Task<AuthorDto?> AddAuthorAsync(AddAuthorDto addAuthorDto)
        {
            string apiUrl = "https://localhost:7292/api/authors";

            var authorstr = Newtonsoft.Json.JsonConvert.SerializeObject(addAuthorDto);
            HttpContent content = new StringContent(authorstr, Encoding.UTF8, "application/json");

            var data = await _iAPIClientService.CallAPIPostAsync(apiUrl, content);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            var author = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthorDto>(data);
            if (author == null)
            {
                return null;
            }
            return author;
        }

        public async Task<AuthorDto?> DeleteAuthorAsync(AuthorDto authorDto, int currentUserId)
        {
            string apiUrl = "https://localhost:7292/api/authors/delete";

            authorDto.DeletedByUserId = currentUserId;
            authorDto.DeletedOn = DateTime.UtcNow;
            authorDto.IsDeleted = true;

            var authorstr = Newtonsoft.Json.JsonConvert.SerializeObject(authorDto);
            HttpContent content = new StringContent(authorstr, Encoding.UTF8, "application/json");

            var data = await _iAPIClientService.CallAPIPostAsync(apiUrl, content);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            var author = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthorDto>(data);
            if (author == null)
            {
                return null;
            }
            return author;
        }

        public async Task<List<AuthorDto>?> GetAllActiveAuthors()
        {
            string apiUrl = "https://localhost:7292/api/authors";
            var data = await _iAPIClientService.CallAPIGetAsync(apiUrl);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            var authors = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AuthorDto>>(data);
            return authors;
        }

        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            string apiUrl = "https://localhost:7292/api/authors/" + id;
            var data = await _iAPIClientService.CallAPIGetAsync(apiUrl);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            var authors = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthorDto>(data);
            return authors;
        }

        public async Task<AuthorDto?> SaveAuthorAsync(AuthorDto authorDto)
        {
            string apiUrl = "https://localhost:7292/api/authors/update";

            var userstr = Newtonsoft.Json.JsonConvert.SerializeObject(authorDto);
            HttpContent content = new StringContent(userstr, Encoding.UTF8, "application/json");

            var data = await _iAPIClientService.CallAPIPostAsync(apiUrl, content);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            var author = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthorDto>(data);
            if (author == null)
            {
                return null;
            }
            return author;
        }
    }
}