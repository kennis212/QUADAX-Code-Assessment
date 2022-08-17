//Kevin Ennis
//8/17/2022

using System;

public class Mastermind
{
    // List of potential numbers in the secret code
    protected static int[] nums = new int[] {1, 2, 3, 4, 5, 6};
    // Length of the secret code
    protected static int answerSize = 4;
    // Total 'lives' or attempts the user has left
    protected static int lives = 9;
    // Total of attempts made thus far
    protected static int tries = 0;
    // Solution that will be randomly assorted in the method generateRandomCode()
    protected static int[] solution = new int[] {1,2,3,4};

    public static void Main() {
        int[] guess = new int[4]; // Array that will take the user's input
        generateRandomCode(); // Creates the secret code to be guessed
        bool shouldGameContinue = true;
        Console.WriteLine("Welcome to Mastermind! Try and guess the secret code!");
        Console.WriteLine("The code is four digits long; potential numbers range from 1 to 6. Please enter each individual guessed digit on a new line.");
        Console.WriteLine("Good luck!");
        while (shouldGameContinue) {
            Console.WriteLine("Enter Guess:");
            for (int i=0; i<4; i++) {
                int.TryParse(Console.ReadLine(), out guess[i]); // Each individual line will be read from the user and added to guess[] in order
            }
            shouldGameContinue = !checkCode(guess) && !isUserOutOfLives(); // The game ends when the user guesses correctly or when the user has run out of lives
        }
        Console.ReadLine();
    }

    /*
    The method takes a given integer array to be used as the user's guess.
    This method combs through the given four-digit guess and compares it to the randomly generated solution.
    If a mismatch is found, reviewCode() is called to convey hints to the user.
    Total tries and lives left are updated as well.
    Should the user's guess match the solution perfectly, the method will return true and end the game.
    */
    public static bool checkCode(int[] guess) {
        for (int i=0; i<guess.Length; i++) {
            if (guess[i] != solution[i]) {
                reviewCode(guess);
                lives--;
                tries++;
                return false;
            }
        }
        Console.WriteLine("Congrats! You correctly guessed the secret code! You win!");
        Console.WriteLine("Number of tries taken: " + tries);
        return true;
    }

    /*
    The method takes a given integer array to provide hints for the user in regards to the correct answer.
    A string array is generated to print said hints for the user according to the placement and selection of guessed digits.
    The user will then be notified of how many lives they have left. On the final life, an encouraging message is generated.
    */
    public static void reviewCode(int[] guess) {
        string[] hints = new string[guess.Length];
        for (int i = 0; i < guess.Length; i++) {
            if (guess[i] == solution[i]) {
                hints[i] = "+";
            } else if (solution.Contains(guess[i])) {
                hints[i] = "-";
            } else {
                hints[i] = " ";
            }
        }
        Array.ForEach(hints, Console.Write); // The console will print out the four hints on the same line.
        Console.WriteLine("");
        Console.WriteLine("Lives left: " + lives);
        if (tries == 8) {
            Console.WriteLine("This is your last chance! Believe in yourself!!");
        }
    }

    /*
    The previously established solution variable is updated with a random assortment of numbers present within nums[].
    */
    public static void generateRandomCode() {
        Random rand = new Random();
        for (var i = 0; i < answerSize; i++) {
            solution[i] = nums[rand.Next(0, nums.Length)];
        }
        return;
    }

    /*
    Once the user has passed a preset number of tries, the method returns true and ends the game.
    The user is informed they are out of lives and what the correct solution to the problem was.
    */
    public static bool isUserOutOfLives() {
        if (tries < 10)
            return false;

        Console.WriteLine($"Out of lives! Game Over!");
        Console.WriteLine($"The solution was: {string.Join("", solution)}");
        return true;
    }
}