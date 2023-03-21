using Yumby.BusinessLogic;
using Yumby.ConsoleUI;
using Yumby.DataAccess;
using Yumby.DataModels;

Title = "Yummmmmmmmby!";
CursorVisible = false;

DbInitializer.CheckForExistingDb(Globals.dbPath);

//MainMenu.Start();

RecipeService recipeService = new(Globals.connectionString);

var testRecipeRepo = new RecipeRepository(Globals.connectionString);

var testRecipe = new Recipe
{
    Name = "Test fish",
    //ServingsYielded = 5
};

testRecipeRepo.Insert(testRecipe);


IEnumerable<Recipe> recipes = testRecipeRepo.SelectAll();
foreach (Recipe recipe in recipes)
{
    WriteLine($"name: {recipe.Name} id: {recipe.RecipeId} servings yielded: {recipe.ServingsYielded}");
}




ConsoleUtils.WaitForKeyPress();