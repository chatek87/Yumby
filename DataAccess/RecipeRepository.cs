using System.Data;

namespace Yumby2;

public class RecipeRepository : IRecipeRepository
{
    private readonly IDbConnection _dbConnection;

    public RecipeRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;        
    }

    public IEnumerable<Recipe> GetAllRecipes()
    {
        throw new NotImplementedException();
    }

    public bool Delete(Recipe Recipe)
    {
        throw new NotImplementedException();
    }

    public bool Insert(Recipe Recipe)
    {
        throw new NotImplementedException();
    }

    public bool Update(Recipe Recipe)
    {
        throw new NotImplementedException();
    }
}