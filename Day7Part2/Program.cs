var lines = File.ReadLines("input.txt");
var sizeOfSystem = 70000000;
var sizeNeededForProgram = 30000000;
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
var sum = GetCandidateDirectoriesSum(root, candidateDirectories);
Console.WriteLine($"The sum of all directories with size less than 100000 is: {sum}");

candidateDirectories = new List<ElfDirectory>();
var currentFreeSpace = sizeOfSystem - root.Size;
var spaceNeededToFreeUp = sizeNeededForProgram - currentFreeSpace;
var directoryToDelete = GetDirectoryToDelete(root, candidateDirectories, spaceNeededToFreeUp);
Console.WriteLine($"Space needed to free up for the program: {spaceNeededToFreeUp}");
Console.WriteLine(
    $"Directory {directoryToDelete.Name} is the smallest directory " +
    $"that can be deleted with a size of {directoryToDelete.Size}");

ElfDirectory GetDirectoryToDelete(ElfDirectory currentDirectory, List<ElfDirectory> candidateDirectories,
    int spaceNeeded)
{
    if (currentDirectory.Size >= spaceNeeded)
        candidateDirectories.Add(currentDirectory);

    foreach (var directory in currentDirectory.Directories)
        GetDirectoryToDelete(directory, candidateDirectories, spaceNeeded);

    return candidateDirectories.OrderBy(c => c.Size).First();
}

int GetCandidateDirectoriesSum(ElfDirectory currentDirectory, List<ElfDirectory> candidateDirectories,
    int sizeLimit = 100000)
{
    if (currentDirectory.Size <= sizeLimit)
        candidateDirectories.Add(currentDirectory);

    foreach (var directory in currentDirectory.Directories) GetCandidateDirectoriesSum(directory, candidateDirectories);

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
            case "..": //move up a level
                currentDirectory = currentDirectory.ParentDirectory;
                break;
            default: //move to specific directory within current directory
                currentDirectory = currentDirectory.Directories.First(d => d.Name == commandArgument);
                break;
        }
    }
}

internal class ElfDirectory
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