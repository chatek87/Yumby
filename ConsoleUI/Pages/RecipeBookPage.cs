using Yumby.DataAccess;
using Yumby.DataModels;
using Yumby.BusinessLogic;

namespace Yumby.ConsoleUI;

public static class RecipeBookPage
{
    public static void Start()
    {
        Run();        
    }

    private static void Run()
    {
        var recipeRepo = new RecipeRepository(Globals.connectionString);
        
        var recipes = recipeRepo.GetAllRecipes();
        var sortedRecipes = recipes.OrderBy(r => r.Value.Name);

        List<string> options = new List<string>();
        foreach (var recipe in sortedRecipes)
        {
            options.Add(recipe.Value.Name);
        }
        Page recipeBookPage = new Page("Select a recipe:", options);

        int selectionIndex = recipeBookPage.Run();

        WriteLine($"You selected selectionIndex: {selectionIndex}");
        // pass value of selectionIndex back to options
        WriteLine($"{options[selectionIndex]}");
        // now use the fetched recipe name to pass to recipesubmenu 
        // or better yet, include the unique recipeId (possibly as a tuple) in the List so that there won't be any confusion fetching recipes with the same name
        // or possibly better still, find a way to pass the dictionary itself to the menu builder so you can skip the middleman steps of List etc...
        // recipesubmenu will take recipe object as arg and facilitate viewing, editing, conversion, delete operations
    }
}
