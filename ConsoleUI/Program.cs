using Yumby.BusinessLogic;
using Yumby.ConsoleUI;
using Yumby.DataAccess;
using Yumby.DataModels;

Title = "Yummmmmmmmby!";
//CursorVisible = false;

DbInitializer.CheckForExistingDb(Globals.dbPath);













var recipeRepo = new RecipeRepository(Globals.connectionString);
var recipes = await recipeRepo.GetAllRecipesAsync();
var sortedRecipes = recipes.OrderBy(r => r.Value.Name);

foreach (var recipe in sortedRecipes)
{
    DisplayRecipeAction.DisplayRecipe(recipe.Value);
}


ConsoleUtils.WaitForKeyPress();