using e_comm.Models;
using E_Commerce.DTO;

namespace e_comm.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(int id);
        int AddUser(User user);
        int UpdateUser(string email, UserDto userDto);
        int DeleteUser(int id);
    }
}
