using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Test5Blazor.Data.Models;
using Test5Blazor.Interfaces;
using Test5Blazor.ViewModels;

namespace Test5Blazor.Services
{
    public class UserServices : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
       

        public UserServices(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return await _userManager.Users
                    .Select(u => new UserViewModel
                    {
                        Id = u.Id,
                        FName = u.FName,
                        LName = u.LName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber
                    }).ToListAsync();
            }
            else
            {
                return await _userManager.Users
                    .Where(u => EF.Functions.Like(u.Email, $"%{searchValue}%"))
                    .Select(u => new UserViewModel
                    {
                        Id = u.Id,
                        FName = u.FName,
                        LName = u.LName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber
                    }).ToListAsync();
            }
        
    }

        public async Task<UserViewModel> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId); // Assuming userId is an int, and FindByIdAsync expects a string
            if (user == null) return null;

            return new UserViewModel
            {
                Id = user.Id, 
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }

      
        

        public async Task<bool> UpdateUserAsync(UserViewModel userModel)
        {
            var user = await _userManager.FindByIdAsync(userModel.Id);
            if (user == null) throw new ArgumentException("User not found");

            user.FName = userModel.FName;
            user.LName = userModel.LName;
            user.Email = userModel.Email;
            user.PhoneNumber = userModel.PhoneNumber;

            await _userManager.UpdateAsync(user);
            return true;
        }
        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return true;

            }
            return false;
        }

        public async Task<bool> AddUser(UserViewModel userModel)
        {
           
            var existingUser = await _userManager.FindByEmailAsync(userModel.Email);
            if (existingUser != null)
            {
                // User already exists
                throw new ArgumentException("A user with the given email already exists.");
            }

            
            var newUser = new ApplicationUser
            {
                FName = userModel.FName,
                LName = userModel.LName,
                Email = userModel.Email,
                UserName = userModel.Email.Split('@')[0],
                PhoneNumber = userModel.PhoneNumber
            };
            var result = await _userManager.CreateAsync(newUser, "A_default_or_generated_password");
            if (!result.Succeeded)
            {
                throw new Exception("User could not be created. Errors: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return true;
        }

    }
}
