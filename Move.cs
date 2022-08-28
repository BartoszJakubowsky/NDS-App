using System;

//co możesz podać? 

//Move - standard comment (Move file path_from path_for)
//-wariacje -> all, extension, folders, only empty folders, 
//--dodatkowe opcje -> rename, save, 

public class Move
{
    public Move()
    {
    }


    private string MoveFiles(string filePath, string finalFilePath,string finalPathExist = "", string whatToMove = "", string extension = "", params string[] fileName)
    {
        string[] files;


        if (Directory.Exists(filePath) == false)
            return "home path does not exist";

        if (whatToMove != "")
        {
            if (fileName.Length == 0)
            {
                if (whatToMove == "files" ^ whatToMove == "file")
                {
                    files = Directory.GetFiles(filePath);
                }
            }

        }

        files = Directory.GetDirectories(filePath, "*", SearchOption.TopDirectoryOnly);

        if (files.Length == 0)
            return "Directory is empty";

        for (int i = 0; i < files.Length; i++)
        {
            if (Directory.Exists(finalFilePath) == false && finalPathExist == "")
            {
                Console.WriteLine("Final path does not exist, do you want tocreate one?");

                do
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                } while (ConsoleKey.key != ConsoleKey.Y );
                Console.Write()

                Directory.CreateDirectory(finalFilePath);
            }

            try
            {
                File.Move(files[i], finalFilePath);
                return "Files moved succesfully";
            }
            catch (Exception err)
            {

                Console.WriteLine(err);
                return "Error occure";
            }

        }






    }



}
