using Moq;
using NUnit.Framework;
using RadioConformanceTests.Instruments;
using RadioConformanceTests.TC;

public class TC_SensitivityValidation
{
    [Test]
    public void ValidatePassVerdict()
    {
        var mockBse = new Mock<IBseInstrument>();
        mockBse.Setup(bse => bse.IsDetected()).Returns(true);
        mockBse.Setup(bse => bse.UeConnected()).Returns(true);
        var test = new TC_Sensitivity(mockBse.Object);
        Assert.That(test.Execute(), Is.EqualTo(TestVerdict.Pass));
        mockBse.Verify(bse => bse.CellOn(), Times.Once());
        mockBse.Verify(bse => bse.CellOff(), Times.Once());
    }

    [Test]
    public void ValidateFailVerdict()
    {
        var mockBse = new Mock<IBseInstrument>();
        mockBse.Setup(bse => bse.IsDetected()).Returns(true);
        mockBse.SetupSequence(bse => bse.UeConnected()).Returns(true).Returns(false);
        var test = new TC_Sensitivity(mockBse.Object);
        Assert.That(test.Execute(), Is.EqualTo(TestVerdict.Fail));
        mockBse.Verify(bse => bse.CellOn(), Times.Once());
        mockBse.Verify(bse => bse.CellOff(), Times.Once());
    }

    [Test]
    public void ValidateFailVerdictWhenBseIsNotConnected()
    {
        var mockBse = new Mock<IBseInstrument>();
        mockBse.Setup(bse => bse.IsDetected()).Returns(false);
        var test = new TC_Sensitivity(mockBse.Object);
        Assert.That(test.Execute(), Is.EqualTo(TestVerdict.Fail));

        mockBse.Verify(bse => bse.CellOn(), Times.Never());
    }
}