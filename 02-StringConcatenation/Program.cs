using System.Diagnostics;
using System.Text;

const int numRepetitions = 10000;
//const int numRepetitions = 4;

MeasureA();
MeasureB();

var resultA = MeasureA();
var resultB = MeasureB();

var diff = 1.0 * resultB / resultA;

Console.WriteLine($"String performance: {resultA} ms");
Console.WriteLine($"String Builder performance: {resultB} ms\n");

long MeasureA()
{
    var s = string.Empty;
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    for (int i = 0; i < numRepetitions; i++)
        s = s + "a";

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}

long MeasureB()
{
    var sb = new StringBuilder();
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    for (int i = 0; i < numRepetitions; i++)
        sb.Append("a");

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}