using System.Diagnostics;

const int arraySize = 1000000;

MeasureA();
MeasureB();

var resultA = MeasureA();
var resultB = MeasureB();
var diff = 1.0 * resultB / resultA;

Console.WriteLine($"Integer performance: {resultA} ms");
Console.WriteLine($"Object performance: {resultB} ms\n");

Console.WriteLine($"Method B is {diff} times slower");

long MeasureA() 
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    int a = 1;
    for (int i = 0; i < arraySize; i++)
        a = a + 1;

    stopWatch.Stop();
    return stopWatch.ElapsedMilliseconds;
}

long MeasureB()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    object a = 1;
    for (int i = 0; i < arraySize; i++)
        a = (int) a + 1;

    stopWatch.Stop();
    return stopWatch.ElapsedMilliseconds;
}