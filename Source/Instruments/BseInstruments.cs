using RadioConformanceTests.Drivers;

namespace RadioConformanceTests.Instruments;

public class BseInstrument : IBseInstrument
{
    private readonly IScpiClient scpi;
    public BseInstrument(IScpiClient scpiInst)
    {
        this.scpi = scpiInst;
    }

    public bool IsDetected()
    {
        var instrumentIdString = this.scpi.Query("BSE:*IDN?");
        return instrumentIdString == "Keysight,BSE";
    }

    public bool UeConnected()
    {
        return this.scpi.Query("BSE:CELL1:UE:CONNECTED") != "1";
    }

    public void CellOff()
    {
        this.scpi.Command("BSE:CELL1:OFF");
    }

    public void CellOn()
    {
        this.scpi.Command("BSE:CELL1:TECH 5G");
        this.scpi.Command("BSE:CELL1:ON");
    }

    public void ConfigureCell(double freq, double power)
    {
        this.scpi.Command($"BSE:CELL1:FREQ {freq}");
        this.scpi.Command($"BSE:CELL1:POW {power}");
    }
}