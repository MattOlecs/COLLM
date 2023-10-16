namespace LMACO.Interfaces.Services;

public interface IGptRequestCostSimulator
{
    double GetPromptPrice(string prompt);
}