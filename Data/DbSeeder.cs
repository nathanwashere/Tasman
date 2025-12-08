using Tasman.Models;

namespace Tasman.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.TravelDestinations.Any())
                return;

            var trips = new List<Travel>
            {
                new Travel
                {
                    Destination = "Moscow Winter Escape",
                    Country = "Russia",
                    StartDate = new DateTime(2025, 1, 14),
                    EndDate = new DateTime(2025, 1, 21),
                    Price = 2800,
                    AvailableRooms = 12,
                    PackageType = "adventure",
                    AgeLimitation = "All ages",
                    Description = "Enjoy a luxurious Moscow winter experience with guided tours, gourmet dining, and exclusive resort stays.",

                    ImageUrl = "/assets/travel/TravelD1.jpg",

                    GalleryImage1Url = "/assets/travel/Moscow1.jpg",
                    GalleryImage2Url = "/assets/travel/Moscow2.jpg",
                    GalleryImage3Url = "/assets/travel/Moscow3.jpg"
                },

                new Travel
                {
                    Destination = "Paris Romance Getaway",
                    Country = "France",
                    StartDate = new DateTime(2025, 2, 5),
                    EndDate = new DateTime(2025, 2, 12),
                    Price = 4500,
                    AvailableRooms = 8,
                    PackageType = "honeymoon",
                    AgeLimitation = "All ages",
                    Description = "Fall in love again with a stunning Parisian experience including Seine cruise, gourmet wine tasting and luxury hotel stay.",

                    ImageUrl = "/assets/travel/TravelD4.jpg",

                    GalleryImage1Url = "/assets/travel/TravelD2.jpg",
                    GalleryImage2Url = "/assets/travel/TravelD3.jpg",
                    GalleryImage3Url = "/assets/travel/TravelD4.jpg"
                },

                new Travel
                {
                    Destination = "Tokyo Neon Adventure",
                    Country = "Japan",
                    StartDate = new DateTime(2025, 3, 10),
                    EndDate = new DateTime(2025, 3, 20),
                    Price = 5200,
                    AvailableRooms = 10,
                    PackageType = "family",
                    AgeLimitation = "All ages",
                    Description = "Explore Tokyo's lights, food, tech, anime culture, temples, and more in this family-friendly package.",

                    ImageUrl = "/assets/travel/TravelD3.jpg",

                    GalleryImage1Url = "/assets/travel/TravelD1.jpg",
                    GalleryImage2Url = "/assets/travel/TravelD2.jpg",
                    GalleryImage3Url = "/assets/travel/TravelD3.jpg"
                }
            };

            context.TravelDestinations.AddRange(trips);
            context.SaveChanges();
        }
    }
}
