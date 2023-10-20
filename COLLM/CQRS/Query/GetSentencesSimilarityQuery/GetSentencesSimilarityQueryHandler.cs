using COLLM.CQRS.Interfaces;
using COLLM.Interfaces.Services;

namespace COLLM.CQRS.Query.GetSentencesSimilarityQuery;

internal class GetSentencesSimilarityQueryHandler : IQueryHandler<GetSentencesSimilarityQuery, double>
{
    private readonly IPythonScriptExecutor _pythonScriptExecutor;

    public GetSentencesSimilarityQueryHandler(IPythonScriptExecutor pythonScriptExecutor)
    {
        _pythonScriptExecutor = pythonScriptExecutor;
    }
    
    public Task<double> Handle(Query.GetSentencesSimilarityQuery.GetSentencesSimilarityQuery query)
    {
        return Task.FromResult(_pythonScriptExecutor.GetSentencesSimilarityUsingSpacy(
            query.SentencesSimilarityDto.FirstSentence, query.SentencesSimilarityDto.SecondSentence));
    }
}