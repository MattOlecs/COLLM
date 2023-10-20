namespace COLLM.Services;

public class ChatGptClient
{
    private readonly HttpClient _httpClient;

    public ChatGptClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendRequestAsync()
    {
        await _httpClient.PostAsync("completions", null);
    }
}