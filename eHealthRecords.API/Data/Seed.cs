using eHealthRecords.API.Models;
using System.Collections.Generic;
using System.Linq;
using eHealthRecords.API.Models.Roles;
using Newtonsoft.Json;

namespace eHealthRecords.API.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext context)
        {
            if (!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();
                    context.Users.Add(user);
                }

                context.SaveChanges();
            }
        }
        public static void SeedRoles(DataContext context)
        {
            if (!context.Roles.Any())
            {
                var roleData = System.IO.File.ReadAllText("Data/UserRoles.json");
                var roles = JsonConvert.DeserializeObject<List<Role>>(roleData);
                foreach (var role in roles)
                {
                    context.Roles.Add(role);
                }

                context.SaveChanges();
            }
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()) //HMAC uses hash based message
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }
    }
}