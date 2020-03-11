using eHealthRecords.API.Models;
using Microsoft.EntityFrameworkCore; //had to install from --> dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 3.0.0  

namespace eHealthRecords.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options){}

        //this will be the table name in sql database
        public DbSet<Value> Values { get; set; } 

        public DbSet<User> Users { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<WatchList> Watch { get; set; }

        //this will allow the user to not put patient on watch list more than once
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<WatchList>()
                .HasKey(k => new {k.DoctorId, k.PatientId});

            builder.Entity<WatchList>()
                .HasOne(u => u.PatientWatch)
                .WithMany(u => u.DoctorWatch)
                .HasForeignKey(u => u.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<WatchList>()
                .HasOne(u => u.DoctorWatch)
                .WithMany(u => u.PatientWatch)
                .HasForeignKey(u => u.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

        }


    }
}