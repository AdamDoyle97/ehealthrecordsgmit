using System;
using System.Collections;
using System.Collections.Generic;
using eHealthRecords.API.Models.Roles;
using Microsoft.EntityFrameworkCore;

namespace eHealthRecords.API.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string Introduction { get; set; }

        public string LookingFor { get; set; }

        public string Conditions { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Photo> Photos { get; set; }

        public ICollection<Watch> PatientWatch { get; set; }
        public ICollection<Watch> DoctorWatch { get; set; }

        public ICollection<Message> MessagesSent { get; set; }

        public ICollection<Message> MessagesReceived { get; set; }

    }
}
