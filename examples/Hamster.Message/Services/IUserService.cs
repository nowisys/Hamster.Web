using Hamster.Messaging.Domain;
using Hamster.Messaging.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hamster.Messaging.Services
{
    public interface IUserService
    {
        User Add(string firstName, string lastName, int age);
        User Add(UserDTO userDTO);
        User Remove(int id);
        List<User> GetAll();
        User GetById(int id);
        List<User> GetByAge(int minAge = 0, int maxAge = 200);
    }
}
