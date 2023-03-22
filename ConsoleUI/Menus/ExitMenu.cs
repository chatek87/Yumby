namespace Yumby.ConsoleUI;

public static class ExitMenu
{
    public static void Start()
    {
        Run();
    }

    private static void Run()
    {
        string prompt = Banner.ExitArt() + "Are you sure you want to exit?";
        string[] options = { "yes", "no, go back" };
        var menu = new Menu(prompt, options);
        int selectionIndex = menu.Run();
        switch (selectionIndex)
        {
            case 0:
                ConsoleUtils.QuitConsole();                
                break;
            case 1:
                MainMenu.Start();
                break;
        }
    }
}