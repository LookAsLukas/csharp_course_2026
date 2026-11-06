using App.Lab2;

namespace AppTests.Lab2;

public class StackMachineTests
{
    [TestCase(new[] { "push Привет! Это снова я! Пока!","pop 5","push Как твои успехи? Плохо?","push qwertyuiop","push 1234567890","pop 27" }, "Привет! Это снова я! Как твои успехи?")]
    [TestCase(new[] { "push Hello,", "push World!" }, "Hello,World!")]
    [TestCase(new[] { "push abcdef", "pop 3", "push XYZ", "pop 2" }, "abcX")]
    [TestCase(new[] { "push hello", "pop 5" }, "")]
    [TestCase(new[] { "push Hello World", "push !", "pop 1" }, "Hello World")]
    public void StackTests(string[] codeLines, string expected)
    {
        var actual = StackMachine.CalculateString(codeLines);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
