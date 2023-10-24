using System.Linq.Expressions;
using DAL.Entities;

namespace DAL.Repositories.Interfaces;

public interface IRequestRepository
{
    Task<IEnumerable<Request>> GetAllRequestsAsync();
    Task AddRequest(Request request);
    Task<Request[]> GetBySimilarity(Func<Request, bool> query);
}