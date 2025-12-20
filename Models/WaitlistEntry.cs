using System;
using System.ComponentModel.DataAnnotations;

namespace Tasman.Models
{
    public class WaitlistEntry
    {
        public int Id { get; set; }

        [Required]
        public int TravelId { get; set; }

        public Travel? Travel { get; set; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;

        [Range(1, 50, ErrorMessage = "Requested rooms must be between 1 and 50")]
        public int RequestedRooms { get; set; } = 1;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool Notified { get; set; }
    }
}
