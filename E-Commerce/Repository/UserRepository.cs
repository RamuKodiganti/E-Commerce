using e_comm.Models;
using E_comm.Models;

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

        public int UpdateUser(int id, User user)
        {
            User user1 = db.Users.Where(x => x.UserId == id).FirstOrDefault();
            user1.UserName = user.UserName;
            user1.Email = user.Email;
            user1.Password = user.Password;
            user1.Role = user.Role;
            db.Entry(user1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges();
        }

    }
}
