using Yumby.ConsoleUI;
using Yumby.DataAccess;

Title = "yumby";
//CursorVisible = false;

DbInitializer.CheckForExistingDb(Globals.dbPath);

WelcomePage.Start();
