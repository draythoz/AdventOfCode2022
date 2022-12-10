var lines = File.ReadLines("testinput.txt");

var head = new Position(0, 0);
var tail = new Position(0, 0);
var visitedPositions = new HashSet<string>() {"0:0"};

foreach (var line in lines)
{
    var commands = line.Split(' ');
    var direction = commands[0];
    var numberOfSpaces = int.Parse(commands[1]);

    for (int i = 0; i < numberOfSpaces; i++)
    {
        MoveHead(ref head, direction);
        CatchUpTail(ref head, ref tail);
        visitedPositions.Add(tail.ToString());
    }
}

// foreach (var entry in visitedPositions)
// {
//     Console.WriteLine(entry);
// }

Console.WriteLine($"Number of visited positions : {visitedPositions.Count}");

void MoveHead(ref Position head, string direction)
{
    switch (direction)
    {
        case "U":
            head.MoveUp();
            break;
        case "D":
            head.MoveDown();
            break;
        case "L":
            head.MoveLeft();
            break;
        case "R":
            head.MoveRight();
            break;
        default:
            break;
    }
}

void CatchUpTail(ref Position head, ref Position tail)
{
    var xDist = head.X - tail.X; //4,1
    var yDist = head.Y - tail.Y; //2,0

    bool moveDiagonally = Math.Abs(xDist) + Math.Abs(yDist) >= 3;

    if (xDist > 1)
    {
        tail.MoveRight();
        if (moveDiagonally) tail.Y = head.Y;
    }

    if (xDist < -1)
    {
        tail.MoveLeft();
        if (moveDiagonally) tail.Y = head.Y;
    }

    if (yDist > 1)
    {
        tail.MoveUp();
        if (moveDiagonally) tail.X = head.X;
    }

    if (yDist < -1)
    {
        tail.MoveDown();
        if (moveDiagonally) tail.X = head.X;
    }
}

internal class Position
{
    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public void MoveUp() => Y++;
    public void MoveDown() => Y--;
    public void MoveRight() => X++;
    public void MoveLeft() => X--;

    public override string ToString()
    {
        return $"{X}:{Y}";
    }
}