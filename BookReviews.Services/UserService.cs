using BookReviews.Data;
using BookReviews.Interfaces;
using BookReviews.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core;

namespace BookReviews.Services
{
    public class UserService : IUserService
    {
        private readonly BookReviewDataContext _context;
        public UserService(BookReviewDataContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> AddUserAsync(AddUserDto user)
        {
            if (user == null || user.Password == null)
            {
                return null;
            }
            // Check for duplicate email address
            var emailCheck = await _context.Users.FirstOrDefaultAsync(z => z.Email == user.Email);
            if (emailCheck != null)
            {
                throw new ArgumentException($"Error.  Email address already exists in the system.{user.Email}", nameof(user.Email));
            }

            using (var context = _context)
            {
                var userObj = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = DataHelpers.PasswordEncrypt(user.Password),
                    IsDeleted = false
                };
                await _context.Users.AddAsync(userObj);
                await _context.SaveChangesAsync();
                return new UserDto
                {
                    Id = userObj.Id,
                    FirstName = userObj.FirstName,
                    LastName = userObj.LastName,
                    Email = userObj.Email,
                    Password = userObj.Password,
                    IsDeleted = false
                };
            }
        }

        public async Task<UserDto?> AuthenticateUserAsync(UserDto user)
        {
            if(string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return null;
            }
            var check = await _context.Users.FirstOrDefaultAsync(e => e.Email == user.Email);
            if (check == null || check.Password == null)
            {
                return null;
            }
            var passwordDecrypted = DataHelpers.PasswordDecrypt(check.Password);
            if (string.IsNullOrEmpty(passwordDecrypted))
            {
                return null;
            }
            if(user.Password.Equals(passwordDecrypted, StringComparison.Ordinal))
            {
                return new UserDto
                {
                    Email = check.Email,
                    Id = check.Id,
                    Password = check.Password,
                    FirstName = check.FirstName,
                    LastName = check.LastName,
                    DeletedByUserId = check.DeletedByUserId,
                    DeletedOn = check.DeletedOn,
                    IsDeleted = check.IsDeleted
                };
            }
            return null;
        }

        // Mark the deleted flag as true for the given user and return void
        public async Task DeleteUserAsync(int id, int currentUserId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            // Can't delete user if doesn't exist
            if (user == null)
            {
                throw new ObjectNotFoundException();
            }
            user.IsDeleted = true;
            user.DeletedByUserId = currentUserId;
            user.DeletedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        // Get Users who have not been deleted
        public async Task<IEnumerable<UserDto>> GetActiveUsersAsync()
        {
            var users = from user in _context.Users
                        where user.IsDeleted == false
                        select new UserDto
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Password = user.Password,
                            IsDeleted = user.IsDeleted,
                            DeletedByUserId = user.DeletedByUserId,
                            DeletedOn = user.DeletedOn
                        };
            return await users.ToListAsync();
        }

        public async Task<UserDto?> GetUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(z => z.Id == id);
            if (user == null)
            {
                return null;
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                IsDeleted = user.IsDeleted,
                DeletedByUserId = user.DeletedByUserId,
                DeletedOn = user.DeletedOn
            };
            return userDto;
        }

        public async Task<bool> IsUserDeletedAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(z => z.Id == userId);
            if (user == null)
            {
                throw new ArgumentException($"Error.  User does not exist in the system.{userId}", nameof(userId));
            }
            return user.IsDeleted;
        }

        public async Task UndeleteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new ObjectNotFoundException();
            }
            user.IsDeleted = false;
            user.DeletedOn = null;
            user.DeletedByUserId = null;
            await _context.SaveChangesAsync();
        }
    }
}