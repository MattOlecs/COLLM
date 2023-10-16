using Microsoft.AspNetCore.Mvc;
using Python.Runtime;

namespace LMACO.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
    
    [HttpPut(Name = "Similarity")]
    public string GetSimilarity()
    {
        if (!PythonEngine.IsInitialized)
        {
            PythonEngine.Initialize();
            PythonEngine.BeginAllowThreads();
        }
        string result;
        using (Py.GIL())
        {
            dynamic spacy = Py.Import("spacy");
            dynamic nlp = spacy.load("en_core_web_sm");
            string sentence1 = "This is a sample sentence.";
            string sentence2 = "Here is an example sentence.";

            dynamic doc1 = nlp(sentence1);
            dynamic doc2 = nlp(sentence2);

            dynamic similarity = doc1.similarity(doc2);
            result = similarity.ToString();
        }

        return result;
    }
}