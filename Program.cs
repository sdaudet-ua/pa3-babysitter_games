using System;

namespace fall_2020_starter_code
{
    class Program
    {
        static void Main(string[] args)
        {
            //Priming Read for Sentinel Value (Main Menu)
            Console.Clear();
            int exit = 0;
            int[] energyPoints = new int[2];
            energyPoints[0] = 200;
            int[] score = new int[2];
            int[] wins = new int[2];
            int[] games = {0};
            while (exit == 0)
            {
                int userChoice = ChooseProgram();
                //This is the selection block for the main menu.  
                switch(userChoice)
                {
                    case 1:
                    PickUpSticks(energyPoints,score,wins,games);
                    break;

                    case 2:
                    MotherMayI(energyPoints,score,wins,games);
                    break;

                    case 3:
                    Scoreboard(energyPoints,score,wins,games);
                    break;

                    case 4:
                    ResetGame(energyPoints,score,wins,games);
                    break;

                    case 5:
                    exit=1;
                    break;
                }
            }
        }
        static int ChooseProgram()
        {
            //This method displays the main menu and returns the chosen value to the main method for selection.
            Console.Write("\n\n\n");
            Console.WriteLine("1:   Play Pick Up Sticks");
            Console.WriteLine("2:   Play Mother, May I");
            Console.WriteLine("3:   View Scoreboard");
            Console.WriteLine("4:   Reset Scoreboard");
            Console.WriteLine("5:   Exit Game\n\n");
            return int.Parse(Console.ReadLine());
        }
        static void PickUpSticks(int[] energyPoints,int[] score,int[] wins,int[] games)
        {
            Console.Clear();
            //Set the in-game EP counter to the global EP value. EPs are separated so that the loser's score does not affect the global EP. 
            energyPoints[1] = energyPoints[0];
            //Priming statements for exit loops. 
            int playGame = 1;
            int exit = 0;
            //While the EP is within bounds and the user does not desire an exit, this loop will run. 
            while (energyPoints[1] < 300 && energyPoints[0] > 0 && exit != 1)
            {            
                string lastPlayer = "user";
                int currentPlayer = 0;
                //While loop for actual gameplay. 
                while (playGame != 0)
                {
                    //Read for how many sticks should be in the pile. 
                    int stickQuantity = GetQuantity("stick");
                    //Recording initial stick quantity for this game, so the quantity can be displayed at the end.
                    int initialStickQuantity = stickQuantity;
                    //This loop will run until there are no more sticks in the pile. 
                    while (stickQuantity > 0)
                    {
                        while (currentPlayer == 0 && stickQuantity > 0)
                        {
                            //User will decide how many sticks to pick up, which will affect stickQuantity, in-game EP, and in-game score.
                            int pickupQuantity = GetQuantity("pickup");
                            lastPlayer = "user";
                            score[0] += pickupQuantity;
                            energyPoints[1] -= pickupQuantity;
                            stickQuantity -= pickupQuantity;
                            //Change to computer turn.
                            currentPlayer = 1;
                        }
                        while (currentPlayer == 1 && stickQuantity >0)
                        {
                            //Computer will pick a number at random between 1-3, which will affect stickQuantity, in-game EP, and in-game score.
                            int pickupQuantity = ComputerPickupQuantity(stickQuantity);
                            lastPlayer = "computer";
                            score[1] += pickupQuantity;
                            energyPoints[1] += pickupQuantity;
                            //The console will display how many sticks the computer picked up, so the user knows what they are playing against. 
                            Console.WriteLine($"The computer picked up {pickupQuantity} sticks.");
                            stickQuantity -= pickupQuantity;
                            //Change back to user turn. 
                            currentPlayer = 0;
                        }

                    }
                    switch(lastPlayer)
                    {
                        //If computer loses, user's score is subtracted from EP, and user wins increases by 1.
                        case "computer":
                        Console.WriteLine($"Computer Loses, there were {initialStickQuantity} sticks in this round.\n\nThe babysitter picked up {score[0]} sticks, and the kids picked up {score[1]} sticks. \n");
                        energyPoints[0] -= score[0];
                        wins[0]++;
                        break;

                        //If user loses, computer's score is added to EP, and computer wins increases by 1.
                        case "user":
                        Console.WriteLine($"User Loses, there were {initialStickQuantity} sticks in this round.");
                        energyPoints[0] += score[1];
                        wins[1]++;
                        break;

                        //Catch-all just in case something goes wrong.
                        default:
                        Console.WriteLine("An unexpected error occurred in the lastPlayer switch.");
                        break;
                    }
                    //Once game is over, add to game counter, ask if user wants to play again. 
                    games[0]++;
                    Console.WriteLine("Would you like to play this game again? \n 0:   No\n 1:   Yes");
                    string keepPlaying = Console.ReadLine();
                    //If the user's input is not blank, it will process the input. 
                    if (!string.IsNullOrEmpty(keepPlaying))
                    {
                        //If the user enters 0 to not play anymore, the playGame while loop will end. 
                        if (int.Parse(keepPlaying) == 0)
                        {
                            playGame = 0;
                        }
                        //If the user enters 1 or any other input, the game will restart. 
                        else
                        {
                            energyPoints[1] = energyPoints[0];
                            score[0] = 0;
                            score[1] = 0;
                        }
                    }
                    //If the user's input is blank, the system will restart this game. 
                    else
                    {
                        energyPoints[1] = energyPoints[0];
                        score[0] = 0;
                        score[1] = 0;
                    }
                }
                //If the user decided to exit, he/she will see the current EP value before returning to the main menu. 
                Console.WriteLine($"There are {energyPoints[0]} Energy Points left! \n\n Press any key to return to the main menu!");
                Console.ReadKey();
                Console.Clear();
                exit = 1;
            }
            //If the EP loop ends from being over the max EP, the system will display game stats and offer the option to exit. 
            if (energyPoints[1] >= 300)
            {
                games[0]++;
                Console.Write($"The kids(computer) have won! \n\n {games[0]} games were played.\nThe babysitter won {wins[0]} games and lost {wins[1]} games.");
                Console.Write("\n\nTo exit, press E. To reset the game and start over, press any other key. ");
                string userInput = Console.ReadLine();
                //If user desires an exit, this if statement will end the program. 
                if (userInput.ToUpper() == "E")
                {
                    Console.Clear();
                    Console.WriteLine("Thank you for playing.");
                    Environment.Exit(0);
                }
                //Resetting scoreboard for a new set of  games. 
                else
                {
                    energyPoints[0] = 200;
                    score[0] =0;
                    score[1] = 0;
                    wins[0] = 0;
                    wins[1] = 0;
                    games[0] = 0;
                }
            }
            //If the EP loop ends from being under the minimum EP, the system will display game stats and offer the option to exit. 
            else if (energyPoints[1] <= 0)
            {
                games[0]++;
                Console.Write($"The babysitter won! \n\n {games[0]} games were played.\nThe babysitter won {wins[0]} games and lost {wins[1]} games.");
                Console.Write("\n\nTo exit, press E. To reset the game and start over, press any other key. ");
                string userInput = Console.ReadLine();
                //If user desires an exit, this if statement will end the program. 
                if (userInput.ToUpper() == "E")
                {
                    Console.Clear();
                    Console.WriteLine("Thank you for playing.");
                    Environment.Exit(0);
                }
                //Resetting scoreboard for a new set of  games.
                else
                {
                    energyPoints[0] = 200;
                    score[0] =0;
                    score[1] = 0;
                    wins[0] = 0;
                    wins[1] = 0;
                    games[0] = 0;
                }
            }
            //If the loop ended from user input, not from an EP value, Clear the console and proceed back to the main menu. 
            else if (exit == 1)
            {
                Console.Clear();
            }
        }
        static int GetQuantity(string item)
        {
            //This method is responsible for retrieving user inputs for quantities in PickUpSticks()
            int quantity = 0;
            int valid = 0;
            //This switch determines, based on the argument provided, if the initial stick quantity is needed or of the pickup quantity is needed. 
            switch(item)
            {
                //This case is for getting the initial quantity of sticks to be placed in the pile. 
                case "stick":
                //This loop ensures that the input is valid before returning the input to the game. 
                while (valid == 0)
                {
                    Console.Write("How many sticks would you like to place in the pile? (Between 20 and 100)(R for Random Choice):     ");
                    string userInput = Console.ReadLine();
                    //Error check: did user enter a blank value. 
                    if (!string.IsNullOrEmpty(userInput))
                    {
                        //If user entered "R", the system will randomly choose a quantity between 20 and 100. 
                        if (userInput.ToUpper() == "R")
                        {
                            Random randomNumber = new Random();
                            //Range of possible choices is only 80. (100-20)
                            quantity = randomNumber.Next(80);
                            //Lowest quantity can be 20, not 0. 
                            quantity += 20;
                        }
                        //Check if the input was a number.
                        else if (int.TryParse(userInput, out quantity))
                        {
                            //If input is numerical, check the integer to ensure it is within the desired range. 
                            quantity = int.Parse(userInput);
                            if (quantity <= 100 && quantity >= 20)
                            {
                                //If valid, the loop stops and the value is returned. 
                                valid = 1;
                            }
                            //Message for out of range input. 
                            else
                            {
                                Console.WriteLine("That number is not inside the allowed range, please try again.");
                            }
                        }
                        //If the input is not R, and not numerical, notify user that the input was invalid and restart loop to get new input. 
                        else
                        {
                            Console.WriteLine("Invalid Input");
                        }
                        //This checks to verify that the computer's random value is within range. 
                        if (quantity <= 100 && quantity >= 20)
                        {
                            valid = 1;
                        }
                    }
                    //If the user's input is blank, this will notify the user of the mistake and restart the loop to get new input. 
                    else
                    {
                        Console.WriteLine("No input detected, please try again.");
                    }
                    
                }
                //Clear Console to hide the number of sticks in the pile, and fall out of the switch to return the quantity. 
                Console.Clear();
                break;

                //This case is for retrieving the pickup quantity. 
                case "pickup":
                //This loop ensures the input is valid before returning it to the game. 
                while (valid == 0)
                {
                    Console.Write("How many sticks would you like to pick up from the pile? (1, 2, or 3):     ");
                    string userInput = Console.ReadLine();
                    //If user input is not blank, the system will process the input. 
                    if (!string.IsNullOrEmpty(userInput))
                    {
                        //Error check: user input must be numeric. 
                        if (int.TryParse(userInput, out quantity))
                        {
                            //If input is numerical, ensure it is within the allowed range. 
                            quantity = int.Parse(userInput);
                            if (quantity <= 3 && quantity >= 1)
                            {
                                //If input is 1, 2, or 3, the system will end the validation loop and return the value to the game.  
                                valid = 1;
                            }
                            //If the input is out of range, notify user and restart loop for new input.
                            else
                            {
                                Console.WriteLine("That number is not inside the allowed range, please try again.");
                            }
                        }
                        //If user input is not blank, but is not numeric, notify user and restart loop for new input. 
                        else
                        {
                            Console.WriteLine("Input must be numeric, please try again.");
                        }
                    }
                    //If user input is blank, notify user and restart loop for new input. 
                    else
                    {
                        Console.WriteLine("No input detected, please try again.");
                    }
                }
                //Fall out of switch to return quantity to game. 
                break;
            }
            return quantity;
        }
        static int ComputerPickupQuantity(int stickQuantity)
        {
            int rndChoice = 1;
            //If there are 4 or more sticks in the pile, computer will roll normally. 
            if (stickQuantity > 3)
            {
                Random randomNumber= new Random();
                rndChoice = randomNumber.Next(3);
                rndChoice++;
            }
            //If there are 3 sticks in the pile, computer will choose a maximum of 2.
            else if (stickQuantity == 3)
            {
                Random randomNumber= new Random();
                rndChoice = randomNumber.Next(2);
                rndChoice++;
            }
            //If there are 2 sticks, the computer will pick up only 1 stick. 
            else if (stickQuantity == 2)
            {
                rndChoice = 1;
            }
            //If there is 1 stick, computer has no choice but to lose, and will pick up the last stick. 
            return rndChoice;
        }
        static void MotherMayI(int[] energyPoints,int[] score,int[] wins,int[] games)
        {
            //Set the in-game EP counter to the global EP value. EPs are separated so that the loser's score does not affect the global EP.
            energyPoints[1] = energyPoints[0];
            //Priming statements for exit loops.
            int playGame = 1;
            int exit =0;
            //Array for keeping track of players' distance to mother. 
            int[] distanceToMother = {0,0};
            int currentPlayer = 0;
            while (energyPoints[1] > 0 && energyPoints[1] < 300 && exit != 1)
            {
                int playAgain=1;
                while (playAgain != 0)
                {
                    while (distanceToMother[0] <= 21 && distanceToMother[1] <= 21 && playGame != 0)
                    {
                        //Reset to player 1 for playing another round. 
                        currentPlayer = 0;
                        Console.Clear();
                        while (currentPlayer == 0 && distanceToMother[0] <= 21 && distanceToMother[1] <= 21)
                        {
                            Console.WriteLine("User's Turn!");
                            //This "final" variable is here to let the user settle on his or her score before moving to the computer's turn. 
                            int final = 0;
                            int[] roll = new int[2];
                            roll[0] = RollDie(10);
                            roll[1] = RollDie(10);
                            Console.WriteLine("The computer has rolled two 10-sided die on your behalf.");
                            Console.WriteLine($"Roll one was {roll[0]} and roll two was {roll[1]}.");
                            int totalRoll = roll[0] + roll[1];
                            distanceToMother[0] = distanceToMother[0] + totalRoll;
                            Console.WriteLine($"\nYou have taken {distanceToMother[0]} steps.\n");
                            energyPoints[1] -= totalRoll;
                            while (distanceToMother[0] <= 21 && final != 1)
                            {
                                Console.Write("Would you like to roll again? (Using a 6-sided die) (Y/N):  ");
                                string rollAgain = Console.ReadLine();
                                //If the input is not blank, process the input. 
                                if (!string.IsNullOrEmpty(rollAgain))
                                {
                                    //Change input to uppercase and process it in the switch.
                                    switch (rollAgain.ToUpper())
                                    {
                                        //If user wants to roll again, this will roll a 6-sided die and add it to distanceToMother.
                                        case "Y":
                                        int newRoll = RollDie(6);
                                        Console.WriteLine($"Your additional roll was {newRoll}.");
                                        distanceToMother[0] += newRoll;
                                        energyPoints[1] -= newRoll;
                                        Console.WriteLine($"\nYou have taken {distanceToMother[0]} total steps.\n");

                                        break;

                                        //If the user does not want to roll again, he/she has settled on his/her score and final is set to 1.
                                        case "N":
                                        final = 1;
                                        break;

                                        //If the user enters something invalid, notify the user and restart loop to get new input. 
                                        default:
                                        Console.WriteLine("Invalid Input, please try again.");
                                        break;
                                    }
                                }
                                //If no input is detected, notify user and restart loop to get new input. 
                                else
                                {
                                    Console.WriteLine("No input detected, please try again.");
                                }
                            }
                            currentPlayer = 1;
                        }
                        while (currentPlayer == 1 && distanceToMother[0] <= 21 && distanceToMother[1] <= 21)
                        {
                            Console.WriteLine("Computer's turn!");
                            //Array for holding the number of rolls for this player. 
                            int[] roll = new int[2];
                            roll[0] = RollDie(10);
                            roll[1] = RollDie(10);
                            int totalRoll = roll[0] + roll[1];
                            distanceToMother[1] += totalRoll;
                            energyPoints[1] += totalRoll;
                            //Computer will continue to roll until it reaches 17 or more. 
                            while (distanceToMother[1] <= 17)
                            {
                                int newRoll = RollDie(6);
                                distanceToMother[1] += newRoll;
                                energyPoints[1] += newRoll;
                            }
                            //The currentPlayer value goes to 3 so that the user does not get another turn, but also so that we can fall out of the computer turn while loop. 
                            currentPlayer = 3;
                            playGame = 0;
                        }
                    }
                    // Console.WriteLine($"{energyPoints[1]} + {distanceToMother[0]} + {distanceToMother[1]}");
                    games[0]++;
                    Console.Clear();
                    if (distanceToMother[1] > 21)
                    {
                        //If computer busts, user's score is subtracted from EP, and user wins increases by 1.
                        Console.WriteLine($"Computer Busted");
                        Console.WriteLine($"The user took {distanceToMother[0]} steps and is the winner!");
                        energyPoints[0] -= distanceToMother[0];
                        wins[0]++;
                    }
                    else if (distanceToMother[0] > 21)
                    {
                        //If user busts, user's score is added to EP, and computer wins increases by 1.
                        Console.WriteLine($"User Busted.");
                        Console.WriteLine($"You took {distanceToMother[0]} steps.");
                        energyPoints[0] += distanceToMother[0];
                        wins[1]++;
                    }
                    else if (distanceToMother[0] > distanceToMother[1])
                    {
                        //If user wins, user's score is subtracted from EP, and user wins increases by 1.
                        Console.WriteLine("User wins!");
                        Console.WriteLine($"The user took {distanceToMother[0]} steps, and the computer took {distanceToMother[1]} steps!");
                        energyPoints[0] -= distanceToMother[0];
                        wins[0]++;
                    }
                    else if (distanceToMother[1] > distanceToMother[0])
                    {
                        //If computer wins, computer's score is added to EP, and computer wins increases by 1.
                        Console.WriteLine("Computer Wins!");
                        Console.WriteLine($"The user took {distanceToMother[0]} steps, and the computer took {distanceToMother[1]} steps!");
                        energyPoints[0] += distanceToMother[1];
                        wins[1]++;
                    }
                    else if (distanceToMother[1] == distanceToMother[0])
                    {
                        //If players tie, computer's score is added to EP, and computer wins increases by 1.
                        Console.WriteLine("Tie! Computer Wins!");
                        Console.WriteLine($"Both players took {distanceToMother[0]} steps!");
                        energyPoints[0] += distanceToMother[1];
                        wins[1]++;
                    }
                    else
                    {
                        //Catch-all just in case something goes wrong.
                        Console.WriteLine("An unexpected error occurred in the lastPlayer switch.");
                    }
                    Console.WriteLine("Would you like to play this game again? \n 0:   No\n 1:   Yes");
                    string keepPlaying = Console.ReadLine();
                    //If the user's input is not blank, it will process the input. 
                    if (!string.IsNullOrEmpty(keepPlaying))
                    {
                        //If the user enters 0 to not play anymore, the playGame while loop will end. 
                        if (int.Parse(keepPlaying) == 0)
                        {
                            playAgain = 0;
                        }
                        //If the user enters 1 or any other input, the game will restart. 
                        else
                        {
                            energyPoints[1] = energyPoints[0];
                            distanceToMother[0] = 0;
                            distanceToMother[1] = 0;
                            playGame = 1;
                        }
                    }
                    //If the user's input is blank, the system will restart this game. 
                    else
                    {
                        energyPoints[1] = energyPoints[0];
                        distanceToMother[0] = 0;
                        distanceToMother[1] = 0;
                        playGame = 1;
                    }
                }
                //If the user decided to exit, he/she will see the current EP value before returning to the main menu. 
                Console.WriteLine($"There are {energyPoints[0]} Energy Points left! \n\n Press any key to return to the main menu!");
                Console.ReadKey();
                Console.Clear();
                exit = 1;
            }
            if (energyPoints[1] >= 300)
            {
                games[0]++;
                Console.Write($"The kids(computer) have won! \n\n {games[0]} games were played.\nThe babysitter won {wins[0]} games and lost {wins[1]} games.");
                Console.Write("\n\nTo exit, press E. To reset the game and start over, press any other key. ");
                string userInput = Console.ReadLine();
                //If user desires an exit, this if statement will end the program. 
                if (userInput.ToUpper() == "E")
                {
                    Console.Clear();
                    Console.WriteLine("Thank you for playing.");
                    Environment.Exit(0);
                }
                //Resetting scoreboard for a new set of  games. 
                else
                {
                    energyPoints[0] = 200;
                    score[0] =0;
                    score[1] = 0;
                    wins[0] = 0;
                    wins[1] = 0;
                    games[0] = 0;
                }
            }
            //If the EP loop ends from being under the minimum EP, the system will display game stats and offer the option to exit. 
            else if (energyPoints[1] <= 0)
            {
                games[0]++;
                Console.Write($"The babysitter won! \n\n {games[0]} games were played.\nThe babysitter won {wins[0]} games and lost {wins[1]} games.");
                Console.Write("\n\nTo exit, press E. To reset the game and start over, press any other key. ");
                string userInput = Console.ReadLine();
                //If user desires an exit, this if statement will end the program. 
                if (userInput.ToUpper() == "E")
                {
                    Console.Clear();
                    Console.WriteLine("Thank you for playing.");
                    Environment.Exit(0);
                }
                //Resetting scoreboard for a new set of  games.
                else
                {
                    energyPoints[0] = 200;
                    score[0] =0;
                    score[1] = 0;
                    wins[0] = 0;
                    wins[1] = 0;
                    games[0] = 0;
                }
            }
            //If the loop ended from user input, not from an EP value, Clear the console and proceed back to the main menu. 
            else if (exit == 1)
            {
                Console.Clear();
            }
        }
        static int RollDie(int sides)
        {
            //This method is responsible for rolling all of the dice in the game. When called, a number of sides must be specified as an argument. 
            //Doing it this way allows one short method to be used, rather than making a method for 10-sided and a different method for 6-sided. 
            Random randomNumber = new Random();
            int result = randomNumber.Next(sides);
            result++;
            return result;
        }
        static void Scoreboard(int[] energyPoints,int[] score,int[] wins,int[] games)
        {
            //The scoreboard will display global values: energyPoints[0], wins[0,1], and games[0].
            Console.Clear();
            //This selection statement decides how to word the first sentence based on the value of energyPoints[0]
            if (energyPoints[0] <= 200)
            {
                Console.WriteLine($"The babysitter has expended {200-energyPoints[0]} Emergy Points!");
            }
            else
            {
                Console.WriteLine($"Oh No! The babysitter has increased the Energy Points by {energyPoints[0]-200}!");
            }
            //Remainder of WriteLines are just presenting data. 
            Console.WriteLine($"The Children have {energyPoints[0]} Energy Points left!");
            Console.WriteLine($"The babysitter has won {wins[0]} games!");
            Console.WriteLine($"The babysitter has lost {wins[1]} games!");
            //When the user is ready, he/she can press any key to return to the main menu. 
            Console.WriteLine("\n\nPress any key to continue!");
            Console.ReadKey();
            Console.Clear();
        }
        static void ResetGame(int[] energyPoints,int[] score,int[] wins,int[] games)
        {
            //This method is responsible for resetting the global score values. 
            Console.Clear();
            //Before resetting the score, the system confirms with the user that it is a purposeful action. Prevents accidental scoreboaerd resetting. 
            Console.Write("Are you sure you want to reset the scoreboard? All progress will be lost. (Y/N): ");
            string userInput = Console.ReadLine().ToUpper();
            switch(userInput)
            {
                //Only if the user enters a "Y", will the scoreboard reset.
                case "Y":
                energyPoints[0] = 200;
                score[0] =0;
                score[1] = 0;
                wins[0] = 0;
                wins[1] = 0;
                games[0] = 0;
                Console.WriteLine("Scoreboard has been reset. Press any key to continue.");
                break;

                //If the user does not properly confirm the reset, the game will not reset, and return to the main menu. 
                default: 
                Console.WriteLine("The Scoreboard was not reset based on user input. \n\n Press any key to return to the main menu.");
                break;
            }
            //After the scoreboard is reset, user must press any key to return to the main menu. 
            Console.ReadKey();
            Console.Clear();
        }
    }
}