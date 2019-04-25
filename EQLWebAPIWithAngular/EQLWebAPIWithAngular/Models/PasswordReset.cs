using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EQLWebAPIWithAngular.Models
{
    public class PasswordReset
    {
        [Key]
        public int Id { get; set; }
        public int Resetcode { get; set; }
        public int userid { get; set; }
        public bool status { get; set; }
    }
}
