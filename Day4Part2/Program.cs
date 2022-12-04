var lines = File.ReadLines("input.txt");

//In how many assignment pairs does one range overlap the other?
//Example line: 2-4,6-8

var hasAnyOverlapCount = 0;
foreach (var line in lines)
{
    var elfSplitLine = line.Split(',');
    var elf1Split = elfSplitLine[0].Split('-');
    var elf1Min = Convert.ToInt32(elf1Split[0]);
    var elf1Max = Convert.ToInt32(elf1Split[1]);
    
    var elf2Split = elfSplitLine[1].Split('-');
    var elf2Min = Convert.ToInt32(elf2Split[0]);
    var elf2Max = Convert.ToInt32(elf2Split[1]);

    if (elf1Max >= elf2Min && elf2Max >= elf1Max) hasAnyOverlapCount++;
    else if (elf1Min <= elf2Max && elf2Min <= elf1Min) hasAnyOverlapCount++;
    else if (elf2Max >= elf1Min && elf1Max >= elf2Max) hasAnyOverlapCount++;
    else if (elf2Min <= elf1Max && elf1Min <= elf2Min) hasAnyOverlapCount++;
}

Console.WriteLine($"Number of pairs that have any overlap: {hasAnyOverlapCount}");