using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using Dapper;
using Yumby.DataModels;

namespace Yumby.DataAccess;

// This class handles all direct communication with the database
public class RecipeRepository : IRecipeRepository
{
    private readonly string _connectionString;

    public RecipeRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<Recipe> GetAll()
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        return db.Query<Recipe>("SELECT * FROM Recipes").ToList();
    }

    public Recipe GetById(int id)
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        return db.Query<Recipe>("SELECT * FROM Recipes WHERE RecipeId = @Id", new { Id = id }).FirstOrDefault();
    }

    public void Insert(Recipe recipe)
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        db.Execute("INSERT INTO Recipes (Name, ServingsYielded) VALUES (@Name, @ServingsYielded)", recipe);
        int recipeId = db.Query<int>("SELECT last_insert_rowid()").Single();
        recipe.RecipeId = recipeId;
        if (recipe.Ingredients != null)
        {
            InsertIngredients(recipe);
        }
        if (recipe.Instructions != null)
        {
            InsertInstructions(recipe);
        }
    }

    public void Update(Recipe recipe)
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        db.Execute("UPDATE Recipes SET Name = @Name, ServingsYielded = @ServingsYielded WHERE RecipeId = @RecipeId", recipe);
        DeleteIngredients(recipe.RecipeId);
        InsertIngredients(recipe);
        DeleteInstructions(recipe.RecipeId);
        InsertInstructions(recipe);
    }

    public void Delete(int id)
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        db.Execute("DELETE FROM Recipes WHERE RecipeId = @Id", new { Id = id });
    }

    private void InsertIngredients(Recipe recipe)
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        foreach (var ingredient in recipe.Ingredients)
        {
            ingredient.RecipeId = recipe.RecipeId;
            db.Execute("INSERT INTO Ingredients (RecipeId, Name, Quantity, Unit) VALUES (@RecipeId, @Name, @Quantity, @Unit)", ingredient);
        }
    }

    private void InsertInstructions(Recipe recipe)
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        int stepNumber = 1;
        foreach (var instruction in recipe.Instructions)
        {
            db.Execute("INSERT INTO Instructions (RecipeId, StepNumber, Description) VALUES (@RecipeId, @StepNumber, @Description)", new { RecipeId = recipe.RecipeId, StepNumber = stepNumber, Description = instruction });
            stepNumber++;
        }
    }

    private void DeleteIngredients(int recipeId)
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        db.Execute("DELETE FROM Ingredients WHERE RecipeId = @RecipeId", new { RecipeId = recipeId });
    }

    private void DeleteInstructions(int recipeId)
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        db.Execute("DELETE FROM Instructions WHERE RecipeId = @RecipeId", new { RecipeId = recipeId });
    }
}