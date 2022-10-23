using System;
using System.IO;
using System.Text.RegularExpressions;


interface IPathSearcher
{
    public static string[] SearchForPath(string sourcePathNames, string finalPathName)
    {
        string[] paths = { sourcePathNames, finalPathName };
        return ActualSearchPathMethod(paths);
    }

    //for rename stuff
    public static string[] SearchForPath(string sourcePathNames)
    {
        string[] paths = { sourcePathNames };
        return ActualSearchPathMethod(paths);
    }

    //if there was no path input
    public static void SearchForPath()
    {
        ActualSearchPathMethod();
    }

    private static string[] ActualSearchPathMethod(params string[] pathNames)
    {
        if (pathNames.Length == 0)
        {
            string[] emptyArr = { "" };
            return emptyArr;
        }

        DriveInfo[] drivesArr = DriveInfo.GetDrives();
        string userName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        Regex mainRegex;
        Regex subRegex_1;
        Regex subRegex_2;
        Regex subRegex_3;
        Regex subRegex_4;



        for (int i = 0; i < pathNames.Length; i++)
        {
            List<string> tempListMainPaths = new List<string>();
            List<string> tempListCombinedPaths = new List<string>();
            string path;



            for (int j = 0; j < drivesArr.Length; j++)
            {
                //if it's main disk
                if (drivesArr[j].Name == Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)))
                    path = $@"{drivesArr[j].Name}\Users\{userName}";
                else
                    path = drivesArr[j].Name;


                string[] allPathInDisk = Directory.GetDirectories(@$"{path}", "*", SearchOption.AllDirectories);


                mainRegex = new Regex(@$"\\\\{pathNames[i]}", RegexOptions.IgnoreCase);
                subRegex_1 = new Regex(@$"C:.*[A - Za - z0 - 9] +\\\\{pathNames[i]}", RegexOptions.IgnoreCase);
                //\someText{pathNames[i]someText}
                subRegex_2 = new Regex(@$"\\\\[a-zA-Z]+{pathNames[i]}[a-zA-Z]+", RegexOptions.IgnoreCase);
                //\{pathNames[i]someText}
                subRegex_3 = new Regex(@$"\\\\{pathNames[i]}[a-zA-Z]+", RegexOptions.IgnoreCase);
                //\{pathNames[i]someText}
                subRegex_4 = new Regex(@$"\\\\[a-zA-Z]+{pathNames[i]}", RegexOptions.IgnoreCase);

                //first search for direct folder name
                //then search for sometching that has it's name
                //if something has it's name -> search for others to compare 


                for (int k = 0; k < allPathInDisk.Length; k++)
                {
                    if (mainRegex.IsMatch(allPathInDisk[k]))
                        tempListMainPaths.Add(allPathInDisk[k]);
                    else
                    {
                        if (subRegex_1.IsMatch(allPathInDisk[k]) || subRegex_2.IsMatch(allPathInDisk[k]) || subRegex_3.IsMatch(allPathInDisk[k]) || subRegex_4.IsMatch(allPathInDisk[k]))
                            tempListCombinedPaths.Add(allPathInDisk[k]);
                        else
                            continue;
                    }
                }
            }

            if(tempListMainPaths.Count > 0)
            {


                //logic which main path to chose -> shortest or the longest
            }
            else if(tempListMainPaths.Count == 0)
            {
                pathNames[i] = tempListMainPaths.Last();
            } 
            else
            {
                if (tempListCombinedPaths.Count > 0)
                {

                }
                else
                    pathNames[i] = "";
            }





        }

    }

}

