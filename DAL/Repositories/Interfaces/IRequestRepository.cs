using DAL.Entities;
using DAL.Records;

namespace DAL.Repositories.Interfaces;

public interface IRequestRepository
{
    Task<IEnumerable<Request>> GetAllRequestsAsync();
    Task AddRequestAsync(Request request);
    Task<Request[]> GetBySimilarityAsync(Func<Request, bool> query);
    Task<Sentence[]> GetWithHighestSimilarityAsync(Func<Request, double> countSimilarity, int length);
    Task SaveChangesAsync();
}