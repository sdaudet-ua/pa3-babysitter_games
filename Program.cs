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
            energyPoints[1] = energyPoints[0];
            int playGame = 1;
            int exit = 0;
            while (energyPoints[1] < 300 && energyPoints[0] > 0 && exit != 1)
            {            
                string lastPlayer = "user";
                int currentPlayer = 0;
                while (playGame != 0)
                {
                    int stickQuantity = GetQuantity("stick");
                    int initialStickQuantity = stickQuantity;
                    while (stickQuantity > 0)
                    {
                        while (currentPlayer == 0 && stickQuantity > 0)
                        {
                            int pickupQuantity = GetQuantity("pickup");
                            lastPlayer = "user";
                            score[0] += pickupQuantity;
                            energyPoints[1] -= pickupQuantity;
                            stickQuantity -= pickupQuantity;
                            currentPlayer = 1;
                        }
                        while (currentPlayer == 1 && stickQuantity >0)
                        {
                            int pickupQuantity = ComputerPickupQuantity(stickQuantity);
                            lastPlayer = "computer";
                            score[1] += pickupQuantity;
                            energyPoints[1] += pickupQuantity;
                            Console.WriteLine($"The computer picked up {pickupQuantity} sticks.");
                            stickQuantity -= pickupQuantity;

                            currentPlayer = 0;
                        }

                    }
                    switch(lastPlayer)
                    {
                        //If computer loses, user's score is subtracted from EP, and user wins increases by 1.
                        case "computer":
                        Console.WriteLine($"Computer Loses, there were {initialStickQuantity} sticks in this round.");
                        energyPoints[0] -= score[0];
                        wins[0]++;
                        break;

                        //If user loses, computer's score is added to EP, and computer wins increases by 1.
                        case "user":
                        Console.WriteLine($"User Loses, there were {initialStickQuantity} sticks in this round.");
                        energyPoints[0] += score[1];
                        wins[1]++;
                        break;

                        default:
                        Console.WriteLine("An unexpected error occurred in the lastPlayer switch.");
                        break;
                    }
                    games[0]++;
                    Console.WriteLine("Would you like to play this game again? \n 0:   No\n 1:   Yes");
                    string keepPlaying = Console.ReadLine();
                    if (int.Parse(keepPlaying) == 0)
                    {
                        playGame = 0;
                    }
                    energyPoints[1] = energyPoints[0];
                    score[0] = 0;
                    score[1] = 0;
                }
                Console.WriteLine($"There are {energyPoints[0]} Energy Points left! \n\n Press any key to return to the main menu!");
                Console.ReadKey();
                Console.Clear();
                exit = 1;
            }
        }
        static int GetQuantity(string item)
        {
            int quantity = 0;
            int valid = 0;
            switch(item)
            {
                case "stick":
                while (valid == 0)
                {
                    Console.Write("How many sticks would you like to place in the pile? (Between 20 and 100)(R for Random Choice):     ");
                    string userInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(userInput))
                    {
                        if (userInput.ToUpper() == "R")
                        {
                            Random randomNumber = new Random();
                            quantity = randomNumber.Next(80);
                            quantity += 20;
                        }
                        else if (int.TryParse(userInput, out quantity))
                        {
                            quantity = int.Parse(userInput);
                            if (quantity <= 100 && quantity >= 20)
                            {
                                valid = 1;
                            }
                            else
                            {
                                Console.WriteLine("That number is not inside the allowed range, please try again.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input");
                        }
                        if (quantity <= 100 && quantity >= 20)
                        {
                            valid = 1;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No input detected, please try again.");
                    }
                    
                }
                Console.Clear();
                break;

                case "pickup":
                while (valid == 0)
                {
                    Console.Write("How many sticks would you like to pick up from the pile? (1, 2, or 3):     ");
                    string userInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(userInput))
                    {
                        if (int.TryParse(userInput, out quantity))
                        {
                            quantity = int.Parse(userInput);              
                            if (quantity <= 3 && quantity >= 1)
                            {
                                valid = 1;
                            }
                            else
                            {
                                Console.WriteLine("That number is not inside the allowed range, please try again.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Input must be numeric, please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No input detected, please try again.");
                    }
                }
                break;
            }
            return quantity;
        }
        static int ComputerPickupQuantity(int stickQuantity)
        {
            int rndChoice = 1;
            if (stickQuantity > 3)
            {
                Random randomNumber= new Random();
                rndChoice = randomNumber.Next(3);
                rndChoice++;
            }
            else if (stickQuantity == 3)
            {
                Random randomNumber= new Random();
                rndChoice = randomNumber.Next(2);
                rndChoice++;
            }
            else if (stickQuantity == 2)
            {
                rndChoice = 1;
            }
            return rndChoice;
        }
        static void MotherMayI(int[] energyPoints,int[] score,int[] wins,int[] games)
        {

        }
        static void Scoreboard(int[] energyPoints,int[] score,int[] wins,int[] games)
        {
            Console.Clear();
            if (energyPoints[0] <= 200)
            {
                Console.WriteLine($"The babysitter has expended {200-energyPoints[0]} Emergy Points!");
            }
            else
            {
                Console.WriteLine($"Oh No! The babysitter has increased the Energy Points by {energyPoints[0]-200}!");
            }
            Console.WriteLine($"The Children have {energyPoints[0]} Energy Points left!");
            Console.WriteLine($"The babysitter has won {wins[0]} games!");
            Console.WriteLine($"The babysitter has lost {wins[1]} games!");
            Console.WriteLine("\n\nPress any key to continue!");
            Console.ReadKey();
            Console.Clear();
        }
        static void ResetGame(int[] energyPoints,int[] score,int[] wins,int[] games)
        {
            Console.Clear();
            Console.Write("Are you sure you want to reset the scoreboard? All progress will be lost. (Y/N): ");
            string userInput = Console.ReadLine().ToUpper();
            switch(userInput)
            {
                case "Y":
                energyPoints[0] = 200;
                score[0] =0;
                score[1] = 0;
                wins[0] = 0;
                wins[1] = 0;
                games[0] = 0;
                Console.WriteLine("Scoreboard has been reset. Press any key to continue.");
                break;

                default: 
                Console.WriteLine("The Scoreboard was not reset based on user input. \n\n Press any key to return to the main menu.");
                break;
            }
            Console.ReadKey();
            Console.Clear();
        }
    }
}
