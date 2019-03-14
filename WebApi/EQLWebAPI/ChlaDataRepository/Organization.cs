using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    public class Organization
    {
        private int _id;
        private string _name;
        private string _address;
        private string _contactno;
        private string _contactemail;
        private string _contactperson;
        private DateTime _registeredon;
        private int _timezone_mins;
        private DateTime _expiry;
        private bool _isactive;


        public int id { get => _id; set => _id = value; }
        public string name { get => _name; set => _name = value; }
        public string address { get => _address; set => _address = value; }
        public string contactno { get => _contactno; set => _contactno = value; }
        public string contactemail { get => _contactemail; set => _contactemail = value; }
        public string contactperson { get => _contactperson; set => _contactperson = value; }
        public DateTime registeredon { get => _registeredon; set => _registeredon = value; }
        public int timezone_mins { get => _timezone_mins; set => _timezone_mins = value; }
        public DateTime expiry { get => _expiry; set => _expiry = value; }
        public bool isactive { get => _isactive; set => _isactive = value; }
    }
}
