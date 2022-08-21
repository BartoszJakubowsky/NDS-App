using System;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.ComponentModel;
//target
//
// stworzyć apkę, która będzie w stanie kopiować elementy do danych folderów
// i zmieniać ich nazwę
//
// dodać opcje, podzielić na opcje szybkie i zaawansowawne
// - zapisanie ustawień danego pliku
// - wybór pliku lub wpisanie jego patternu
// - co z nim zrobić -> usunąć, przenieść, przekopiować?
// - czy zmienić nazwę? Jak ją zmienić? (ustawić domyślnie jak katalogu)


//co zrobione
//
// wybór mennu z konsoli


class Program
{
    static void Main(string[] args)
    {
        SettingsInit();
        MainProgram();



    }

    public static void MainProgram()
    {
        //          //
        //   MENU   //
        //          //
        Console.WriteLine("\t\t\tNDS App v2\n");
        Console.WriteLine("Copy [1] Move [2] Create Folder [3] Settings [4] Close [5]\n");

        //                  //   
        //  chosen option  //
        //                //
        int choice = ConsoleReadKey();

        switch (choice)
        {
            case 1:
                break; ;
            case 2:
                break;
            case 3:
                break;
            case 4:
        
                SettingsInit();
                MainProgram();
                break;
            case 5:
                CloseApp();
                MainProgram();
                break;
        }

        MainProgram();







    }
    public static int ConsoleReadKey()
    {
        bool flag = false;
        string _val = "";
        int pressedNumber = 0;
        ConsoleKeyInfo key;


        Console.Write("Enter your choice: ");
        do
        {
            key = Console.ReadKey(true);

            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {

                bool _x = int.TryParse(key.KeyChar.ToString(), out pressedNumber);


                if (_x)
                {
                    //
                    //if user would get out of menu option's range
                    //
                    // also it's marked because of trouble to find this
                    if (pressedNumber > 5 ^ pressedNumber == 0)
                    {
                        continue;
                    }
                    _val += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else
                {
                    continue;
                }
            }
            else if (key.Key == ConsoleKey.Backspace && _val.Length > 0)
            {

                _val = _val.Substring(0, (_val.Length - 1));
                Console.Write("\b \b");
            }

            //if user would not chose any number
            else if (key.Key == ConsoleKey.Enter)
            {
                if (_val.Length == 0)
                {
                    continue;
                }
                flag = true;
            }

        }
        // Stops Receving Keys Once Enter is Pressed
        while (!flag);


        Console.WriteLine();
        return pressedNumber;
    }
    public static object SettingsInit()
    {

        // get current directory
        //string currentDir = Directory.GetCurrentDirectory();
        string currentDir = @"C:\Users\barto\OneDrive\Pan_Programista\C#\# Projekty\Projekt NDS v2\NDS App v2";

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
            //actually this code shoudn't be ever executed
            Console.WriteLine();
            return "file not found" ;
        }
        else
        {
            Settings settings = new Settings()
            {
                DefaultSettings = "true",
                MufaPattern = "default",
                AndSomethingElse = "default"
            };

            //create and send string to current dir
            string jsonFile = JsonConvert.SerializeObject(settings);
            File.WriteAllText($@"{currentDir}\settings_1.json", jsonFile);

            return settings;
        }

    }
    public static void CloseApp()
    {
        //napisać kod, który sprawdza czy wszystko jest cacy

        Console.WriteLine("\nAre you sure you want to close application?");
        Console.WriteLine("\nPress [Y]/[N]: ");

        bool flag = false;
        string _val = "";
        char pressedYesNo = '.';
        ConsoleKeyInfo key;


        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Y)
            {
                     pressedYesNo = 'Y';
                    _val += key.KeyChar;
                    Console.Write(key.KeyChar);
            }
            else if(key.Key == ConsoleKey.N)
            {
                pressedYesNo = 'N';
                _val += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && _val.Length > 0)
            {

                _val = _val.Substring(0, (_val.Length - 1));
                Console.Write("\b \b");
            }

            //if user would not chose any number
            else if (key.Key == ConsoleKey.Enter)
            {
                if (_val.Length == 0)
                {
                    continue;
                }
                flag = true;
            }

        }
        // Stops Receving Keys Once Enter is Pressed
        while (!flag);

        switch (pressedYesNo)
        {
            case 'Y':
                Environment.Exit(0);
                break;
            case 'N':
                break;
        }

    }

}