char ch;
var startWindow = new char[4] {default(char),default(char),default(char),default(char)};
StreamReader reader;
reader = new StreamReader(@"input.txt");
var characterIndex = 0;
do
{
    ch = (char)reader.Read();
    Array.Copy(startWindow, 1, startWindow, 0, startWindow.Length - 1);
    startWindow[3] = ch;
    characterIndex++;
} while (!reader.EndOfStream && !AllUniqueChars(startWindow));

reader.Dispose();
Console.WriteLine(startWindow);
Console.WriteLine(characterIndex);

bool AllUniqueChars(char[] stringToTest)
{
    if (stringToTest[0] == default(char)) return false;
    
    for (int i = 0; i < stringToTest.Length; i++)
    {
        var currentChar = stringToTest[i];
        for (int j = 0; j < stringToTest.Length; j++)
        {
            if (i == j)
                continue;

            if (currentChar == stringToTest[j]) return false;
        }
    }

    return true;
}