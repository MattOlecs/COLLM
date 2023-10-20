using DAL.Contexts;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

internal class RequestRepository : IRequestRepository
{
    private readonly CollmContext _dbContext;

    public RequestRepository(CollmContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Request>> GetAllRequestsAsync()
    {
        return await _dbContext.Requests.ToListAsync();
    }
}