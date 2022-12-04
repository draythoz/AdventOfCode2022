var lines = File.ReadLines("input.txt");
var elfCalories = new List<int>();

var calorieSum = 0;

foreach (var line in lines)
    if (line.Length > 0)
    {
        calorieSum += int.Parse(line);
    }
    else
    {
        elfCalories.Add(calorieSum);
        calorieSum = 0;
    }

var topThree = elfCalories.OrderDescending().Take(3);
Console.WriteLine($"Highest elf calorie count: {topThree.First()}");
Console.WriteLine($"2nd Highest elf calorie count: {topThree.Skip(1).First()}");
Console.WriteLine($"3rd Highest elf calorie count: {topThree.Last()}");
Console.WriteLine($"Total elf calorie count of top 3: {topThree.Sum()}");