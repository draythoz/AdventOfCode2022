using System.Text;

char ch;
const int sizeOfStartWindow = 14;
const char initializationChar = '!';
var sb = new StringBuilder();
for (var i = 0; i < sizeOfStartWindow; i++) sb.Append(initializationChar);
var startWindow = sb.ToString().ToCharArray();

StreamReader reader;
reader = new StreamReader(@"input.txt");
var characterIndex = 0;
do
{
    ch = (char)reader.Read();
    Array.Copy(startWindow, 1, startWindow, 0, startWindow.Length - 1);
    startWindow[sizeOfStartWindow - 1] = ch;
    characterIndex++;
} while (!reader.EndOfStream && !IsFullSet(startWindow));

reader.Dispose();
Console.WriteLine(startWindow);
Console.WriteLine(characterIndex);

bool IsFullSet(char[] foobar)
{
    if (foobar[0] != initializationChar && foobar.ToHashSet().Count() == sizeOfStartWindow) return true;

    return false;
}