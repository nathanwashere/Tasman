using Tasman.Models;

namespace Tasman.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            SeedAdminUser(context);

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
                    Description = "Luxurious Moscow winter experience with guided tours, gourmet dining, and exclusive resort stays.",
                    ImageUrl = "https://images.unsplash.com/photo-1489515217757-5fd1be406fef?auto=format&fit=crop&w=1200&q=80",
                    GalleryImage1Url = "https://images.unsplash.com/photo-1469474968028-56623f02e42e?auto=format&fit=crop&w=1200&q=80",
                    GalleryImage2Url = "https://images.unsplash.com/photo-1452274381522-521513015433?auto=format&fit=crop&w=1200&q=80",
                    GalleryImage3Url = "https://images.unsplash.com/photo-1473181488821-2d23949a045a?auto=format&fit=crop&w=1200&q=80"
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
                    Description = "Seine cruise, gourmet wine tasting, and a luxury hotel stay in Paris.",
                    ImageUrl = "https://images.unsplash.com/photo-1502602898657-3e91760cbb34?auto=format&fit=crop&w=1200&q=80",
                    GalleryImage1Url = "https://images.unsplash.com/photo-1522098543979-ffc7f79d5c1d?auto=format&fit=crop&w=1200&q=80",
                    GalleryImage2Url = "https://images.unsplash.com/photo-1488747279002-c8523379faaa?auto=format&fit=crop&w=1200&q=80",
                    GalleryImage3Url = "https://images.unsplash.com/photo-1467269204594-9661b134dd2b?auto=format&fit=crop&w=1200&q=80"
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
                    Description = "Lights, food, tech, anime culture, temples, and more in a family-friendly Tokyo package.",
                    ImageUrl = "https://images.unsplash.com/photo-1505066836043-7a2ae83d1338?auto=format&fit=crop&w=1200&q=80",
                    GalleryImage1Url = "https://images.unsplash.com/photo-1473181488821-2d23949a045a?auto=format&fit=crop&w=800&q=80",
                    GalleryImage2Url = "https://images.unsplash.com/photo-1504788363733-507549153474?auto=format&fit=crop&w=800&q=80",
                    GalleryImage3Url = "https://images.unsplash.com/photo-1498747946579-bde604cb8f44?auto=format&fit=crop&w=800&q=80"
                },
                new Travel
                {
                    Destination = "Tel Aviv Beach Escape",
                    Country = "Israel",
                    StartDate = new DateTime(2025, 4, 4),
                    EndDate = new DateTime(2025, 4, 11),
                    Price = 3100,
                    AvailableRooms = 15,
                    PackageType = "luxury",
                    AgeLimitation = "All ages",
                    Description = "Sunset boardwalks, boutique hotels, and world-class dining on the Mediterranean.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Dead Sea Detox",
                    Country = "Israel",
                    StartDate = new DateTime(2025, 5, 2),
                    EndDate = new DateTime(2025, 5, 8),
                    Price = 2700,
                    AvailableRooms = 9,
                    PackageType = "wellness",
                    AgeLimitation = "All ages",
                    Description = "Float in mineral-rich waters, spa treatments, and desert sunsets.",
                    ImageUrl = "https://images.unsplash.com/photo-1500530855697-b586d89ba3ee?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Jerusalem Heritage Walks",
                    Country = "Israel",
                    StartDate = new DateTime(2025, 6, 1),
                    EndDate = new DateTime(2025, 6, 7),
                    Price = 2600,
                    AvailableRooms = 11,
                    PackageType = "heritage",
                    AgeLimitation = "All ages",
                    Description = "Old City tours, culinary markets, and sunset rooftops.",
                    ImageUrl = "https://images.unsplash.com/photo-1505764706515-aa95265c5abc?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Eilat Reef Adventure",
                    Country = "Israel",
                    StartDate = new DateTime(2025, 7, 12),
                    EndDate = new DateTime(2025, 7, 18),
                    Price = 2950,
                    AvailableRooms = 14,
                    PackageType = "adventure",
                    AgeLimitation = "All ages",
                    Description = "Snorkel the Red Sea, boat cruises, and coral reef exploration.",
                    ImageUrl = "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Cappadocia Sunrise Balloons",
                    Country = "Türkiye",
                    StartDate = new DateTime(2025, 8, 9),
                    EndDate = new DateTime(2025, 8, 15),
                    Price = 3300,
                    AvailableRooms = 10,
                    PackageType = "adventure",
                    AgeLimitation = "All ages",
                    Description = "Cave hotels, balloon rides, and fairy chimney sunsets.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Santorini Sunset Bliss",
                    Country = "Greece",
                    StartDate = new DateTime(2025, 9, 5),
                    EndDate = new DateTime(2025, 9, 12),
                    Price = 4200,
                    AvailableRooms = 10,
                    PackageType = "honeymoon",
                    AgeLimitation = "All ages",
                    Description = "Caldera views, whitewashed villas, and Aegean cuisine.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Rome Classics",
                    Country = "Italy",
                    StartDate = new DateTime(2025, 10, 3),
                    EndDate = new DateTime(2025, 10, 10),
                    Price = 3500,
                    AvailableRooms = 12,
                    PackageType = "heritage",
                    AgeLimitation = "All ages",
                    Description = "Colosseum, Vatican, and trattoria nights.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Swiss Alps Escape",
                    Country = "Switzerland",
                    StartDate = new DateTime(2025, 12, 1),
                    EndDate = new DateTime(2025, 12, 8),
                    Price = 4800,
                    AvailableRooms = 9,
                    PackageType = "luxury",
                    AgeLimitation = "All ages",
                    Description = "Grindelwald chalets, glacier views, and alpine spas.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Maldives Overwater Dream",
                    Country = "Maldives",
                    StartDate = new DateTime(2025, 11, 20),
                    EndDate = new DateTime(2025, 11, 27),
                    Price = 7200,
                    AvailableRooms = 6,
                    PackageType = "honeymoon",
                    AgeLimitation = "All ages",
                    Description = "Overwater villas, reef snorkeling, and private dinners.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "New York Lights",
                    Country = "USA",
                    StartDate = new DateTime(2025, 4, 18),
                    EndDate = new DateTime(2025, 4, 25),
                    Price = 3900,
                    AvailableRooms = 16,
                    PackageType = "city",
                    AgeLimitation = "All ages",
                    Description = "Broadway nights, rooftop bars, and skyline cruises.",
                    ImageUrl = "https://images.unsplash.com/photo-1469474968028-56623f02e42e?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Bali Wellness Retreat",
                    Country = "Indonesia",
                    StartDate = new DateTime(2025, 5, 20),
                    EndDate = new DateTime(2025, 5, 27),
                    Price = 3400,
                    AvailableRooms = 13,
                    PackageType = "wellness",
                    AgeLimitation = "All ages",
                    Description = "Ubud rice terraces, yoga, and tropical beaches.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Sydney Harbour Escape",
                    Country = "Australia",
                    StartDate = new DateTime(2025, 1, 28),
                    EndDate = new DateTime(2025, 2, 4),
                    Price = 4600,
                    AvailableRooms = 11,
                    PackageType = "family",
                    AgeLimitation = "All ages",
                    Description = "Opera House sails, Bondi mornings, and harbor cruises.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Cape Town Winelands",
                    Country = "South Africa",
                    StartDate = new DateTime(2025, 3, 2),
                    EndDate = new DateTime(2025, 3, 9),
                    Price = 3700,
                    AvailableRooms = 14,
                    PackageType = "luxury",
                    AgeLimitation = "All ages",
                    Description = "Table Mountain, vineyards, and coastal drives.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Iceland Aurora Hunt",
                    Country = "Iceland",
                    StartDate = new DateTime(2025, 2, 15),
                    EndDate = new DateTime(2025, 2, 21),
                    Price = 5000,
                    AvailableRooms = 8,
                    PackageType = "adventure",
                    AgeLimitation = "All ages",
                    Description = "Northern lights, blue lagoons, and black sand beaches.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Machu Picchu Trek",
                    Country = "Peru",
                    StartDate = new DateTime(2025, 9, 1),
                    EndDate = new DateTime(2025, 9, 10),
                    Price = 4300,
                    AvailableRooms = 10,
                    PackageType = "adventure",
                    AgeLimitation = "18+",
                    Description = "Sacred Valley acclimatization and guided Inca Trail.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Dubai Desert Luxe",
                    Country = "UAE",
                    StartDate = new DateTime(2025, 11, 5),
                    EndDate = new DateTime(2025, 11, 11),
                    Price = 4100,
                    AvailableRooms = 15,
                    PackageType = "luxury",
                    AgeLimitation = "All ages",
                    Description = "Skyline views, dune safaris, and designer stays.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Bangkok Street Feast",
                    Country = "Thailand",
                    StartDate = new DateTime(2025, 10, 12),
                    EndDate = new DateTime(2025, 10, 19),
                    Price = 2500,
                    AvailableRooms = 18,
                    PackageType = "city",
                    AgeLimitation = "All ages",
                    Description = "Night markets, temples, and river cruises.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Seychelles Serenity",
                    Country = "Seychelles",
                    StartDate = new DateTime(2025, 8, 1),
                    EndDate = new DateTime(2025, 8, 8),
                    Price = 6200,
                    AvailableRooms = 7,
                    PackageType = "honeymoon",
                    AgeLimitation = "All ages",
                    Description = "Granite boulders, turquoise bays, and barefoot luxury.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Barcelona Gaudí Trails",
                    Country = "Spain",
                    StartDate = new DateTime(2025, 5, 8),
                    EndDate = new DateTime(2025, 5, 15),
                    Price = 3200,
                    AvailableRooms = 16,
                    PackageType = "city",
                    AgeLimitation = "All ages",
                    Description = "Sagrada Família, tapas hopping, and beach evenings.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Maui Volcano Sunrise",
                    Country = "USA",
                    StartDate = new DateTime(2025, 6, 18),
                    EndDate = new DateTime(2025, 6, 25),
                    Price = 5300,
                    AvailableRooms = 10,
                    PackageType = "family",
                    AgeLimitation = "All ages",
                    Description = "Road to Hana, Haleakalā sunrise, and surf lessons.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Vancouver Rockies Rail",
                    Country = "Canada",
                    StartDate = new DateTime(2025, 7, 2),
                    EndDate = new DateTime(2025, 7, 9),
                    Price = 4400,
                    AvailableRooms = 12,
                    PackageType = "adventure",
                    AgeLimitation = "All ages",
                    Description = "Scenic train, mountain lakes, and coastal city time.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                },
                new Travel
                {
                    Destination = "Petra & Wadi Rum Skies",
                    Country = "Jordan",
                    StartDate = new DateTime(2025, 3, 22),
                    EndDate = new DateTime(2025, 3, 29),
                    Price = 3600,
                    AvailableRooms = 12,
                    PackageType = "adventure",
                    AgeLimitation = "All ages",
                    Description = "Pink city of Petra, desert camps, and stargazing in Wadi Rum.",
                    ImageUrl = "https://images.unsplash.com/photo-1505761671935-60b3a7427bad?auto=format&fit=crop&w=1200&q=80"
                }
            };

            // Seed a few active discounts and recommended slots
            var now = DateTime.UtcNow;
            foreach (var trip in trips.Take(5))
            {
                trip.DiscountPrice = (int)(trip.Price * 0.85);
                trip.DiscountEndsAt = now.AddDays(5);
                trip.IsRecommended = true;
            }
            int order = 1;
            foreach (var trip in trips)
            {
                trip.DisplayOrder = order++;
            }

            var fallbackGallery = new[]
            {
                "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?auto=format&fit=crop&w=1200&q=80",
                "https://images.unsplash.com/photo-1500530855697-b586d89ba3ee?auto=format&fit=crop&w=1200&q=80",
                "https://images.unsplash.com/photo-1493558103817-58b2924bce98?auto=format&fit=crop&w=1200&q=80"
            };

            foreach (var trip in trips)
            {
                trip.GalleryImage1Url ??= trip.ImageUrl ?? fallbackGallery[0];
                trip.GalleryImage2Url ??= fallbackGallery[1];
                trip.GalleryImage3Url ??= fallbackGallery[2];

                if (!context.TravelDestinations.Any(t => t.Destination == trip.Destination))
                {
                    context.TravelDestinations.Add(trip);
                }
            }

            context.SaveChanges();
        }

        private static void SeedAdminUser(ApplicationDbContext context)
        {
            if (context.Users.Any(u => u.Email == "admin@tasman.com"))
                return;

            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<Models.User>();
            var admin = new Models.User
            {
                Email = "admin@tasman.com",
                FirstName = "Tasman",
                LastName = "Admin",
                PhoneNumber = "000-000-0000",
                IsAdmin = true
            };

            admin.Password = hasher.HashPassword(admin, "Admin#123");
            context.Users.Add(admin);
            context.SaveChanges();
        }
    }
}
