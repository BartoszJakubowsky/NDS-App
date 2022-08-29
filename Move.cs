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


    private string MoveFiles(string filePath, string finalFilePath, string subFolders = "", string whatToMove = "", string extension = "", params string[] fileName)
    {
        string[] files;
        //filename -> ilość plików
        //subfolders
        //extension -> declares if it's file or not 
        //what to move - file or directory
        if (Directory.Exists(filePath) == false)
            return "home path or file does not exist";

        if (finalFilePath == "")
            return "target path does not exist";


        static string Optional(string filePath, string subFolders, string whatToMove, string extension, params string[] fileName)
        {

            string[] fileNameChecker(string[] fileName)
            {

                List<string> tempListOfSubFolders = new List<string>();

                for (int i = 0; i < fileName.Length; i++)
                {
                    if (Directory.Exists($@"{filePath}\{fileName[i]}") == true)
                    {
                        tempListOfSubFolders.Add(fileName[i]);
                        Console.WriteLine($"File {fileName[i]} does not exists in given folder");
                    }
                }

                if (tempListOfSubFolders.Count != 0)
                {
                    return tempListOfSubFolders.ToArray();
                }

                string[] emptyArr = new string[] { "" };
                return emptyArr;


            }

            List<string> finalPath = new List<string>();    
            //first check if the given path exist

            if (Directory.Exists(filePath) == false)
                return "Soruce path does not exist";

            string[] filesArr = null;
            if (fileName.Length != 0)
            {
                if (fileNameChecker(fileName)[0] == "")
                    return "Any of files or folders were found in given directory";
                else
                {
                    filesArr = fileNameChecker(fileName);
                }
            }

            if (subFolders == "yes" ^ subFolders == "y" ^ subFolders == "tak" ^ subFolders == "t")
            {

                List<string[]> tempSubFolders = new List<string[]>();

                if (filesArr != null)
                {
                    for (int i = 0; i < filesArr.Length; i++)
                    {
                        if (Path.GetExtension(filesArr[i]) == String.Empty)
                        {
                            if (Directory.GetFileSystemEntries(filesArr[i], "*", SearchOption.AllDirectories).Length == 0)
                            {
                                Console.WriteLine($"Folder {filesArr[i]} is empty");
                            }
                            else
                            {
                                tempSubFolders.Add(Directory.GetFileSystemEntries(filesArr[i], "*", SearchOption.AllDirectories));
                            }
                        }
                    }

                    if (tempSubFolders.Count == 0)
                        return "Wrong option -> can't get subfolders of a file";
                }
                else
                {
                    if (Path.GetExtension(filePath) != String.Empty)
                        return "Wrong option -> can't get subfolder of a file";

                    if (Directory.GetFileSystemEntries(filePath, "*", SearchOption.AllDirectories).Length == 0)
                    {
                        Console.WriteLine($"There is no subfolders in {filePath}");
                    }
                    else
                    {
                        tempSubFolders.Add(Directory.GetFileSystemEntries(filePath, "*", SearchOption.AllDirectories));
                    }
                }

                if(tempSubFolders.Count == 0)

                string[] finalPath = tempSubFolders.ToArray();

            }
        }


        if (fileName.Length != 0)
        {

        }


        if (fileName.Length == 0)
        {
        }



    }



}
