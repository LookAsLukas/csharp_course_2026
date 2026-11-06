using App.Lab2;

namespace AppTests.Lab2;

public class LogAggregatorTests
{
    [Test]
    public void MergeLogs_OnlyGarbage_ReturnsEmptyString()
    {
        var result = LogAggregator.MergeLogs("garbage", "[invalid]", null);
        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void MergeLogs_SingleGroup_SortsByTime()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:02] [trace-A] AuthService: Second",
            "[2023-10-25 14:05:01] [trace-A] AuthService: First"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:01] AuthService: First",
            "[2023-10-25 14:05:02] AuthService: Second"
        });

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MergeLogs_NoArguments_ReturnsEmptyString()
    {
        var result = LogAggregator.MergeLogs();

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void MergeLogs_MultipleGroups_GroupsAndSortsEachGroup()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:00] [trace-B] Gateway: Request received",
            "[2023-10-25 14:05:01] [trace-A] AuthService: User logged in",
            "[2023-10-25 14:05:05] [trace-B] BillingService: Charge attempted",
            "[2023-10-25 14:05:02] [trace-A] AuthService: Token generated"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-B] ---",
            "[2023-10-25 14:05:00] Gateway: Request received",
            "[2023-10-25 14:05:05] BillingService: Charge attempted",
            "",
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:01] AuthService: User logged in",
            "[2023-10-25 14:05:02] AuthService: Token generated"
        });

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MergeLogs_SameTimestamp_SortsByServiceName()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:01] [trace-A] BillingService: Charge attempted",
            "[2023-10-25 14:05:01] [trace-A] AuthService: User logged in"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:01] AuthService: User logged in",
            "[2023-10-25 14:05:01] BillingService: Charge attempted"
        });

        Assert.That(result, Is.EqualTo(expected));
    }
}
