using System.ComponentModel.DataAnnotations;

namespace Data_hub.Models
{
    public class FavoriteCoworker
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
