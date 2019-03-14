using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository
{
    public class OrganizationDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string contactno { get; set; }
        public string contactemail { get; set; }
        public string contactperson { get; set; }
        public DateTime registeredon { get; set; }
        public int timezone_mins { get; set; }
        public DateTime expiry { get; set; }
        public bool isactive { get; set; }
    }
}
