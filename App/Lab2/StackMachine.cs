using System.Text;

namespace App.Lab2;

public class StackMachine
{
    public static string CalculateString(string[] codeLines)
    {
        var result = new StringBuilder("");

        foreach (var codeLine in codeLines)
        {
            if (codeLine.StartsWith("pop"))
            {
                var toRemove = Int32.Parse(codeLine.Substring(4));
                result.Remove(result.Length - toRemove, toRemove);
            }
            else
            {
                var toAdd = codeLine.Substring(5);
                result.Append(toAdd);
            }
        }
        
        return result.ToString();
    }
}