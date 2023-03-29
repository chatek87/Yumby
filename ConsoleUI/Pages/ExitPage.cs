﻿namespace Yumby.ConsoleUI;

public static class ExitPage
{
    public static void Start()
    {
        Run();
    }

    private static void Run()
    {
        string prompt = Banner.ExitArt() + "Are you sure you want to exit?";
        List<string> options = new List<string>{ "yes", "go back" };
        var exitPage = new Page(prompt, options);
        int selectionIndex = exitPage.Run();
        switch (selectionIndex)
        {
            case 0:
                ConsoleUtils.QuitConsole();                
                break;
            case 1:
                //MainMenu.Start();
                break;
        }
        //return;
    }
}