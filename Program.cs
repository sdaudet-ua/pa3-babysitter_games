using System;

namespace fall_2020_starter_code
{
    class Program
    {
        static void Main(string[] args)
        {
            //Priming Read for Sentinel Value (Main Menu)
            int exit = 0;
            int[] energyPoints = new int[2];
            energyPoints[0] = 200;
            int[] score = new int[2];
            int[] wins = new int[2];
            while (exit == 0)
            {
                int userChoice = ChooseProgram();
                //This is the selection block for the main menu.  
                switch(userChoice)
                {
                    case 1:
                    PickUpSticks(energyPoints,score,wins);
                    break;

                    case 2:
                    MotherMayI();
                    break;

                    case 3:
                    Scoreboard();
                    break;

                    case 4:
                    ResetGame();
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
            Console.WriteLine("1:   Play Pick Up Sticks");
            Console.WriteLine("2:   Play Mother, May I");
            Console.WriteLine("3:   View Scoreboard");
            Console.WriteLine("4:   Reset Scoreboard");
            Console.WriteLine("5:   Exit Game");
            return int.Parse(Console.ReadLine());
        }   
        static void PickUpSticks(int[] energyPoints,int[] score,int[] wins)
        {
            energyPoints[1] = energyPoints[0];
            while (energyPoints[1] < 300 && energyPoints[0] > 0)
            {
                int playGame = 1;
                string lastPlayer = "user";
                int currentPlayer = 0;
                while (playGame == 1)
                {
                    int stickQuantity = GetQuantity("stick");
                    while (stickQuantity > 0)
                    {
                        while (currentPlayer == 0)
                        {
                            int pickupQuantity = GetQuantity("pickup");
                            stickQuantity -= pickupQuantity;
                            score[0] += pickupQuantity;
                            energyPoints[1] -= pickupQuantity;
                            lastPlayer = "user";
                            currentPlayer = 1;
                        }
                        while (currentPlayer == 1)
                        {
                            int pickupQuantity = ComputerPickupQuantity(stickQuantity);
                        }

                    }
                }
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
                    Console.Write("How many sticks would you like to place in the pile? (Between 20 and 100):     ");
                    quantity = int.Parse(Console.ReadLine());
                    if (quantity <= 100 && quantity >= 20)
                    {
                        valid = 1;
                    }
                    else
                    {
                        Console.WriteLine("That number is not inside the allowed range, please try again.");
                    }
                }
                break;

                case "pickup":
                while (valid == 0)
                {
                    Console.Write("How many sticks would you like to pick up from the pile? (1, 2, or 3):     ");
                    quantity = int.Parse(Console.ReadLine());
                    if (quantity <= 3 && quantity >= 1)
                    {
                        valid = 1;
                    }
                    else
                    {
                        Console.WriteLine("That number is not inside the allowed range, please try again.");
                    }
                }
                break;
            }
            Console.Clear();
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
        static void MotherMayI()
        {

        }
        static void Scoreboard()
        {

        }
        static void ResetGame()
        {

        }
    }
}
