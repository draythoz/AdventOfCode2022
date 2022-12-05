var lines = File.ReadLines("input.txt").ToArray();

var stacks = new List<Stack<char>>();

#region Stack Initialization

var postInitializationIndex = 0;
for (var i = 0; i <= lines.Count(); i++)
{
    if (lines[i].StartsWith("m")) //initialization lines start with the stack number 
    {
        postInitializationIndex = i;
        break;
    }

    var splitInitialization = lines[i].Split(' ');
    var stack = new Stack<char>();
    for (var j = 1; j < splitInitialization.Length; j++) stack.Push(char.Parse(splitInitialization[j]));
    stacks.Add(stack);
}

#endregion

//initialization complete, time to perform the tasks
for (var x = postInitializationIndex; x < lines.Length; x++)
{
    //Ex line: move 6 from 6 to 5
    var splitInstructions = lines[x].Split(' ');
    var numberStacksToMove = int.Parse(splitInstructions[1]);
    var sourceStack = int.Parse(splitInstructions[3]) - 1; //-1 to match up to the initialization array of Stacks
    var destinationStack = int.Parse(splitInstructions[5]) - 1; //same reason

    //The main difference from the previous code is that we now push from the source stack to a tempstack
    //Then we pop from the tempStack onto the destinationStack.
    var tempStack = new Stack<char>();
    for (var i = 0; i < numberStacksToMove; i++) tempStack.Push(stacks[sourceStack].Pop());
    while (tempStack.Count > 0) stacks[destinationStack].Push(tempStack.Pop());
}

foreach (var stack in stacks) Console.Write(stack.Peek());