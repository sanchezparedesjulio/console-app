///Change direction to instruments

///using RadioConformanceTests.Scpi;
using RadioConformanceTests.Instruments;

namespace RadioConformanceTests.TC;



public class TC_Sensitivity 
{
    /// <summary>
    /// Test case configuration
    /// </summary>
    private double cfg_StartFreq_MHz = 0;
    private double cfg_EndFreq_MHz = 6000;
    private double cfg_FreqStep_MHz = 500;

    private double cfg_StartPower_DBm = -10;
    private double cfg_EndPower_DBm = -90;
    private double cfg_PowerStep_DBm = -10;
    private readonly IBseInstrument bse;
  
    /// Seaparate in diferents parts, ScpiClients
    public TC_Sensitivity(IBseInstrument instrument)
    {
       
        this.bse = instrument;
    }

    /// Make unitary Tests-> Test Veredics 
    public TestVerdict Execute()
    {
        try
        {
            var finalVerdict = TestVerdict.Pass;
            

            if(!this.bse.IsDetected())
            {
                Console.WriteLine($"TC_Sensitivity::Fail BSE not detected");
                return TestVerdict.Fail;
            }

            // Start Cell
            this.bse.CellOn();

            // Frequency sweep
            uint stepCount = 1 ;

            for(double freq = cfg_StartFreq_MHz; freq <= cfg_EndFreq_MHz; freq += cfg_FreqStep_MHz)
            {
               

                for(double power=cfg_StartPower_DBm; power >= cfg_EndPower_DBm; power+=cfg_PowerStep_DBm)
                {
                    Console.WriteLine($"TC_Sensitivity:: Step {stepCount} Configure BSE {freq}MHz {power}dBm");
                
                    this.bse.ConfigureCell(freq,power);

                    // Wait for UE to connect
                    Thread.Sleep(100);

                    if (!this.bse.UeConnected())
                    {
                        
                        finalVerdict = TestVerdict.Fail;
                    }
                    stepCount++;
                }
            }

            Console.WriteLine($"TC_Sensitivity:: {finalVerdict}");
            return finalVerdict;
        }
        catch
        {
            return TestVerdict.Error;
        }
        finally
        {
            this.bse.CellOff();
            
        }
    }

}