using e_comm.Models;
using E_comm.Models;
using E_Commerce.DTO;
using Microsoft.EntityFrameworkCore;

namespace e_comm.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext db;

        public UserRepository(DataContext db)
        {
            this.db = db;
        }
        public int AddUser(User user)
        {
            db.Users.Add(user);
            return db.SaveChanges();

        }

        public int DeleteUser(int id)
        {
            User user = db.Users.Where(x => x.UserId == id).FirstOrDefault();
            db.Users.Remove(user);
            return db.SaveChanges();
            //User user= db.Users.Find(id);
        }

        public User GetUser(int id)
        {
            return db.Users.Where(x => x.UserId == id).FirstOrDefault();

        }

        public List<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public User GetUserByEmail(string email)
        {
            return db.Users.FirstOrDefault(x => x.Email == email);
        }

        public int UpdateUser(string email, UserDto userDto)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                user.Password = userDto.Password;
                return db.SaveChanges();
            }
            return 0;
        }

    }
}
