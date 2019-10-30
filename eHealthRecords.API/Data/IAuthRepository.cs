using System.Threading.Tasks;
using eHealthRecords.API.Models;

namespace eHealthRecords.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password); //user register

         Task<User> Login(string username, string password); //user login

         Task<bool> UserExists(string username); //checking if user exists

         
    }
}