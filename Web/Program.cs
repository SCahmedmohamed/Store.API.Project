
using Doman.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Peresistences;
using Peresistences.Data.Contexts;
using Services;
using Services.Abstractions;
using Services.Mapping.About_Products;
using Shared.ErrorModels;
using Web.Extenstions;
using Web.MiddelWares;

namespace Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token like this: Bearer {your token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

            builder.Services.AddAllService(builder.Configuration);

            var app = builder.Build();

            await app.AddAllMiddelWaresAsync();

            app.Run();
        }
    }
}
/*
 * 
    Customer => string id , list of BasketItems
    BasketItem => ProductId, ProductName, PictureURL , Quantity, Price

 */
