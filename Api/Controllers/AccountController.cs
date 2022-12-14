using System.Security.Cryptography;
using System.Text;
using Api.Data;
using Api.DTOs;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class AccountController :BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context= context;
        }
        
        [HttpPost("register")] //Post://api/account/register?username=test&password=test
        public async Task<ActionResult<UserDto>>Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName)) return BadRequest("UserName is Taken");
            using var hmac = new HMACSHA512();
             var user  = new AppUser()
             {
                UserName=registerDto.UserName.ToString(),
                PasswordHash =hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt=hmac.Key
             };
             _context.Users.Add(user);
             await _context.SaveChangesAsync();
             return new UserDto
             {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
             };
             
        }

        //login Method
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>>Login(LoginDto loginDto)
        {
             var user =await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
             if(user == null)
             return Unauthorized("Invalid Username");
             using var hmac = new HMACSHA512(user.PasswordSalt);
             var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
             for(int i=0 ; i <  ComputeHash.Length;i++)
             {
                if(ComputeHash[i] != user.PasswordHash[i])
                return Unauthorized("Invalid Password");
             }
             return new UserDto
             {
                Username= user.UserName,
                Token= _tokenService.CreateToken(user),
             };


        }
        

        

        //checkUserMethod
        private async Task<bool>UserExists(string UserName)
        {
            return  await _context.Users.AnyAsync(x => x.UserName == UserName.ToLower());
        }
    }
}