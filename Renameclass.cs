using System;

public class RenameClass : Save
{
	public RenameClass() : base("RenameData")
	{
	}

	public void Init(string[] commands)
    {
        string finalFilePath = "";
        string whatToRename = "";
        string howToRename = "";
        string extension = "";
        string subFolders = "";


        bool commandChecker(string command, string[] commands, int index)
        {
            if (command.Contains("--"))
            {
                if (command == "--name" ^ command == "howrename")
                {
                    howToRename = commands[index + 1];
                }
                else if (command == "--subfolders")
                {
                    subFolders = commands[index + 1];
                }
                else if (command == "--whatrename")
                {
                    whatToRename = commands[index + 1];
                }
                else if (command == "--extension")
                    extension = commands[index + 1];
                else if (command.Contains("--"))
                {
                    Console.WriteLine($"\nCommand {command} is not recognized");
                    return false;
                }
            }


            if (commands.Length >= 2)
            {

                commands[1] = @"C:\Users\Nowe Jakubki\Pictures\docelowe\czy stworzy";
                finalFilePath = commands[1];

                return true;

            }
            else
            {
                return false;
            }

        }



        for (int i = 1; i < commands.Length; i++)
        {
            if (commandChecker(commands[i], commands, i) == false)
                return;
        }


        string result = RenameFiles (finalFilePath, whatToRename, extension, subFolders, howToRename);
        Console.WriteLine($"\n {result}");

    }


    public string RenameFiles(string finalFilePath = "", string whatToRename = "", string extension = "", string subFolders = "", string howToRename = "", params string[] fileName)
    {
        if (finalFilePath == "")
            return "Given path does not exist\n\n";

        if (howToRename == "")
            return "Name file was not given";
        

        //change for finall file path and change in other to work this option
        string[] files = Search_and_SubFolders(subFolders);

        WhatFileToRename(files, whatToRename, extension, howToRename);
        return "\nRenaming operation has done\n\n";

        
        void WhatFileToRename(string[] filesArr, string whatToRename, string extension, string fileName)
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
                    Console.WriteLine($"\nExtension '{extension}' was not found'");
                else
                    Console.WriteLine($"\n\nMoved files: {counter}");

            }

            if (extension != "")
            {
                Extension(extension);
                return;
            }

            if (whatToRename != "")
            {
                if (whatToRename == "file" ^ whatToRename == "files")
                {



                    for (int i = 0; i < filesArr.Length; i++)
                    {

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
                else if (whatToRename == "directory" ^ whatToRename == "dir" ^ whatToRename == "folder" ^ whatToRename == "folders")
                {
                    filesArr = Directory.GetDirectories(finalFilePath);

                    for (int i = 0; i < filesArr.Length; i++)
                    {

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


                    if (Directory.Exists($@"{finalFilePath}\{fileName}") ^ File.Exists($@"{finalFilePath}\{fileName}"))
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

            if (subFolders == "" ^ subFolders == "no")
                return Directory.GetFileSystemEntries(finalFilePath, "*", SearchOption.TopDirectoryOnly);
            else
                return Directory.GetFileSystemEntries(finalFilePath, "*", SearchOption.AllDirectories);

        }

    }
}

