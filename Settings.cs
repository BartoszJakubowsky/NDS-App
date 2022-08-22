using System;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.ComponentModel;


//zmienne dać do metody, żeby tego nie wywoływać milion razy
//rozwiązać problem ze zwracaniem obiektu
//  -z jednej strony mógłbym przypisać nowy obiekt w Program.cs i miałbym zaktualizowane ustawienia | del --force 
//  -z drugiej po co tworzyć dwa razy obiekty - mogę w klasie ustawić, że dane się podmienią        | --force this option


public class Settings
{
    public Settings()
    {
        DefaultSettings = "true";
        MufaPattern = "default";
        AndSomethingElse = "default";

    }
    //
    // settings values
    //

    private string defaultSettings = "true";


    

    public string DefaultSettings { get { return defaultSettings; ; } set { defaultSettings = value; } } 
    public string MufaPattern { get; set; }
    public string AndSomethingElse { get; set; }

    public Settings SetUp()
    {

        //to avoid errors
        return new Settings();
    }

    public Settings SettingsInit()
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


            }
            // this code shoudn't be ever executed
            Console.WriteLine("Settings file not found");
            return new Settings();
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
}


