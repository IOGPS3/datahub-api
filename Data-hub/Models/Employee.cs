namespace Data_hub.Models;

using System.ComponentModel.DataAnnotations;

public class Employee
{
    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress]
    public string Email { get; set; }
    public List<FavoriteCoworker>? Favorites { get; set; }
    [Required(ErrorMessage = "Current location is required")]
    public string Location { get; set; }
    public string MeetingStatus { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    public List<string> NotifyWhenFreeList { get; set; }

    public Employee()
    {
        NotifyWhenFreeList = new List<string>();
    }
}