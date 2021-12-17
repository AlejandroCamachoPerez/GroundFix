// See https://aka.ms/new-console-template for more information

const long corruptedFileSize= 522;


static void SearchDamageExe()
{
    DirectoryInfo currentDirectory = new DirectoryInfo("");
    foreach (var file in currentDirectory.EnumerateFiles())
    {
        if (file.Extension == ".exe" && file.Length == corruptedFileSize && file.Name[0] == 'g')
        {
            
        }
    }
}



