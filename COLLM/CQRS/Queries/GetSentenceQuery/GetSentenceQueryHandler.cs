using COLLM.CQRS.Interfaces;
using COLLM.DTO;
using DAL.Repositories.Interfaces;

namespace COLLM.CQRS.Queries.GetSentenceQuery;

public class GetSentenceQueryHandler : IQueryHandler<GetSentenceQuery, SentenceDTO>
{
    private readonly IRequestRepository _requestRepository;

    public GetSentenceQueryHandler(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }
    
    public async Task<SentenceDTO> Handle(GetSentenceQuery query)
    {
        var sentence = await _requestRepository
            .GetAsync(query.Id);

        if (sentence is null)
        {
            throw new Exception("Sentence not found");
        }

        return new SentenceDTO(sentence.Text);
    }
}