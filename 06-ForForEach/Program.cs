using System.Collections;
using System.Diagnostics;

const int numElements = 10000000;

var arrayList = new ArrayList(numElements);
var genericList = new List<int>(numElements);
var array = new int[numElements];

PrepareList();

MeasureA1();
MeasureA2();
MeasureB1();
MeasureB2();
MeasureC1();
MeasureC2();

var resultA1 = MeasureA1();
var resultA2 = MeasureA2();
var resultB1 = MeasureB1();
var resultB2 = MeasureB2();
var resultC1 = MeasureC1();
var resultC2 = MeasureC2();

Console.WriteLine($"ArrayList For {resultA1} ms");
Console.WriteLine($"ArrayList Foreach {resultA2} ms");
Console.WriteLine($"List<int> For {resultB1} ms");
Console.WriteLine($"List<int> Foreach {resultB2} ms");
Console.WriteLine($"int[] For {resultC1} ms");
Console.WriteLine($"int[] Foreach {resultC2} ms");


void PrepareList()
{
    var random = new Random();
    for (int i = 0; i < numElements; i++)
    {
        int number = random.Next(256);
        arrayList.Add(number);
        genericList.Add(number);
        array[i] = number;
    }
}

long MeasureA1()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    for (int i = 0; i < numElements; i++)
    {
        int result = (int) arrayList[i];
    }

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}

long MeasureA2()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    foreach (int i in arrayList)
    {
        int result = i;
    }

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}

long MeasureB1()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    for (int i = 0; i < numElements; i++)
    {
        int result = genericList[i];
    }

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}

long MeasureB2()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    foreach (int i in genericList)
    {
        int result = i;
    }

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}

long MeasureC1()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    for (int i = 0; i < numElements; i++)
    {
        int result = array[i];
    }

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}

long MeasureC2()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    foreach (int i in array)
    {
        int result = i;
    }

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}


