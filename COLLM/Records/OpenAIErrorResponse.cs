using System.Text.Json.Serialization;

namespace COLLM.Records;

public record OpenAIErrorResponse
{
    [JsonPropertyName("error")]
    public OpenAIError Error { get; set; }
};