using Test5Blazor.ViewModels;

namespace Test5Blazor.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync(string searchValue);
        Task<UserViewModel> GetUserByIdAsync(string userId);
        Task<bool> AddUser(UserViewModel user);
        Task<bool> UpdateUserAsync(UserViewModel user);
        Task<bool> DeleteUserAsync(string userId);
    }
}
