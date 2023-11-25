using RadioConformanceTests.Drivers;
using RadioConformanceTests.Instruments;
using RadioConformanceTests.TC;

namespace RadioConformanceTests.TC;

class Program 
{
    const string BSE_ADDRESS = "10.10.10.1";
    static void Main(string[] args)
    {
        IScpiClient bseConnection = new ScpiClient(BSE_ADDRESS);
        IBseInstrument bse = new BseInstrument(bseConnection);
        TC_Sensitivity test = new TC_Sensitivity(bse);


        Console.WriteLine("RadioConformanceTests - TC_Sensitivity : START");
        var testVerdict = test.Execute();

        Console.WriteLine($"RadioConformanceTests - Verdict : {testVerdict}");
        Console.WriteLine("RadioConformanceTests - TC_Sensitivity : END");


        Console.WriteLine("Press Q to quit..");
        while (Console.ReadKey().Key != ConsoleKey.Q);
        Environment.Exit(0);
    }
}






