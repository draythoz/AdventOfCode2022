var lines = File.ReadLines("input.txt");
var sacks = new List<Rucksack>();
var elfGroups = new List<ElfGroup>();
var currentGroup = new ElfGroup();
var groupCounter = 1;
foreach (var line in lines)
{
    switch (groupCounter)
    {
        case 1:
            currentGroup.Elf1Contents = line.ToArray();
            groupCounter++;
            break;
        case 2:
            currentGroup.Elf2Contents = line.ToArray();
            groupCounter++;
            break;
        case 3:
            currentGroup.Elf3Contents = line.ToArray();
            groupCounter = 1;
            elfGroups.Add(currentGroup);
            currentGroup = new ElfGroup();
            break;
    }

    var splitPoint = line.Length / 2;
    var compartmentOne = line.Take(splitPoint).ToArray();
    var compartmentTwo = line.Skip(splitPoint).Take(splitPoint).ToArray();
    sacks.Add(new Rucksack(compartmentOne, compartmentTwo));
}

var overlapItems = new List<char>();
foreach (var sack in sacks) overlapItems.Add(GetOverlapItem(sack));

var badgeItems = new List<char>();
foreach (var group in elfGroups) badgeItems.Add(GetBadgeItem(group));

var prioritySumOverlapItems = 0;
foreach (var item in overlapItems) prioritySumOverlapItems += GetItemPriority(item);

Console.WriteLine($"Priority Sum of all overlapped items is: {prioritySumOverlapItems}");

var prioritySumBadgeItems = 0;
foreach (var item in badgeItems) prioritySumBadgeItems += GetItemPriority(item);

Console.WriteLine($"Priority Sum of all badge items is: {prioritySumBadgeItems}");

char GetOverlapItem(Rucksack sack)
{
    foreach (var item in sack.CompartmentOne)
        if (sack.CompartmentTwo.Contains(item))
            return item;

    throw new ArgumentException();

}

char GetBadgeItem(ElfGroup elfGroup)
{
    foreach (var item in elfGroup.Elf1Contents)
        if (elfGroup.Elf2Contents.Contains(item) && elfGroup.Elf3Contents.Contains(item))
            return item;

    throw new ArgumentException();
}

int GetItemPriority(char item)
{
    if (item > 96) return item - 96;

    return item - 38;
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

public class ElfGroup
{
    public char[] Elf1Contents { get; set; }
    public char[] Elf2Contents { get; set; }
    public char[] Elf3Contents { get; set; }
}