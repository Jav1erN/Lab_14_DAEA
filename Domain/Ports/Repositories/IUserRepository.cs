namespace Domain.Repositories;

using Domain.Entities;

public interface IUserRepository
{
    Task<List<user>> GetAllAsync(CancellationToken cancellationToken);
}
