namespace TicketFlow.Repository.Abstractions.Entities;

public interface IEntityBase<TKey>
{
    TKey Id { get; set; }
}