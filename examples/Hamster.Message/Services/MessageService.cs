using Hamster.Messaging.Domain;
using Hamster.Messaging.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hamster.Messaging.Services
{
    public class MessageService : IMessageService
    {
        IUserService _userService;
        Dictionary<int, Message> _messages = new Dictionary<int, Message>();

        public MessageService(IUserService userService)
        {
            _userService = userService;
        }

        public Message Add(int userId, string text)
        {
            User user = _userService.GetById(userId);
            Message message = buildMessage(user);
            message.Text = text;
            message.Posted = DateTime.Now;
            _messages.Add(message.Id, message);
            return message;
        }

        public Message Add(MessageDTO messageDTO)
        {
            User user = _userService.GetById(messageDTO.UserId);
            Message message = buildMessage(user);
            message.Text = messageDTO.Text;
            message.Posted = DateTime.Now;
            _messages.Add(message.Id, message);
            return message;
        }

        public List<Message> GetByPosted(DateTime start)
        {
            return _messages.Where(entry => entry.Value.Posted >= start).Select(entry => entry.Value).ToList<Message>();
        }

        public List<Message> GetByPosted(DateTime start, DateTime end)
        {
            return _messages.Where(entry => entry.Value.Posted >= start && entry.Value.Posted <= end).Select(entry => entry.Value).ToList<Message>();
        }

        public List<Message> GetByUser(User user)
        {
            return _messages.Where(entry => entry.Value.Author.Equals(user)).Select(entry => entry.Value).ToList<Message>();
        }

        public Message GetById(int id)
        {
            Message message;
            _messages.TryGetValue(id, out message);
            if(message == null)
            {
                throw new KeyNotFoundException("Id (" + id + ") not found.");
            }
            return message;
        }

        public Message Remove(int id)
        {
            Message message = GetById(id);
            _messages.Remove(id);
            return message;
        }

        public List<Message> GetAll()
        {
            return _messages.Values.ToList<Message>();
        }

        private Message buildMessage(User user)
        {
            if (_messages.Count == 0)
            {
                return new Message(0, user);
            }
            else
            {
                return new Message(_messages.OrderBy(entry => entry.Key).Last().Key + 1, user);
            }
        }
    }
}
