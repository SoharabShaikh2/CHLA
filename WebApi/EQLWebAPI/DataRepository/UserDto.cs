using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository
{
    public class UserDto
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public DateTime expiry { get; set; }
        public int organizationid { get; set; }
        public int usertypeid { get; set; }
        public bool isactive { get; set; }
    }

    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
