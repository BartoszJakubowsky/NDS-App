using System;
//to do -> add to settings to posibility for hints -> current one and previous one (commit c1a86a1)


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

        //int to hold previous cursor position 
        int previousCursorPosition = 0;

        //string with all typed characters
        JsonSerializerClass deserialize = new JsonSerializerClass();

        //setting previousCursorPosition
        HistoryValid hisVal = new HistoryValid();

        //for delete hints prpuse
        int previousTypedCommandsLength = 0;



        do
        {
            key = Console.ReadKey(true);

            if (key.Key != ConsoleKey.Enter)
                whatKeyWasTyped(key, consoleChars, ref cursorPosition, deserialize, hisVal, ref previousCursorPosition, ref previousTypedCommandsLength);
            //in the future add here ctrl grabber (before whatKeyWasTyped)->
            //if next key will be A, C, V, F and etc. -> send it to functions below...



        } while (key.Key != ConsoleKey.Enter);

        //add here erase all typed line


        return consoleChars.ToArray();


    }

    private static void whatKeyWasTyped(ConsoleKeyInfo typedKey, List<string> consoleChars, ref int cursorPosition,
        JsonSerializerClass deserialize, HistoryValid hisVal, ref int previousCursorPosition, ref int previousTypedCommandsLength)
    {
        string jsonHistorySourceFile = @$"{Directory.GetCurrentDirectory()}\jsonFiles\savedHistory.json";

        JsonFile historyFiles = null;
        if (File.Exists(jsonHistorySourceFile))
            historyFiles = deserialize.JsonDeserialized();

        //right cursor barier
        int commandsLength = consoleChars.Count;

        if (typedKey.Key == ConsoleKey.LeftArrow || typedKey.Key == ConsoleKey.RightArrow || typedKey.Key == ConsoleKey.Home || typedKey.Key == ConsoleKey.End)
        {
            //typedKey -> arrow or home/end
            //cursorPosition for setting moving range for cursor
            //commandsLength for right barirer for cursor
            //historyValid.IsHistory true/false for extend right barier

            cursorPosition = moveCursor(typedKey, ref cursorPosition, commandsLength, ref previousCursorPosition);

        }
        else if (typedKey.Key == ConsoleKey.Tab)
        {
            AutoCompiler(hisVal, typedKey, consoleChars, ref cursorPosition, ref previousCursorPosition);
        }
        else if (typedKey.Key == ConsoleKey.Backspace || typedKey.Key == ConsoleKey.Delete)
        {
            Delete(typedKey, ref cursorPosition, commandsLength, consoleChars, ref previousTypedCommandsLength);
            HistoryShower(consoleChars, historyFiles, typedKey, ref cursorPosition, ref previousCursorPosition, hisVal, ref previousTypedCommandsLength);
        }
        else
        {
            Write(typedKey, consoleChars, ref cursorPosition, ref previousCursorPosition, ref previousTypedCommandsLength);
            HistoryShower(consoleChars, historyFiles, typedKey, ref cursorPosition, ref previousCursorPosition, hisVal, ref previousTypedCommandsLength);

        }

    }

    private static int moveCursor(ConsoleKeyInfo whatArrow, ref int cursorPosition, int rightCursorBarier, ref int previousCursorPosition)
    {
        previousCursorPosition = cursorPosition;

        if (whatArrow.Key == ConsoleKey.LeftArrow)
        {
            //left barier
            if (cursorPosition == 0)
                return cursorPosition;

            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop); ;
            return cursorPosition - 1;
        }
        else if (whatArrow.Key == ConsoleKey.RightArrow)
        {
            //right barrier
            if (cursorPosition == rightCursorBarier)
                return cursorPosition;

            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            return cursorPosition + 1;
        }
        else if (whatArrow.Key == ConsoleKey.Home)
        {
            //left barier
            if (cursorPosition == 0)
                return cursorPosition;

            Console.SetCursorPosition(Console.CursorLeft - cursorPosition, Console.CursorTop); ;
            return cursorPosition = 0;
        }
        else //for end
        {
            //right barrier
            if (cursorPosition == rightCursorBarier)
                return cursorPosition;

            Console.SetCursorPosition(Console.CursorLeft + (rightCursorBarier - cursorPosition), Console.CursorTop);
            return cursorPosition = rightCursorBarier;
        }

    }


    private static void Write(ConsoleKeyInfo typedChar, List<string> consoleChars, ref int cursorPosition, ref int previousCursorPosition, ref int previousTypedCommandsLength)
    {

        previousTypedCommandsLength = consoleChars.Count;
        previousCursorPosition = cursorPosition;

        char addChar = typedChar.KeyChar;
        //if new char is typed after all others before
        if (cursorPosition == consoleChars.Count)
        {
            Console.Write(addChar);
            consoleChars.Add(addChar.ToString());
            cursorPosition += 1;

        }
        else //if (cursorPosition < consoleChars.Count)

        {

            List<string> temp = new List<string>();

            for (int i = cursorPosition; i <= consoleChars.Count; i++)
            {
                if (i == cursorPosition)
                {
                    temp.Add(consoleChars[i]);
                    consoleChars[i] = addChar.ToString();
                }
                else if (i == consoleChars.Count)
                {
                    consoleChars.Add(temp.Last());
                    break;
                }
                else
                {
                    temp.Add(consoleChars[i]);
                    consoleChars[i] = temp.First();
                    temp.RemoveAt(0);
                }
            }

            Console.CursorVisible = false;
            for (int i = cursorPosition; i < consoleChars.Count; i++)
            {
                Console.Write(consoleChars[i]);
            }
            cursorPosition += 1;
            int howFarMoveCursor = consoleChars.Count - cursorPosition;
            Console.SetCursorPosition(Console.CursorLeft - howFarMoveCursor, Console.CursorTop);
            Console.CursorVisible = true;


        }


    }

    private static void Delete(ConsoleKeyInfo remove, ref int cursorPosition, int commandsLength, List<string> consoleChars, ref int previousTypedCommandsLength)
    {



        if (remove.Key == ConsoleKey.Backspace)
        {

            //check if left cursor reached barier
            if (cursorPosition == 0)
                return;

            //remove char from list

            cursorPosition -= 1;
            consoleChars.RemoveAt(cursorPosition);


            //remove char form console
            Console.CursorVisible = false;
            for (int i = cursorPosition; i < consoleChars.Count + 1; i++)
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
        //ConsoleKey.Delete
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

        previousTypedCommandsLength = commandsLength + 1;

    }

    private static void AutoCompiler(HistoryValid history, ConsoleKeyInfo consoleKey, List<string> consoleChars, ref int cursorPosition, ref int previousCursorPosition)
    {
        //if there is no history 
        if (history.IsHistory == false)
            return;

        Console.CursorVisible = false;

        if (consoleKey.Key == ConsoleKey.Tab)
        {
            //set new previousCursorPosition
            previousCursorPosition = cursorPosition;

            //set cursor at the end of actual commands length
            Console.SetCursorPosition(Console.CursorLeft + (consoleChars.Count - cursorPosition), Console.CursorTop);

            //add hitns to string list and show them at the screen
            for (int i = 0; i < history.MatchedHistory.Length; i++)
            {
                string element = history.MatchedHistory.ElementAt(i).ToString();
                Console.Write(element);
                consoleChars.Add(element);
            }

            //set new cursor position at the end of all commands
            cursorPosition = consoleChars.Count;

            //now history does not match anymore
            history.IsHistory = false;
            history.MatchedHistory = "";

        }
        //else if ctrl + f (future)

        Console.CursorVisible = true;

    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////                          history     /////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //
    //
    //it checks typed input, matched to history, and shows the hints
    public static void HistoryShower(List<string> consoleChars, JsonFile historyFiles, ConsoleKeyInfo consoleKey, ref int cursorPosition,
        ref int previousCursorPosition, HistoryValid hisVal, ref int previousTypedCommandsLength)
    {
        //check if history json exist
        if (historyFiles == null)
        {
            hisVal.IsHistory = false;
            hisVal.MatchedHistory = "";
            return;
        }
        //string[] typedStringsArray = CharListToStringArray(consoleChars);
        string typedStrings = "";

        if (consoleChars.Count > 0)
        {
            for (int i = 0; i < consoleChars.Count; i++)
            {
                typedStrings += consoleChars[i];
            }
        }

        //for (int i = 0; i < typedStringsArray.Length; i++)
        //{
        //    typedStrings += typedStringsArray[i];

            //    if (i != typedStringsArray.Length - 1)
            //        typedStrings += " ";
            //}



            //for posibble use in belowed function
        int _cursorPosition = cursorPosition;
        int _previousCursorPosition = previousCursorPosition;
        int _previousTypedCommandsLength = previousTypedCommandsLength;

        void showHistory()
        {

            //right barier for cursor
            int rightCursorBarier = consoleChars.Count;

            //to put cursor back where it was
            int howFarMoveCursor = rightCursorBarier - _cursorPosition;

            void deleteHints()
            {


                //it means there is first typed character and so console shouldn't have any hints before it
                if (_previousCursorPosition == 0 && hisVal.MatchedHistory == "")
                    return;

                //to put cursor at the end of hints string
                int hintsLength = hisVal.MatchedHistory.Length;

                if (hintsLength == 0)
                    return;



                void eraseHints()
                {

                    Console.CursorVisible = false;


                    Console.SetCursorPosition(Console.CursorLeft + howFarMoveCursor + hintsLength, Console.CursorTop);

                    for (int i = 0; i < hintsLength; i++)
                    {
                        Console.Write("\b \b");
                    }

                    Console.SetCursorPosition(Console.CursorLeft - howFarMoveCursor, Console.CursorTop);

                    Console.CursorVisible = true;
                }

                if (consoleChars.Count < _previousTypedCommandsLength)
                {
                    hintsLength += 1;
                    eraseHints();

                }
                else
                {

                    eraseHints();
                }
            }





            for (int l = historyFiles.HistoryInput.Count - 1; l >= 0; l--)
            {
                //start of hints program = clear all the hints that might be on screen
                if (l == historyFiles.HistoryInput.Count - 1 && hisVal.IsHistory == true)
                    deleteHints();

                //if there is no input 
                if (typedStrings.Length == 0)
                    return;

                string hintsString = "";
                for (int i = 0; i < historyFiles.HistoryInput[l].TypedInput.Count; i++)
                {
                    hintsString += historyFiles.HistoryInput[l].TypedInput[i].Input;

                    if (i != historyFiles.HistoryInput[l].TypedInput.Count - 1)
                        hintsString += " ";
                }


                if (l == 0 /*last loop */)
                {
                    if (typedStrings.Length > hintsString.Length || typedStrings != hintsString.Remove(typedStrings.Length))
                    {
                        hisVal.IsHistory = false;

                        deleteHints();

                        hisVal.MatchedHistory = "";

                        return;
                    }

                }
                else if (typedStrings.Length > hintsString.Length || typedStrings != hintsString.Remove(typedStrings.Length))
                {
                    continue;
                }
                else
                {
                    hisVal.IsHistory = true;
                    hisVal.MatchedHistory = hintsString.Remove(0, typedStrings.Length);
                    break;
                }
            }

            //just to organize code 
            if (hisVal.IsHistory)
            {
                //start to show rest hints on console
                //to avoid strange cursor behavior
                Console.CursorVisible = false;

                //to put cursor at the end of typed string from it's current position
                Console.SetCursorPosition(Console.CursorLeft + howFarMoveCursor, Console.CursorTop);

                //for spaces issue
                if (consoleKey.Key == ConsoleKey.Spacebar && hisVal.MatchedHistory.First().ToString() == " " || consoleKey.Key == ConsoleKey.Backspace && consoleChars.Last() == " " && hisVal.MatchedHistory.First().ToString() == " ")
                    hisVal.MatchedHistory = hisVal.MatchedHistory.ToString().Remove(0,1);
          

                //to show hints in darker color
                Console.ForegroundColor = ConsoleColor.DarkGray;

                //show first hint right next to one that matches
                Console.Write(hisVal.MatchedHistory);

                //cursor back to normal visible + normal position
                Console.SetCursorPosition(Console.CursorLeft - howFarMoveCursor - hisVal.MatchedHistory.Length, Console.CursorTop);
                Console.ResetColor();
                Console.CursorVisible = true;
                return;
            }

        }



        if (consoleKey.Key == ConsoleKey.Backspace || consoleKey.Key == ConsoleKey.Delete)
        {
            showHistory();
        }
        else
        {
            showHistory();
        }


        //now it holds previos cursorPosition -> it's needed for comparing where it is to delete hints
        previousCursorPosition = cursorPosition;
    }

    //exception if there would be unhalded code behavior
    private static Exception Exception()
    {
        throw new NotImplementedException();
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

                if (i + 1 != tempArr.Length)
                    continue;
            }

            listToFinalArr.Add(tempStr);
            tempStr = "";
        }

        return listToFinalArr.ToArray();
    }
}


