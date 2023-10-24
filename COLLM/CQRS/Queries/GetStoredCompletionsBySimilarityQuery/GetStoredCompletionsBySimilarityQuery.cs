using COLLM.CQRS.Interfaces;
using DAL.Entities;

namespace COLLM.CQRS.Queries.GetStoredCompletionsBySimilarityQuery;

public record GetStoredCompletionsBySimilarityQuery(string Prompt, double Similarity) : IQuery<Request[]>;