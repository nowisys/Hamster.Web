using System;
using System.Collections.Generic;
using System.Text;
using Hamster.Messaging.Domain;
using System.Linq;
using Hamster.Messaging.Domain.DTO;

namespace Hamster.Messaging.Services
{
    public class UserService : IUserService
    {
        Dictionary<int, User> _users = new Dictionary<int, User>();

        public UserService() { }

        public User Add(UserDTO userDTO)
        {
            User user = buildUser();
            user.Age = userDTO.Age;
            user.Firstname = userDTO.Firstname;
            user.Lastname = userDTO.Lastname;
            _users.Add(user.Id, user);
            return user;
        }

        public User Add(string firstName, string lastName, int age)
        {
            User user = new User(_users.OrderBy(entry => entry.Key).Last().Key + 1);
            user.Firstname = firstName;
            user.Lastname = lastName;
            user.Age = age;
            _users.Add(user.Id, user);
            return user;
        }

        public User Remove(int id)
        {
            User user = GetById(id);
            _users.Remove(id);
            return user;
        }
        
        public List<User> GetAll()
        {
            return _users.Values.ToList<User>();
        }

        public User GetById(int id)
        {
            User user;
            _users.TryGetValue(id, out user);
            if(user == null)
            {
                throw new KeyNotFoundException("Id (" + id + ") not found.");
            }
            return user;
        }

        public List<User> GetByAge(int minAge = 0, int maxAge = 200)
        {
            return _users.Where(entry => entry.Value.Age >= minAge && entry.Value.Age <= maxAge).Select(entry => entry.Value).ToList();
        }

        private User buildUser()
        {
            if(_users.Count == 0)
            {
                return new User(0);
            }
            else
            {
                return new User(_users.OrderBy(entry => entry.Key).Last().Key + 1);
            }
        }
    }
}
