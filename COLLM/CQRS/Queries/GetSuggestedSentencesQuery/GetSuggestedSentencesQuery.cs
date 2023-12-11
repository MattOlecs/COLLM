using COLLM.CQRS.Interfaces;
using COLLM.DTO;

namespace COLLM.CQRS.Queries.GetSuggestedSentencesQuery;

public record GetSuggestedSentencesQuery(GetSuggestedSentencesDTO GetSuggestedSentencesDto) : IQuery<SentenceDTO[]>;