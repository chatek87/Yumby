using Yumby.BusinessLogic;
using Yumby.DataAccess;
using Yumby.DataModels;

namespace Yumby.ConsoleUI.Basic;

public class BasicUI
{
    public void Start()
    {
        WriteLine("Welcome to Yumby!");
        WriteLine("Please enter a selection");
        WriteLine("1 - View All Recipes");
        WriteLine("2 - Exit");
    }
}