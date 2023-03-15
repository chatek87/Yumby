using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace Yumby2;

public interface IRecipeRepository
{
    IEnumerable<Recipe> GetAllRecipes();
    bool Insert(Recipe Recipe);
    bool Update(Recipe Recipe);
    bool Delete(Recipe Recipe);
}