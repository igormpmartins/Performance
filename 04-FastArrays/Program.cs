using System.Collections;
using System.Diagnostics;
using System.Text;

const int numElements = 20000;
//const int numElements = 4;

MeasureA();
MeasureB();
MeasureC();

var resultA = MeasureA();
var resultB = MeasureB();
var resultC = MeasureC();

Console.WriteLine($"int[,] performance: {resultA} ms");
Console.WriteLine($"flattened array performance: {resultB} ms");
Console.WriteLine($"jagged array performance: {resultC} ms");

long MeasureA()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    int [,] list = new int[numElements, numElements];

    for (int i = 0; i < numElements; i++)
        for (int j = 0; j < numElements; j++)
            list[i, j] = 1;

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}

long MeasureB()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    int[] list = new int[numElements * numElements];

    for (int i = 0; i < numElements; i++)
        for (int j = 0; j < numElements; j++)
        {
            int index = numElements * i + j;
            list[index] = 1;
        }

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}


long MeasureC()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    int[][] list = new int[numElements][];

    //for (int i = 0; i < numElements; i++)
        //list[i] = new int[numElements];

    for (int i = 0; i < numElements; i++)
    {
        list[i] = new int[numElements];

        for (int j = 0; j < numElements; j++)
            list[i][j] = 1;
    }

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}


