using Newtonsoft.Json;
using System.Text.RegularExpressions;   


//InOut jako segregator na metody
//
//dodać możliwość dodawania cyfr po wpisaniu - i --
//
//dodać blokadę dodawania komendy - i -- po wpisaniu instrukcji


class Program
{
    static void Main(string[] args)
    {

        Settings settings = SettingsInit();
        MainProgram(settings);


    }
        
    public static void MainProgram(Settings settings)
    {
        Menu();
        string[] choice = InOut.ConsoleReadKey(settings);
        CommandSender(choice);
        MainProgram(settings);



    }

    public static void Menu()
    {
        //          //
        //   MENU   //
        //          //
        Console.WriteLine("\t\t\tNDS App v2\n");
        Console.WriteLine("Copy [1] Move [2] Create Folder [3] Settings [4] Close [5]\n");
        Console.WriteLine("or type command (type help to see more)\n");

        //                  //   
        //  chosen option  //
        //                //
        Console.Write("Enter your choice: ");
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
            else if (key.Key == ConsoleKey.N)
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
        else
        {
            Settings settings = new Settings();


            //create and send string to current dir
            string jsonFile = JsonConvert.SerializeObject(settings);
            File.WriteAllText($@"{currentDir}\settings_1.json", jsonFile);

            return settings;
        }

    }

    public static void CommandSender(string[] commands)
    {
        string toDo = commands[0];

        if (toDo.Contains("-"))
        {
            if (toDo.Contains("--"))
            {
                Console.WriteLine("\nAt first type -command");
            }
            else
            {
                if (toDo == "-move")
                {
                    MoveFilesClass moveFiles = new MoveFilesClass();
                    moveFiles.Init(commands);
                }
                else if (toDo == "-help")
                {
                    Settings settings = new Settings();
                    settings.Help();
                }
                else if(toDo == "-copy")
                {
                    CopyFileClass copyFiles = new CopyFileClass();
                    copyFiles.Init(commands);
                }
                else
                {
                    Console.WriteLine("\nCommand cannot be used at the moment");
                }
                //Console.WriteLine($"\nPodano komendę {toDo.Substring(1,toDo.Length-1)}");
            }
        }
        else
        {
            Console.WriteLine("\nYou didn't write a command");
        }
    }

}

