using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChlaDataRepository
{
    public class User : IUser
    {
        private int _id;
        private string _firstname;
        private string _lastname;
        private string _username;
        private string _password;
        private string _email;
        private DateTime _expiry;
        private int _organizationid;
        private int _usertypeid;
        private bool _isactive;

        [Key]
        public int id { get => _id; set => _id = value; }
        public string firstname { get => _firstname; set => _firstname = value; }
        public string lastname { get => _lastname; set => _lastname = value; }
        public string username { get => _username; set => _username = value; }
        public string password { get => _password; set => _password = value; }
        public string email { get => _email; set => _email = value; }
        public DateTime expiry { get => _expiry; set => _expiry = value; }
        public int organizationid { get => _organizationid; set => _organizationid = value; }
        public int usertypeid { get => _usertypeid; set => _usertypeid = value; }
        public bool isactive { get => _isactive; set => _isactive = value; }
    }
}
