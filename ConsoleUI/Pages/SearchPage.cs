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
        List<string> options = new List<string> { "search recipe by name", "search by ingredient", "go back" };    //add "search by category" functionality? -> tags for recipe category ie breakfast, italian, gluten free, etc.
        var searchPageMenu = new Menu(prompt, options);
        int selectionIndex = searchPageMenu.Run();
        switch (selectionIndex)
        {
            case 0:
                var searchedRecipe = GetRecipeAction.GetRecipeByName();
                if (searchedRecipe != null)
                {
                    SelectedRecipePage.Start(searchedRecipe);
                }
                break;
            case 1:
                var recipesContainingSearchedIngredient = GetRecipeAction.GetRecipesByIngredient();
                if (recipesContainingSearchedIngredient != null)
                {
                    foreach (var recipe in recipesContainingSearchedIngredient)
                    {
                        Console.WriteLine(recipe.Name);
                    }
                    ConsoleUtils.WaitForKeyPress();
                }
                break;
            case 2:
                break;
        }
        //return;
    }
}
