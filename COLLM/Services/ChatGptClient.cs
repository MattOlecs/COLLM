using System.Text;
using System.Text.Json;
using COLLM.Exceptions;
using COLLM.Records;

namespace COLLM.Services;

public class ChatGptClient
{
    private readonly HttpClient _httpClient;

    public ChatGptClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> SendRequestAsync(string prompt)
    {
        var request = new OpenAIRequest
        {
            Model = "gpt-3.5-turbo",
            Prompt = prompt,
            Temperature = 0.8f,
            MaxTokens = 50
        };
        
        string requestJson = JsonSerializer.Serialize(request);
        HttpResponseMessage response = await _httpClient.PostAsync("/v1/chat/completions", new StringContent(requestJson, Encoding.UTF8, "application/json"));
        string responseJson = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = JsonSerializer.Deserialize<OpenAIErrorResponse>(responseJson);
            throw new ChatGptException(errorResponse?.Error.Message ?? "Unknown error");
        }

        Root deserializedRoot = JsonSerializer.Deserialize<Root>(responseJson) ?? throw new ChatGptException("Error deserializing gpt response");
        return deserializedRoot.Choices!.First().Text!;
    }
}