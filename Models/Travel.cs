using System;

namespace Tasman.Models
{
    public class Travel
    {
        public int Id { get; set; }  // Primary key

        public string Destination { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Price { get; set; }
        public int? DiscountPrice { get; set; }
        public DateTime? DiscountEndsAt { get; set; }

        public bool IsVisible { get; set; } = true;
        public bool IsRecommended { get; set; }
        public int DisplayOrder { get; set; }

        // Booking windows (days before start date)
        public int BookableDaysBeforeStart { get; set; } = 1;
        public int CancellableDaysBeforeStart { get; set; } = 1;

        public int AvailableRooms { get; set; }

        public string PackageType { get; set; } = string.Empty;  // family, honeymoon, etc.
        public string AgeLimitation { get; set; } = string.Empty; // "18+", "All ages"
        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;  // Single image

        public string? GalleryImage1Url { get; set; } // for the gallery of each one 
        public string? GalleryImage2Url { get; set; }
        public string? GalleryImage3Url { get; set; }
    }
}
