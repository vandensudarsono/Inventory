using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public  class UserLogin
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Please enter username")]
        [Display(Name = "Please Enter Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [Display(Name = "Enter Password")]
        public string Password { get; set; }
        //public int isActive {get; set; }

    }
}