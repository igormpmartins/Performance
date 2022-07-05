using System.Collections;
using System.Diagnostics;
using System.Text;

const int numElements = 10000000;
//const int numElements = 4;

MeasureA();
MeasureB();
MeasureC();

var resultA = MeasureA();
var resultB = MeasureB();
var resultC = MeasureC();

Console.WriteLine($"ArrayList performance: {resultA} ms");
Console.WriteLine($"List<int> performance: {resultB} ms");
Console.WriteLine($"int[] performance: {resultC} ms");

long MeasureA()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    ArrayList list = new ArrayList(numElements);
    //ArrayList list = new ArrayList();

    for (int i = 0; i < numElements; i++)
        list.Add(i);

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}

long MeasureB()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    List<int> list = new List<int>(numElements);
    //List<int> list = new List<int>();

    for (int i = 0; i < numElements; i++)
        list.Add(i);

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}

long MeasureC()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    int[] list = new int[numElements];

    for (int i = 0; i < numElements; i++)
        list[i] = i;

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}