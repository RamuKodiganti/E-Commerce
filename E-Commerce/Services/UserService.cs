using e_comm.Models;
using e_comm.Repository;
using E_Commerce.DTO;
using E_Commerce.Repository;
using UserWebAPI.Exceptions;
using UserWebAPI.Exceptions.DemoAPI.Exception;

namespace e_comm.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repo;
        private readonly IShoppingCartRepository cartrepo;

        public UserService(IUserRepository repo)
        {
            this.repo = repo;
            this.cartrepo = cartrepo;
        }
        public int AddUser(User user)
        {
            if (repo.GetUser(user.UserId) != null)
            {
                throw new UserAlreadyExistsException($"USer with id {user.UserId} already exists");

            }


            user.ShoppingCart = new ShoppingCart
            {
                //UserId = userId,
                User = user
            };
            //cartrepo.AddCart(cart);
            //return repo.AddUser(user);
            int userId = repo.AddUser(user);
            return userId;
        }

        public int DeleteUser(int id)
        {
            if (repo.GetUser(id) == null)
            {
                throw new UserNotFoundException($"User with id {id} not found");
            }

            return repo.DeleteUser(id);
        }
        public User GetUser(int id)
        {
            User user = repo.GetUser(id);
            if (user == null)
            {
                throw new UserNotFoundException($"User with id {id} not found");
            }
            return user;
        }

        public List<User> GetUsers()
        {
            return repo.GetUsers();
        }

        public int UpdateUser(string email, UserDto userDto)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty", nameof(email));

            if (userDto == null)
                throw new ArgumentNullException(nameof(userDto));

            var existingUser = repo.GetUserByEmail(email);  // Use GetUserByEmail instead
            if (existingUser == null)
            {
                throw new UserNotFoundException($"User with email {email} not found");
            }
            return repo.UpdateUser(email, userDto);
        }
    }
}
