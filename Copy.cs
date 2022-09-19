using System;

public class CopyFileClass
{
    public CopyFileClass()
    {
    }


    public void Init(string[] commands)
    {
        string filePath = "";
        string finalFilePath = "";
        string create = "";
        string subFolders = "";
        string whatToCopy = "";
        string extension = "";

        bool commandChecker(string command, string[] commands, int index)
        {
            if (command.Contains("--"))
            {
                if (command == "--create")
                    create = command;
                else if (command == "--subfolders")
                    subFolders = command;
                else if (command == "--whatcopy")
                {
                    whatToCopy = commands[index + 1];
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


        string result = CopyFiles(filePath, finalFilePath, create, subFolders, whatToCopy, extension);
        Console.WriteLine($"\n {result}");

    }


    public string CopyFiles(string filePath, string finalFilePath, string create = "", string subFolders = "", string whatToCopy = "", string extension = "", params string[] fileName)
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
            return "Copying operation didn't end succesfully\n\n";
        }
        else
        {
            if (Create(create) == false)
            {
                return "Copying operation didn't end succesfully\n\n";
            }
        }



        string[] files = Search_and_SubFolders(subFolders);
        WhatFileToCopy(files, whatToCopy, extension);
        return "\nCopying operation has done\n\n";

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
        void WhatFileToCopy(string[] filesArr, string whatToCopy, string extension)
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
                            File.Copy(filesArr[i], $@"{finalFilePath}\{fileName} ({j})");
                        }
                        else
                        {
                            File.Copy(filesArr[i], $@"{finalFilePath}\{fileName}");
                        }
                        counter++;
                    }
                }

                if (counter == 0)
                    Console.WriteLine($"\nExtension '{extension} was not found'");
                else
                    Console.WriteLine($"\nCopied files: {counter}");

            }

            if (extension != "")
            {
                Extension(extension);
                return;
            }

            if (whatToCopy != "")
            {
                if (whatToCopy == "file" ^ whatToCopy == "files")
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
                                File.Copy(filesArr[i], $@"{finalFilePath}\{fileName} ({j})");
                            }
                            else
                            {
                                File.Copy(filesArr[i], @$"{finalFilePath}\{fileName}");
                            }
                        }

                    }
                }
                else if (whatToCopy == "directory" ^ whatToCopy == "dir" ^ whatToCopy == "folder" ^ whatToCopy == "folders")
                {
                    filesArr = Directory.GetDirectories(filePath);
                    for (int i = 0; i < filesArr.Length; i++)
                    {
                        string fileName = Path.GetFileName(filesArr[i]);

                        if (Directory.Exists(filesArr[i]))
                        {
                            if (Directory.Exists($@"{finalFilePath}\{fileName}") && Path.GetExtension(filesArr[i]) == "")
                            {
                                int j = 0;
                                do
                                {
                                    j++;
                                } while (Directory.Exists($@"{finalFilePath}\{fileName} ({j})"));
                                Directory.CreateDirectory($@"{finalFilePath}\{fileName} ({j})");
                            }
                            else
                            {
                                Directory.CreateDirectory(@$"{finalFilePath}\{fileName}");
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

                    if (Directory.Exists($@"{finalFilePath}\{fileName}") ^ File.Exists($@"{finalFilePath}\{fileName}"))
                    {
                        int j = 0;
                        do
                        {
                            j++;
                        } while (Directory.Exists($@"{finalFilePath}\{fileName} ({j})") ^ File.Exists($@"{finalFilePath}\{fileName} ({j})"));

                        if (Path.GetExtension($@"{finalFilePath}\{fileName} ({j})") == "")
                            Directory.CreateDirectory($@"{finalFilePath}\{fileName} ({j})");
                        else
                            File.Copy(filesArr[i], $@"{finalFilePath}\{fileName} ({j})");
                    }
                    else
                    {
                        if (Path.GetExtension(filesArr[i]) == "")
                            Directory.CreateDirectory(@$"{finalFilePath}\{fileName}");

                        else
                            File.Copy(filesArr[i], @$"{finalFilePath}\{fileName}");
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


