using System.Text.RegularExpressions;

//dodać jsona do odczytu kolorów

public class InOut
{


    public static string[] ConsoleReadKey(Settings settings)
    {
        //flag for stopping loop
        bool flag = false;

        //pressed char displayer on console
        string typedCharacter = "";

        //for string[] of numbers to parse
        int pressedNumber = 0;

        //list for sending new chars to char-to-string method parser
        List<string> typedCommand = new List<string>();

        // :)
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


                    //text colour changing during special commands
                    //it have to be executed after writing character
                    TextColor(charToStringsCommands(typedCommand), key.Key, settings);


                    //print on console
                    typedCharacter += key.KeyChar;
                    Console.Write(key.KeyChar);


                }

            }
            //else for backspace
            else if (key.Key == ConsoleKey.Backspace && typedCharacter.Length > 0)
            {
                Console.Write("\b \b");
                typedCharacter = typedCharacter.Substring(0, (typedCharacter.Length - 1));

                TextColor(charToStringsCommands(typedCommand, ConsoleKey.Backspace), key.Key, settings);

            }

            //exit from loop
            else if (key.Key == ConsoleKey.Enter)
            {
                //if user would not chose any number
                if (typedCharacter.Length == 0)
                {
                    continue;
                }
                flag = true;
                textColor();
            }

        }
        while (!flag);

        //checking if input was numberChoice or strings command
        string[] returnChoice = charToStringsCommands(typedCommand);

        if (returnChoice.Length == 0)
        {
            Console.WriteLine();
            Console.Write($"działa: {pressedNumber}");
            returnChoice = new [] { pressedNumber.ToString()};
            return returnChoice;
        }
        else
        {
            return returnChoice;
        }

    }
    //defaut random key -> it's only waiting for backspace
	public static string[] charToStringsCommands(List<string> charInput, ConsoleKey backspace = ConsoleKey.Attention)
	{

        string[] finalArray;
        string newItem = "";
        List<string> temp = new List<string>();

        if (backspace == ConsoleKey.Backspace)
        {
            charInput.RemoveAt(charInput.Count - 1);
            newItem = String.Join("", charInput);
            temp = newItem.Split(" ").ToList();

            return finalArray = temp.ToArray();
        }


            newItem = String.Join("", charInput);
            temp = newItem.Split(" ").ToList();


        return finalArray = temp.ToArray();
    }

    public static void TextColor(string[] input, ConsoleKey Key, Settings settings)
    {
        //write code that checks json file for changed color


        //regex had to be use due to simplines of lastTypedWord.Contains
        Regex regex1 = new Regex("-[a-zA-Z]+", RegexOptions.IgnoreCase);
        Regex regex2 = new Regex("--[a-zA-Z]+", RegexOptions.IgnoreCase);
        string lastTypedWord = input[input.Length - 1];


        if (Key == ConsoleKey.Backspace && lastTypedWord.Length == 1 && lastTypedWord == "-")
        {

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.CursorVisible = false;
            Console.Write("\b");
            Console.Write("-");
            Console.CursorVisible = true;

            return;
        }
        else if (lastTypedWord.Contains("--"))
        {

            //to replace one "-" with grey color
            if (lastTypedWord.Length == 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.CursorVisible = false;
                Console.Write("\b");
                Console.Write("-");
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
                Console.ForegroundColor = ConsoleColor.DarkMagenta;

                //Console.CursorVisible = false;
                ////Console.Write("\b");
                ////Console.Write("-");
                //Console.CursorVisible = true;

            }
            else if (regex1.IsMatch(lastTypedWord))
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            }
        }    
        else
        {
            Console.ResetColor();
        }
    }

    //for resetting color after pressing enter
    public static void textColor()
    {
        Console.ResetColor();
    }
}
