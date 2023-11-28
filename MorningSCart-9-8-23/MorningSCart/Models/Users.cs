using System.ComponentModel.DataAnnotations;

namespace MorningSCart.Models
{
    public class Users
    {
        [Key]
        public int UsersId { get; set; }
        [Display(Name ="User Name")]
        [Required]
        [MinLength(3,ErrorMessage ="Min 3 Char Req"),MaxLength(50)]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(3, ErrorMessage = "Min 3 Char Req"), MaxLength(50)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(3, ErrorMessage = "Min 3 Char Req"), MaxLength(50)]
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage ="Password Mismatch")]
        public string CPassword { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [MinLength(3, ErrorMessage = "Min 3 Char Req"), MaxLength(50)]
        public string Email { get; set; }        
        [Required]
        [MinLength(11, ErrorMessage = "Min 3 Char Req"), MaxLength(20)]
        public string PhoneNo { get; set; }        
        [Required]
        [Display(Name ="Home Address")]
        [MinLength(10, ErrorMessage = "Min 3 Char Req"), MaxLength(250)]
        public string HAddress { get; set; }
    }
}
