using System;


public class HistoryValid
{

    public HistoryValid()
    {
        this.MatchedHistory = "";
        this.IsHistory = false;
    }

    public string MatchedHistory { get; set; }
    public bool IsHistory { get; set; }
}



public class ConsoleActivity 
{


    public static string[] TypedCharsToConsole()
    {
        //to check what key is pressed
        ConsoleKeyInfo key;

        //to saved all written characters
        List<string> consoleChars = new List<string>();

        //to delete chosen char from input
        int cursorPosition = consoleChars.Count;

        //string with all typed characters
        JsonSerializerClass deserialize = new JsonSerializerClass();

        do
        {
            key = Console.ReadKey(true);
            whatKeyWasTyped(key, consoleChars, ref cursorPosition, deserialize);


        } while (key.Key != ConsoleKey.Enter);

        return consoleChars.ToArray();


    }

    private static void whatKeyWasTyped(ConsoleKeyInfo typedKey, List<string> consoleChars, ref int cursorPosition, JsonSerializerClass deserialize)
    {
        //json file with history input
        string sourceFile = @"C:\Users\Nowe Jakubki\OneDrive\Pan_Programista\C#\# Projekty\Projekt NDS v2 - PC\NDS-App-v2\cos\savedHistory.json";

        JsonFile historyFiles = null;
        if (File.Exists(sourceFile))
        {
            historyFiles = deserialize.JsonDeserialized();
        }

        HistoryValid historyValid = new HistoryValid();


        //right cursor barier
        int commandsLength = consoleChars.Count;

        if (typedKey.Key == ConsoleKey.LeftArrow ^ typedKey.Key == ConsoleKey.RightArrow ^ typedKey.Key == ConsoleKey.Home ^ typedKey.Key == ConsoleKey.End)
        {
            //typedKey -> arrow or home/end
            //cursorPosition for setting moving range for cursor
            //commandsLength for right barirer for cursor
            //historyValid.IsHistory true/false for extend right barier

            cursorPosition = moveCursor(typedKey, ref cursorPosition, commandsLength, historyValid);

        }
        else if (typedKey.Key == ConsoleKey.Tab)
        {
            //AutoCompiler();
        }
        else if (typedKey.Key == ConsoleKey.Backspace ^ typedKey.Key == ConsoleKey.Delete)
        {
            Delete(typedKey, ref cursorPosition , commandsLength, consoleChars); 
        }
        else
        {
            Write(typedKey, consoleChars, ref cursorPosition);
            historyValid = HistoryShower(consoleChars, historyFiles, ConsoleKey.Home);
            //putted it here insted in Write to code less trasher


        }

    }

    private static int moveCursor(ConsoleKeyInfo whatArrow, ref int cursorPosition, int rightCursorBarier, HistoryValid history)
    {


        if (whatArrow.Key == ConsoleKey.LeftArrow)
        {
            //left barier
            if (cursorPosition == 0)
                return cursorPosition;

            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop); ;
            return cursorPosition -1;
        }
        else if (whatArrow.Key == ConsoleKey.RightArrow)
        {
            //right barrier
            if (cursorPosition == rightCursorBarier)
            {
                //check if there is historyShower
                if (history.IsHistory == true)
                {

                }
                else
                    return cursorPosition;
            }

            Console.SetCursorPosition(Console.GetCursorPosition().Left + 1, Console.GetCursorPosition().Top);
            return cursorPosition + 1;
        }
        else if(whatArrow.Key == ConsoleKey.Home)
        {
            //left barier
            if (cursorPosition == 0)
                return cursorPosition;

            Console.SetCursorPosition(Console.CursorLeft - rightCursorBarier, Console.CursorTop); ;
            return cursorPosition = 0;
        }
        else //for end
        {
            //right barrier
            if (cursorPosition == rightCursorBarier)
                return cursorPosition;

            Console.SetCursorPosition(Console.GetCursorPosition().Left + rightCursorBarier, Console.GetCursorPosition().Top);
            return cursorPosition = rightCursorBarier;
        }

    }


    private static void Write(ConsoleKeyInfo typedChar, List<string> consoleChars, ref int cursorPosition) 
    {
        char addChar = typedChar.KeyChar;
        Console.Write(addChar);

        consoleChars.Add(addChar.ToString());
        cursorPosition = cursorPosition + 1;
        
    }

    private static void Delete(ConsoleKeyInfo remove, ref int cursorPosition, int commandsLength, List<string> consoleChars)
    {


        if (remove.Key == ConsoleKey.Backspace)
        {

            //check if left cursor reached barier
            if (cursorPosition == 0)
                return;

            //remove char from list
            cursorPosition = cursorPosition -1;
            consoleChars.RemoveAt(cursorPosition);


            //remove char form console
            Console.CursorVisible = false;
            for(int i = cursorPosition; i < consoleChars.Count+1; i++)
            {
                if (i == consoleChars.Count && i == cursorPosition)
                {
                    Console.Write("\b \b");
                }
                else
                {

                    if (i == cursorPosition)
                    {
                        Console.Write("\b \b");
                        Console.Write(consoleChars[i]);

                    }
                    else if (i == consoleChars.Count)
                    {
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                        Console.Write("\b \b");
                    }
                    else
                    {
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                        Console.Write("\b \b");
                        Console.Write(consoleChars[i]);
                    }

                }

            }
            Console.SetCursorPosition(Console.CursorLeft - (consoleChars.Count - cursorPosition), Console.CursorTop);
            Console.CursorVisible = true;

        }
        else
        {
            //check if right cursor reached barier
            if (cursorPosition == commandsLength)
                return;
            //remove char from list
            consoleChars.RemoveAt(cursorPosition);
            //remove char from console -> opposit direction to \b
            //remove char form console
            Console.CursorVisible = false;

            if (cursorPosition == consoleChars.Count)
            {
                Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                Console.Write("\b \b");
            }
            for (int i = cursorPosition; i < consoleChars.Count; i++)
            {
                    Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                    Console.Write("\b \b");
                    Console.Write(consoleChars[i]);

                    if (i == consoleChars.Count - 1)
                    {
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                        Console.Write("\b \b");
                    }

            }
            Console.SetCursorPosition(Console.CursorLeft - (consoleChars.Count - cursorPosition), Console.CursorTop);
            Console.CursorVisible = true;


        }
    }

    private static void AutoCompiler()
    {
        //...
    }

    public static HistoryValid HistoryShower(List<string> consoleChars, JsonFile historyFiles, ConsoleKey arrowUpDown)
    {
        HistoryValid hisVal = new HistoryValid();

        if (historyFiles == null)
        {
            hisVal.IsHistory = false;
            hisVal.MatchedHistory = "";
            return hisVal;
        }
        hisVal.IsHistory = true;
        string[] typedStrings = CharListToStringArray(consoleChars);
        //final string
        string finalString = "";
        //
        //converter from array to string
        //string tempString = "";
        //for (int i = 0; i < test.Length; i++)
        //{

        //    tempString += test[i];

        //    if (i + 1 != test.Length)
        //        tempString += " ";

        //}

        //if arr is up or down
        if (arrowUpDown == ConsoleKey.DownArrow ^ arrowUpDown == ConsoleKey.UpArrow)
        {
            //future code below


            //first check last command, when typed copy/move/rename etc. search only for those with this action (same for below)

            //add new right barier 
            //add element to list



        }
        else
        {
            //
            //take last typed command, take it length and compare to history
            //
            //last typed string
            string lastStringFromArray = typedStrings[typedStrings.Length - 1];

            //length of last typed string
            int length = lastStringFromArray.Length;

            //string for history purpouse
            string historyString;
            for (int i = 0; i < typedStrings.Length; i++)
            {
                for (int j = 0; j < historyFiles.HistoryInput[0].TypedInput.Count; j++)
                {


                    historyString = historyFiles.HistoryInput[0].TypedInput[j].Input;

                    if (lastStringFromArray.Length > historyString.Length)
                        continue;

                    finalString = lastStringFromArray + historyString.Remove(0, length);

                    if (finalString == historyString)
                    {
                        finalString = historyString.Remove(0, length);
                    }
                    
                }
                

            }
            
        }
        hisVal.IsHistory = true;
        hisVal.MatchedHistory = finalString;
        return hisVal;




    }

    private static string[] CharListToStringArray(List<string> consoleChars)
    {

        string[] tempArr = consoleChars.ToArray();
        List<string> listToFinalArr = new List<string>();
        string tempStr = "";

        for (int i = 0; i < tempArr.Length; i++)
        {
            if (tempArr[i] != " ")
            {
                tempStr += tempArr[i];

                if (i+1 != tempArr.Length)
                    continue;
            }

            listToFinalArr.Add(tempStr);
            tempStr = "";
        }

        return listToFinalArr.ToArray();
    }
}


