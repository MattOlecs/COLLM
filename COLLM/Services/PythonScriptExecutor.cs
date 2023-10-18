using COLLM.Interfaces.Services;
using Python.Runtime;

namespace COLLM.Services;

internal class PythonScriptExecutor : IPythonScriptExecutor
{
    public PythonScriptExecutor()
    {
        if (PythonEngine.IsInitialized)
        {
            return;
        }
        
        PythonEngine.Initialize();
        PythonEngine.BeginAllowThreads();
    }
    
    public double GetSentencesSimilarityUsingSpacy(string firstSentence, string secondSentence)
    {
        string result;
        using (Py.GIL())
        {
            dynamic spacy = Py.Import("spacy");
            dynamic nlp = spacy.load("en_core_web_sm");
            // dynamic nlp = spacy.load("en_core_web_trf");

            dynamic doc1 = nlp(firstSentence);
            dynamic doc2 = nlp(secondSentence);

            dynamic similarity = doc1.similarity(doc2);
            result = similarity.ToString();
        }

        return Convert.ToDouble(result);
    }
}