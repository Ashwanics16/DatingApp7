using Api.DTOs;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")] //api/users
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
           var users= await _userRepository.GetUsersAsync();
           var userToReturn =_mapper.Map<IEnumerable<MemberDto>>(users);
           return Ok(userToReturn);
        }


        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>>Getuser(string username)
        {
          var user =await _userRepository.GetUserByUsernameAsync(username);
          return _mapper.Map<MemberDto>(user);

      
        }
    }
}