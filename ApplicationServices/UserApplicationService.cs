using ApplicationInterfaces;
using BookReviews.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices
{
    public class UserApplicationService : IUserApplicationService
    {
        public IAPIClientService _iAPIClientService;

        public UserApplicationService(IAPIClientService iAPIClientService)
        {
            _iAPIClientService = iAPIClientService;
        }

        public async Task<UserDto> AuthenticateUserAsync(string email, string password)
        {
            var user = new UserDto
            {
                Email = email,
                Password = password,
            };

            string apiUrl = "https://localhost:7292/api/users/authenticate";

            var userstr = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(userstr, Encoding.UTF8, "application/json");

            var data = await _iAPIClientService.CallAPIPostAsync(apiUrl, content);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            var authorized = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDto>(data);
            if (authorized == null)
            {
                return null;
            }
            return authorized;
        }

    }
}
