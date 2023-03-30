using Yumby.DataModels;
using Yumby.BusinessLogic;

namespace Yumby.ConsoleUI;

public class SelectedRecipePage
{
    public static void Start(Recipe selectedRecipe)
    {
        Run(selectedRecipe);
    }

    private static void Run(Recipe selectedRecipe)
    {
        string prompt = selectedRecipe.Name;
        List<string> options = new List<string> { "view recipe", "change serving size", "view shopping list", "go back" };
        var selectedRecipePageMenu = new Menu(prompt, options);
        int selectionIndex = selectedRecipePageMenu.Run();
        switch (selectionIndex)
        {
            case 0:
                // view recipe
                DisplayRecipeAction.DisplayRecipe(selectedRecipe);
                break;
            case 1:
                // change serving size
                var convertedRecipe = ConversionUtility.ChangeServingSize(selectedRecipe);
                DisplayRecipeAction.DisplayRecipe(convertedRecipe); 
                break;
            case 2:
                // view shopping list
                break;
            case 3:
                // go back
                AllRecipesPage.Start();
                break;
        }
        Run(selectedRecipe);
    }
}
