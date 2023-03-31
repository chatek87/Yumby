using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Yumby.DataModels;

namespace Yumby.DataAccess;

//Interface Segregation Principle:
//This class defines the interface which the RecipeRepository must implement
public interface IRecipeRepository
{
    IEnumerable<Recipe> SelectAll();
    Recipe SelectById(int id);
    void Insert(Recipe Recipe);
    void Update(Recipe Recipe);
    void Delete(int id);
}