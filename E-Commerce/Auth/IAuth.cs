namespace e_comm.Auth
{
    public interface IAuth
    {
        string Authentication(string email, string password);
    }
}
