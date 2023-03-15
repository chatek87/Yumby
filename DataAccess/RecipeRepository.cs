using Dapper;
using System.Data;

namespace Yumby2;

public class RecipeRepository : IRecipeRepository
{
    private readonly IDbConnection _dbConnection;

    public RecipeRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;        
    }

    public IEnumerable<Recipe> GetAll()
    {
        return _dbConnection.Query<Recipe>("SELECT * FROM Recipes");
    }

    public Recipe GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Insert(Recipe Recipe)
    {
        string sql = "INSERT INTO Recipes (Name, Price) VALUES (@Name, @Price)";
        _dbConnection.Execute(sql, Recipe);
    }

    public void Update(Recipe Recipe)
    {
        string sql = "UPDATE Recipe SET Name = @Name, Price = @Price WHERE Id = @Id";
        _dbConnection.Execute(sql, Recipe);
    }

    public void Delete(int id)
    {
        string sql = "DELETE FROM Recipes WHERE Id = @Id";
        _dbConnection.Execute(sql, new { RecipeId = id });
    }
}