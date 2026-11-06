namespace App.Lab2;

public class Frequency
{
    public static Dictionary<string, string> FrequencyAnalysis(string inputString)
    {
        var frequencies = new Dictionary<string, Dictionary<string, int>>();
        var sentences = inputString.Split('.');
        
        foreach (var sentence in sentences)
        {
            var words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length - 1; i++)
            {
                var word = words[i];
                var nextWord = words[i + 1];

                if (!frequencies.ContainsKey(word))
                {
                    frequencies.Add(word, new Dictionary<string, int>());
                }
                if (!frequencies[word].ContainsKey(nextWord))
                {
                    frequencies[word].Add(nextWord, 0);
                }
                frequencies[word][nextWord] += 1;
                
                if (i != words.Length - 2)
                {
                    var digramm = $"{word} {nextWord}";

                    if (!frequencies.ContainsKey(digramm))
                    {
                        frequencies.Add(digramm, new Dictionary<string, int>());
                    }
                    if (!frequencies[digramm].ContainsKey(words[i + 2]))
                    {
                        frequencies[digramm].Add(words[i + 2], 0);
                    }
                    frequencies[digramm][words[i + 2]] += 1;
                }
            }
        }

        var result = new Dictionary<string, string>();
        foreach (var ngramm in frequencies)
        {
            int maxCount = 0;
            var mostFrequent = "";
            foreach (var count in ngramm.Value)
            {
                if (count.Value > maxCount)
                {
                    mostFrequent = count.Key;
                    maxCount = count.Value;
                }
            }

            result[ngramm.Key] = mostFrequent;
        }
        
        return result;
    }
}