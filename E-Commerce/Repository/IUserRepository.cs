using e_comm.Models;

namespace e_comm.Repository
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUser(int id);
        int AddUser(User user);
        int UpdateUser(int id, User user);
        int DeleteUser(int id);

    }
}
