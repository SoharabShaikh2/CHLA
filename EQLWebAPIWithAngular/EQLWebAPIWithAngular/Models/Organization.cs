using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EQLWebAPIWithAngular.Models
{
    public class Organization
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }
       
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }
        [DisplayName("Contact Email")]
        public string ContactEmail { get; set; }
        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }
        [DisplayName("Registered On")]
        [Required]
        //[DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RegisteredOn { get; set; }
        
        [Required]
        //[DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Expiry { get; set; }
        [DisplayName("Time Zone (Mins)")]
        [Required]
        public int Timezone_Mins { get; set; }
        [DisplayName("Active")]
        [Required]
        public bool IsActive { get; set; }
        [DisplayName("Unique URL Identifier")]
        [Required]
        public string RouteName { get; set; }
        public List<User> User { get; set; }
    }
}
