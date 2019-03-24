using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    public interface IOrganization
    {
        int id { get; set; }
        string name { get; set; }
        string address { get; set; }
        string contactno { get; set; }
        string contactemail { get; set; }
        string contactperson { get; set; }
        DateTime registeredon { get; set; }
        int timezone_mins { get; set; }
        DateTime expiry { get; set; }
        bool isactive { get; set; }
    }
}
