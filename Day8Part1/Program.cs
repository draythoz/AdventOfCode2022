var lines = File.ReadLines("input.txt");

var numberOfRows = lines.Count();
var numberOfColumns = lines.First().Length;
var forest = new int[numberOfRows, numberOfColumns];

for (var row = 0; row < numberOfRows; row++)
{
    var line = lines.ElementAt(row);
    for (var col = 0; col < numberOfColumns; col++) forest[row, col] = line[col];
}

var numberOfVisibleTrees = numberOfColumns * 2 + (numberOfRows - 2) * 2;
for (var i = 1; i < numberOfRows - 1; i++)
for (var j = 1; j < numberOfColumns - 1; j++)
    if (IsVisible(forest[i, j], i, j, numberOfRows, numberOfColumns, forest))
        numberOfVisibleTrees++;

Console.WriteLine($"Number of visible trees: {numberOfVisibleTrees}");

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