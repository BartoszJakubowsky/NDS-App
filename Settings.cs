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
        mufaPattern = "false";
        andSomethingElse = "false";
    }


    private string defaultSettings;
    private string mufaPattern;
    private string andSomethingElse;


    public string DefaultSettings { get { return defaultSettings; } set { defaultSettings = value; } }
    public string MufaPattern { get { return mufaPattern; } set { mufaPattern = value; } }
    public string AndSomethingElse { get { return andSomethingElse; } set { andSomethingElse = value; } }

    public Settings SetUp()
    {
   
        //to avoid errors
        return new Settings();
    }

 }


