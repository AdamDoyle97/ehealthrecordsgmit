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

        public async Task<Watch> GetWatch(int userId, int recipientId)
        {
            return await _context.WatchList.FirstOrDefaultAsync(u =>
            u.DoctorId == userId && u.PatientId == recipientId);
        }

        // setting main photo of user
        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

// get photo URL
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
            var users = _context.Users.Include(p => p.Photos).OrderByDescending(u => u.LastActive).AsQueryable();

            users = users.Where(u => u.Id != userParams.UserId);

            users = users.Where(u => u.Gender == userParams.Gender);
            if (!string.IsNullOrEmpty(userParams.Name))
                users = users.Where(u => u.Username.ToLower().Contains(userParams.Name.ToLower()));

            //returns the array list of doctors placing patient on watch list
            if (userParams.DoctorWatch)
            {
                var userDoctor = await GetUserWatchList(userParams.UserId, userParams.DoctorWatch);
                users = users.Where(u => userDoctor.Contains(u.Id));
            }

            // returns list of patients on watch list
            if (userParams.PatientWatch)
            {
                var userPatient = await GetUserWatchList(userParams.UserId, userParams.DoctorWatch);
                users = users.Where(u => userPatient.Contains(u.Id));
            }

            if (userParams.MinAge != 5 || userParams.MaxAge != 99)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

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

        private async Task<IEnumerable<int>> GetUserWatchList(int id, bool watchers)
        {
            var user = await _context.Users
            .Include(x => x.DoctorWatch)
            .Include(x => x.PatientWatch)
            .FirstOrDefaultAsync(u => u.Id == id);

            if (watchers)
            {
                return user.DoctorWatch.Where(u => u.PatientId == id).Select(i => i.DoctorId); // returns the list of doctors on watch list
            }
            else
            {
                return user.PatientWatch.Where(u => u.DoctorId == id).Select(i => i.PatientId); // returns patients on watch list
            }
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .AsQueryable();

            switch (messageParams.MessageContainer)
            {
                case "Inbox":
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId
                        && u.RecipientDeleted == false);
                    break;
                case "Outbox":
                    messages = messages.Where(u => u.SenderId == messageParams.UserId
                        && u.SenderDeleted == false);
                    break;
                default:
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId
                        && u.RecipientDeleted == false && u.IsRead == false);
                    break;
            }

            messages = messages.OrderByDescending(d => d.MessageSent);
            return await PagedList<Message>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            var messages = await _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .Where(m => m.RecipientId == userId && m.RecipientDeleted == false && m.SenderId == recipientId
                        || m.RecipientId == recipientId && m.SenderId == userId && m.SenderDeleted == false)
                .OrderByDescending(m => m.MessageSent)
                .ToListAsync();

            return messages;
        }

        public async Task<IEnumerable<User>> GetUsersRequest()
        {
            var users = await _context.Users.Where(x => x.RoleId == 4).ToListAsync();
            return users;
        }
        public async Task<bool> UpdateRoleId(User model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
    }
}
