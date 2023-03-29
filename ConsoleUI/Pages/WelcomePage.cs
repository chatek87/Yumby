using System.Security.Cryptography.X509Certificates;
using Yumby.DataAccess;

namespace Yumby.ConsoleUI;

public class WelcomePage
{
    public static void Start()
    {
        Run();
    }

    private static void Run()
    {
        string prompt = Banner.YumbyArt() + "welcome to yumby!";
        List<string> options = new List<string>{ "my recipes", "about", "exit" };
        var welcomePage = new Page(prompt, options);
        int selectionIndex = welcomePage.Run();

        switch (selectionIndex)
        {
            case 0:
                RecipeBookPage.Start();
                break;
            case 1:
                DisplayAboutInfo();
                break;
            case 2:
                ExitMenu.Start();
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