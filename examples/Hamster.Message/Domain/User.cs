using System;
using System.Collections.Generic;
using System.Text;

namespace Hamster.Messaging.Domain
{
    public class User
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private int _age;
        
        public User(int id)
        {
            _id = id;
        }

        public int Id
        {
            get { return _id; }
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

        public string Fullname
        {
            get { return _firstName + " " + _lastName; }
        }
        
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }
    }
}
