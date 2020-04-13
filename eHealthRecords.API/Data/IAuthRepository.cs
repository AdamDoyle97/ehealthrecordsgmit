using System.Threading.Tasks;
using eHealthRecords.API.Models;

namespace eHealthRecords.API.Data
{
    // Interface authrpository 
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password); //user register returns user

         Task<User> Login(string username, string password); //user login returns user

         Task<bool> UserExists(string username); //checking if user exists returns boolean true or false

         
    }
}