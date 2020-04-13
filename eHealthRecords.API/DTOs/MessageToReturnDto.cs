using System;

namespace eHealthRecords.API.DTOs
{
    public class MessageToReturnDto
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public string SenderKnownAs { get; set; }

        public string SenderPhotoUrl { get; set; }

        public int RecipientId { get; set; }

        public string RecipientKnownAs { get; set; }

        public string RecipientPhotoUrl { get; set; }

        public string Content { get; set; } // keeping the string of messages

        public bool IsRead { get; set; } // check if message is read

        public DateTime? DateRead { get; set; }

        public DateTime MessageSent { get; set; }
    }
}