using System;
using System.Collections.Generic;
using System.Text;

namespace Hamster.Messaging.Domain.DTO
{
    public class UserDTO
    {
        private int _age;
        private string _firstName;
        private string _lastName;

        public UserDTO()
        {

        }

        public string Firstname
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string Lastname
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }
    }
}
