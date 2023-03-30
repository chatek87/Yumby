using System.Threading.Channels;

namespace Yumby.ConsoleUI;

public class AddInstructionsLineAction
{
    public string? AddInstructionsLine()
    {
        string? instructionsLine = Console.ReadLine();
        return instructionsLine;
    }
}