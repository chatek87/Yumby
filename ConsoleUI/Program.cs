using Yumby.BusinessLogic;
using Yumby.ConsoleUI;
using Yumby.DataAccess;
using Yumby.DataModels;

Title = "Yummmmmmmmby!";
//CursorVisible = false;

DbInitializer.CheckForExistingDb(Globals.dbPath);

RecipeService recipeService = new(Globals.connectionString);
IEnumerable<Recipe> recipes = recipeService.GetAllRecipes();
foreach (Recipe recipe in recipes)
{
    Console.WriteLine(recipe.Name);
    foreach (Ingredient ingredient in recipe.Ingredients)
    {
        Console.WriteLine(ingredient.Name);
    }
}


ConsoleUtils.WaitForKeyPress();