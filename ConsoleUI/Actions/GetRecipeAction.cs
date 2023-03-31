using Yumby.DataAccess;
using Yumby.DataModels;

namespace Yumby.ConsoleUI;

public static class GetRecipeAction
{
    public static Recipe GetRecipeByName()
    {
        var recipeRepo = new RecipeRepository(Globals.connectionString);

        Console.WriteLine("Enter the name of an existing recipe");

        string searchedRecipeName = Console.ReadLine();
        
        var searchedRecipe = recipeRepo.GetByName(searchedRecipeName.ToLower());

        Console.Clear();

        if (searchedRecipe == null)
        {
            Console.WriteLine($"Sorry, no recipe called {searchedRecipeName}");
            ConsoleUtils.WaitForKeyPress();
            return null;
        }
        else
        {        
            return searchedRecipe;
        }
    }

    public static List<Recipe> GetRecipesByIngredient() 
    {
        var recipeRepo = new RecipeRepository(Globals.connectionString);

        Console.WriteLine("Search for recipes containing which ingredient?");

        string searchedIngredientName = Console.ReadLine();

        var recipesContainingIngredient = recipeRepo.GetByIngredient(searchedIngredientName.ToLower());

        Console.Clear();

        if (recipesContainingIngredient.Count == 0)
        {
            Console.WriteLine($"Sorry, no recipes containing {searchedIngredientName}");
            ConsoleUtils.WaitForKeyPress();
            return null;
        }
        else
        {
            Console.WriteLine($"Recipes containing {searchedIngredientName}:");
            return recipesContainingIngredient;
        }
    }
}