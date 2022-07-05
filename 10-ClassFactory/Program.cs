using System.Diagnostics;
using System.Reflection.Emit;
using System.Text;

const int REPEATS = 1000000;

var result1 = Measure1("System.Text.StringBuilder");
var result2 = Measure2("System.Text.StringBuilder");
var result3 = Measure3("System.Text.StringBuilder");

Console.WriteLine($"Compile-time construction: {result1}");
Console.WriteLine($"Dynamic construction: {result2}");
Console.WriteLine($"CIL construction: {result3}");

double Measure1(string typeName)
{
    var sw = Stopwatch.StartNew();
    for (int i = 0; i < REPEATS; i++)
    {
        switch (typeName)
        {
            case "System.Text.StringBuilder":
                new StringBuilder();
                break;
            default:
                throw new NotImplementedException();
        }
    }
    sw.Stop();
    return sw.Elapsed.TotalMilliseconds;
}

double Measure2(string typeName)
{
    var sw = Stopwatch.StartNew();
    for (int i = 0; i < REPEATS; i++)
       Activator.CreateInstance(Type.GetType(typeName));

    sw.Stop();
    return sw.Elapsed.TotalMilliseconds;
}

double Measure3(string typeName)
{
    var sw = Stopwatch.StartNew();
    for (int i = 0; i < REPEATS; i++)
    {
        Test.ClassCreator classCreator = Test.GetClassCreator(typeName);
        classCreator();
    }

    sw.Stop();
    return sw.Elapsed.TotalMilliseconds;
}

class Test
{
    // a delegate to create the object
    // a delegate to create the object
    public delegate object ClassCreator();

    // dictionary to cache class creators
    public static Dictionary<string, ClassCreator> ClassCreators = new Dictionary<string, ClassCreator>();

    public static ClassCreator GetClassCreator(string typeName)
    {
        //get delegate from dictionary
        if (ClassCreators.ContainsKey(typeName))
          return ClassCreators[typeName];

        //get the default constructor of the type
        var t = Type.GetType(typeName);
        var ctor = t.GetConstructor(new Type[0]);

        //create a new dynamic method that constructs and returns the type
        var methodName = t.Name + "Ctor";
        var dm = new DynamicMethod(methodName, t, new Type[0], typeof(object).Module);
        var lgen = dm.GetILGenerator();

        lgen.Emit(OpCodes.Newobj, ctor);
        lgen.Emit(OpCodes.Ret);

        //add delegate to the dictionary and return
        var creator = (ClassCreator) dm.CreateDelegate(typeof(ClassCreator));
        ClassCreators.Add(typeName, creator);
        return creator;
    }
}