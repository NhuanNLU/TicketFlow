namespace TicketFlow.Service.UserCase.V1.Profile;

public class Response
{
    public class GetProfileResponse
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Avatar { get; set; }
        public required string Gender { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public required DateTimeOffset DateOfBirth { get; set; }
        public required string Bio { get; set; }
    }
}