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

        MainProgram();


    }

    public static void MainProgram()
    {

        Menu();
        string[] commands = ConsoleActivity.TypedCharsToConsole();


        CommandSender(commands);
        MainProgram();

        

    }

    public static void Menu()
    {
        //          //
        //   MENU   //
        //          //
        Console.WriteLine("\t\t\tNDS App v2\n");
        Console.WriteLine("Typed what do you want to do\n");
        Console.WriteLine("or type -help to see options\n");

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
                    MoveFilesClass moveFiles = new MoveFilesClass(commands);
                    moveFiles.SaveInput(commands);
                }
                else if (toDo == "-help")
                {
                    Settings settings = new Settings();
                    settings.Help();
                }
                else if (toDo == "-copy")
                {
                    CopyFileClass copyFiles = new CopyFileClass();
                    copyFiles.Init(commands);
                    copyFiles.SaveInput(commands);
                }
                else if (toDo == "-rename")
                {
                    RenameClass renameFiles = new RenameClass();
                    renameFiles.Init(commands);
                    renameFiles.SaveInput(commands);
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


