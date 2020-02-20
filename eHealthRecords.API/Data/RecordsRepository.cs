using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eHealthRecords.API.Models;
using eHealthRecords.API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace eHealthRecords.API.Data
{
    public class RecordsRepository : IRecordsRepository
    {
        private readonly DataContext _context;
        public RecordsRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users =  _context.Users.Include(p => p.Photos).OrderByDescending(u => u.LastActive).AsQueryable();

            users = users.Where(u => u.Id != userParams.UserId);

            users = users.Where(u => u.Gender == userParams.Gender);

            // if (userParams.DoctorWatch)
            // {
            //     var userDoctor = await GetUserWatchList(userParams.UserId, userParams.DoctorWatch);
            //     users = users.Where(u => userDoctor.Contains(u.Id));
            // }

            // if (userParams.PatientWatch)
            // {
            //     var userPatient = await GetUserWatchList(userParams.UserId, userParams.DoctorWatch);
            //     users = users.Where(u => userPatient.Contains(u.Id));
            // }

            if (userParams.MinAge !=5 || userParams.MaxAge !=99)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDob =DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created": 
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        // private async Task<IEnumerable<int>> GetUserWatchList(int id, bool watchers)
        // {
        //     var user = await _context.Users
        //     .Include(x => x.DoctorWatch)
        //     .Include(x => x.PatientWatch)
        //     .FirstOrDefaultAsync(u => u.Id == id);

        //     if (watchers)
        //     {
        //         return user.DoctorWatch.Where(u => u.PatientId == id).Select(i => i.DoctorId);
        //     }
        //     else
        //     {
        //         return user.PatientWatch.Where(u => u.DoctorId == id).Select(i => i.PatientId);
        //     }
        // }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
