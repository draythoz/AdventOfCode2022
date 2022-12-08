var lines = File.ReadLines("input.txt");
var root = new ElfDirectory("/", null);
var currentDirectory = root;
foreach (var line in lines)
{
    var lineType = line[0];
    var parts = line.Split(' ');
    switch (lineType)
    {
        case '$':
            HandleCommand(line, ref currentDirectory);
            break;
        case 'd':
            var directoryName = parts[1];
            currentDirectory.Directories.Add(new ElfDirectory(directoryName, currentDirectory));
            break;
        default:
            var fileSize = int.Parse(parts[0]);
            var fileName = parts[1];
            currentDirectory.Files.Add(new ElfFile(fileName, fileSize));
            break;
    }
}

var candidateDirectories = new List<ElfDirectory>();
var sum = GetCandidateDirectoriesSum(root, candidateDirectories, 100000);
Console.WriteLine(sum);

int GetCandidateDirectoriesSum(ElfDirectory currentDirectory, List<ElfDirectory> candidateDirectories,
    int sizeLimit = 100000)
{
    if (currentDirectory.Size <= sizeLimit) 
        candidateDirectories.Add(currentDirectory);

    foreach (var directory in currentDirectory.Directories)
    { 
        GetCandidateDirectoriesSum(directory, candidateDirectories);
    }

    return candidateDirectories.Select(c => c.Size).Sum();
}


void HandleCommand(string command, ref ElfDirectory currentDirectory)
{
    //Ex: $ cd /
    //    $ ls  <-- don't care
    var parts = command.Split(' ');
    if (parts[1] == "cd")
    {
        var commandArgument = parts[2];
        switch (commandArgument)
        {
            case "/": //move to root
                while (currentDirectory.ParentDirectory is not null)
                    currentDirectory = currentDirectory.ParentDirectory;
                break;
            case "..":  //move up a level
                currentDirectory = currentDirectory.ParentDirectory;
                break;
            default: //move to specific directory within current directory
                currentDirectory = currentDirectory.Directories.First(d => d.Name == commandArgument);
                break;
        }
    }
}

class ElfDirectory
{
    public ElfDirectory(string name, ElfDirectory parentDirectory)
    {
        Name = name;
        ParentDirectory = parentDirectory;
    }

    public string Name { get; set; }
    public ElfDirectory ParentDirectory { get; }
    public List<ElfDirectory> Directories { get; set; } = new();
    public List<ElfFile> Files { get; set; } = new();

    public int Size
    {
        get
        {
            var filesSize = Files.Select(f => f.Size).Sum();
            var directorySize = Directories.Select(d => d.Size).Sum();
            return filesSize + directorySize;
        }
    }
}

internal class ElfFile
{
    public ElfFile(string name, int size)
    {
        Size = size;
        Name = name;
    }
    
    public string Name { get; set; }
    public int Size { get; set; }
}