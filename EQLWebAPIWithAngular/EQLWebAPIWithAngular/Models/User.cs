using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EQLWebAPIWithAngular.Models
{
    public class User
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        //[DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Expiry { get; set; }
        [DisplayName("Active")]
        [Required]
        public bool IsActive { get; set; }
        [DisplayName("Organization")]
        public int OrganizationId { get; set; }
        [DisplayName("User Type")]
        public int UserTypeId { get; set; }
        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }
        [ForeignKey("UserTypeId")]
        public UserType UserType { get; set; }
    }
}
