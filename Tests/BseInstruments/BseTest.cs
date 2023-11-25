using Moq;
using NUnit.Framework;
using RadioConformanceTests.Drivers;
using RadioConformanceTests.Instruments;
//Make the test using mocks
public class BseTests
{
    [Test]
    public void ConfigureCellTest()
    {
        var mockScpi = new Mock<IScpiClient>();
        var bse = new BseInstrument(mockScpi.Object);
        bse.ConfigureCell(-10, -10);
        mockScpi.Verify(v => v.Command("BSE:CELL1:FREQ -10"));
        mockScpi.Verify(v => v.Command("BSE:CELL1:POW -10"));
    }

    [Test]
    public void BseInstrumnetDiscoveryTest()
    {
        var mockScpi = new Mock<IScpiClient>();
        mockScpi.Setup(s => s.Query("*IDN?")).Returns("Keysight,BSE,1.0").Verifiable();
        var bse = new BseInstrument(mockScpi.Object);
        Assert.IsTrue(bse.IsDetected());
        mockScpi.Verify();
    }

    [Test]
    public void BseInstrumentNotDetected()
    {
        var mockScpi = new Mock<IScpiClient>();
        mockScpi.Setup(s => s.Query("*IDN?")).Returns("").Verifiable();
        var bse = new BseInstrument(mockScpi.Object);
        Assert.IsFalse(bse.IsDetected());
        mockScpi.Verify();
    }
}