using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChlaDataRepository
{
    public interface IUser
    {
        [Key]
        int id { get; set; }
        string firstname { get; set; }
        string lastname { get; set; }
        string username { get; set; }
        string password { get; set; }
        string email { get; set; }
        DateTime expiry { get; set; }
        int organizationid { get; set; }
        int usertypeid { get; set; }
        bool isactive { get; set; }
    }
}
