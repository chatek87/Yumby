using System.Data.SQLite;
using Dapper;

namespace Yumby2;

public static class DbInitializer
{
    static void CreateAndSeedDb(string dbPath)
{
    SQLiteConnection.CreateFile(dbPath);
    string connectionString = $"Data Source={dbPath};Version=3;";
    SQLiteConnection connection = new SQLiteConnection(connectionString);

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
                        FOREIGN KEY(RecipeId) REFERENCES Recipes(RecipeId)
                    )");

    connection.Execute(@"CREATE TABLE Instructions (
                        InstructionId INTEGER PRIMARY KEY AUTOINCREMENT,
                        RecipeId INTEGER,
                        Step TEXT,
                        FOREIGN KEY(RecipeId) REFERENCES Recipes(RecipeId)
                    )");

    // Insert some sample recipes into the Recipes table
    connection.Execute(@"INSERT INTO Recipes (Name, ServingsYielded) VALUES
                        ('Chocolate Cake', 8),
                        ('Pancakes', 4),
                        ('Spaghetti Bolognese', 6)
                    ");

    // Insert some sample ingredients into the Ingredients table
    connection.Execute(@"INSERT INTO Ingredients (RecipeId, Name, Quantity) VALUES
                        (1, 'Flour', 2.5),
                        (1, 'Sugar', 2),
                        (1, 'Cocoa Powder', 1),
                        (2, 'Flour', 1.5),
                        (2, 'Baking Powder', 1),
                        (2, 'Salt', 0.5),
                        (2, 'Milk', 1),
                        (3, 'Ground Beef', 1),
                        (3, 'Onion', 1),
                        (3, 'Garlic', 2),
                        (3, 'Carrots', 2),
                        (3, 'Celery', 2),
                        (3, 'Canned Tomatoes', 2),
                        (3, 'Tomato Paste', 1),
                        (3, 'Red Wine', 1),
                        (3, 'Dried Oregano', 1),
                        (3, 'Dried Basil', 1)
                    ");

    connection.Execute(@"INSERT INTO Instructions (RecipeId, Step) VALUES
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