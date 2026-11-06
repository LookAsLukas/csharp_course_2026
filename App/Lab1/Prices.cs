namespace App.Lab1;

public static class Prices
{
    public static string GetCurrencyAlias(int price, bool isShorNotation, bool isFirstCapital)
    {
        char firstLetter = isFirstCapital ? 'Р' : 'р';
        int firstDigit = price % 10;
        int secondDigit = price % 100 / 10;
        
        string restLetters = "ублей";
        switch (isShorNotation)
        {
            case true:
                restLetters = "уб.";
                break;
            default:
            {
                restLetters = firstDigit switch
                {
                    1 when secondDigit != 1 => "убль",
                    >= 2 and <= 4 when secondDigit != 1 => "убля",
                    _ => restLetters
                };

                break;
            }
        }

        return $"{firstLetter}{restLetters}";
    }
}