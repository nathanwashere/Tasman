using System.ComponentModel.DataAnnotations;

namespace Tasman.ViewModels
{
    public class BookingRequestViewModel
    {
        [Required]
        public int TravelId { get; set; }

        [Range(1, 50, ErrorMessage = "Rooms must be between 1 and 50")]
        public int Rooms { get; set; } = 1;

        public string? Notes { get; set; }
    }
}
