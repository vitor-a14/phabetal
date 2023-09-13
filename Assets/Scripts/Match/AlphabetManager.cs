using System.Collections;
using System.Collections.Generic;
using System;

// Handle the alphabet and letter order logic
public class AlphabetManager {
    public static char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    public static bool CompareLetters(char a, char b) {
        int indexA = Array.IndexOf(alphabet, a);
        int indexB = Array.IndexOf(alphabet, b);

        return a > b;
    }

    public static string GetRandomLetter() {
        Random random = new Random();
        int randomIndex = random.Next(0, alphabet.Length - 1);

        return alphabet[randomIndex].ToString();
    }
}
