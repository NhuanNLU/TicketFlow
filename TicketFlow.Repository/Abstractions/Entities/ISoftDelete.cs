namespace TicketFlow.Repository.Abstractions.Entities;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}