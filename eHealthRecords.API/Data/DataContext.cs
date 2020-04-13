using eHealthRecords.API.Models;
using eHealthRecords.API.Models.Roles;
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

        public DbSet<Watch> WatchList { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Role> Roles { get; set; }

        //this will allow the user to not put patient on watch list more than once
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Watch>()
                .HasKey(k => new {k.DoctorId, k.PatientId});

            builder.Entity<Watch>()
                .HasOne(u => u.PatientWatch)
                .WithMany(u => u.DoctorWatch)
                .HasForeignKey(u => u.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Watch>()
                .HasOne(u => u.DoctorWatch)
                .WithMany(u => u.PatientWatch)
                .HasForeignKey(u => u.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}