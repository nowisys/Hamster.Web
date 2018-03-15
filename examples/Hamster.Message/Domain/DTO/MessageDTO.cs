using System;
using System.Collections.Generic;
using System.Text;

namespace Hamster.Messaging.Domain.DTO
{
    public class MessageDTO
    {
        private int _userId;
        private string _text;

        public MessageDTO() { }

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
    }
}
