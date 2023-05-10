namespace Data_hub.Models;


using FluentValidation;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(employee => employee.Name).NotEmpty();
        RuleFor(employee => employee.Email).NotEmpty().EmailAddress();
        RuleFor(employee => employee.Location).NotEmpty();
        RuleFor(employee => employee.MeetingStatus).NotEmpty().Must(status => status.ToLowerInvariant() == "available" || status == "inMeeting");
    }
}

