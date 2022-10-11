using ApplicationInterfaces;
using BookReviews.Models.Dto;

namespace ApplicationServices
{
    public class AuthorApplicationService : IAuthorApplicationService
    {
        public IAPIClientService _iAPIClientService;

        public AuthorApplicationService(IAPIClientService iAPIClientService)
        {
            _iAPIClientService = iAPIClientService;
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
    }
}