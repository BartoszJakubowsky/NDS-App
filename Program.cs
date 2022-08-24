using System;
using Newtonsoft.Json;
using System.Text.RegularExpressions;


//problemy - code z zmianą koloru textu jest szybciej wywoływany..
// + naprawione bug z y/n
//



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
        int choice = ConsoleReadKey();

        switch (choice)
        {
            case 1:
                break; ;
            case 2:
                break;
            case 3:
                break;

            //          //
            // Settings //
            //          //
            case 4:
                settings.SetUp();
                MainProgram(settings);
                break;
            //          //
            //   Exit   //
            //          //
            case 5:
                CloseApp();
                break;
        }

        MainProgram(settings);



    }

    public static void Menu()
    {
        //          //
        //   MENU   //
        //          //
        Console.WriteLine("\t\t\tNDS App v2\n");
        Console.WriteLine("Copy [1] Move [2] Create Folder [3] Settings [4] Close [5]\n");

        //                  //   
        //  chosen option  //
        //                //
        Console.Write("Enter your choice: ");
    }
    public static int ConsoleReadKey()
    {
        bool flag = false;
        string typedCharacter = "";
        int pressedNumber = 0;
        List<string> typedCommand = new List<string>();
        ConsoleKeyInfo key;



        do
        {
            key = Console.ReadKey(true);

            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {

                bool intChoice = int.TryParse(key.KeyChar.ToString(), out pressedNumber);

                if (intChoice)
                {
                    if (typedCharacter.Length > 0)
                        continue;
                    //
                    //if user would get out of menu option's range
                    //
                    // also it's marked because of trouble to find this
                    if (pressedNumber > 5 ^ pressedNumber == 0)
                    {
                        continue;
                    }
                    typedCharacter += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else
                {

                    //
                    //if first character would be space
                    if (typedCharacter.Length == 0)
                        if (key.Key == ConsoleKey.Spacebar)
                            continue;

                    //add char to list
                    typedCommand.Add(key.KeyChar.ToString());




                    //print on console
                    typedCharacter += key.KeyChar;
                    Console.Write(key.KeyChar);


                    //random consolekey 
                    //text colour changing during special commands
                    //it have to be executed after writing character
                    textColor(charToStringsCommands(typedCommand, ConsoleKey.Attention));

                }

            }
            //else for backspace
            else if (key.Key == ConsoleKey.Backspace && typedCharacter.Length > 0)
            {
                textColor(charToStringsCommands(typedCommand, ConsoleKey.Backspace));

                typedCharacter = typedCharacter.Substring(0, (typedCharacter.Length - 1));
                Console.Write("\b \b");
            }

            //if user would not chose any number
            else if (key.Key == ConsoleKey.Enter)
            {
                if (typedCharacter.Length == 0)
                {
                    continue;
                }
                flag = true;
            }

        }
        // Stops Receving Keys Once Enter is Pressed
        while (!flag);


        //Console.WriteLine();
        //foreach (var item in charToStringsCommands(typedCommand))
        //{
        //    Console.WriteLine(item + " ");
        //}
        return pressedNumber;
    }



    public static void textColor(string[] input)
    {
        //regex had to be use due to simplines of lastTypedWord.Contains
        Regex regex1 = new Regex("-[a-zA-Z]+", RegexOptions.IgnoreCase);
        Regex regex2 = new Regex("--[a-zA-Z]+", RegexOptions.IgnoreCase);
        string lastTypedWord = input[input.Length - 1];

        if (lastTypedWord.Contains("--"))
        {

            //to replace one "-" with grey color
            if (lastTypedWord.Length == 2)
            {
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("\b\b");
                Console.Write("--");
                Console.CursorVisible = true;
            }
            else if (regex2.IsMatch(lastTypedWord))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }

        }
        else if (lastTypedWord.Contains("-"))
        {

            if (lastTypedWord.Length == 1)
            {
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("\b");
                Console.Write("-");
                Console.CursorVisible = true;
            }
            else if (regex1.IsMatch(lastTypedWord))
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            }
        }
        else if (lastTypedWord == "") 
        {

            return;
        }
        else
        {
            Console.ResetColor();
        }
    }

    public static string[] charToStringsCommands(List<string> charInput, ConsoleKey backspace)
    {

        string[] finalArray;
        string newItem;
        List<string> temp = new List<string>();

        if (backspace == ConsoleKey.Backspace)
        {
            charInput.RemoveAt(charInput.Count - 1);
            newItem = String.Join("", temp);
            temp = newItem.Split(" ").ToList();

            return finalArray = temp.ToArray();
        }

        foreach (var item in charInput)
        {
            newItem = String.Join("", charInput);
            temp = newItem.Split(" ").ToList();
        }

        return finalArray = temp.ToArray();

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
}

