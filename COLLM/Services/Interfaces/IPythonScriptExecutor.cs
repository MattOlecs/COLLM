using COLLM.Services;

namespace COLLM.Interfaces.Services;

public interface IPythonScriptExecutor
{
    double GetSentencesSimilarityUsingSpacy(string firstSentence, string secondSentence);
}