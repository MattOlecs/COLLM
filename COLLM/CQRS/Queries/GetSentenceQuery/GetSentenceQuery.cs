using COLLM.CQRS.Interfaces;
using COLLM.DTO;

namespace COLLM.CQRS.Queries.GetSentenceQuery;

public record GetSentenceQuery(int Id) : IQuery<SentenceDTO>;