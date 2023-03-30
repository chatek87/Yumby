using Yumby.DataAccess;
using Yumby.DataModels;
using Yumby.BusinessLogic;

namespace Yumby.ConsoleUI;

public static class AllRecipesPage
{
    public static void Start()
    {
        Run();        
    }

    private static void Run()
    {
        while (true) 
        {
            var recipeRepo = new RecipeRepository(Globals.connectionString);
        
            var recipes = recipeRepo.GetAllRecipes();
            var sortedRecipes = recipes.OrderBy(r => r.Value.Name);

            var sortedRecipesList = sortedRecipes.ToList();
            List<string> options = new List<string>();
       
            foreach (var recipe in sortedRecipes)
            {
                options.Add(recipe.Value.Name);
            }
            options.Add("go back");
        
            Menu allRecipesPageMenu = new Menu("<<recipe book>>", options);
            int selectionIndex = allRecipesPageMenu.Run();

            //selection logic
            if (selectionIndex == options.Count - 1)
            {
                return;
                //WelcomePage.Start();
            }
            else
            {
                var selectedRecipe = sortedRecipesList[selectionIndex].Value;
                SelectedRecipePage.Start(selectedRecipe); 
            }
        }
    }
}
