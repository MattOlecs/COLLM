using COLLM.CQRS.Interfaces;
using COLLM.DTO;

namespace COLLM.CQRS.Queries.GetChatGptResponseQuery;

public record GetChatGptResponseQuery(string Prompt) : IQuery<ReadAIAnswerDTO>;