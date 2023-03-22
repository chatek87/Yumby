using System.Threading.Channels;

namespace Yumby.ConsoleUI.Actions;

public class AddInstructionsLineAction
{
    public string? AddInstructionsLine()
    {
        string? instructionsLine = Console.ReadLine();
        return instructionsLine;
    }
}