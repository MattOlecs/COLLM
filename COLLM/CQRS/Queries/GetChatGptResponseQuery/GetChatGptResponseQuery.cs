using COLLM.CQRS.Interfaces;

namespace COLLM.CQRS.Queries.GetChatGptResponseQuery;

public record GetChatGptResponseQuery(string Prompt) : IQuery<string>;