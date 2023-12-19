
namespace COLLM.Interfaces.Services;

public interface IPythonScriptExecutor
{
    double GetSentencesSimilarityUsingSpacy(string firstSentence, string secondSentence);
    double[] GetSentencesSimilarityUsingSpacy(string[] first, string[] second);
}