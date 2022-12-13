using System.Text;

var lines = File.ReadLines("input.txt");
var cycles = new List<Cycle> { new(0, 1, 0) };
var currentX = 1;
var currentCycle = 1;
foreach (var line in lines)
    if (line == "noop")
    {
        cycles.Add(new Cycle(currentCycle++, currentX, currentX));
    }
    else
    {
        var addToX = int.Parse(line.Split(" ")[1]);
        cycles.Add(new Cycle(currentCycle++, currentX, currentX));
        cycles.Add(new Cycle(currentCycle++, currentX, currentX += addToX));
    }

var sb = new StringBuilder();
var drawPosition = 0;
var drawWindow = UpdateDrawWindow(1);

foreach (var cycle in cycles.Skip(1))
{
    if (cycle.CycleNumber % 40 == 1)
    {
        sb.AppendLine();
        drawPosition = 0;
    }

    if (drawWindow.Contains(drawPosition)) sb.Append("#");
    else sb.Append(".");

    drawPosition++;
    drawWindow = UpdateDrawWindow(cycle.EndX);
}

Console.WriteLine(sb.ToString());

int[] UpdateDrawWindow(int xPosition)
{
    return new int[3] { xPosition - 1, xPosition, xPosition + 1 };
}


internal class Cycle
{
    public Cycle(int cycleNumber, int startX, int endX)
    {
        CycleNumber = cycleNumber;
        StartX = startX;
        EndX = endX;
    }

    public int CycleNumber { get; set; }
    public int StartX { get; set; }
    public int EndX { get; set; }
}