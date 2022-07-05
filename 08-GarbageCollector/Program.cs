using System.Collections;
using System.Diagnostics;
using System.Text;

var stopwatch = new Stopwatch();
stopwatch.Start();

//call your method!
//OptmStrings();
OptmArrayLists();

stopwatch.Stop();
Console.WriteLine($"Ellapsed: {stopwatch.Elapsed.TotalMilliseconds}");

//Methods
void OptmStrings()
{
    var sb = new StringBuilder();
    for (int i = 0; i < 100000; i++)
    {
        //slow, new string for each iteration!
        //sb.Append(i.ToString() + "KB");

        //faster, less strings!
        sb.Append(i);
        sb.Append("KB");
    }
}

void OptmArrayLists()
{
    //slow, no type!
    //var list = new ArrayList();

    //faster, less box/unbox!
    var list = new List<int>();

    for (int i = 0; i < 1000000; i++)
    {
        list.Add(i);
    }
}

//main idea:
//short lived small objects and long lived large objects (85k)
//if not change them for:
// * large objects should be: long lived or reduced to small objects
// * small objects should be: short lived or increate to large objects!

//void OptmTimeForObjects()
//{
//    var obj = new MyObject();
//    //lots of other stuff
//    UseTheObject(obj);


//    //better
//    var obj = new MyObject();
//    UseTheObject(obj);
//    obj = null;

//}

//void ForceLongLiveLargeObject()
//{
//    //slow, large objects stays as short lived!!
//    var list = new ArrayList(85190);
//    UseList(list);
//    //later on
//    list = new ArrayList(85190);
//    useList(list);

//    //better, stays as long lived
//    var list = new ArrayList(85190);
//    UseList(list);
//    //later on
//    list.Clear();
//    useList(list);

//}

//void ForceShortLivedShortObjects()
//{
//    //lots os new objects!
//    var list = new ArrayList(85190);
//    for (int i = 0; i < 100000; i++)
//        list.Add(new Pair(i, i + 1));

//    //faster, two arrays :) 
//    int[] list1 = new int[85190];
//    int[] list2 = new int[85190];

//    for (int i = 0; i < 10000; i++)
//    {
//        list1[i] = i;
//        list2[i] = i + 1;
//    }
//}

//void KeepObjectSmallForShortLiving()
//{
//    //oversized type!
//    var buffer = new int[32768];
//    for (int i = 0; i < buffer.Length; i++)
//        buffer[i] = GetByte(i);

//    //proper type!
//    var buffer = new byte[32768];
//    for (int i = 0; i < buffer.Length; i++)
//        buffer[i] = GetByte(i);

//}

//void KeepObjectLargeForLongLiving()
//{
//    var list = new ArrayList();
//    //lots of code
//    UseTheList(list);


//    //creating as large!
//    var list = new ArrayList(85190);
//    //lots of code
//    UseTheList(list);
//}