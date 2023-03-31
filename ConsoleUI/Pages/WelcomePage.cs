namespace Yumby.ConsoleUI;

public static class WelcomePage
{
    public static void Start()
    {
        Run();
    }

    private static void Run()
    {
        while (true)
        {
            string prompt = Banner.YumbyBlockLetters() + "welcome to yumby!\n";
            List<string> options = new List<string> { "all recipes", "search", "add recipe", "edit recipe", "delete recipe", "about", "exit" };
            var welcomePageMenu = new Menu(prompt, options);
            int selectionIndex = welcomePageMenu.Run();

            switch (selectionIndex)
            {
                case 0:
                    // all recipes
                    AllRecipesPage.Start();
                    break;
                case 1:
                    // search
                    SearchPage.Start();
                    break;
                case 2:
                    //add recipe
                    CreateNewRecipeAction.CreateNewRecipe();
                    break;
                case 3:
                    // edit recipe
                    break;
                case 4:
                    // delete recipe
                    break;
                case 5:
                    // about
                    DisplayAboutInfo();
                    break;
                case 6:
                    // exit 
                    ExitPage.Start();
                    break;
            }
        }
    }

    private static void DisplayAboutInfo()
    {
        Console.Clear();
        Console.WriteLine("yumby is a personal recipe keeper");
        Console.WriteLine("use it to search, store, edit, and find recipes");
        Console.WriteLine("\n");
        while (true)
        {
            Console.WriteLine("Press any key to return to the Main Menu");
            Console.ReadKey(true);
            break;
        }
    }
}