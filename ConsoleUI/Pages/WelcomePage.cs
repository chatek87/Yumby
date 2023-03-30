namespace Yumby.ConsoleUI;

public static class WelcomePage
{
    public static void Start()
    {
        Run();
    }

    private static void Run()
    {
        string prompt = Banner.YumbyArt() + "welcome to yumby!";
        List<string> options = new List<string>{ "all recipes", "search", "add recipe", "edit recipe", "delete recipe", "about", "exit" };
        var welcomePageMenu = new Menu(prompt, options);
        int selectionIndex = welcomePageMenu.Run();

        switch (selectionIndex)
        {
            case 0:
                AllRecipesPage.Start();
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break; 
            case 5:
                DisplayAboutInfo();
                break;
            case 6:
                ExitPage.Start();
                break;


        }
    }

    private static void DisplayAboutInfo()
    {
        Console.Clear();
        Console.WriteLine("yumby is a personal recipe keeper");
        Console.WriteLine("use it to search, store, edit, and find recipes");
        Console.WriteLine("\n");
        Console.WriteLine("Press any key to return to the Main Menu");
        Console.ReadKey(true);
        Run();
    }
}