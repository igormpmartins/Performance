using System.Diagnostics;
using System.Text;

const int elements = 1000000;
const int digits = 5;

List<int> numbers = new List<int>();
Dictionary<int, string> lookup = new Dictionary<int, string>
{
    {0, "zero" },
    {1, "one" },
    {2, "two" },
    {3, "three" },
    {4, "four" },
    {5, "five" },
    {6, "six" },
    {7, "seven" },
    {8, "eight" },
    {9, "nine" }
};

PrepareList();

MeasureA();
MeasureB();

var resultA = MeasureA();
var resultB = MeasureB();

Console.WriteLine($"Lookup {resultA} ms");
Console.WriteLine($"Lookup with check: {resultB} ms");


void PrepareList()
{
    var random = new Random();
    for (int i = 0; i < elements; i++)
        numbers.Add(random.Next(11));
}

long MeasureA()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    for (int i = 0; i < elements; i++)
    {
        string s = null;
        try
        {
            s = lookup[numbers[i]];
        }
        catch (KeyNotFoundException)
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
        string s = null;
        int key = numbers[i];

        if (lookup.ContainsKey(key))
            s = lookup[key];
    }

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}