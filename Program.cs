Console.WriteLine("Starting Process...");

SearchDamageExe(@"../");

Console.WriteLine("All process done, press enter to exit");
Console.ReadLine();

const long corruptedFileSize = 534016;


static void SearchDamageExe(string path)
{

    Console.WriteLine($"Searching in {path}");

    //Save the current directory info
    DirectoryInfo currentDirectory = new DirectoryInfo(path);

    //Perform the process for any sub-folder on the current directory
    foreach (var directory in currentDirectory.EnumerateDirectories())
    {
        SearchDamageExe(directory.FullName);
    }

    //Perform the process for the current folder

    //Search if are any file that has been corrupted by the Ground virus
    foreach (var file in currentDirectory.EnumerateFiles())
    {
        FileInfo virusFile;

        if (IsAffected(file, currentDirectory.EnumerateFiles(), out virusFile))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"I FOUND AN INFECTED FILE! ({virusFile})");

            DeleteAndRestore(file, virusFile);
        }
    }

    Console.WriteLine($"All done at {path}");
}

static void DeleteAndRestore(FileInfo originalFile, FileInfo virusFile)
{
    string originalName = virusFile.FullName;
    Console.ForegroundColor = ConsoleColor.Green;
    virusFile.Delete();
    Console.WriteLine($"Virus file deleted :)");

    originalFile.MoveTo(originalName);
    Console.WriteLine("Original file restored :)");
    Console.ForegroundColor = ConsoleColor.White;
}

static bool IsAffected(FileInfo originalFile, IEnumerable<FileInfo> files, out FileInfo virusFile)
{
    //Confirm is my original file is a .exe file and have a g at the begening of the name
    if (originalFile.Extension == ".exe" && originalFile.Name.Length > 1 && originalFile.Name[0] == 'g')
    {
        //Saving the original file name in to a variable
        string originalFileName = originalFile.Name.Substring(1);


        //Searching if are another .exe that have the same name that my original file, and the same length that the virus file
        //on all the directory
        foreach (var currentFile in files)
        {
            if (currentFile.Extension == ".exe" && originalFileName == currentFile.Name && currentFile.Length == corruptedFileSize)
            {
                virusFile = currentFile;
                return true;
            }

        }
    }
    virusFile = originalFile;

    return false;
}