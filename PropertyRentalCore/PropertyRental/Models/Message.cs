using System;
using System.Collections.Generic;

namespace PropertyRental.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string? Subject { get; set; }
        public string? MessageBody { get; set; }
        public DateTime? Timestamp { get; set; }
        public int MessageStatusId { get; set; }

        public virtual MessageStatus MessageStatus { get; set; } = null!;
        public virtual User Receiver { get; set; } = null!;
        public virtual User Sender { get; set; } = null!;
    }
}
