using Yumby2;

Title = "Yummmmmmmmby!";

//1. check if db exists
//string dbPath = "mydatabase.sqlite3";
//if (!File.Exists(dbPath))
//{
//    DbInitializer.CreateAndSeedDb(dbPath);
//}
DbInitializer.CheckForExistingDb(Globals.dbPath);

ReadKey();