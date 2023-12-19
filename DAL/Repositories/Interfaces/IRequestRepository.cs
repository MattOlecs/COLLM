using DAL.Entities;
using DAL.Records;

namespace DAL.Repositories.Interfaces;

public interface IRequestRepository
{
    Task<Sentence?> GetAsync(int id);
    Task AddRequestAsync(Request request);
    Task<Request[]> GetBySimilarityAsync(Func<Request[], double[]> similarityFunc, double similarity);
    Task<Sentence[]> GetWithHighestSimilarityAsync(Func<Request[], double[]> countSimilarity, int length);
    Task SaveChangesAsync();
}