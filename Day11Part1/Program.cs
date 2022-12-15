using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

var monkeys = ParseMonkeys("input.txt");
PerformMonkeyBusiness(monkeys, 20);

var top2 = monkeys.OrderByDescending(m => m.ItemsTouched - m.CurrentItems.Count).Take(2);

foreach (var monkey in top2) Console.WriteLine(monkey.ToString());
var topBusinessMonkey = top2.First();
var secondBusinessMonkey = top2.Last();
var totalMonkeyBusiness = (topBusinessMonkey.ItemsTouched - topBusinessMonkey.CurrentItems.Count) *
                          (secondBusinessMonkey.ItemsTouched - secondBusinessMonkey.CurrentItems.Count);
Console.WriteLine($"Total Monkey Business: {totalMonkeyBusiness}");


void PerformMonkeyBusiness(List<Monkey> monkeys, int rounds)
{
    for (var round = 1; round <= 20; round++)
    for (var i = 0; i < monkeys.Count; i++)
    {
        var monkey = monkeys[i];
        for (var index = 0; index < monkey.CurrentItems.Count; index++)
        {
            var item = monkey.CurrentItems[index];
            var newWorryValue = 0;
            if (monkey.WorryOperation == '^')
            {
                newWorryValue = item * item;
            }
            else
            {
                var dt = new DataTable();
                newWorryValue =
                    int.Parse(dt.Compute($"{item} {monkey.WorryOperation} {monkey.WorryOperationModifier}", "")
                        .ToString()!);
            }

            item = newWorryValue / 3;

            if (item % monkey.DivisibleByModifier == 0)
            {
                monkeys[monkey.DivisibleBySuccessRecipient].CurrentItems.Add(item);
                monkeys[monkey.DivisibleBySuccessRecipient].ItemsTouched++;
            }
            else
            {
                monkeys[monkey.DivisibleByFailureRecipient].CurrentItems.Add(item);
                monkeys[monkey.DivisibleByFailureRecipient].ItemsTouched++;
            }
        }

        monkey.CurrentItems.Clear();
    }
}


List<Monkey> ParseMonkeys(string fileName)
{
    var monkeys = new List<Monkey>();
    using (var reader = new StreamReader(File.OpenRead(fileName)))
    {
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if (line.StartsWith("Monkey"))
            {
                var monkeyId = 0;
                var monkeyItems = new List<int>();
                var monkeyOperation = '+';
                var monkeyOperationValue = 1;
                var monkeyDivisibleByModifier = 1;
                var monkeyDivisibleBySuccessRecipient = 0;
                var monkeyDivisibleByFailureRecipient = 0;


                //Line 1
                monkeyId = int.Parse(line[^2].ToString());

                //Line 2
                line = reader.ReadLine();

                var items = line.Split(":")[1].Split(",");
                foreach (var item in items) monkeyItems.Add(int.Parse(item));

                //Line 3
                line = reader.ReadLine();
                var operationSection = line.Split("=")[1];
                if (operationSection.Contains("old * old"))
                {
                    monkeyOperation = '^';
                    monkeyOperationValue = 2;
                }
                else
                {
                    monkeyOperation = operationSection.Contains("+") ? '+' : '*';
                    var lastValue = line.Substring(line.LastIndexOf(" ") + 1);
                    monkeyOperationValue = int.Parse(lastValue);
                }

                //Line 4
                line = reader.ReadLine();
                var divisibleValue = line.Substring(line.LastIndexOf(" ") + 1);
                monkeyDivisibleByModifier = int.Parse(divisibleValue);

                //Line 5
                line = reader.ReadLine();
                var divisibleSuccessMonkey = line.Substring(line.LastIndexOf(" ") + 1);
                monkeyDivisibleBySuccessRecipient = int.Parse(divisibleSuccessMonkey);

                //Line 6
                line = reader.ReadLine();
                var divisibleFailureMonkey = line.Substring(line.LastIndexOf(" ") + 1);
                monkeyDivisibleByFailureRecipient = int.Parse(divisibleFailureMonkey);

                var monkey = new Monkey
                {
                    Id = monkeyId,
                    CurrentItems = monkeyItems,
                    WorryOperation = monkeyOperation,
                    WorryOperationModifier = monkeyOperationValue,
                    DivisibleByModifier = monkeyDivisibleByModifier,
                    DivisibleBySuccessRecipient = monkeyDivisibleBySuccessRecipient,
                    DivisibleByFailureRecipient = monkeyDivisibleByFailureRecipient,
                    ItemsTouched = monkeyItems.Count
                };
                monkeys.Add(monkey);

                Console.WriteLine(monkey.ToString());
            }

            reader.ReadLine();
        }
    }

    return monkeys;
}

public class Monkey
{
    [Key] public int Id { get; set; }

    public List<int> CurrentItems { get; set; }
    public int ItemsTouched { get; set; }
    public char WorryOperation { get; set; }
    public int WorryOperationModifier { get; set; }
    public int DivisibleByModifier { get; set; }
    public int DivisibleBySuccessRecipient { get; set; }
    public int DivisibleByFailureRecipient { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Monkey Id: {Id}");
        sb.AppendLine($"Current Items: {string.Join(",", CurrentItems)}");
        sb.AppendLine($"Worry Operation: Old {WorryOperation} {WorryOperationModifier}");
        sb.AppendLine($"Divisible By Modifier {DivisibleByModifier}");
        sb.AppendLine($"If divisible by {DivisibleByModifier} then throw to monkey {DivisibleBySuccessRecipient}");
        sb.AppendLine($"If not divisible by {DivisibleByModifier} then throw to monkey {DivisibleByFailureRecipient}");
        sb.AppendLine($"Total Items Touched: {ItemsTouched - CurrentItems.Count}");
        return sb.ToString();
    }
}