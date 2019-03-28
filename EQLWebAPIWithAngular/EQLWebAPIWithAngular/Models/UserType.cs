using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EQLWebAPIWithAngular.Models
{
    public class UserType
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [DisplayName("User Type")]
        public string Type { get; set; }
        public List<User> User { get; set; }
    }
}
