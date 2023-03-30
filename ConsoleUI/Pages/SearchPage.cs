namespace Yumby.ConsoleUI;

public static class SearchPage
{
    public static void Start()
    {
        Run();
    }

    private static void Run()
    {
        string prompt = string.Empty;
        List<string> options = new List<string> { "search by recipe name", "search by ingredient", "go back" };    //add "search by category" functionality? -> tags for recipe category ie breakfast, italian, gluten free, etc.
        var searchPageMenu = new Menu(prompt, options);
        int selectionIndex = searchPageMenu.Run();
        switch (selectionIndex)
        {
            case 0:
                break;
            case 1:
                //MainMenu.Start();
                break;
        }
        //return;
    }
}
