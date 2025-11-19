using Doman.Contracts;
using Doman.Entities.About_Identity;
using Doman.Entities.About_Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Peresistences.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Peresistences
{
    public class DbInitialzer(
        StoreDbContext _context , 
        IdentityDbContext _identityContext,
        UserManager<AppUser> _userManager,
        RoleManager<IdentityRole> _roleManager
        ) : IDbInitializer
    {
        // Auto Create Database and Apply All Pending Migrations
        public async Task InitializeAsync()
        {
            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                await _context.Database.MigrateAsync();
            }
            // Data Seeding :
            // 1. Check If There Is Any Data In The ProductTypes Table
            if (!_context.prodcuctTypes.Any()){
                //2. Read Brand Data From Json File
                var BrandData = await File.ReadAllTextAsync(@"..\Infrastructure\Peresistences\Data\DataSeeding\brands.json");
                //3. Convert The Json Data To List Of ProductBrand
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                if (Brands != null && Brands.Count > 0)
                {
                    await _context.ProductBrands.AddRangeAsync(Brands);
                }
            }

            if (!_context.prodcuctTypes.Any())
            {
                var TypeData = await File.ReadAllTextAsync(@"..\Infrastructure\Peresistences\Data\DataSeeding\types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                if(Types != null && Types.Count > 0)
                {
                    await _context.prodcuctTypes.AddRangeAsync(Types);
                }
            }
            if (!_context.Products.Any())
            {
                var ProductData = await File.ReadAllTextAsync(@"..\Infrastructure\Peresistences\Data\DataSeeding\products.json");
                var Productss = JsonSerializer.Deserialize<List<Product>>(ProductData);
                if (Productss != null && Productss.Count > 0)
                {
                    await _context.Products.AddRangeAsync(Productss);
                }
            }



            await _context.SaveChangesAsync();


        }

        public async Task InitializeIdentityAsync()
        {
            if (_identityContext.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                await _identityContext.Database.MigrateAsync();
            }

            // Data Seeding

            if (!_identityContext.Roles.Any())
            {
               
                await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            }
            if (!_identityContext.Users.Any())
            {
                var superAdmin = new AppUser()
                {
                    UserName = "SuperAdmin",
                    DisplayName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    PhoneNumber = "0123456789"
                };
                var admin = new AppUser()
                {
                    UserName = "Admin",
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "9876543210"
                };

                await _userManager.CreateAsync(superAdmin , "P@ssW0rd");
                await _userManager.CreateAsync(admin , "P@ssW0rd");

                await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                await _userManager.AddToRoleAsync(admin, "Admin");

            }
        }
    }
}