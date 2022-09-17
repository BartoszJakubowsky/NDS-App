using System;

//co możesz podać? 

//Move - standard comment (Move file path_from path_for)
//
//INSTRUKCJA
// najpierw dodaję ścieżkę pierwotną, potem finalną ścieżkę i to musi być
// + w wariacjach mogę podać co ma być przeniesione, domyślnie będzie wszystko 
// 
//-wariacje -> all, extension, folders, only empty folders, 
//--dodatkowe opcje -> rename, save, 

public class MoveFilesClass
{
    public MoveFilesClass()
    {
    }

    public void Init(string[] commands)
    {
        string filePath = "";
        string finalFilePath = "";
        string create = "";
        string subFolders = "";
        string whatToMove = "";
        string extension = "";

        void commandChecker(string command, string[] commands, int index)
        {
            if (command.Contains("--"))
            {
                if (command == "--create")
                    create = command;
                else if (command == "--subfolders")
                    subFolders = command;
                else if (command == whatToMove)
                {
                    whatToMove = commands[index + 1];
                }
                else if (command == "--extension")
                    extension = commands[index + 1];
            }

            if (commands.Length >= 3)
            {
                commands[1] = @"C:\Users\Nowe Jakubki\Pictures\Próba";
                filePath = commands[1];
            }

            if (commands.Length >= 3)
            {
                commands[2] = @"C:\Users\Nowe Jakubki\Pictures\docelowe";
                finalFilePath = commands[2];
            }

        }

        for (int i = 1; i < commands.Length; i++)
        {
            commandChecker(commands[i], commands, i);
        }


        string result = MoveFiles(filePath, finalFilePath, create, subFolders, whatToMove, extension);
        Console.WriteLine($"\n {result}"); 

    }

    public string MoveFiles(string filePath, string finalFilePath, string create = "", string subFolders = "", string whatToMove = "", string extension = "", params string[] fileName)
    {
        //filename -> ilość plików
        //subfolders
        //extension -> declares if it's file or not 
        //what to move - file or directory



        //na start sprawdzić
        // - czy istnieją obie ścieżki + czy istnieje create
        // - czy źródłowa ścieżka ma pliki

        //
        //source path checker
        if (Directory.Exists(filePath) == false)
            return "home path or file does not exist";

        if (Directory.GetFileSystemEntries(filePath, "*", SearchOption.TopDirectoryOnly).Length == 0)
            return "home path is empty";
        //
        //final file path checker / creator
        if (finalFilePath == "")
        {
            if(Create(create) == false)
            {
                return "Final path does not exist and --create option was not writtend";
            }
        }
        else if (finalFilePath == "")
        {
            return "You didn't write final path ";
        }
        
        string[] files = Search_and_SubFolders(subFolders);
        WhatFileToMove(files, whatToMove, extension);
        return "Files moved";

        bool Create(string create)
        {
            if (Directory.Exists(finalFilePath) == false)
            {
                if (create != "")
                {
                    Directory.CreateDirectory(finalFilePath);
                    return true;

                }
                else
                    Console.WriteLine("target path does not exist and you didn't wanted to create one");
                return false;
            }
            else
            {
                Console.WriteLine("Directory " + finalFilePath + " already exist");
                int n = 0;
                do
                {
                    n++;

                } while (Directory.Exists(@$"{finalFilePath}\({n})") == false);

                Directory.CreateDirectory(@$"{finalFilePath}\({n})");
                return true;

            }

        }
        string[] Search_and_SubFolders(string subFolders)
        {

            List<string[]> tempSubFolders = new List<string[]>();

            if (subFolders == "")
            {
                return Directory.GetFileSystemEntries(filePath, "*", SearchOption.TopDirectoryOnly);
            }
            else
            {
                return Directory.GetFileSystemEntries(filePath, "*", SearchOption.AllDirectories);
            }

        }
        void WhatFileToMove(string[] filesArr, string whatToMove, string extension)
        {


            void Extension(string extension)
            {
                int counter = 0;

                if (extension == "pdf")
                {
                    for (int i = 0; i < filesArr.Length; i++)
                    {
                        if (Path.GetExtension(filesArr[i]) == ".pdf")
                        {
                            string fileName = Path.GetFileName(filesArr[i]);
                            File.Move(filesArr[i], @$"{finalFilePath}\{fileName}");
                            counter++;
                        }
                    }
                    if (counter == 0)
                        Console.WriteLine("PDF files not found");
                    else
                        Console.WriteLine($"{counter} files were moved");

                }
                else if (extension == "jpg")
                {
                    for (int i = 0; i < filesArr.Length; i++)
                    {
                        if (Path.GetExtension(filesArr[i]) == ".jpg")
                        {
                            string fileName = Path.GetFileName(filesArr[i]);
                            File.Move(filesArr[i], @$"{finalFilePath}\{fileName}");
                            counter++;
                        }
                    }
                    if (counter == 0)
                        Console.WriteLine("JPG files not found");
                    else
                        Console.WriteLine($"{counter} files were moved");
                }
                else if (extension == "png")
                {
                    for (int i = 0; i < filesArr.Length; i++)
                    {
                        if (Path.GetExtension(filesArr[i]) == ".png")
                        {
                            string fileName = Path.GetFileName(filesArr[i]);
                            File.Move(filesArr[i], @$"{finalFilePath}\{fileName}");
                            counter++;
                        }
                    }
                    if (counter == 0)
                        Console.WriteLine("PNG files not found");
                    else
                        Console.WriteLine($"{counter} files were moved");
                }
                else if (extension == "xlsx" ^ extension == "excel")
                {
                    for (int i = 0; i < filesArr.Length; i++)
                    {
                        if (Path.GetExtension(filesArr[i]) == ".xlsx")
                        {
                            string fileName = Path.GetFileName(filesArr[i]);
                            File.Move(filesArr[i], @$"{finalFilePath}\{fileName}");
                            counter++;
                        }
                    }
                    if (counter == 0)
                        Console.WriteLine("Excel files not found");
                    else
                        Console.WriteLine($"{counter} files were moved");
                }
                else if (extension == "docx" ^ extension == "word")
                {
                    for (int i = 0; i < filesArr.Length; i++)
                    {
                        if (Path.GetExtension(filesArr[i]) == ".docx")
                        {
                            string fileName = Path.GetFileName(filesArr[i]);
                            File.Move(filesArr[i], @$"{finalFilePath}\{fileName}");
                            counter++;
                        }
                    }
                    if (counter == 0)
                        Console.WriteLine("Word files not found");
                    else
                        Console.WriteLine($"{counter} files were moved");
                }
            }

            if (extension != "")
            {
                Extension(extension);
                return;
            }

            if (whatToMove != "")
            {
                if (whatToMove == "file" ^ whatToMove == "files")
                {



                    for (int i = 0; i < filesArr.Length; i++)
                    {
                        string fileName = Path.GetFileName(filesArr[i]);

                        if (File.Exists(filesArr[i]))
                            File.Move(filesArr[i], @$"{finalFilePath}\{fileName}");
                    }
                }
                else if (whatToMove == "directory" ^ whatToMove == "dir" ^ whatToMove == "folder" ^ whatToMove == "folders")
                {
                    for (int i = 0; i < filesArr.Length; i++)
                    {

                        string fileName = Path.GetFileName(filesArr[i]);

                        if (Directory.Exists(filesArr[i]))
                            Directory.Move(filesArr[i], @$"{finalFilePath}\{fileName}");

                    }
                }
            }
            else
            {
                for (int i = 0; i < filesArr.Length; i++)
                {
                    string fileName = Path.GetFileName(filesArr[i]);

                    Directory.Move(filesArr[i], @$"{finalFilePath}\{fileName}");
                }
            }
        }

        //tu utworzyć osobne dla files -> wiadmom czego szukać to nie będzie potrzeba automatyzacji












    }



}
