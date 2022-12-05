var lines = File.ReadLines("input.txt").ToArray();

var stacks = new List<Stack<char>>();

var postInitializationIndex = 0;
for (int i = 0; i <= lines.Count(); i++)
{
    if (lines[i].StartsWith("m"))  //initialization lines start with the stack number 
    {
        postInitializationIndex = i;
        break;
    }

    var splitInitialization = lines[i].Split(' ');
    var stackIndex = int.Parse(splitInitialization[0]);
    var stack = new Stack<char>();
    for (int j = 1; j < splitInitialization.Length; j++)
    {
        stack.Push(char.Parse(splitInitialization[j]));
    }
    stacks.Add(stack);
}

//initialization complete, time to perform the tasks
for (int x = postInitializationIndex; x < lines.Length; x++)
{
    var splitInstructions = lines[x].Split(' ');
    var numberToMove = int.Parse(splitInstructions[1]);
    var stackToMoveFrom = int.Parse(splitInstructions[3]) - 1;  //-1 to match up to the initialization array of Stacks
    var stackToMoveTo = int.Parse(splitInstructions[5]) - 1;  //same reason

    for (int i = 0; i < numberToMove; i++)
    {
        var item = stacks[stackToMoveFrom].Pop();
        stacks[stackToMoveTo].Push(item);
    }
}

foreach (var stack in stacks)
{
    Console.Write(stack.Peek());
}