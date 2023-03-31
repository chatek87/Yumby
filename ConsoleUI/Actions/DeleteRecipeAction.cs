using Yumby.DataAccess;
using Yumby.DataModels;

namespace Yumby.ConsoleUI;

public static class DeleteRecipeAction
{
    public static void DeleteRecipe()
    {
        Console.WriteLine("Enter the name of the recipe to be deleted");
        string recipeToBeDeletedName = Console.ReadLine();

        var recipeRepo = new RecipeRepository(Globals.connectionString);
        recipeRepo.DeleteRecipe(recipeToBeDeletedName);
    }
}