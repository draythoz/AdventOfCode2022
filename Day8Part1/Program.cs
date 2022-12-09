var lines = File.ReadLines("input.txt");

int numberOfRows = lines.Count();
int numberOfColumns = lines.First().Length;
int[,] forest = new int[numberOfRows, numberOfColumns];

for (int row = 0; row < numberOfRows; row++)
{
    var line = lines.ElementAt(row);
    for (int col = 0; col < numberOfColumns; col++)
    {
        forest[row, col] = line[col];
    }
}

var numberOfVisibleTrees = (numberOfColumns * 2) + ((numberOfRows -2) * 2);
for (int i = 1; i < numberOfRows - 1; i++)
{
    for (int j = 1; j < numberOfColumns - 1; j++)
    {
        if (IsVisible(forest[i, j], i, j, numberOfRows, numberOfColumns, forest))
        {
            numberOfVisibleTrees++;
        }
    }
}

Console.WriteLine($"Number of visible trees: {numberOfVisibleTrees}");

bool IsVisible(int treeHeight, int row, int column, int numberOfRows, int numberOfColumns, int[,] forest)
{
    var isVisibleTop = true;
    for (int i = 0; i < row; i++)
    {
        if (forest[i, column] >= treeHeight) isVisibleTop = false;
    } 
    
    var isVisibleBottom = true;
    for (int i = row+1; i < numberOfRows; i++)
    {
        if (forest[i, column] >= treeHeight) isVisibleBottom = false;
    } 
    
    var isVisibleLeft = true;
    for (int j = 0; j < column; j++)
    {
        if (forest[row, j] >= treeHeight) isVisibleLeft = false;
    }
    
    var isVisibleByRowRight = true;
    for (int j = column + 1; j < numberOfColumns; j++)
    {
        if (forest[row, j] >= treeHeight) isVisibleByRowRight = false;
    }

    // Console.WriteLine($"Row/Column ={row}/{column}");
    // Console.WriteLine($"{nameof(isVisibleTop)}:{isVisibleTop}");
    // Console.WriteLine($"{nameof(isVisibleBottom)}:{isVisibleBottom}");
    // Console.WriteLine($"{nameof(isVisibleLeft)}:{isVisibleLeft}");
    // Console.WriteLine($"{nameof(isVisibleByRowRight)}:{isVisibleByRowRight}");
    
    return isVisibleTop || isVisibleBottom || isVisibleLeft || isVisibleByRowRight;
}