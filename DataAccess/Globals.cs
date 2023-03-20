namespace Yumby.DataAccess;

public static class Globals
{
    public static readonly string dbPath = "mydatabase.sqlite3";

    public static readonly string connectionString = $"Data Source={dbPath};Version=3;";
}