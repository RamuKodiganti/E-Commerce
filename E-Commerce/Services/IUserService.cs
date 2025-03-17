using e_comm.Models;

namespace e_comm.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(int id);
        int AddUser(User user);
        int UpdateUser(int id, User user);
        int DeleteUser(int id);
    }
}
