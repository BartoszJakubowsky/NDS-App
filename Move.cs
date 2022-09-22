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

public class MoveFilesClass : Save
{
    public MoveFilesClass() :base("MoveData")
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

        bool commandChecker(string command, string[] commands, int index)
        {
            if (command.Contains("--"))
            {
                if (command == "--create")
                    create = command;
                else if (command == "--subfolders")
                    subFolders = command;
                else if (command == "--whatmove")
                {
                    whatToMove = commands[index + 1];
                }
                else if (command == "--extension")
                    extension = commands[index + 1];
                else if (command.Contains("--"))
                {
                    Console.WriteLine($"\nCommand {command} is not recognized");
                    return false;
                }
            }

            if (commands.Length >= 3)
            {
                commands[1] = @"C:\Users\Nowe Jakubki\Pictures\Próba";
                filePath = commands[1];
            }

            if (commands.Length >= 3)
            {

                commands[2] = @"C:\Users\Nowe Jakubki\Pictures\docelowe\czy stworzy";
                finalFilePath = commands[2];
            }
            return true;

        }

        

        for (int i = 1; i < commands.Length; i++)
        {
            if (commandChecker(commands[i], commands, i) == false)
                return;
        }


        string result = MoveFiles(filePath, finalFilePath, create, subFolders, whatToMove, extension);
        Console.WriteLine($"\n {result}");

    }


    public string MoveFiles(string filePath, string finalFilePath, string create = "", string subFolders = "", string whatToMove = "", string extension = "", params string[] fileName)
    {
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
            Create(create);
            return "Moving operation didn't end succesfully\n\n";
        }
        else
        {
            if (Create(create) == false)
            {
                return "Moving operation didn't end succesfully\n\n";
            }
        }



        string[] files = Search_and_SubFolders(subFolders);
        WhatFileToMove(files, whatToMove, extension);
        return "\nMoving operation has done\n\n";

        bool Create(string create)
        {
            if (Directory.Exists(finalFilePath) == false)
            {
                if (create != "" && finalFilePath != "")
                {
                    try
                    {
                        Directory.CreateDirectory(finalFilePath);
                        return true;

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\nCreating directory has failed");
                        throw;
                    }

                }
                else
                    Console.WriteLine("\ntarget path does not exist, you didn't wanted to create one or you didn't put final path");
                return false;
            }
            else
            {

                if (create == "")
                {
                    return true;
                }

                Console.WriteLine();
                Console.Write("\n\nDirectory " + finalFilePath + " already exist ->");
                int n = 0;
                do
                {
                    n++;

                } while (Directory.Exists(@$"{finalFilePath} ({n})"));

                Directory.CreateDirectory(@$"{finalFilePath} ({n})");
                finalFilePath = @$"{finalFilePath} ({n})";
                Console.Write($" we created folder \"{Path.GetFileName(finalFilePath)}\"\n");
                return true;

            }

        }
        void WhatFileToMove(string[] filesArr, string whatToMove, string extension)
        {


            void Extension(string extension)
            {
                int counter = 0;


                if (extension.Substring(0, 1) == ".")
                {
                    extension = extension.Substring(1, extension.Length - 1);
                    Console.WriteLine(extension);
                }

                for (int i = 0; i < filesArr.Length; i++)
                {
                    if (Path.GetExtension(filesArr[i]) == $".{extension}")
                    {
                        string fileName = Path.GetFileName(filesArr[i]);

                        if (File.Exists($@"{finalFilePath}\{fileName}"))
                        {
                            int j = 0;
                            do
                            {
                                j++;
                            } while (File.Exists($@"{finalFilePath}\{fileName} ({j})"));
                            File.Move(filesArr[i], $@"{finalFilePath}\{fileName} ({j})");
                        }
                        else
                        {
                            File.Move(filesArr[i], $@"{finalFilePath}\{fileName}");
                        }
                        counter++;
                    }
                }

                if (counter == 0)
                    Console.WriteLine($"\nExtension '{extension} was not found'");
                else
                    Console.WriteLine($"\n\nMoved files: {counter}");

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
                        {
                            if (File.Exists($@"{finalFilePath}\{fileName}"))
                            {
                                int j = 0;
                                do
                                {
                                    j++;
                                } while (File.Exists($@"{finalFilePath}\{fileName} ({j})"));
                                File.Move(filesArr[i], $@"{finalFilePath}\{fileName} ({j})");
                            }
                            else
                            {
                                File.Move(filesArr[i], @$"{finalFilePath}\{fileName}");
                            }
                        }

                    }
                }
                else if (whatToMove == "directory" ^ whatToMove == "dir" ^ whatToMove == "folder" ^ whatToMove == "folders")
                {
                    filesArr = Directory.GetDirectories(filePath);
                    for (int i = 0; i < filesArr.Length; i++)
                    {
                        string fileName = Path.GetFileName(filesArr[i]);

                        if (Directory.Exists(filesArr[i]))
                        {
                            if (Directory.Exists($@"{finalFilePath}\{fileName}"))
                            {
                                int j = 0;
                                do
                                {
                                    j++;
                                } while (Directory.Exists($@"{finalFilePath}\{fileName} ({j})"));
                                Directory.Move(filesArr[i], $@"{finalFilePath}\{fileName} ({j})");
                            }
                            else
                            {
                                Directory.Move(filesArr[i], @$"{finalFilePath}\{fileName}");
                            }

                        }
                    }
                }

            }
            else
            {
                for (int i = 0; i < filesArr.Length; i++)
                {
                    string fileName = Path.GetFileName(filesArr[i]);

                    if (Directory.Exists($@"{finalFilePath}\{fileName}")  ^ File.Exists($@"{finalFilePath}\{fileName}"))
                    {
                        int j = 0;
                        do
                        {
                            j++;
                        } while (Directory.Exists($@"{finalFilePath}\{fileName} ({j})") ^ File.Exists($@"{finalFilePath}\{fileName} ({j})"));
                        Directory.Move(filesArr[i], $@"{finalFilePath}\{fileName} ({j})");
                    }
                    else
                    {
                        Directory.Move(filesArr[i], @$"{finalFilePath}\{fileName}");
                    }
                }

            }
        }

        string[] Search_and_SubFolders(string subFolders)
        {

            List<string[]> tempSubFolders = new List<string[]>();

            //if (subFolders == "")
            //{
            return Directory.GetFileSystemEntries(filePath, "*", SearchOption.TopDirectoryOnly);
            //}
            // else
            // {
            //     return Directory.GetFileSystemEntries(filePath, "*", SearchOption.AllDirectories);
            // }
            //
            // commented due to lack of need to move subfolders (they move with parent... )
            // 
        }

    }


}
