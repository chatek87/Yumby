namespace Yumby.ConsoleUI;

//Single Responsibility Principle:
//This class's only responsibility is to handle the scrolling menu functionality
public class Menu
{
    private int _selectionIndex;
    private List<string> _options;
    private string _prompt;

    public Menu(string prompt, List<string> options)
    {
        _prompt = prompt;
        _options = options;
        _selectionIndex = 0;
    }

    private void DisplayOptions()
    {
        WriteLine(_prompt);
        //WriteLine(" ");
        for (int i = 0; i < _options.Count; i++)
        {
            string currentOption = _options[i];
            string prefix;
            string suffix;

            if (i == _selectionIndex)
            {
                prefix = ":) <<";
                suffix = ">> (:";
                ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                prefix = "     ";
                suffix = "     ";
                ForegroundColor = ConsoleColor.DarkMagenta;
            }
            WriteLine($"{prefix} {currentOption} {suffix}");
        }
        ForegroundColor = ConsoleColor.DarkMagenta;
    }

    public int Run()
    {
        ConsoleKey keyPressed;
        do
        {
            Clear();
            DisplayOptions();
            ConsoleKeyInfo keyInfo = ReadKey(true);
            keyPressed = keyInfo.Key;
            if (keyPressed == ConsoleKey.UpArrow)
            {
                _selectionIndex--;
                if (_selectionIndex == -1)
                {
                    _selectionIndex = _options.Count - 1;
                }
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                _selectionIndex++;
                if (_selectionIndex == _options.Count)
                {
                    _selectionIndex = 0;
                }
            }
        } while (keyPressed != ConsoleKey.Enter);

        return _selectionIndex;
    }
}