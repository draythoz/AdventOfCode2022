var lines = File.ReadLines(@"C:/tmp/aoc/3/input.txt");
var sacks = new List<Rucksack>();
foreach (var line in lines)
{
    var splitPoint = line.Length / 2;
    var compartmentOne = line.Take(splitPoint).ToArray();
    var compartmentTwo = line.Skip(splitPoint).Take(splitPoint).ToArray();
    sacks.Add(new Rucksack(compartmentOne, compartmentTwo));
}

var overlapItems = new List<char>();
foreach (var sack in sacks) overlapItems.Add(GetOverlapItem(sack));

var prioritySum = 0;
foreach (var item in overlapItems)
{
    var priority = 0;
    if (item > 96)
    {
        priority = item - 96;
        prioritySum += priority;
    }
    else
    {
        priority = item - 38;
        prioritySum += priority;
    }
    //Console.WriteLine($"{item} : {priority}");
}

Console.WriteLine($"Priority Sum of all overlapped items is: {prioritySum}");

char GetOverlapItem(Rucksack sack)
{
    foreach (var item in sack.CompartmentOne)
        if (sack.CompartmentTwo.Contains(item))
            return item;

    return '!';
}

internal class Rucksack
{
    public Rucksack(char[] compartmentOne, char[] compartmentTwo)
    {
        CompartmentOne = compartmentOne;
        CompartmentTwo = compartmentTwo;
    }

    public char[] CompartmentOne { get; set; }
    public char[] CompartmentTwo { get; set; }
}