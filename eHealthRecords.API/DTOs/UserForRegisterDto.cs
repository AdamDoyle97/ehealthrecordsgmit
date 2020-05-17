<<<<<<< HEAD
using System;
=======
>>>>>>> eHealthRecords/Messages
using System.ComponentModel.DataAnnotations;

namespace eHealthRecords.API.DTOs
{
    //Data Transfer Object (DTO) is used for transfering the username and password to the view when registering
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
<<<<<<< HEAD
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Password must be between 4 - 8 characters")]
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
        public string KnownAs { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public UserForRegisterDto ()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
=======

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Password must be between 4 - 8 characters")]
        public string Password { get; set; }
>>>>>>> eHealthRecords/Messages

        // [Required]
        // public DateTime DateOfBirth { get; set; }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> eHealthRecords/Messages
