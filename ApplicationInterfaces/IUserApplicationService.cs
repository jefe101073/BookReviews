using BookReviews.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationInterfaces
{
    public interface IUserApplicationService
    {
        Task<UserDto> AuthenticateUserAsync(string email, string password);
    }
}
