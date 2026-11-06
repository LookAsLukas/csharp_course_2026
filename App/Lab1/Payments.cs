namespace App.Lab1;

public enum PaymentsPlan
{
    Differentiated,
    Annuity
}

public static class Payments
{
    public static decimal CalculateTotalPayments(PaymentsPlan plan, decimal rate, int monthsCount, decimal amount)
    {
        rate /= 1200;
        switch (plan)
        {
            case PaymentsPlan.Annuity:
                rate += 1;
                return amount * Pow(rate, monthsCount) / (Pow(rate, monthsCount) - 1) * (rate - 1) * monthsCount;
            case PaymentsPlan.Differentiated:
                return amount * (monthsCount + 1) / 2 * rate + amount;
        }

        return 0;
    }

    private static decimal Pow(decimal a, int b)
    {
        decimal r = 1;
        while (b-- > 0)
        {
            r *= a;
        }
        return r;
    }
}