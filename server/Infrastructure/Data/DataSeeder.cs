using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public static class DataSeeder
  {
    public static async Task SeedAsync(AppDbContext context)
    {
      // Ensure database is created
      await context.Database.EnsureCreatedAsync();

      // Seed Categories first
      // Ensure all required categories exist
      var requiredCategories = new[]
      {
        new { Name = "Electronics", Brand = "Tech" },
        new { Name = "Clothing", Brand = "Fashion" },
        new { Name = "Books", Brand = "Literature" },
        new { Name = "Sports", Brand = "Fitness" },
        new { Name = "Home", Brand = "Living" }
      };

      foreach (var reqCat in requiredCategories)
      {
        if (!await context.Categories.AnyAsync(c => c.Name == reqCat.Name))
        {
          await context.Categories.AddAsync(new Category
          {
            Name = reqCat.Name,
            Brand = reqCat.Brand
          });
        }
      }
      await context.SaveChangesAsync();

      // Seed Users
      if (!await context.Users.AnyAsync())
      {
        var adminUser = new User
        {
          Username = "admin@nashstore.com",
          Role = "Admin",
          PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!")
        };

        var customerUser = new User
        {
          Username = "customer@nashstore.com",
          Role = "Customer",
          PasswordHash = BCrypt.Net.BCrypt.HashPassword("Customer123!")
        };

        var customer2 = new User
        {
          Username = "jane.smith@example.com",
          Role = "Customer",
          PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!")
        };

        await context.Users.AddRangeAsync(adminUser, customerUser, customer2);
        await context.SaveChangesAsync();
      }

      // Seed Products
      if (!await context.Products.AnyAsync())
      {
        var electronics = await context.Categories.FirstAsync(c => c.Name == "Electronics");
        var clothing = await context.Categories.FirstAsync(c => c.Name == "Clothing");
        var books = await context.Categories.FirstAsync(c => c.Name == "Books");
        var sports = await context.Categories.FirstAsync(c => c.Name == "Sports");
        var home = await context.Categories.FirstAsync(c => c.Name == "Home");

        var products = new List<Product>
                {
                    // Electronics
                    new Product
                    {
                        Name = "iPhone 15 Pro",
                        Note = "Latest Apple smartphone with A17 Pro chip, titanium design, and advanced camera system. Features 6.1-inch Super Retina XDR display.",
                        Price = 999.99m,
                        Quantity = 50,
                        CategoryId = electronics.Id,
                        Image = "https://images.unsplash.com/photo-1592750475338-74b7b21085ab?w=500",
                        Date = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "Samsung Galaxy S24 Ultra",
                        Note = "Premium Android smartphone with S Pen, 200MP camera, and AI-powered features. 6.8-inch Dynamic AMOLED display.",
                        Price = 1199.99m,
                        Quantity = 35,
                        CategoryId = electronics.Id,
                        Image = "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=500",
                        Date = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "MacBook Air M3",
                        Note = "Ultra-thin laptop with Apple M3 chip, 13.6-inch Liquid Retina display, and up to 18 hours of battery life.",
                        Price = 1299.99m,
                        Quantity = 25,
                        CategoryId = electronics.Id,
                        Image = "https://images.unsplash.com/photo-1541807084-5c52b6b3adef?w=500",
                        Date = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "Sony WH-1000XM5 Headphones",
                        Note = "Industry-leading noise canceling wireless headphones with 30-hour battery life and premium sound quality.",
                        Price = 399.99m,
                        Quantity = 60,
                        CategoryId = electronics.Id,
                        Image = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500",
                        Date = DateTime.UtcNow
                    },

                    // Clothing
                    new Product
                    {
                        Name = "Nike Air Max 270",
                        Note = "Comfortable lifestyle shoes with Max Air unit in the heel and breathable mesh upper. Perfect for everyday wear.",
                        Price = 149.99m,
                        Quantity = 100,
                        CategoryId = clothing.Id,
                        Image = "https://images.unsplash.com/photo-1549298916-b41d501d3772?w=500",
                        Date = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "Levi's 501 Original Jeans",
                        Note = "Classic straight-fit jeans with button fly. Made from 100% cotton denim with authentic vintage styling.",
                        Price = 89.99m,
                        Quantity = 75,
                        CategoryId = clothing.Id,
                        Image = "https://images.unsplash.com/photo-1542272604-787c3835535d?w=500",
                        Date = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "Adidas Ultraboost 22",
                        Note = "High-performance running shoes with Boost midsole technology and Primeknit upper for maximum comfort.",
                        Price = 189.99m,
                        Quantity = 80,
                        CategoryId = clothing.Id,
                        Image = "https://images.unsplash.com/photo-1600185365483-26d7a4cc7519?w=500",
                        Date = DateTime.UtcNow
                    },

                    // Books
                    new Product
                    {
                        Name = "Clean Code by Robert Martin",
                        Note = "A handbook of agile software craftsmanship. Essential reading for any developer looking to write better code.",
                        Price = 44.99m,
                        Quantity = 120,
                        CategoryId = books.Id,
                        Image = "https://images.unsplash.com/photo-1532012197267-da84d127e765?w=500",
                        Date = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "The Pragmatic Programmer",
                        Note = "Your journey to mastery. Updated for the modern software development landscape with new insights and practices.",
                        Price = 49.99m,
                        Quantity = 95,
                        CategoryId = books.Id,
                        Image = "https://images.unsplash.com/photo-1481627834876-b7833e8f5570?w=500",
                        Date = DateTime.UtcNow
                    },

                    // Sports & Fitness
                    new Product
                    {
                        Name = "Yoga Mat Premium",
                        Note = "High-quality non-slip yoga mat with extra cushioning. Perfect for yoga, pilates, and general fitness exercises.",
                        Price = 59.99m,
                        Quantity = 150,
                        CategoryId = sports.Id,
                        Image = "https://images.unsplash.com/photo-1544367567-0f2fcb009e0b?w=500",
                        Date = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "Adjustable Dumbbells Set",
                        Note = "Space-saving adjustable dumbbells with weight range from 5 to 50 pounds. Perfect for home workouts.",
                        Price = 299.99m,
                        Quantity = 40,
                        CategoryId = sports.Id,
                        Image = "https://images.unsplash.com/photo-1571019613454-1cb2f99b2d8b?w=500",
                        Date = DateTime.UtcNow
                    },

                    // Home & Garden
                    new Product
                    {
                        Name = "Smart Home Security Camera",
                        Note = "Wireless security camera with 1080p HD video, night vision, and smartphone alerts. Easy installation.",
                        Price = 129.99m,
                        Quantity = 85,
                        CategoryId = home.Id,
                        Image = "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=500",
                        Date = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "LED Desk Lamp",
                        Note = "Modern LED desk lamp with adjustable brightness, USB charging port, and sleek minimalist design.",
                        Price = 79.99m,
                        Quantity = 110,
                        CategoryId = home.Id,
                        Image = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=500",
                        Date = DateTime.UtcNow
                    }
                };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
      }

      // Seed Sample Orders
      if (!await context.Orders.AnyAsync())
      {
        var customer = await context.Users.FirstAsync(u => u.Username == "customer@nashstore.com");
        var customer2 = await context.Users.FirstAsync(u => u.Username == "jane.smith@example.com");
        var products = await context.Products.Take(5).ToListAsync();

        var orders = new List<Order>
                {
                    new Order
                    {
                        OrderId = "ORD-001",
                        Email = customer.Username,
                        Phone = "123-456-7890",
                        Address = "123 Main St, City, State 12345",
                        Price = 1149.98m,
                        Note = "Rush delivery requested",
                        Date = DateTime.UtcNow.AddDays(-10)
                    },
                    new Order
                    {
                        OrderId = "ORD-002",
                        Email = customer2.Username,
                        Phone = "987-654-3210",
                        Address = "456 Oak Ave, City, State 67890",
                        Price = 439.98m,
                        Note = "Gift wrap requested",
                        Date = DateTime.UtcNow.AddDays(-5)
                    },
                    new Order
                    {
                        OrderId = "ORD-003",
                        Email = customer.Username,
                        Phone = "123-456-7890",
                        Address = "123 Main St, City, State 12345",
                        Price = 89.99m,
                        Note = "Standard shipping",
                        Date = DateTime.UtcNow.AddDays(-2)
                    }
                };

        await context.Orders.AddRangeAsync(orders);
        await context.SaveChangesAsync();

        // Get products and orders from database for order details
        var allProducts = await context.Products.ToListAsync();
        var allOrders = await context.Orders.ToListAsync();

        // Add Order Details (make sure we have at least 5 products)
        if (allProducts.Count >= 5 && allOrders.Count >= 3)
        {
          var orderDetails = new List<OrderDetail>
                  {
                      // First order details
                      new OrderDetail
                      {
                          OrderId = allOrders[0].Id,
                          ProductId = allProducts[0].Id, // First product
                          Quantity = 1,
                          Price = allProducts[0].Price
                      },
                      new OrderDetail
                      {
                          OrderId = allOrders[0].Id,
                          ProductId = allProducts[1].Id, // Second product
                          Quantity = 1,
                          Price = allProducts[1].Price
                      },

                      // Second order details
                      new OrderDetail
                      {
                          OrderId = allOrders[1].Id,
                          ProductId = allProducts[2].Id, // Third product
                          Quantity = 1,
                          Price = allProducts[2].Price
                      },
                      new OrderDetail
                      {
                          OrderId = allOrders[1].Id,
                          ProductId = allProducts[3].Id, // Fourth product
                          Quantity = 1,
                          Price = allProducts[3].Price
                      },

                      // Third order details
                      new OrderDetail
                      {
                          OrderId = allOrders[2].Id,
                          ProductId = allProducts[4].Id, // Fifth product
                          Quantity = 1,
                          Price = allProducts[4].Price
                      }
                  };

          await context.OrderDetails.AddRangeAsync(orderDetails);
          await context.SaveChangesAsync();
        }
      }
    }
  }
}
