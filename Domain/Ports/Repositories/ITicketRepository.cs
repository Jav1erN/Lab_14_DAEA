namespace Domain.Repositories;

using Domain.Entities;

public interface ITicketRepository
{
    Task<List<ticket>> GetAllAsync(CancellationToken cancellationToken);
}
