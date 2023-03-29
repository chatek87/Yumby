
using Yumby.BusinessLogic;
using Yumby.ConsoleUI;
using Yumby.DataAccess;
using Yumby.DataModels;

Title = "Yummmmmmmmby!";
//CursorVisible = false;

DbInitializer.CheckForExistingDb(Globals.dbPath);

WelcomePage.Start();



/*var recipeRepo = new RecipeRepository(Globals.connectionString);


var recipes = await recipeRepo.GetAllRecipesAsync();
var sortedRecipes = recipes.OrderBy(r => r.Value.Name);

//foreach (var recipe in sortedRecipes)
//{
//    DisplayRecipeAction.DisplayRecipe(recipe.Value);
//}
List<string> options = new List<string>(); 
foreach (var recipe in sortedRecipes)
{
    options.Add(recipe.Value.Name);
}
Page page = new Page("Select a recipe:", options);

page.Run();
*/


ConsoleUtils.WaitForKeyPress();


