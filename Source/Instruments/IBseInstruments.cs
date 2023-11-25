namespace RadioConformanceTests.Instruments;
///Define BSE to emulate the functionality  <summary>
/// of the programm without a real BSE. It allow the option to 
/// make tests.
/// </summary>
public interface IBseInstrument
{
    bool IsDetected();
    void CellOn();
    void ConfigureCell(double freq, double power);
    void CellOff(); 
    bool UeConnected();
}