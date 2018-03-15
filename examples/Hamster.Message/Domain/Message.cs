using System;
using System.Collections.Generic;
using System.Text;

namespace Hamster.Messaging.Domain
{
    public class Message
    {
        private User _author;
        private int _id;
        private DateTime _posted;
        private string _text;
        
        public Message(int id, User author)
        {
            _id = id;
            _author = author;
        }

        public int Id
        {
            get { return _id; }
        }

        public User Author
        {
            get { return _author; }
            set { _author = value; }
        }
        
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        
        public DateTime Posted
        {
            get { return _posted; }
            set { _posted = value; }
        }
    }
}
