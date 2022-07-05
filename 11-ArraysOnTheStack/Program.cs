using System.Diagnostics;

long resultA = 0;
long resultB = 0;

//Needs this loop, otherwise we get a stack overflow for unsafe []!
for (int i = 0; i < 1000; i++)
{
    resultA += MeasureA(100000);
    resultB += MeasureB(100000);
}

Console.WriteLine($"int[] performance: {resultA} ms");
Console.WriteLine($"stack int[] performance: {resultA} ms");

long MeasureA(int elements)
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    int[] list = new int[elements];

    for (int i = 0; i < elements; i++)
        list[i] = i;

    stopWatch.Stop();

    return stopWatch.ElapsedMilliseconds;
}

long MeasureB(int elements)
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    unsafe
    {
        int* list = stackalloc int[elements];

        for (int i = 0; i < elements; i++)
            list[i] = i;

        stopWatch.Stop();
    }

    return stopWatch.ElapsedMilliseconds;
}