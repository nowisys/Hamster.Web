using Hamster.Messaging.Domain;
using Hamster.Messaging.Domain.DTO;
using Hamster.Messaging.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hamster.Messaging.Controllers
{
    [Route("api/messaging/[controller]")]
    public class UserController : Controller
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        // GET: api/messaging/user/
        [HttpGet]
        public List<User> All()
        {
            return _userService.GetAll();
        }

        // GET: api/messaging/user/{id}
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userService.GetById(id);
        }

        // POST: api/messaging/user/
        [HttpPost]
        public User Create([FromBody] UserDTO userDTO)
        {
            return _userService.Add(userDTO);
        }

        // DELETE: api/messaging/message/{id}
        [HttpDelete("{id}")]
        public User Delete(int id)
        {
            return _userService.Remove(id);
        }
    }
}
