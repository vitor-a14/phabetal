using System;
using System.Linq;

// Handle the alphabet and letter order logic
public class AlphabetManager {
    public static char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    //Check if the first letter is bigger than the second letter
    public static bool CompareLetters(char a, char b) {
        int indexA = Array.IndexOf(alphabet, a);
        int indexB = Array.IndexOf(alphabet, b);

        return a > b;
    }

    public static string GetRandomLetter(char[] avoidedLetters) {
        char[] searchRange = alphabet.Where(letter => !avoidedLetters.Contains(letter)).ToArray();

        Random random = new Random();
        int randomIndex = random.Next(0, searchRange.Length - 1);

        return searchRange[randomIndex].ToString();
    }
}
