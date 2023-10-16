using System;
using System.Collections.Generic;

namespace PropertyRental.Models
{
    public partial class MessageStatus
    {
        public MessageStatus()
        {
            Messages = new HashSet<Message>();
        }

        public int MessageStatusId { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
