using System.Threading.Tasks;
using System.Collections.Generic;
using eHealthRecords.API.Models;
using eHealthRecords.API.Helpers;

namespace eHealthRecords.API.Data
{
    public interface IRecordsRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<User> GetUser(int id);
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhotoForUser(int userId);
    }
}
