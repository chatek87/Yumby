Title = "Yummmmmmmmby!";

//1. check if db exists
string dbPath = "mydatabase.sqlite3";
if (!File.Exists(dbPath))
{
    CreateAndSeedDb(dbPath);
}


ReadKey();