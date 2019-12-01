using System.ComponentModel.DataAnnotations;

namespace eHealthRecords.API.DTOs
{
    //Data Transfer Object (DTO) is used for transfering the username and password when registering
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Password must be between 4 - 8 characters")]
        public string Password { get; set; }

        // [Required]
        // public DateTime DateOfBirth { get; set; }
    }
}