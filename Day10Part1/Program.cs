var lines = File.ReadLines("input.txt");
int[] interestingCycleNumbers= new int[6] { 20, 60, 100, 140, 180, 220 };
var cycles = new List<Cycle> { new Cycle(0, 1, 0) };
var currentX = 1;
var currentCycle = 1;
foreach (var line in lines)
{
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
}

var interestingCycles = new List<Cycle>();
interestingCycles.AddRange(cycles.Where(c => interestingCycleNumbers.Contains(c.CycleNumber)));
foreach (var pointOfInterest in interestingCycles)
{
    Console.WriteLine(
        $"Cycle Number: {pointOfInterest.CycleNumber} with X startValue: {pointOfInterest.StartX} and X endValue: {pointOfInterest.EndX}");
    Console.WriteLine($"Signal Strength: {pointOfInterest.CycleNumber * pointOfInterest.StartX}");
}

var totalSignalStrength = interestingCycles.Select(c => c.CycleNumber * c.StartX).Sum();
Console.WriteLine($"Total Signal Strength: {totalSignalStrength}");

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