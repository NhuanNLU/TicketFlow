namespace TicketFlow.Service.UserCase.V1.Profile;

public class Request
{
    public class UpdateProfileRequest
    {
        public string? Username { get; set; }
        public string? Avatar { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public string? Bio { get; set; }
    }
}