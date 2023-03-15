using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace Yumby2;

public interface IRecipeRepository
{
    IEnumerable<Recipe> GetAll();
    Recipe GetById(int id);
    void Insert(Recipe Recipe);
    void Update(Recipe Recipe);
    void Delete(int id);
}