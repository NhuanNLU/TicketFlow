namespace TicketFlow.Service.UserCase.V1.Queries.Identity.Login;

public class LoginResponseQuery
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}