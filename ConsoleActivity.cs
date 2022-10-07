using System;

// MOJE PRZEMYŚLENIA 
//jeżeli już się nie maczuje, musi usunąć resztę -> gdzieś musi to zapisywać
//wtedy usuwa od momentu ostatni maczujący + 1 (bo dzięki temu ostatniemu się nie maczowało)
//do zmiany last typedWord->musi validować cały input bo jak coś zmienię na początku to już nie będzie takie samo
//możliwe, że mogę to zaimplementować osobno dla strzałek i delete -> bo tylko tam może taka sytuacja wystąpić
//
//do dodania nie zwracanie uwagi na wielkość liter

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
        //string sourceFile = @"C:\Users\Nowe Jakubki\OneDrive\Pan_Programista\C#\# Projekty\Projekt NDS v2 - PC\NDS-App-v2\cos\savedHistory.json";

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
            Delete(typedKey, ref cursorPosition, commandsLength, consoleChars);

            //historyValid -> shows if there is a history and shows displayed hints
            historyValid = HistoryShower(consoleChars, historyFiles, typedKey, ref cursorPosition);
        }
        else
        {
            Write(typedKey, consoleChars, ref cursorPosition);

            historyValid = HistoryShower(consoleChars, historyFiles, typedKey, ref cursorPosition);
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
            return cursorPosition - 1;
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
        else if (whatArrow.Key == ConsoleKey.Home)
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
            cursorPosition = cursorPosition - 1;
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


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////                          history     /////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //
    //
    //it checks typed input, matched to history, and shows the hints
    public static HistoryValid HistoryShower(List<string> consoleChars, JsonFile historyFiles, ConsoleKeyInfo consoleKey, ref int cursorPosition)
    {
        HistoryValid hisVal = new HistoryValid();


        if (historyFiles == null ^ consoleChars.Count == 0)
        {
            hisVal.IsHistory = false;
            hisVal.MatchedHistory = "";
            return hisVal;
        }
        hisVal.IsHistory = true;
        string[] typedStrings = CharListToStringArray(consoleChars);
        //final string
       



        //for posibble use in belowed function
        int cursorPosition2 = cursorPosition;

        void showHistory()
        {

            //variables needed for authints
            //
            //              
            //take last typed command, take it length and compare to history


            //last typed string
            string lastStringFromArray = typedStrings[typedStrings.Length - 1];

            //length of last typed string
            int lastTypedStringLength = lastStringFromArray.Length;

            //string for history purpouse
            string historyString;

            //nessesarry for moving cursor to it's previous position
            int allHistoryStringsLength = 0;

            //variable to cooperate autohints when spacebar is pressed
            int isSpaceBar = 0;
            
            //first string that shows rest of the freshed word
            string firstHintString = "";

            //all showed words
            string allShowedHints = "";

            //
            //logic below

            void deleteHints(string matchedString, string lastTypedWord)
            {

                //jeżeli różnica jest 0 -> jest na końcu


                //check the cursor position with right barier -> program will know how far get to delete hints


                //minus one because I override last mached word
                int howFarMoveCursor = hisVal.MatchedHistory.Length;
                //if I overwrite one char hint or there was any
                if (howFarMoveCursor <= 0)
                    return;
                Console.WriteLine();
                Console.WriteLine("----------");
                Console.WriteLine(allShowedHints);
                Console.WriteLine("----------");
                Environment.Exit(0);
                Console.CursorVisible = false;
                Console.SetCursorPosition(Console.CursorLeft + howFarMoveCursor, Console.CursorTop);

                for (int i = 0; i <= howFarMoveCursor; i++)
                {
                    Console.Write("\b \b");
                }
            }



            //here it start to search through each of json history input
            for (int j = 0; j < historyFiles.HistoryInput[0].TypedInput.Count; j++)
            {


                //start of hints program = clear all the hints that might be on screen



                //type json Input to some variable
                historyString = historyFiles.HistoryInput[0].TypedInput[j].Input;


                //check what was typed, then add to it rest of the current json word
                if (lastStringFromArray.Length <= historyString.Length)
                {
                    //variable changes only for if statements, later it's changes again ti it's final purpouse
                    firstHintString = lastStringFromArray + historyString.Remove(0, lastTypedStringLength);
                }
                else //if the word was longer, search for next one (meaby this one will fit)
                {
                    //if this is the last one word and it didn't matched, let it through the code to reach delete history method
                    if (j == historyFiles.HistoryInput[0].TypedInput.Count - 1)
                    {

                        //add 1 to j to let know program that this is the last wor
                        j++;
                        firstHintString = "";

                    }
                    //next loop to find matched word
                    else
                    {
                        continue;
                    }
                }


                //if word didn't match or this is last word and it didn't match
                //return empty string and false as history didn't match == not exist and erase previous history
                if (firstHintString != historyString && j == historyFiles.HistoryInput[0].TypedInput.Count)
                {
                    //it won't have history -> flag false
                    hisVal.IsHistory = false;
                    deleteHints(firstHintString, lastStringFromArray);
                    return;
                }
                else if(firstHintString != historyString)
                {
                    continue;
                }
                //simple else would fit but for now i'm goint to leave it (for luck :D)
                else //if (finalHintsString == historyString)
                {
                    //back to it's final look
                    //rest of hint string that matches witch typed string and json input string
                    firstHintString = historyString.Remove(0, lastTypedStringLength);

                    //start to show rest hints on console
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(firstHintString);

                    for (int k = j + 1; k < historyFiles.HistoryInput[0].TypedInput.Count; k++)
                    {

                        //it has history -> flag hange
                        hisVal.IsHistory = true;
                        //add first matched string hint to memory
                        if (k == j + 1)
                            hisVal.MatchedHistory += firstHintString;


                        //this var changes to holder of the next json input string
                        historyString = historyFiles.HistoryInput[0].TypedInput[k].Input;

                        //to avoid moving hints after pressing space bar the firs one does not add " "
                        if (consoleKey.Key == ConsoleKey.Spacebar)
                        {
                            if (isSpaceBar == 0)
                            {
                                //add hint without space char
                                Console.Write(historyString);
                                hisVal.MatchedHistory += historyString;
                                //allHistoryStringsLength += historyString.Length;
                                isSpaceBar++;
                            }
                            else //DRY cryies when sees it :D
                            {
                                Console.Write(" " + historyString);
                                hisVal.MatchedHistory += " " + historyString;
                                //allHistoryStringsLength += historyString.Length + 1;
                            }

                        }
                        //normal when space isn't pressed
                        else
                        {
                            Console.Write(" " + historyString);
                            hisVal.MatchedHistory += " " + historyString;
                            //allHistoryStringsLength += historyString.Length + 1;
                        }

                       
                    }

                    //cursor back to normal visible + normal position
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(Console.CursorLeft - hisVal.MatchedHistory.Length, Console.CursorTop);
                    //Console.SetCursorPosition(Console.CursorLeft - firstHintString.Length - allHistoryStringsLength, Console.CursorTop);
                    Console.ResetColor();
                    Console.CursorVisible = true;

                    return;

                }

            }


            //code should never reach this line
            Console.WriteLine("something is broken with historyString");

        }





        ////if arr is up or down
        //if (consoleKey.Key == ConsoleKey.DownArrow ^ consoleKey.Key == ConsoleKey.UpArrow)
        //{
        //    //future code below


        //    //first check last command, when typed copy/move/rename etc. search only for those with this action (same for below)

        //    //add new right barier 
        //    //add element to list



        //}



        if (consoleKey.Key == ConsoleKey.Backspace ^ consoleKey.Key == ConsoleKey.Delete)
        {
            showHistory();
        }
        else
        {
            showHistory();
        }
        return hisVal;




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


