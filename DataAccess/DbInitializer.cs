using System.Data.SQLite;
using Dapper;

namespace Yumby.DataAccess;

public static class DbInitializer
{
    public static void CheckForExistingDb(string dbPath)
    {
        if (!File.Exists(dbPath))
        {
            CreateAndSeedDb(dbPath);
            Console.WriteLine("path created");
        }
        else
        {
            Console.WriteLine("path found");
            return;
        }
    }
    public static void CreateAndSeedDb(string dbPath)
    {
        SQLiteConnection.CreateFile(dbPath);
        
        SQLiteConnection connection = new SQLiteConnection(Globals.connectionString);

        // Create tables
        connection.Execute(@"CREATE TABLE Recipes (
                            RecipeId INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT,
                            ServingsYielded DECIMAL
                        )");

        connection.Execute(@"CREATE TABLE Ingredients (
                            IngredientId INTEGER PRIMARY KEY AUTOINCREMENT,
                            RecipeId INTEGER,
                            Name TEXT,
                            Quantity DECIMAL,
                            UnitOfMeasurement TEXT,
                            FOREIGN KEY(RecipeId) REFERENCES Recipes(RecipeId)
                        )");

        connection.Execute(@"CREATE TABLE Instructions (
                            InstructionId INTEGER PRIMARY KEY AUTOINCREMENT,
                            RecipeId INTEGER,
                            StepNumber TEXT,
                            Description TEXT,
                            FOREIGN KEY(RecipeId) REFERENCES Recipes(RecipeId)
                        )");

        // Insert some sample recipes into the Recipes table
        connection.Execute(@"INSERT INTO Recipes (Name, ServingsYielded) VALUES
                            ('chocolate cake', 8),
                            ('pancakes', 4),
                            ('spaghetti bolognese', 6)
                        ");

        // Insert some sample ingredients into the Ingredients table
        connection.Execute(@"INSERT INTO Ingredients (RecipeId, Name, Quantity, UnitOfMeasurement) VALUES
                            (1, 'flour', 2.5, 'cups'),
                            (1, 'sugar', 2, 'cups'),
                            (1, 'cocoa powder', 1, 'cups'),
                            (2, 'flour', 1.5, 'cups'),
                            (2, 'baking powder', 1, 'cups'),
                            (2, 'salt', 0.5, 'tsp'),
                            (2, 'milk', 1, 'cup'),
                            (3, 'ground beef', 1, 'lbs'),
                            (3, 'onion', 1, ''),
                            (3, 'garlic', 2, ''),
                            (3, 'carrots', 2, ''),
                            (3, 'celery', 2, ''),
                            (3, 'canned tomatoes', 2, '12 oz cans'),
                            (3, 'tomato paste', 1, '8 oz can'),
                            (3, 'red wine', 1, 'cup'),
                            (3, 'dried oregano', 1, 'tbsp'),
                            (3, 'dried basil', 1, 'tbsp')
                        ");

        connection.Execute(@"INSERT INTO Instructions (RecipeId, Description) VALUES
                            (1, 'Preheat the oven to 350 degrees F.'),
                            (1, 'In a large bowl, whisk together the flour, sugar, cocoa powder, baking soda, baking powder, and salt.'),
                            (1, 'In a separate bowl, whisk together the eggs, buttermilk, vegetable oil, and vanilla extract.'),
                            (1, 'Pour the wet ingredients into the dry ingredients and whisk until just combined.'),
                            (1, 'Pour the batter into a greased 9-inch cake pan and bake for 35-40 minutes, or until a toothpick inserted into the center comes out clean.'),
                            (2, 'In a large bowl, whisk together the flour, baking powder, salt, and sugar.'),
                            (2, 'In a separate bowl, whisk together the milk, eggs, and melted butter.'),
                            (2, 'Pour the wet ingredients into the dry ingredients and whisk until just combined.'),
                            (2, 'Heat a non-stick skillet or griddle over medium heat and ladle 1/4 cup of batter per pancake.'),
                            (2, 'Cook the pancakes for 2-3 minutes per side, or until golden brown.'),
                            (3, 'In a large pot, heat the olive oil over medium heat.'),
                            (3, 'Add the ground beef and cook until browned, breaking up any large clumps with a wooden spoon.'),
                            (3, 'Add the onion, garlic, carrots, and celery and cook until the vegetables are tender.'),
                            (3, 'Add the canned tomatoes, tomato paste, red wine, oregano, basil, and salt and pepper to taste.'),
                            (3, 'Bring the sauce to a simmer and cook for 30 minutes.'),
                            (3, 'Serve the sauce over cooked spaghetti noodles.')
                        ");

        connection.Close();
    }
}