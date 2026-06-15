namespace Infrastructure.Repositories;

using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ticket>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.tickets
            .AsNoTracking()
            .Include(x => x.user)
            .ToListAsync(cancellationToken);
    }
}
