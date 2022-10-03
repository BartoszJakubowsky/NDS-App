using System;

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

        do
        {
            key = Console.ReadKey(true);
            whatKeyWasTyped(key, consoleChars, ref cursorPosition);


        } while (key.Key != ConsoleKey.Enter);

        return consoleChars.ToArray();


    }

    private static void whatKeyWasTyped(ConsoleKeyInfo typedKey, List<string> consoleChars, ref int cursorPosition)
    {
        //right cursor barier
        int commandsLength = consoleChars.Count;

        if (typedKey.Key == ConsoleKey.LeftArrow ^ typedKey.Key == ConsoleKey.RightArrow ^ typedKey.Key == ConsoleKey.Home ^ typedKey.Key == ConsoleKey.End)
        {
            cursorPosition = moveCursor(typedKey, ref cursorPosition, commandsLength);

        }
        else if (typedKey.Key == ConsoleKey.Tab)
        {
            //autoCompiler();
        }
        else if (typedKey.Key == ConsoleKey.Backspace ^ typedKey.Key == ConsoleKey.Delete)
        {
            Delete(typedKey, ref cursorPosition , commandsLength, consoleChars); 
        }
        else
        {
            Write(typedKey, consoleChars, ref cursorPosition);
        }

    }

    private static int moveCursor(ConsoleKeyInfo whatArrow, ref int cursorPosition, int rightCursorBarier)
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
                return cursorPosition;

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

}
