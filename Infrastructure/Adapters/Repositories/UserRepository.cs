namespace Infrastructure.Repositories;

using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<user>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
