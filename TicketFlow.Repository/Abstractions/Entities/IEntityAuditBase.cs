namespace TicketFlow.Repository.Abstractions.Entities;

public interface IEntityAuditBase<TKey>: IEntityBase<TKey>, IAuditBase
{
    
}