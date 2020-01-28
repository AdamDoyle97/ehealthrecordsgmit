using System;
using System.Threading.Tasks;
using eHealthRecords.API.Models;
using Microsoft.EntityFrameworkCore;

namespace eHealthRecords.API.Data
{ // this is the concrete authrepository 
// tells class were using IAuth
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Username == username); //will return matching username or null if no match
        
            if(username == null)
                return null;

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) // returns true or false depending if password macthes
                return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) //HMAC uses hash based message
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // if hashes match means password is correct
                for(int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] !=passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt); //using out to pass hash and salt as a reference and not a value

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync(); //this saves changes back to the datbase

            return user; 
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()) //HMAC uses hash based message
            {
                passwordSalt = hmac.Key; /// setting password salt to the randomly generated key
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // gets password as a byte array
            }
            
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(x => x.Username == username)) // compare this username against all other usernames
                return true;

            return false;
        }
    }
}