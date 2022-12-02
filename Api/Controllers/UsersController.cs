using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{   [Authorize]
    [ApiController]
    [Route("api/[controller]")] //api/users
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;

        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUser()
        {
            var user = await _context.Users.ToListAsync();

            return user;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> Getuser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            return user;
        }
    }
}