namespace LMACO.Interfaces.Services;

public interface IPythonScriptExecutor
{
    double GetSentencesSimilarityUsingSpacy(string firstSentence, string secondSentence);
}