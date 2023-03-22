using Yumby.DataModels;
using Yumby.DataAccess;

namespace Yumby.BusinessLogic;


public class RecipeService
{
    private readonly IRecipeRepository _recipeRepository;

    public RecipeService(string connectionString)
    {
        _recipeRepository = new RecipeRepository(connectionString);
    }

    //public RecipeService(IRecipeRepository recipeRepository)
    //{
    //    _recipeRepository = recipeRepository;        
    //}

    public IEnumerable<Recipe> GetAllRecipes()
    {
        return _recipeRepository.SelectAll();
    }

    public Recipe GetRecipeById(int id)
    {
        return _recipeRepository.SelectById(id);
    }

    public void AddRecipe(Recipe Recipe)
    {
        _recipeRepository.Insert(Recipe);
    }

    public void UpdateRecipe(Recipe Recipe)
    {
        _recipeRepository.Update(Recipe);
    }

    public void DeleteRecipe(int id)
    {
        _recipeRepository.Delete(id);
    }

}