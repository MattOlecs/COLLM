using DAL.Contexts;
using DAL.Entities;
using DAL.Records;
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

    public async Task<Sentence?> GetAsync(int id)
    {
        var request = await _dbContext
            .Requests
            .FirstOrDefaultAsync(r => r.ID == id);

        return request is null 
            ? null
            : new Sentence(request.ID, request.Answer, 0);
    }

    public async Task<IEnumerable<Request>> GetAllRequestsAsync()
    {
        return await _dbContext.Requests.ToListAsync();
    }

    public async Task AddRequestAsync(Request request)
    {
        await _dbContext.Requests.AddAsync(request);
    }

    public async Task<Request[]> GetBySimilarityAsync(Func<Request, bool> query)
    {
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
    
    public async Task<Sentence[]> GetWithHighestSimilarityAsync(Func<Request, double> countSimilarity, int length)
    {
        const int batchSize = 100;
        Sentence[] result = new Sentence[length];
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

            var temp = batch
                .Select(x => new Sentence(x.ID, x.Question, countSimilarity(x)))
                .OrderBy(x => x.Similarity)
                .TakeLast(length)
                .ToArray();

            for (int i = 0; i < temp.Count(); i++)
            {
                Sentence? oldSentence = result[i];
                Sentence? newSentence = temp[i];
                if (oldSentence == null || newSentence?.Similarity > oldSentence?.Similarity)
                {
                    result[i] = temp[i];
                }
            }

            result = result
                .Where(x => x != null)
                .OrderBy(x => x.Similarity)
                .ToArray();
            
            skip += batchSize;
        }

        return result.ToArray();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}