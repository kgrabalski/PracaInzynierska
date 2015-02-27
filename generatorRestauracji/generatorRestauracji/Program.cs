using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using generatorRestauracji.Models;

namespace generatorRestauracji
{
    class Program
    {
        private static string[] images;
        private static Dictionary<string, int> imageIds;
        private static List<DishGroupModel> dishes;
        private static RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

        static void PrepareImages()
        {
            images = new string[]
            {
                "pizza.png",
                "sospom.jpg",
                "sosczosnek.jpg",
                "chinczyk1.jpg",
                "schabowy.jpg",
                "pierogi.jpg",
                "golabki.jpg",
                "mielony.jpg",
                "bigos.jpg",
                "golonka.jpg",
                "wloska.jpg",
                "chinska.png",
                "polska.jpg"
            };
            imageIds = new Dictionary<string, int>();

            using (var ctx = new FoodSearchEntities())
            {
                SHA256 sha = SHA256.Create();
                foreach (var image in images)
                {
                    var bytes = File.ReadAllBytes(Path.Combine("D:\\", image));
                    var hash = sha.ComputeHash(bytes);

                    var match = ctx.Hashes.ToList().FirstOrDefault(x => x.Hash1.SequenceEqual(hash));
                    if (match != null)
                    {
                        imageIds.Add(image, match.ImageId);
                    }
                    else
                    {
                        var mime = "image/" + Path.GetExtension(image).TrimStart('.');

                        var img = ctx.Images.Add(new Image() { ImageData = bytes, ContentType = mime });
                        ctx.SaveChanges();
                        imageIds.Add(image, img.ImageId);
                        ctx.Hashes.Add(new Hash() { ImageId = img.ImageId, Hash1 = hash });
                        ctx.SaveChanges();
                    }
                }
            }
        }

        static void PrepareDishes()
        {
            dishes = new List<DishGroupModel>()
            {
                new DishGroupModel()
                {
                    Name = "Pizza", Cuisine = Cuisines.Włoska, Dishes = new List<DishModel>()
                    {
                        new DishModel() {PriceFrom = 20, PriceTo = 35, Name = "Pizza Margherita", Image = 0},
                        new DishModel() {PriceFrom = 20, PriceTo = 35, Name = "Pizza Capriciossa", Image = 0},
                        new DishModel() {PriceFrom = 20, PriceTo = 35, Name = "Pizza Hawajska", Image = 0},
                        new DishModel() {PriceFrom = 20, PriceTo = 35, Name = "Pizza Diablo", Image = 0},
                        new DishModel() {PriceFrom = 20, PriceTo = 35, Name = "Pizza Primavera", Image = 0},
                        new DishModel() {PriceFrom = 20, PriceTo = 35, Name = "Pizza Pepperoni", Image = 0},
                        new DishModel() {PriceFrom = 20, PriceTo = 35, Name = "Pizza Kebab", Image = 0},
                        new DishModel() {PriceFrom = 20, PriceTo = 35, Name = "Pizza Rucola", Image = 0},
                        new DishModel() {PriceFrom = 20, PriceTo = 35, Name = "Pizza Americana bbq", Image = 0},
                        new DishModel() {PriceFrom = 20, PriceTo = 35, Name = "Pizza Frutti di mare", Image = 0},
                        new DishModel() {PriceFrom = 20, PriceTo = 35, Name = "Pizza Quattro fromagi", Image = 0},
                        new DishModel() {PriceFrom = 20, PriceTo = 35, Name = "Pizza Kompozycja własna", Image = 0}
                    }
                },
                new DishGroupModel()
                {
                    Name = "Sos do pizzy", Cuisine = Cuisines.Włoska, Dishes = new List<DishModel>()
                    {
                        new DishModel() {PriceFrom = 1, PriceTo = 3, Name = "Sos ładogdny", Image = 1},
                        new DishModel() {PriceFrom = 1, PriceTo = 3, Name = "Sos pikantny", Image = 1},
                        new DishModel() {PriceFrom = 1, PriceTo = 3, Name = "Sos czosnkowy", Image = 2}
                    }
                },
                new DishGroupModel()
                {
                    Name = "Kurczak", Cuisine = Cuisines.Chińska, Dishes = new List<DishModel>()
                    {
                        new DishModel() {PriceFrom = 20, PriceTo = 30, Name = "Kurczak w sosie słodko-kwaśnym", Image = 3},
                        new DishModel() {PriceFrom = 20, PriceTo = 30, Name = "Kurczak w na ostro", Image = 3},
                        new DishModel() {PriceFrom = 20, PriceTo = 30, Name = "Kurczak curry", Image = 3},
                        new DishModel() {PriceFrom = 20, PriceTo = 30, Name = "Kurczak z warzywami", Image = 3},
                        new DishModel() {PriceFrom = 20, PriceTo = 30, Name = "Kurczak w cieście", Image = 3},
                        new DishModel() {PriceFrom = 20, PriceTo = 30, Name = "Kurczak z orzechami nerkowca", Image = 3}
                    }
                },
                new DishGroupModel()
                {
                    Name = "Dania domowe", Cuisine = Cuisines.Polska, Dishes = new List<DishModel>()
                    {
                        new DishModel() {PriceFrom = 16, PriceTo = 30, Name = "Kotlet schabowy", Image = 4},
                        new DishModel() {PriceFrom = 16, PriceTo = 30, Name = "Pierogi ruskie", Image = 5},
                        new DishModel() {PriceFrom = 16, PriceTo = 30, Name = "Gołąbki", Image = 6},
                        new DishModel() {PriceFrom = 16, PriceTo = 30, Name = "Kotlet mielony", Image = 7},
                        new DishModel() {PriceFrom = 16, PriceTo = 30, Name = "Bigos", Image = 8},
                        new DishModel() {PriceFrom = 16, PriceTo = 30, Name = "Golonka", Image = 9},
                    }
                }
            };
        }

        private static int GetRandomNumber(int max)
        {
            var bytes = new byte[2];
            random.GetBytes(bytes);
            return Math.Abs(BitConverter.ToInt16(bytes, 0) % max);
        }

        static void GenerateUsers(int count)
        {
            var imiona = new string[]
            {
                "Jan", "Andrzej", "Piotr", "Krzysztof", "Stanisław", "Tomasz", "Paweł", "Józef", "Marcin", "Marek", 
                "Michał", "Grzegorz", "Jerzy", "Tadeusz", "Adam", "Łukasz", "Zbigniew", "Ryszard", "Dariusz", "Henryk", 
                "Mariusz", "Kazimierz", "Wojciech", "Robert", "Mateusz", "Marian", "Rafał", "Jacek", "Janusz", "Mirosław", 
                "Maciej", "Sławomir", "Jarosław", "Kamil", "Wiesław", "Roman", "Władysław", "Jakub", "Artur", "Zdzisław", 
                "Edward", "Mieczysław", "Damian", "Dawid", "Przemysław", "Sebastian", "Czesław", "Leszek", "Daniel", "Waldemar" 
            };
            var nazwiska = new string[]
            {
                "Nowak", "Kowalski", "Wiśniewski", "Wójcik", "Kowalczyk", "Kamiński", "Lewandowski", "Zieliński", "Woźniak", "Szymański",
                "Dąbrowski", "Kozłowski", "Jankowski", "Mazur", "Wojciechowski", "Kwiatkowski", "Krawczyk", "Kaczmarek", "Piotrowski", "Grabowski",
                "Zając", "Pawłowski", "Król", "Michalski", "Wróbel", "Wieczorek", "Jabłoński", "Nowakowski", "Majewski", "Stępień", 
                "Olszewski", "Jaworski", "Malinowski", "Dudek", "Adamczyk", "Pawlak", "Górski", "Nowicki", "Sikora", "Walczak",
                "Witkowski", "Baran", "Rutkowski", "Michalak", "Szewczyk", "Ostrowski", "Tomaszewski", "Pietrzak", "Zalewski", "Wróblewski",
            };
            var passwordHash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("user"));

            using (var ctx = new FoodSearchEntities())
            {
                var addresses = ctx.Addresses
                    .OrderBy(x => Guid.NewGuid())
                    .Take(count)
                    .ToList();

                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        string firstName = imiona[GetRandomNumber(50)];
                        string lastName = nazwiska[GetRandomNumber(50)];
                        Guid userId = Guid.NewGuid();

                        ctx.Users.Add(new User()
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Email = string.Format("{0}{1}{2}@fakedomain.pl", firstName.ToLower()[0], lastName.ToLower(), GetRandomNumber(count)),
                            PhoneNumber = "123456789",
                            CreateDate = DateTime.Now,
                            Password = passwordHash,
                            UserStateId = 1,
                            UserTypeId = 1,
                            UserId = userId
                        });
                        ctx.DeliveryAddresses.Add(new DeliveryAddress()
                        {
                            AddressId = addresses[i].AddressId,
                            FlatNumber = string.Empty,
                            UserId = userId
                        });
                        ctx.SaveChanges();
                        Console.WriteLine("Dodano usera: {0} {1}", firstName, lastName);
                    }
                    catch (Exception)
                    {
                    }
                    
                }
            }
        }

        static void GenerateRestaurants()
        {
            int restaurantNumber = 1;

            using (var ctx = new FoodSearchEntities())
            {              
                //restauracje
                var cuisines = new[] {Cuisines.Włoska, Cuisines.Chińska, Cuisines.Polska};
                var passwordHash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("admin"));
                var r = new Random();
                foreach (var district in ctx.Districts.ToList())
                {
                    int howMany = ctx.Addresses.Count(x => x.DistrictId == district.DistrictId)/200;
                    var addresses = ctx.Addresses
                        .Where(x => x.DistrictId == district.DistrictId)
                        .OrderBy(x => Guid.NewGuid())
                        .Take(howMany)
                        .ToList();

                    for (int i = 0; i < howMany; i++)
                    {
                        int tmp = r.Next(0, 3);
                        int cuisineId = (int) cuisines[tmp];
                        string logo = images[tmp + 10];
                        string restNumber = string.Format("{0:000}", restaurantNumber++);

                        Console.WriteLine("Restauracja {0} w {1}", restNumber, district.Name);

                        //dodanie restauracji (dodaje tez usera wlasciciel)
                        string restaurantName = "Restauracja " + restNumber;
                        int addressId = addresses[i].AddressId;
                        int logoId = imageIds[logo];
                        var rest = ctx.CreateRestaurant(restaurantName, addressId, logoId,
                            userFirstName: "Właściciel",
                            userLastName: "Restauracji",
                            userEmail: string.Format("restauracja{0}@foodsearch.pl", restNumber),
                            userPhone: "123456789",
                            userPassword: passwordHash);
                        Guid restId = rest.First().Value;
                        ctx.SaveChanges();

                        //godziny otwarcia
                        for (int j = 1; j <= 7; j++)
                        {
                            ctx.OpeningHours.Add(new OpeningHour()
                            {
                                RestaurantId = restId,
                                Day = (short) j,
                                TimeFrom = new TimeSpan(0, 8, 0, 0),
                                TimeTo = new TimeSpan(0, 22, 0, 0)
                            });
                        }
                        ctx.SaveChanges();
                        
                        //dania
                        var restDishes = dishes.Where(x => (int) x.Cuisine == cuisineId);
                        foreach (var dishGroup in restDishes)
                        {
                            var dg = ctx.DishGroups.Add(new DishGroup() {Name = dishGroup.Name, RestaurantId = restId});
                            ctx.SaveChanges();

                            foreach (var dishModel in dishGroup.Dishes)
                            {
                                ctx.Dishes.Add(new Dish()
                                {
                                    DishGroupId = dg.DishGroupId,
                                    RestaurantId = restId,
                                    DishName = dishModel.Name,
                                    Price = r.Next(dishModel.PriceFrom, dishModel.PriceTo),
                                    ImageId = imageIds[images[dishModel.Image]]
                                });
                            }
                            ctx.SaveChanges();
                        }

                        //kuchnie
                        ctx.RestaurantCuisines.Add(new RestaurantCuisine()
                        {
                            CuisineId = cuisineId,
                            RestaurantId = restId
                        });
                        ctx.SaveChanges();
                    }
                }
            }
        }

        static void UpdateRestaurants()
        {
            using (var ctx = new FoodSearchEntities())
            {
                foreach (var restaurant in ctx.Restaurants)
                {
                    restaurant.IsOpen = true;
                    restaurant.DeliveryPrice = GetRandomNumber(5) + 1;
                    restaurant.FreeDeliveryFrom = GetRandomNumber(16) + 10;
                }
                ctx.SaveChanges();
            }
        }

        static void GenerateOpinions()
        {
            var opinions = new string[]
            {
                "Zła", "Kiepska", "Średnia", "Dobra", "Świetna!"
            };
            var r = new Random();

            using (var ctx = new FoodSearchEntities())
            {
                var restaurants = ctx.Restaurants.OrderBy(x => x.Name).ToList();
                foreach (var restaurant in restaurants)
                {
                    int count = r.Next(2, 20);
                    var users = ctx.Users
                        .Where(x => x.UserTypeId == 1)
                        .OrderBy(x => Guid.NewGuid())
                        .Take(count)
                        .ToList();

                    
                    for (int i = 0; i < count; i++)
                    {
                        int rating = GetRandomNumber(5);
                        ctx.Opinions.Add(new Opinion()
                        {
                            UserId = users[i].UserId,
                            RestaurantId = restaurant.RestaurantId,
                            Comment = opinions[rating],
                            Rating = (short) (rating + 1),
                            CreateDate = DateTime.Now
                        });
                    }
                    ctx.SaveChanges();
                    Console.WriteLine("{0}: dodano {1} opinii", restaurant.Name, count);
                }
            }
        }

        static void Main(string[] args)
        {
            PrepareImages();
            PrepareDishes();
            GenerateRestaurants();
            UpdateRestaurants();
            GenerateUsers(500);
            GenerateOpinions();
        }
    }
}
