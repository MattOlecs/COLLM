using COLLM.CQRS.Interfaces;
using COLLM.DTO;

namespace COLLM.CQRS.Query.GetSentencesSimilarityQuery;

internal record GetSentencesSimilarityQuery(SentencesSimilarityDTO SentencesSimilarityDto) : IQuery<double>;