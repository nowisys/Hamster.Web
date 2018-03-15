using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Hamster.Messaging.Services;
using Hamster.Messaging.Domain;
using Hamster.Messaging.Domain.DTO;

namespace Hamster.Messaging.Controllers
{
    [Route("api/messaging/[controller]")]
    public class MessageController : Controller
    {
        private IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // GET: api/messaging/message/
        [HttpGet]
        public List<Message> All()
        {
            return _messageService.GetAll();
        }

        // GET: api/messaging/message/{id}
        [HttpGet("{id}")]
        public Message Get(int id)
        {
            return _messageService.GetById(id);
        }

        // POST: api/messaging/message/
        [HttpPost]
        public Message Create([FromBody] MessageDTO messageDTO)
        {
            return _messageService.Add(messageDTO);
        }

        // DELETE: api/messaging/message/{id}
        [HttpDelete("{id}")]
        public Message Delete(int id)
        {
            return _messageService.Remove(id);
        }
    }
}
