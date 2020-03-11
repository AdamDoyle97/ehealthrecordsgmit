namespace eHealthRecords.API.Models
{
    public class Watch
    {
        public int PatientId { get; set; } // likee
        public int DoctorId { get; set; } // liker
        public User PatientWatch { get; set; } // likee
        public User DoctorWatch { get; set; } //liker
    }
}