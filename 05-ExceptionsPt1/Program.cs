using System.Diagnostics;
using System.Text;

const int elements = 1000000;
const int digits = 5;

char[] digitArray = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'X' };
List<string> numbers = new List<string>();

PrepareList();

MeasureA();
MeasureB();

var resultA = MeasureA();
var resultB = MeasureB();

Console.WriteLine($"int.Parse {resultA} ms");
Console.WriteLine($"TryParse: {resultB} ms");


void PrepareList()
{
    var random = new Random();
    for (int i = 0; i < elements; i++)
    {
        var sb = new StringBuilder();
        for (int d = 0; d < digits; d++)
        {
            var index = random.Next(11);
            sb.Append(digitArray[index]);
        }
        numbers.Add(sb.ToString());
    }
}

long MeasureA()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    for (int i = 0; i < elements; i++)
    {
        try
        {
            int.Parse(numbers[i]);
        }
        catch (FormatException) 
        {
        }
    }

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}

long MeasureB()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    for (int i = 0; i < elements; i++)
    {
        try
        {
            int.TryParse(numbers[i], out _);
        }
        catch (FormatException) 
        {
        }
    }

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}