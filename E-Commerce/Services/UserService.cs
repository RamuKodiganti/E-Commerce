using e_comm.Models;
using e_comm.Repository;
using UserWebAPI.Exceptions;
using UserWebAPI.Exceptions.DemoAPI.Exception;

namespace e_comm.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repo;

        public UserService(IUserRepository repo)
        {
            this.repo = repo;
        }

        public int AddUser(User user)
        {
            if (repo.GetUser(user.UserId) != null)
            {
                throw new CustomerAlreadyExistsException($"Customer with id {user.UserId} already exists");

            }
            return repo.AddUser(user);
        }

        public int DeleteUser(int id)
        {
            if (repo.GetUser(id) == null)
            {
                throw new CustomerNotFoundException($"Customer with id {id} not found");
            }

            return repo.DeleteUser(id);
        }
        public User GetUser(int id)
        {
            User user = repo.GetUser(id);
            if (user == null)
            {
                throw new CustomerNotFoundException($"Customer with id {id} not found");
            }
            return user;
        }

        public List<User> GetUsers()
        {
            return repo.GetUsers();
        }

        public int UpdateUser(int id, User user)
        {
            if (repo.GetUser(id) == null)
            {
                throw new CustomerNotFoundException($"Customer with id {id} not found");
            }
            return repo.UpdateUser(id, user);
        }
    }
}
