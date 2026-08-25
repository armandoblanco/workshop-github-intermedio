using FinancialUtils;

namespace FinancialUtils.Tests;

public class CalculatorTests
{
    // --- Add ---

    [Fact]
    public void Add_TwoPositives_ReturnsCorrectSum()
    {
        var result = Calculator.Add(2m, 3m);
        Assert.Equal(5m, result);
    }

    [Fact]
    public void Add_WithNegative_ReturnsCorrectSum()
    {
        var result = Calculator.Add(-1m, 5m);
        Assert.Equal(4m, result);
    }

    [Fact]
    public void Add_WithZero_ReturnsSameValue()
    {
        Assert.Equal(7m, Calculator.Add(0m, 7m));
    }

    // --- Subtract ---

    [Fact]
    public void Subtract_ReturnsCorrectDifference()
    {
        Assert.Equal(6m, Calculator.Subtract(10m, 4m));
    }

    [Fact]
    public void Subtract_ResultIsNegative_WhenBGreaterThanA()
    {
        Assert.Equal(-5m, Calculator.Subtract(3m, 8m));
    }

    // --- Multiply ---

    [Theory]
    [InlineData(4, 5, 20)]
    [InlineData(99, 0, 0)]
    [InlineData(-3, -4, 12)]
    [InlineData(1, 1, 1)]
    public void Multiply_ReturnsCorrectProduct(decimal a, decimal b, decimal expected)
    {
        Assert.Equal(expected, Calculator.Multiply(a, b));
    }

    // --- Divide ---

    [Fact]
    public void Divide_ReturnsCorrectQuotient()
    {
        Assert.Equal(5m, Calculator.Divide(10m, 2m));
    }

    [Fact]
    public void Divide_NonIntegerResult_ReturnsDecimal()
    {
        Assert.Equal(3.5m, Calculator.Divide(7m, 2m));
    }

    [Fact]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() => Calculator.Divide(5m, 0m));
    }

    // --- CompoundInterest ---

    [Fact]
    public void CompoundInterest_CalculatesCorrectly()
    {
        // 1000 * (1 + 0.05)^12 = 1795.856...
        var result = Calculator.CompoundInterest(1000m, 0.05m, 12);
        Assert.Equal(1795.856m, Math.Round(result, 3));
    }

    [Fact]
    public void CompoundInterest_ZeroRate_ReturnsPrincipal()
    {
        Assert.Equal(1000m, Calculator.CompoundInterest(1000m, 0m, 12));
    }

    [Fact]
    public void CompoundInterest_OnePeriod_AddsSingleInterest()
    {
        var result = Calculator.CompoundInterest(1000m, 0.1m, 1);
        Assert.Equal(1100m, Math.Round(result, 0));
    }

    [Fact]
    public void CompoundInterest_NegativePrincipal_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Calculator.CompoundInterest(-100m, 0.05m, 12));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CompoundInterest_InvalidPeriods_ThrowsArgumentException(int periods)
    {
        Assert.Throws<ArgumentException>(() => Calculator.CompoundInterest(1000m, 0.05m, periods));
    }

    // --- LoanPayment ---

    [Fact]
    public void LoanPayment_CalculatesMonthlyPaymentCorrectly()
    {
        // Préstamo 100,000 a 12% anual, 12 meses → ~8,884.88
        var result = Calculator.LoanPayment(100000m, 0.12m, 12);
        Assert.Equal(8884.88m, result);
    }

    [Fact]
    public void LoanPayment_ZeroRate_DividesPrincipalByMonths()
    {
        var result = Calculator.LoanPayment(12000m, 0m, 12);
        Assert.Equal(1000m, result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1000)]
    public void LoanPayment_InvalidPrincipal_ThrowsArgumentException(decimal principal)
    {
        Assert.Throws<ArgumentException>(() => Calculator.LoanPayment(principal, 0.12m, 12));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-6)]
    public void LoanPayment_InvalidMonths_ThrowsArgumentException(int months)
    {
        Assert.Throws<ArgumentException>(() => Calculator.LoanPayment(10000m, 0.12m, months));
    }

    // --- NetPresentValue ---

    [Fact]
    public void NetPresentValue_CalculatesCorrectly()
    {
        // Inversión inicial -1000, flujos futuros de 400 por 3 periodos a tasa 10%
        // NPV = -1000 + 400/1.1 + 400/1.21 + 400/1.331 ≈ -5.26
        var cashFlows = new[] { -1000m, 400m, 400m, 400m };
        var result = Calculator.NetPresentValue(0.10m, cashFlows);
        Assert.Equal(-5.26m, result);
    }

    [Fact]
    public void NetPresentValue_SingleFlow_ReturnsFlow()
    {
        // Con tasa 0 y un flujo, el VPN es el mismo flujo
        var result = Calculator.NetPresentValue(0m, new[] { 500m });
        Assert.Equal(500m, result);
    }

    [Fact]
    public void NetPresentValue_EmptyCashFlows_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Calculator.NetPresentValue(0.10m, Array.Empty<decimal>()));
    }

    [Fact]
    public void NetPresentValue_NullCashFlows_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Calculator.NetPresentValue(0.10m, null!));
    }
}
