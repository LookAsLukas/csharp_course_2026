using App.Lab2;

namespace AppTests.Lab2;

public class FrequencyTests
{
    [TestCase("She stood up. Then she left.", new[]
    {
        "She stood", "stood up", "Then she", "she left", "She stood up", "Then she left"
    })]
    [TestCase("a b c d. b c d. e b c a d.", new[]
    {
        "a b", "b c", "c d", "e b", "a b c", "b c d", "e b c", "c a d"
    })]
    [TestCase("hello world", new[] { "hello world" })]
    [TestCase("a b c a b d a b c", new[]
    {
        "a b", "b c", "c a", "d a", "a b c", "b c a", "c a b", "b d a", "d a b"
    })]
    [TestCase("", new string[0])]
    public void Tests(string inputText, string[] dictionaryLines)
    {
        var actual = Frequency.FrequencyAnalysis(inputText)
            .OrderBy(kv => kv.Key.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length)
            .Select(kv => $"{kv.Key} {kv.Value}")
            .ToArray();

        Assert.That(actual, Is.EqualTo(dictionaryLines));
    }
}
