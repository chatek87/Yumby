using Yumby.BusinessLogic;
using Yumby.ConsoleUI;
using Yumby.DataAccess;
using Yumby.DataModels;

Title = "Yummmmmmmmby!";
//CursorVisible = false;

DbInitializer.CheckForExistingDb(Globals.dbPath);

var recipeRepo = new RecipeRepository(Globals.connectionString);

Recipe testRecipe = new();
testRecipe = recipeRepo.GetById(4); //only returns ingredients for 1-3. 4-5 were entered via console app method as it is now, therefore the ingredients were not persisted to db. 
foreach (Ingredient ingredient in testRecipe.Ingredients)
{
    Console.WriteLine(ingredient.Name);
}

/*RecipeService recipeService = new(Globals.connectionString);

IEnumerable<Recipe> recipes = recipeService.GetAllRecipes();
foreach (Recipe recipe in recipes)
{
    Console.WriteLine(recipe.Name);
    foreach (Ingredient ingredient in recipe.Ingredients)
    {
        Console.WriteLine(ingredient.Name);
    }
}*/


ConsoleUtils.WaitForKeyPress();