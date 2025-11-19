using Doman.Entities.About_Identity;
using Doman.Exceptions.BadRequest;
using Doman.Exceptions.NotFound;
using Doman.Exceptions.Unauthorized;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions.About_Auth;
using Shared.About_Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.About_Auth
{ 
    public class AuthService(UserManager<AppUser> _userManager , IConfiguration _configuration) : IAuthService
    {
        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null)
            {
                throw new UserNotFoundException(request.Email);
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user,request.Password);
            if (!isPasswordValid)
            {
                throw new UnauthorizedException();
            }

            var res = new UserResponse()
            {
                Email = request.Email,
                DisplayName = user.DisplayName,
                Token = await GenerateTokenAsync(user)
            };

            return res;
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new AppUser()
            {
                DisplayName = request.DisplayName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new RegistrationBadRequest(result.Errors.Select(E=>E.Description).ToList());
            }
            var res = new UserResponse()
            {
                Email = request.Email,
                DisplayName = user.DisplayName,
                Token = await GenerateTokenAsync(user)
            };
            return res;
        }


        private async Task<string> GenerateTokenAsync(AppUser user)
        {
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:SecurityKey"]));

            var Token = new JwtSecurityToken(
                issuer: _configuration["JwtOptions:Issuer"],
                audience: _configuration["JwtOptions:Audience"],
                expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JwtOptions:SpanTime"])),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
