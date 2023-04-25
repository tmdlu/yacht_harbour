using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace yacht_harbour.Models
{
    public class AccountFront
    {
        [Required]
        public int IdAccount { get; set; }

        [Required(ErrorMessage = "Email wymagany!")]
        [EmailAddress(ErrorMessage = "Zły format!")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }




        [Required(ErrorMessage = "Imię wymagane!")]
        [StringLength(15, ErrorMessage = "Za długie!")]
        [MinLength(3, ErrorMessage = "Za krótkie!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nazwisko wymagane!")]
        [StringLength(15, ErrorMessage = "Za długie!")]
        [MinLength(3, ErrorMessage = "Za krótkie!")]
        public string Surname { get; set; }

        [StringLength(11, ErrorMessage = "Za długie!")]
        [Phone(ErrorMessage = "Zły format!")]
        public string PhoneNumber { get; set; }

        
        public string ClubStatus { get; set; }

        public string Role { get; set; }



    }
}
