using eHealthRecords.API.Models;
using Microsoft.EntityFrameworkCore; //had to install from --> dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 3.0.0  

namespace eHealthRecords.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options){}

        public DbSet<Value> Values { get; set; } //this will be the table name in sql database

        public DbSet<User> Users { get; set; }

        public DbSet<Photo> Photos { get; set; }

    }
}