using ApplicationInterfaces;
using BookReviews.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices
{
    public class ReviewApplicationService : IReviewApplicationService
    {
        public IAPIClientService _iAPIClientService;

        public ReviewApplicationService(IAPIClientService iAPIClientService)
        {
            _iAPIClientService = iAPIClientService;
        }

        public async Task<List<ReviewDto>?> GetAllActiveReviews()
        {
            string apiUrl = "https://localhost:7292/api/reviews";
            var data = await _iAPIClientService.CallAPIGetAsync(apiUrl);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            var reviewsDto = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReviewDto>>(data);
            return reviewsDto;
        }
    }
}
