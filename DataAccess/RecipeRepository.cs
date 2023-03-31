using Dapper;
using System.Data;
using System.Data.SQLite;
using Yumby.DataModels;

namespace Yumby.DataAccess;

//Interface Segregation Principle:
//This class implements the IRecipeRepository interface, which provides a layer of abstraction that decouples the actions
//in the UI/business logic layer from the data access layer. This allows easy swapping of db implementation.

public class RecipeRepository : IRecipeRepository
{
    private readonly string _connectionString;

    public RecipeRepository(string connectionString)
    {
        _connectionString = connectionString;
    }


    public IEnumerable<Recipe> SelectAllRecipesPopulateDictionary() //TODO: Add instructions join.
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        const string query = @"
        SELECT r.RecipeId, r.Name, r.ServingsYielded,
               i.IngredientId, i.Name, i.Quantity, i.UnitOfMeasurement
        FROM Recipes r
        LEFT JOIN Ingredients i ON r.RecipeId = i.RecipeId";
        var recipes = new Dictionary<int, Recipe>();
        db.Query<Recipe, Ingredient, Recipe>(query, (recipe, ingredient) =>
        {
            if (!recipes.TryGetValue(recipe.RecipeId, out var recipeEntry))
            {
                recipeEntry = recipe;
                recipeEntry.Ingredients = new List<Ingredient>();
                recipes.Add(recipeEntry.RecipeId, recipeEntry);
            }
            recipeEntry.Ingredients.Add(ingredient);
            return recipeEntry;
        }, splitOn: "IngredientId");
        return recipes.Values;
    }


    public Dictionary<int, Recipe> GetAllRecipes()
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        // Query the database for all recipes and their ingredients
        string recipeQuery = "SELECT RecipeId, Name, ServingsYielded FROM Recipes";
        string ingredientsQuery = "SELECT RecipeId, Name, Quantity, UnitOfMeasurement FROM Ingredients";
        var recipes = db.Query<Recipe>(recipeQuery);
        var ingredients = db.Query<Ingredient>(ingredientsQuery);

        // Group the ingredients by recipe ID
        var ingredientsByRecipeId = ingredients.GroupBy(i => i.RecipeId)
                                               .ToDictionary(g => g.Key, g => g.ToList());

        // Query the database for all recipes' instructions
        string instructionsQuery = "SELECT RecipeId, Description FROM Instructions";
        var instructions = db.Query<(int RecipeId, string Description)>(instructionsQuery);

        // Group the instructions by recipe ID
        var instructionsByRecipeId = instructions.GroupBy(i => i.RecipeId)
                                                  .ToDictionary(g => g.Key, g => g.Select(i => i.Description).ToList());

        // Create a dictionary of recipes
        var recipeDictionary = new Dictionary<int, Recipe>();
        foreach (var recipe in recipes)
        {
            // Create a new recipe object
            var newRecipe = new Recipe
            {
                RecipeId = recipe.RecipeId,
                Name = recipe.Name,
                ServingsYielded = recipe.ServingsYielded,
                Ingredients = ingredientsByRecipeId.GetValueOrDefault(recipe.RecipeId, new List<Ingredient>()),
                Instructions = instructionsByRecipeId.GetValueOrDefault(recipe.RecipeId, new List<string>())
            };

            // Add the recipe to the dictionary
            recipeDictionary.Add(recipe.RecipeId, newRecipe);
        }

        // Return the dictionary of recipes
        return recipeDictionary;
    }


    public async Task<Dictionary<int, Recipe>> GetAllRecipesAsync()
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        // Query the database for all recipes and their ingredients
        string recipeQuery = "SELECT RecipeId, Name, ServingsYielded FROM Recipes";
        string ingredientsQuery = "SELECT RecipeId, Name, Quantity, UnitOfMeasurement FROM Ingredients";
        var recipes = await db.QueryAsync<Recipe>(recipeQuery);
        var ingredients = await db.QueryAsync<Ingredient>(ingredientsQuery);

        // Group the ingredients by recipe ID
        var ingredientsByRecipeId = ingredients.GroupBy(i => i.RecipeId)
                                               .ToDictionary(g => g.Key, g => g.ToList());

        // Query the database for all recipes' instructions
        string instructionsQuery = "SELECT RecipeId, Description FROM Instructions";
        var instructions = await db.QueryAsync<(int RecipeId, string Description)>(instructionsQuery);

        // Group the instructions by recipe ID
        var instructionsByRecipeId = instructions.GroupBy(i => i.RecipeId)
                                                  .ToDictionary(g => g.Key, g => g.Select(i => i.Description).ToList());

        // Create a dictionary of recipes
        var recipeDictionary = new Dictionary<int, Recipe>();
        foreach (var recipe in recipes)
        {
            // Create a new recipe object
            var newRecipe = new Recipe
            {
                RecipeId = recipe.RecipeId,
                Name = recipe.Name,
                ServingsYielded = recipe.ServingsYielded,
                Ingredients = ingredientsByRecipeId.GetValueOrDefault(recipe.RecipeId, new List<Ingredient>()),
                Instructions = instructionsByRecipeId.GetValueOrDefault(recipe.RecipeId, new List<string>())
            };

            // Add the recipe to the dictionary
            recipeDictionary.Add(recipe.RecipeId, newRecipe);
        }

        // Return the dictionary of recipes
        return recipeDictionary;
    }


    public IEnumerable<Recipe> SelectAll()
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        return db.Query<Recipe>("SELECT * FROM Recipes").ToList();
    }

   
    public Recipe GetRecipe(int recipeId)
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        // Query the database for the recipe and its ingredients
        string recipeQuery = "SELECT RecipeId, Name, ServingsYielded FROM Recipes WHERE RecipeId = @RecipeId";
        string ingredientsQuery = "SELECT RecipeId, Name, Quantity, UnitOfMeasurement FROM Ingredients WHERE RecipeId = @RecipeId";
        var recipe = db.QuerySingleOrDefault<Recipe>(recipeQuery, new { RecipeId = recipeId });
        var ingredients = db.Query<Ingredient>(ingredientsQuery, new { RecipeId = recipeId });

        // If no recipe was found, return null
        if (recipe == null)
        {
            return null;
        }

        // Add the ingredients to the recipe object
        recipe.Ingredients = ingredients.ToList();

        // Query the database for the recipe's instructions
        string instructionsQuery = "SELECT Text FROM Instructions WHERE RecipeId = @RecipeId";
        var instructions = db.Query<string>(instructionsQuery, new { RecipeId = recipeId });

        // Add the instructions to the recipe object
        recipe.Instructions = instructions.ToList();

        // Return the recipe object
        return recipe;
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\\

    public Recipe GetByName(string recipeName)
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        const string sql = @"
        SELECT RecipeId, Name, ServingsYielded
        FROM Recipes
        WHERE Name = @recipeName;

        SELECT IngredientId, Name, Quantity, UnitOfMeasurement
        FROM Ingredients
        WHERE RecipeId = (SELECT RecipeId FROM Recipes WHERE Name = @recipeName);

        SELECT StepNumber, Description
        FROM Instructions
        WHERE RecipeId = (SELECT RecipeId FROM Recipes WHERE Name = @recipeName);
    ";

        using var result = db.QueryMultiple(sql, new { recipeName });
        var recipe = result.Read<Recipe>().SingleOrDefault();

        if (recipe != null)
        {
            recipe.Ingredients = result.Read<Ingredient>().ToList();
            recipe.Instructions = result.Read<string>().ToList();
        }
        return recipe;
    }


    public Recipe GetById(int recipeId)
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        const string sql = @"
            SELECT RecipeId, Name, ServingsYielded
            FROM Recipes
            WHERE RecipeId = @recipeId;

            SELECT IngredientId, Name, Quantity, UnitOfMeasurement
            FROM Ingredients
            WHERE RecipeId = @recipeId;

            SELECT StepNumber, Description
            FROM Instructions
            WHERE RecipeId = @recipeId;
        ";

        using var result = db.QueryMultiple(sql, new { recipeId });
        var recipe = result.Read<Recipe>().SingleOrDefault();

        if (recipe != null)
        {
            recipe.Ingredients = result.Read<Ingredient>().ToList();
            recipe.Instructions = result.Read<string>().ToList();
        }
        return recipe;
    }

    public List<Recipe> GetByIngredient(string ingredientName)
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        const string sql = @"
        SELECT r.RecipeId, r.Name, r.ServingsYielded
        FROM Recipes r
        INNER JOIN Ingredients i ON r.RecipeId = i.RecipeId
        WHERE i.Name = @ingredientName;
    ";

        var recipes = db.Query<Recipe>(sql, new { ingredientName }).ToList();

        foreach (var recipe in recipes)
        {
            const string ingredientSql = @"
            SELECT IngredientId, Name, Quantity, UnitOfMeasurement
            FROM Ingredients
            WHERE RecipeId = @recipeId;
        ";

            recipe.Ingredients = db.Query<Ingredient>(ingredientSql, new { recipeId = recipe.RecipeId }).ToList();

            const string instructionSql = @"
            SELECT StepNumber, Description
            FROM Instructions
            WHERE RecipeId = @recipeId;
        ";

            recipe.Instructions = db.Query<string>(instructionSql, new { recipeId = recipe.RecipeId }).ToList();
        }

        return recipes;
    }

    public Recipe SelectById(int id)
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        return db.Query<Recipe>("SELECT * FROM Recipes WHERE RecipeId = @Id", new { Id = id }).FirstOrDefault();
    }


    public void Insert(Recipe recipe)
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        db.Execute("INSERT INTO Recipes (RecipeId, Name, ServingsYielded) VALUES (@RecipeId, @Name, @ServingsYielded)", recipe);
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

    private void InsertIngredients(Recipe recipe)
    {
        using IDbConnection db = new SQLiteConnection(_connectionString);
        foreach (var ingredient in recipe.Ingredients)
        {
            ingredient.RecipeId = recipe.RecipeId;
            db.Execute("INSERT INTO Ingredients (RecipeId, Name, Quantity, UnitOfMeasurement) VALUES (@RecipeId, @Name, @Quantity, @UnitOfMeasurement)", ingredient);
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

    public void DeleteRecipe(string recipeName)
    {
        using (SQLiteConnection connection = new SQLiteConnection(Globals.connectionString))
        {
            // Get the RecipeId for the specified recipe
            int recipeId = connection.QueryFirstOrDefault<int>("SELECT RecipeId FROM Recipes WHERE Name = @RecipeName", new { RecipeName = recipeName });

            if (recipeId == default(int))
            {
                Console.WriteLine($"Recipe '{recipeName}' not found in database.");
                return;
            }

            // Delete the recipe from the Recipes table
            connection.Execute("DELETE FROM Recipes WHERE RecipeId = @RecipeId", new { RecipeId = recipeId });

            // Delete the ingredients associated with the recipe from the Ingredients table
            connection.Execute("DELETE FROM Ingredients WHERE RecipeId = @RecipeId", new { RecipeId = recipeId });

            // Delete the instructions associated with the recipe from the Instructions table
            connection.Execute("DELETE FROM Instructions WHERE RecipeId = @RecipeId", new { RecipeId = recipeId });

            Console.WriteLine($"Recipe '{recipeName}' and its associated ingredients and instructions have been deleted from the database.");
        }
    }




}