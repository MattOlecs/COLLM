using DAL.Entities;

namespace DAL.Repositories.Interfaces;

public interface IRequestRepository
{
    Task<IEnumerable<Request>> GetAllRequestsAsync();
}