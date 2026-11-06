using App;
using App.Lab1;

namespace AppTests.Lab1;

public class PricesTests
{
    [TestCase(1000052, "рубля", false, false)]
    [TestCase(25, "рублей", false, false)]
    [TestCase(1000011, "рублей", false, false)]
    [TestCase(100001, "рубль", false, false)]
    [TestCase(24, "рубля", false, false)]
    [TestCase(10000004, "рубля", false, false)]
    [TestCase(24, "Рубля", false, true)]
    [TestCase(10000004, "руб.", true, false)]
    [TestCase(10000004, "Руб.", true, true)]
    public void TestPasses_When_Result_Correct(int price, string expected, bool shrt, bool cap)
    {
        var actual = Prices.GetCurrencyAlias(price, shrt, cap);
        Assert.That(actual, Is.EqualTo(expected));
    }
    
}