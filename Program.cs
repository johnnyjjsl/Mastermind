namespace Mastermind;

class Program
{
    static void Main(string[] args)
    {
        //Initialize variable that will allow us to play the game
        bool playGame = true;

        //While the variable is true, the game will run
        do {
            playGame = MastermindGame(playGame);
        } while (playGame);


        //Function containing the game logic that returns a boolean indicating whether the player wants to continue playing
        static bool MastermindGame(bool playGame) {
            //Instantiate random number generator to generate answer to guess
            Random random = new Random();

            //Initialize variable containing number of attempts
            int attempts = 10;

            //Next two lines introduce game to the player
            Console.WriteLine("Let's play Mastermind! Guess the 4-digit number.");
            Console.WriteLine($"You have {attempts} attempts");

            //Initialize data structure holding random number, will be used to access positions of digits
            int[] randomNumber = new int[4];

            //Initialize variable holding random number, will be printed at the end of the game
            int answer = 0;

            //For-loop used to randomly generate a digit between 1-6, will then populate the randomNumber
            //data structure and answer variable to build the random number
            for (int i = 0; i < 4; i++) {
                int digit = random.Next(1, 7);
                randomNumber[i] = digit;
                answer = (answer * 10) + digit;
            }

            //Initialize boolean variable to determine if the user guessed the random number correctly
            bool guessedNumber = false;

            //While loop used to allow player to guess 10 times, decrementing the number of attempts after
            //each incorrect attempt
            while (attempts > 0) {

                //Prompt user to guess the number
                Console.Write("What number am I guessing? ");

                //Create input variable that stores the user's input
                string input = Console.ReadLine();

                //If the input is invalid or is not an integer, warn user to try again until the input is valid
                if (input.Length != 4 || !input.All(char.IsDigit)) {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                //Once the input is valid, convert the input into a character array, then convert that array into
                //an integer array; this data structure will be used to compare values at respective positions
                int[] guessedInput = Array.ConvertAll(input.ToCharArray(), c => (int)Char.GetNumericValue(c));

                //Initialize boolean variable indicating if the guess is correct
                bool isCorrect = true;

                //Iterate over the guessed input and random number array and compare values
                for (int i = 0; i < 4; i++) {
                    //If there is a mismatch between the input and the random number at any given index,
                    //break the loop and set the boolean variable to false
                    if (guessedInput[i] != randomNumber[i]) {
                        isCorrect = false;
                        break;
                    }
                }

                //If the guess was correct, indicate to the player that their answer was correct and end the game.
                if (isCorrect) {
                    Console.WriteLine($"You are correct! The number was {answer}.");
                    guessedNumber = true;
                    break;
                }
                else {
                    //If the guess was incorrect, reduce the number of attempts
                    attempts--;

                    //Call helper function that will print out a hint to the player of the digits guessed correctly
                    PrintHint(randomNumber, guessedInput);

                    //Let the player know how many attempts are left if there are any attempts left
                    if (attempts != 0) {
                        Console.WriteLine($"You have {attempts} attempt(s) remaining.");
                    }
                }
            }

            //If the number was never guessed correctly, reveal the answer to the user
            if (!guessedNumber) {
                Console.WriteLine($"You ran out of attempts! The answer was {answer}.");
            }

            //Prompt user to make a choice if they want to play the Mastermind game again
            do {
                Console.Write("Do you want to play again? (Y/N) ");

                //Initialize variable to store user response
                string choice  = Console.ReadLine().Trim().ToLower();

                //If response is invalid, continue to run the prompt, otherwise return boolean indicating
                //user choice
                if (choice != "y" && choice != "n") {
                    continue;
                } else if (choice == "y") {
                    return true;
                } else {
                    return false;
                }
            } while (true);

        }

        //Helper function that prints a hint to the player
        static void PrintHint(int[] randomNumber, int[] guessedInput) {

            //Initialize array that stores the random answer, will be used to mark off correct digits and positions
            int[] answerMarker = new int[4];
            randomNumber.CopyTo(answerMarker, 0);

            //Intitialize array that stores the user input, will be used to mark off correct guesses
            int[] guessMarker = new int[4];
            guessedInput.CopyTo(guessMarker, 0);

            //Iterate over the arrays marking correct guesses & valid digits and positions
            for (int i = 0; i < 4; i++) {
                //If the player guessed a digit in the correct position, the console will print a plus sign
                if (answerMarker[i] == guessMarker[i]) {
                    Console.Write("+");

                    //Mark off the positions where there is a correct position
                    answerMarker[i] = 0;
                    guessMarker[i] = 0;
                }
            }

            //Iterate over the array again to identify correct guessing of digits in invalid positions
            for (int i = 0; i < 4; i++) {

                //Only consider positions where the user did not guess correctly
                if (guessMarker[i] != 0) {

                    //Initialize index variable that indicates if a user's guessed digit exists in the random number
                    int index = Array.IndexOf(answerMarker, guessMarker[i]);

                    //If the guessed digit does exist, the console will print a minus sign
                    if (index != -1) {
                        Console.Write("-");

                        //Mark off the positions where there exists a valid digit
                        answerMarker[index] = 0;
                        guessMarker[i] = 0;
                    }
                }
            }
            //Write a new line after having displayed the hint to the user
            Console.WriteLine();
        }
    }
}
