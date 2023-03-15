namespace Yumby2;

public class RecipeService
{
    private readonly IRecipeRepository _recipeRepository;

    public RecipeService(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;        
    }

    public IEnumerable<Recipe> GetAllRecipes()
    {
        return _recipeRepository.GetAll();
    }

    public Recipe GetRecipeById(int id)
    {
        return _recipeRepository.GetById(id);
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