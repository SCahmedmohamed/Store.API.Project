using Doman.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Peresistences;
using Services;
using Shared.ErrorModels;
using Web.MiddelWares;

namespace Web.Extenstions
{
    public static class Extenstions
    {
        public static IServiceCollection AddAllService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices(configuration);


            services.Configure<ApiBehaviorOptions>(Config =>
            {
                Config.InvalidModelStateResponseFactory = (actionContext =>
                {

                    var response = new ValidationErrorResponse
                    {
                        StatusCode = 400,
                        Message = "Validation Failed",
                        Errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors.Select(error => new ValidationError
                        {
                            Field = x.Key.GetHashCode(),
                            Error = new List<string> { error.ErrorMessage }
                        }))
                    };

                    return new BadRequestObjectResult("");
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JwtOptions:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtOptions:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JwtOptions:SecurityKey"])),
                };
            });



            return services;
        }

        public static async Task<WebApplication> AddAllMiddelWaresAsync(this WebApplication app)
        {

            using var Scope = app.Services.CreateScope();
            var DbInitializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await DbInitializer.InitializeAsync();
            await DbInitializer.InitializeIdentityAsync();



            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            app.UseStaticFiles();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();


            return app;
        }
    }
}
