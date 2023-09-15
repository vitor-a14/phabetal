using System;
using System.Linq;
using UnityEngine;

// Handle the alphabet and letter order logic
public class AlphabetManager {
    public static char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    //Check if the first letter is bigger than the second letter
    public static bool CompareLetters(char a, char b) {
        int indexA = Array.IndexOf(alphabet, a);
        int indexB = Array.IndexOf(alphabet, b);

        return a > b;
    }

    public static string GetOutputLetter(char[] avoidedLetters, bool isGreaterThan) {
        char[] searchRange = alphabet.Where(letter => !avoidedLetters.Contains(letter)).ToArray();

        System.Random random = new System.Random();
        int randomIndex = random.Next(0, searchRange.Length - 1);

        return searchRange[randomIndex].ToString();
    }

    public static string GetPlayerChoice(char[] avoidedLetters, char[] outputLetters, bool correctAnswer, bool isGreaterThan) {
        char[] searchRange;
        int outputIndex;

        if(correctAnswer) {
            if(isGreaterThan) {
                outputIndex = FindIndexInAlphabet(outputLetters, true);
                searchRange = alphabet.Where(letter => !avoidedLetters.Contains(letter) && Array.IndexOf(alphabet, letter) > outputIndex).ToArray();
            } else {
                outputIndex = FindIndexInAlphabet(outputLetters, false);
                searchRange = alphabet.Where(letter => !avoidedLetters.Contains(letter) && Array.IndexOf(alphabet, letter) < outputIndex).ToArray();
            }
        } else {
            if(isGreaterThan) {
                outputIndex = FindIndexInAlphabet(outputLetters, false);
                searchRange = alphabet.Where(letter => !avoidedLetters.Contains(letter) && Array.IndexOf(alphabet, letter) < outputIndex).ToArray();
            } else {
                outputIndex = FindIndexInAlphabet(outputLetters, true);
                searchRange = alphabet.Where(letter => !avoidedLetters.Contains(letter) && Array.IndexOf(alphabet, letter) > outputIndex).ToArray();
            }

            if(searchRange.Length <= 0)
                searchRange = alphabet.Where(letter => !avoidedLetters.Contains(letter)).ToArray();
        }

        System.Random random = new System.Random();
        int randomIndex = random.Next(0, searchRange.Length - 1);

        return searchRange[randomIndex].ToString();
    }

    public static int FindIndexInAlphabet(char[] letters, bool findSmallerIndex) {
        int chosenIndex = Array.IndexOf(alphabet, letters[0]);

        foreach(char letter in letters) {
            int index = Array.IndexOf(alphabet, letter);
            if(findSmallerIndex) {
                if(index < chosenIndex) chosenIndex = index;
            } else {
                if(index > chosenIndex) chosenIndex = index;
            }
        }

        return chosenIndex;
    }
}
