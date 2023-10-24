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

    public async Task AddRequest(Request request)
    {
        await _dbContext.Requests.AddAsync(request);
    }

    public async Task<Request[]> GetBySimilarity(Func<Request, bool> query)
    {
        // return await _dbContext
        //     .Requests
        //     .Where(query)
        //     .ToArrayAsync();
        //
        const int batchSize = 100;
        List<Request> result = new List<Request>();
        int skip = 0;

        while (true)
        {
            var batch = await _dbContext
                .Requests
                .Skip(skip)
                .Take(batchSize)
                .ToListAsync();

            if (batch.Count == 0)
            {
                // No more records to process
                break;
            }

            result.AddRange(batch.Where(query));
            skip += batchSize;
        }

        return result.ToArray();
    }
}