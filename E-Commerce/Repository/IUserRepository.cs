using e_comm.Models;
using E_Commerce.DTO;

namespace e_comm.Repository
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUser(int id);
        User GetUserByEmail(string email);  // Add this method
        int AddUser(User user);
        int UpdateUser(string email, UserDto userDto);
        int DeleteUser(int id);

    }
}
