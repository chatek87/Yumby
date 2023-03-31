yumby
=======

a personal recipe book application  
------------

#####  code KY Software Development 2 capstone project  </code>

yumby is a console application written in C# that allows a user to enter, store, and recall recipe information  
yumby also allows a user to easily adjust a recipe to suit the desired number of portions served  
  
data is persisted through the use of a local sqlite3 database  

### instructions for running:
If using Visual Studio, ensure that Yumby.ConsoleUI is selected as startup project.  
If using VS Code, cd to Yumby.ConsoleUI and execute dotnet run.  
You might need to execute dotnet restore if you are experiencing any errors loading packages.  
The database will automatically be generated and seeded with sample data within the ConsoleUI bin folder the first time you run the program.  

### app instructions:
use the up/down arrow keys to navigate menu and ENTER to select menu option  
follow the prompts  

### features list:
* Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program   
<code> see: AllRecipesPage.cs</code>

* Add comments to your code explaining how you are using at least 2 of the solid principles  
<code> (Single Responsibility) see: ConversionUtility.cs, Menu.cs, ShoppingListAction.cs </code>  
<code> (Interface Segregation) see: IRecipeRepository.cs, RecipeRepository.cs </code>

* Have 2 or more tables (entities) in your application that are related and have a function return data from both entities. In entity framework, this is equivalent to a join  
<code> see: RecipeRepository.cs - GetAllRecipes() </code>

* Query your database using a raw SQL query, not EF   
<code> see: RecipeRepository.cs - all methods </code>

* Make your application asynchronous  
<code> see: RecipeRepository.cs - GetAllRecipesAsync() </code>



