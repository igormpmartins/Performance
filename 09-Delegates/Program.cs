using System.Diagnostics;

const int REPEATS = 1000000;
const int EXPERIMENTS = 100;

double manual = 0;
double unicast = 0;
double multicast = 0;

for (int i = 0; i < EXPERIMENTS; i++)
{
    manual += Measure1();
    unicast += Measure2();
    multicast += Measure3();
}

Console.WriteLine($"Manual: {manual}");
Console.WriteLine($"Unicast : {unicast}");
Console.WriteLine($"Multicast: {multicast}");

double Measure1()
{
    var sw = Stopwatch.StartNew();
    for (int i = 0; i < REPEATS; i++)
    {
        Test.Add1(1234, 2345, out _);
        Test.Add2(1234, 2345, out _);
    }
    sw.Stop();

    return sw.Elapsed.TotalMilliseconds;
}

double Measure2()
{
    var sw = Stopwatch.StartNew();
    Test.AddDelegate add1 = Test.Add1;
    Test.AddDelegate add2 = Test.Add2;

    for (int i = 0; i < REPEATS; i++)
    {
        add1(1234, 2345, out _);
        add2(1234, 2345, out _);
    }

    sw.Stop();

    return sw.Elapsed.TotalMilliseconds;
}

double Measure3()
{
    var sw = Stopwatch.StartNew();
    Test.AddDelegate multiAdd = Test.Add1;
    multiAdd += Test.Add2;

    for (int i = 0; i < REPEATS; i++)
        multiAdd(1234, 2345, out _);
    sw.Stop();

    return sw.Elapsed.TotalMilliseconds;
}

public class Test
{
    public delegate void AddDelegate(int a, int b, out int result);
    public static void Add1(int a, int b, out int result) => result = a + b;
    public static void Add2(int a, int b, out int result) => result = a + b;
}