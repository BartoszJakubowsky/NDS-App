using System;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.ComponentModel;


// na razie abond project settings 


class Settings
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


/*
  public static Settings SettingsInit()
    {

        // get current directory
        //string currentDir = Directory.GetCurrentDirectory();
        string currentDir = @"C:\Users\Nowe Jakubki\OneDrive\Pan_Programista\C#\# Projekty\Projekt NDS v2 - PC\NDS-App-v2";

        //
        // create search pattern for settings jsons files
        Regex regex = new Regex("settings_[1-9]\\.json", RegexOptions.IgnoreCase);

        //
        // create list for settings files
        List<string> settingsList = new List<string>();


        foreach (string file in Directory.GetFiles(currentDir))
        {
            if (regex.IsMatch(file))
            {
                settingsList.Add(file);
            }
        }


        if (settingsList.ToArray().Length > 0)
        {
            foreach (string json in settingsList.ToArray())
            {
                //download string from current directory
                string jsonFileSerialized = File.ReadAllText(json);
                Settings settings = JsonConvert.DeserializeObject<Settings>(jsonFileSerialized);

                if (settings.DefaultSettings == "true")
                {
                    string fileName = Path.Combine("", Path.GetFileName(json));
                    Console.WriteLine($"Settings loaded from {fileName}\n\n\n");
                    return settings;


                }
                //actually this code shoudn't be ever executed
                Console.WriteLine();
                return new Settings();



            }
        }
        else
        {
            Settings settings = new Settings();


            //create and send string to current dir
            string jsonFile = JsonConvert.SerializeObject(settings);
            File.WriteAllText($@"{currentDir}\settings_1.json", jsonFile);

            return settings;
        }

    }


*/