var lines = File.ReadLines("input.txt");

var numberOfRows = lines.Count();
var numberOfColumns = lines.First().Length;
var forest = new int[numberOfRows, numberOfColumns];

for (var row = 0; row < numberOfRows; row++)
{
    var line = lines.ElementAt(row);
    for (var col = 0; col < numberOfColumns; col++)
    {
        forest[row, col] = line[col] - '0'; // - '0' is kind of a hack to get the non ascii int
        Console.Write(line[col]);
    }

    Console.WriteLine();
}

var numberOfVisibleTrees = numberOfColumns * 2 + (numberOfRows - 2) * 2;
var highestScenicScore = 0;
var rowOfHighest = 0;
var columnOfHighest = 0;
for (var i = 1; i < numberOfRows - 1; i++)
for (var j = 1; j < numberOfColumns - 1; j++)
{
    var treeHeight = forest[i, j];
    if (IsVisible(treeHeight, i, j, numberOfRows, numberOfColumns, forest)) numberOfVisibleTrees++;

    var scenicScore = GetScenicScore(treeHeight, i, j, numberOfRows, numberOfColumns, forest);
    if (highestScenicScore < scenicScore)
    {
        highestScenicScore = scenicScore;
        rowOfHighest = i;
        columnOfHighest = j;
    }
}


Console.WriteLine($"Number of visible trees: {numberOfVisibleTrees}");
Console.WriteLine($"Highest Scenic Score: {highestScenicScore} at {rowOfHighest}:{columnOfHighest}");

bool IsVisible(int treeHeight, int row, int column, int numberOfRows, int numberOfColumns, int[,] forest)
{
    var isVisibleTop = true;
    for (var i = 0; i < row; i++)
        if (forest[i, column] >= treeHeight)
            isVisibleTop = false;

    var isVisibleBottom = true;
    for (var i = row + 1; i < numberOfRows; i++)
        if (forest[i, column] >= treeHeight)
            isVisibleBottom = false;

    var isVisibleLeft = true;
    for (var j = 0; j < column; j++)
        if (forest[row, j] >= treeHeight)
            isVisibleLeft = false;

    var isVisibleByRowRight = true;
    for (var j = column + 1; j < numberOfColumns; j++)
        if (forest[row, j] >= treeHeight)
            isVisibleByRowRight = false;

    // Console.WriteLine($"Row/Column ={row}/{column}");
    // Console.WriteLine($"{nameof(isVisibleTop)}:{isVisibleTop}");
    // Console.WriteLine($"{nameof(isVisibleBottom)}:{isVisibleBottom}");
    // Console.WriteLine($"{nameof(isVisibleLeft)}:{isVisibleLeft}");
    // Console.WriteLine($"{nameof(isVisibleByRowRight)}:{isVisibleByRowRight}");

    return isVisibleTop || isVisibleBottom || isVisibleLeft || isVisibleByRowRight;
}

int GetScenicScore(int treeHeight, int row, int column, int numberOfRows, int numberOfColumns, int[,] forest)
{
    var treesVisibleAbove = 0;
    for (var i = row - 1; i >= 0; i--)
    {
        treesVisibleAbove++;
        if (forest[i, column] >= treeHeight) break;
    }

    treesVisibleAbove = Math.Max(1, treesVisibleAbove);

    var treesVisibleBelow = 0;
    for (var i = row + 1; i < numberOfRows; i++)
    {
        treesVisibleBelow++;
        if (forest[i, column] >= treeHeight) break;
    }

    treesVisibleBelow = Math.Max(1, treesVisibleBelow);


    var treesVisibleLeft = 0;
    for (var j = column - 1; j >= 0; j--)
    {
        treesVisibleLeft++;
        if (forest[row, j] >= treeHeight) break;
    }

    treesVisibleLeft = Math.Max(1, treesVisibleLeft);

    var treesVisibleRight = 0;
    for (var j = column + 1; j < numberOfColumns; j++)
    {
        treesVisibleRight++;
        if (forest[row, j] >= treeHeight) break;
    }

    var scenicScore = treesVisibleAbove * treesVisibleBelow * treesVisibleLeft * treesVisibleRight;

    if (row == 30 && column == 44) //the winner
    {
        Console.WriteLine($"Row/Column = {row}/{column}");
        Console.WriteLine($"Tree Height = {treeHeight}");
        Console.WriteLine($"{nameof(scenicScore)}:{scenicScore}");
        Console.WriteLine($"{nameof(treesVisibleAbove)}:{treesVisibleAbove}");
        Console.WriteLine($"{nameof(treesVisibleBelow)}:{treesVisibleBelow}");
        Console.WriteLine($"{nameof(treesVisibleLeft)}:{treesVisibleLeft}");
        Console.WriteLine($"{nameof(treesVisibleRight)}:{treesVisibleRight}");
    }


    return scenicScore;
}