using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System;

public class TestCamp
{
    static int workeTimes;

    public async static Task Major()
    {
        Console.WriteLine("TestCamp-------------");

        //TestMethod().ContinueWith(task => Console.WriteLine("In ContinueWith: {0}", workeTimes));
        await TestMethod().ContinueWith(task => Console.WriteLine("In ContinueWith: {0}", workeTimes));
        //await TestMethod();

        Console.WriteLine("workTimes: {0}", workeTimes);
    }

    public async static Task TestMethod()
    {
        await Task.Delay(3000);
        workeTimes = 5;
        Console.WriteLine("TestMethod after await...");
    }
}