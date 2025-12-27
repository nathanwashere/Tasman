using System;
using System.ComponentModel.DataAnnotations;

namespace Tasman.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int TravelId { get; set; }

        public Travel? Travel { get; set; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;

        [Range(1, 50, ErrorMessage = "Rooms must be between 1 and 50")]
        public int Rooms { get; set; } = 1;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Booked"; // Booked, Cancelled

        public DateTime? CancelledAt { get; set; }

        public int TotalPrice { get; set; }
    }
}
