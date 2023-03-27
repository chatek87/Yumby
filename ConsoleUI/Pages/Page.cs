namespace Yumby.ConsoleUI;

public class Page
{
    private int _selectionIndex;
    private string[] _options;
    private string _prompt;

    public Page(string prompt, string[] options)
    {
        _prompt = prompt;
        _options = options;
        _selectionIndex = 0;
    }

    private void DisplayOptions()
    {
        WriteLine(_prompt);
        //WriteLine(" ");
        for (int i = 0; i < _options.Length; i++)
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
                    _selectionIndex = _options.Length - 1;
                }
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                _selectionIndex++;
                if (_selectionIndex == _options.Length)
                {
                    _selectionIndex = 0;
                }
            }
        }
        while (keyPressed != ConsoleKey.Enter);
        return _selectionIndex;
    }
}