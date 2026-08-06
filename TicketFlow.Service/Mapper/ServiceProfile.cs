using AutoMapper;
using TicketFlow.Repository.Entities;
using TicketFlow.Service.UserCase.V1.Profile;

namespace TicketFlow.Service.Mapper;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Request.UpdateProfileRequest, User>()
            // CẤU HÌNH QUAN TRỌNG: Chỉ map các trường khác null từ Request (nếu client không truyền thì không ghi đè null vào DB)
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<User, Response.GetProfileResponse>();
    }
}