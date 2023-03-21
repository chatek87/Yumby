using Yumby.ConsoleUI;
using Yumby.DataAccess;
using Yumby.DataModels;

Title = "Yummmmmmmmby!";
CursorVisible = false;

DbInitializer.CheckForExistingDb(Globals.dbPath);

//MainMenu.Start();

var testRecipeRepo = new RecipeRepository(Globals.connectionString);

var testRecipe = new Recipe
{
    Name = "Test dish",
    ServingsYielded = 5
};

testRecipeRepo.Insert(testRecipe);


IEnumerable<Recipe> recipes = testRecipeRepo.GetAll();
foreach (Recipe recipe in recipes)
{
    WriteLine($"name: {recipe.Name} id: {recipe.RecipeId}");
}




ConsoleUtils.WaitForKeyPress();