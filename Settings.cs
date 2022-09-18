using System;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.ComponentModel;


// na razie abond project settings 


public class Settings
{
    public Settings()
    {
        defaultSettings = "false";
        extenColor = "default";
        optionColor = "false";
    }


    private string defaultSettings;
    private string extenColor;
    private string optionColor;


    public string DefaultSettings { get { return defaultSettings; } set { defaultSettings = value; } }
    public string ExtenColor { get { return extenColor; } set { extenColor = value; } }
    public string OptionColor { get { return optionColor; } set { optionColor = value; } }

    public Settings SetUp()
    {
   
        //to avoid errors
        return new Settings();
    }

    public void Help()
    {
        Console.WriteLine("\n");
        Console.WriteLine("MOVE commands:");
        Console.WriteLine("--extension: ... -> it's accepting one parameter witch moves files only with given extension");
        Console.WriteLine("--create -> by default it creates final directory if this does not exist");
        Console.WriteLine("--whatmove: ... -< it's accepting one of parameters: file / folder -> and moves only files or only folders\n\n");
    }


 }


