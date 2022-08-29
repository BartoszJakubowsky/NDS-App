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

 }


