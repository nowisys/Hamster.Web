using Hamster.Messaging.Domain;
using Hamster.Messaging.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hamster.Messaging.Services
{
    public interface IMessageService
    {
        Message Add(int userId, string text);
        Message Add(MessageDTO messageDTO);
        Message Remove(int id);
        Message GetById(int id);
        List<Message> GetAll();
        List<Message> GetByUser(User user);
        List<Message> GetByPosted(DateTime start);
        List<Message> GetByPosted(DateTime start, DateTime end);
    }
}
