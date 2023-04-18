namespace Data_hub.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string email { get; set; }
        public List<string>? favorites { get; set; }
        [Required(ErrorMessage = "Current location is required")]
        public string location { get; set; }
        public string meetingStatus { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }
     
    }
}
