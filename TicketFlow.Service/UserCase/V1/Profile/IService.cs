namespace TicketFlow.Service.UserCase.V1.Profile;

public interface IService
{
    public Task<Response.GetProfileResponse> GetProfile(CancellationToken cancellationToken);
    public Task UpdateProfile(Request.UpdateProfileRequest request, CancellationToken cancellationToken);
}