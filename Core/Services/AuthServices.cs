using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Moduls.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services_Abstraction;
using Shared.MedulesDto.AuthModels;

namespace Services
{
    public class AuthServices(IOptions<JwtOptions> options,
                              UserManager<AppUser> userManager) : IAuthServices
    {
   

        public async Task<bool> FindByEmailAsync(string email)
        {
            var result =await userManager.FindByEmailAsync(email);
            if(result == null) return false;
            return true;
        }
        public async Task<RegisterResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var CheckForUser =await FindByEmailAsync(registerDto.Email);
            if (CheckForUser == true) throw new Exception($"There Are User With Email {registerDto.Email}");
            var User = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
            };
            var CreateUser =await userManager.CreateAsync(User,registerDto.Password);
            if (!CreateUser.Succeeded)
            {
                throw new Exception($"Create User With UserName {registerDto.UserName} Failed!!");
            }
           
            var adminEmails = new List<string> { "admin1@example.com", "admin2@example.com" };
            List<string> roles = new List<string>();
            if (adminEmails.Contains(registerDto.Email))
            {
                await userManager.AddToRoleAsync(User, "Admin");
                roles.Add("Admin");
            }
            else
            {
                await userManager.AddToRoleAsync(User, "User");
                roles.Add("User");
            }
            var result = new RegisterResultDto()
            {
                DispalyName = registerDto.DisplayName,
                Email = registerDto.Email,
                Tooken = GenerateJwtToken(User,roles),
            };
            return  result;
        }
        public async Task<LoginResultDto> LoginAsync(LoginDto loginDto)
        {
            var User = await userManager.FindByEmailAsync(loginDto.Email);
            if (User == null) throw new Exception($"Email Or Password Is Not Correct !!");
            var CheckForCreate =await userManager.CheckPasswordAsync(User, loginDto.Password);
             if(CheckForCreate == false) throw new Exception($"Email Or Password Is Not Correct !!");
             var roles =await userManager.GetRolesAsync(User);
            var result = new LoginResultDto()
            {
                Email = User.Email,
                DispalyName=User.DisplayName,
                Tooken= GenerateJwtToken(User,(List<string>)roles),
            };
            return result;
        }
        private  string GenerateJwtToken(AppUser user,List<string> roles)
        {
            var JwtParameters = options.Value;
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName), 
            };
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var SecurityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtParameters.SecurityKey));
            var token = new JwtSecurityToken
                (
                  issuer : JwtParameters.Issuer,
                  audience : JwtParameters.Audiences,
                  claims :claims,
                  expires:DateTime.UtcNow.AddDays(JwtParameters.DirationInDay),
                  signingCredentials :new SigningCredentials(SecurityKey,SecurityAlgorithms.HmacSha256Signature)
                ) { };
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
