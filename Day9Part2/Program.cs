var lines = File.ReadLines("input.txt");
const int segments = 10;
const int finalTailIndex = segments - 2;
var head = new Position(0, 0);
var tails = new Position[segments - 1];
for (var i = 0; i <= finalTailIndex; i++) tails[i] = new Position(0, 0);

var visitedPositions = new HashSet<string> { "0:0" };

foreach (var line in lines)
{
    var commands = line.Split(' ');
    var direction = commands[0];
    var numberOfSpaces = int.Parse(commands[1]);

    for (var i = 0; i < numberOfSpaces; i++)
    {
        MoveHead(ref head, direction);
        CatchUpTail(ref head, ref tails[0]);
        for (var t = 1; t <= finalTailIndex; t++) CatchUpTail(ref tails[t - 1], ref tails[t]);

        var finalTailPosition = tails[finalTailIndex].ToString();
        visitedPositions.Add(finalTailPosition);
    }
}

// foreach (var entry in visitedPositions)
// {
//     Console.WriteLine(entry);
// }

Console.WriteLine($"Number of visited positions : {visitedPositions.Count}");
// foreach (var position in visitedPositions)
// {
//     Console.WriteLine(position);
// }

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
    }
}

void CatchUpTail(ref Position head, ref Position tail)
{
    var xDist = head.X - tail.X; //4,1
    var yDist = head.Y - tail.Y; //2,0

    var absoluteDistance = Math.Abs(xDist) + Math.Abs(yDist);
    var moveDiagonally = absoluteDistance >= 3;

    if (moveDiagonally)
    {
        MoveTailDiagonally(xDist, yDist, ref tail);
        return;
    }

    if (xDist > 1)
    {
        tail.MoveRight();
        return;
    }

    if (xDist < -1)
    {
        tail.MoveLeft();
        return;
    }

    if (yDist > 1)
    {
        tail.MoveUp();
        return;
    }

    if (yDist < -1)
    {
        tail.MoveDown();
    }
}

void MoveTailDiagonally(int xDist, int yDist, ref Position tail)
{
    if (xDist > 0) tail.MoveRight();
    else tail.MoveLeft();

    if (yDist > 0) tail.MoveUp();
    else tail.MoveDown();
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

    public void MoveUp()
    {
        Y++;
    }

    public void MoveDown()
    {
        Y--;
    }

    public void MoveRight()
    {
        X++;
    }

    public void MoveLeft()
    {
        X--;
    }

    public override string ToString()
    {
        return $"{X}:{Y}";
    }
}