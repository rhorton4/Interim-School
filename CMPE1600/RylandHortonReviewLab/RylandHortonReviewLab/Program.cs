//******************************************************************************************
//Program: CMPE 1600 Cash Calculator
//Date: January 8, 2016
//Author: Ryland Horton
//Class: CMPE 1600
//Section: A01
//Instructor: JD Silver
//*******************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDIDrawer;
using System.Drawing;

namespace RylandHortonReviewLab
{
    class Program
    {
        static void Main(string[] args)
        {
            //declare vars
            bool runProg = true;            // flags to see if program should continue running and restart
            decimal userMoney;              // stores total amount of money user is calcuating
            CDrawer window = new CDrawer(); // window where money is visually drawn
            decimal[,] cashAndCoins = null; // array to store cash/coin amounts and discrete amounts of each for
                                            // a given cash value
            
            //loops if the user wishes to restart and try again.
            do
            {
                //resets all values, if possible
                if (cashAndCoins != null) Array.Clear(cashAndCoins, 1, 10);
                window.Clear();
                Console.Clear();
                Console.WriteLine("\t\tCMPE 1700 Money Calculator\n\n");
                
                // gets input from user, and sends it to be calculated and stored in the array.
                userMoney = getMoney();
                cashAndCoins = calcMoney(userMoney);

                //displays the amount and draws it in GDIDrawer.
                Console.WriteLine("Now displaying {0:C}", userMoney);
                drawMoney(window, userMoney, cashAndCoins);

                //checks to see if user wants to run again, which they can do by pressing the Enter key.
                Console.Write("Press Enter to run again, or any other key to exit.");
                if (!(Console.ReadKey().Key.Equals(ConsoleKey.Enter)))
                    runProg = false;
            } while (runProg);

        }
        //*************************************************************************************************
        //Method: static public decimal getMoney()
        //Purpose: Takes input from user, ensures it is usable input while also filtering spaces and $ symbols.
        //Parameters: None.
        //Returns: decimal - total cash the user wishes to calculate.
        //**************************************************************************************************
        static public decimal getMoney()
        {
            //declare vars
            decimal retVal = 0; // total cash to be returned to program
            string userVal;     // total cash as inputted by user, stored to be formatted
            bool error = false; // flag to loop if inappropriate input is entered.

            //loops while user has not inputted appropriate value.
            do
            {
                //resets value, prompts for input, then formats out excess spaces and $ symbols.
                retVal = 0;
                Console.Write("Enter the amount of currency to calculate: ");
                userVal = Console.ReadLine().Replace('$', ' ').Trim();
                
                //checks for exceptions and tries to format into a decimal.

                error = false;
                try
                {
                    retVal = decimal.Parse(userVal);
                }

                //displays error and loops if exception is caught.
                catch
                {
                    error = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nError. Please enter a valid currency amount.");
                    Console.ResetColor();
                }

                //displays error and loops if value is too high or too low.
                if (retVal < 0 || retVal > 9999999)
                {
                    error = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nError. Please enter a valid currency amount.");
                    Console.ResetColor();
                }

                
            } while (error);
            
            //returns value if all is accepted.
            return retVal;
        }
        //******************************************************************************************
        //Method: static public decimal[,] calcMoney(decimal cashVal)
        //Purpose: Creates an array of the various values of cash and coin for a given number of cash.
        //Parameters: decimal cashVal - the total amount of cash to be divided into denominations
        //Returns: decimal[,] - an array of denominations of cash and coin and the amount of each denomination.
        //******************************************************************************************
        static public decimal[,] calcMoney(decimal cashVal)
        {
            //declare vars
            decimal[,] currVals = new decimal[2, 10]; // array to store amounts

            //creates index 0. This stores the types of denominations.
            currVals[0, 0] = 50;
            currVals[0, 1] = 20;
            currVals[0, 2] = 10;
            currVals[0, 3] = 5;
            currVals[0, 4] = 2;
            currVals[0, 5] = 1;
            currVals[0, 6] = 0.25m;
            currVals[0, 7] = 0.1m;
            currVals[0, 8] = 0.05m;
            currVals[0, 9] = 0.01m;

            //creates index 1, which stores the amount of denominations that the current amount of money
            //can be divided into.
            currVals[1, 0] = Math.Floor(cashVal / 50m);
            currVals[1, 1] = Math.Floor(cashVal % 50m / 20m);
            currVals[1, 2] = Math.Floor(cashVal % 50m % 20m / 10m);
            currVals[1, 3] = Math.Floor(cashVal % 50m % 20m % 10m / 5m);
            currVals[1, 4] = Math.Floor(cashVal % 50m % 20m % 10m % 5m / 2m);
            currVals[1, 5] = Math.Floor(cashVal % 50m % 20m % 10m % 5m % 2m / 1m);
            currVals[1, 6] = Math.Floor(cashVal % 50m % 20m % 10m % 5m % 2m % 1m / 0.25m);
            currVals[1, 7] = Math.Floor(cashVal % 50m % 20m % 10m % 5m % 2m % 1m % 0.25m / 0.10m);
            currVals[1, 8] = Math.Floor(cashVal % 50m % 20m % 10m % 5m % 2m % 1m % 0.25m % 0.10m / 0.05m);
            currVals[1, 9] = Math.Round(cashVal % 50m % 20m % 10m % 5m % 2 % 1m % 0.25m % 0.10m % 0.05m / 0.01m);

            //returns array of denomination amounts and total number of each denomination.
            return currVals;
        }

        //***************************************************************************************************
        //Method: static public void drawMoney(CDrawer window, decimal cashVal, decimal[,] currVals)
        //Purpose: Displays visually the amounts of denominations to the user in GDI Drawer.
        //Parameters: CDrawer window - the GDI Drawer window to be drawn to.
        //            decimal cashVal - the total amount of cash being divided.
        //            decimal[,] currVals - the array of denominations being divided into.
        //Returns: void
        //****************************************************************************************************

        static public void drawMoney(CDrawer window, decimal cashVal, decimal[,] currVals)
        {
            //declare vars
            Color[] colours = { Color.Firebrick, Color.LightGreen, Color.Magenta, Color.LightSkyBlue,
                Color.Silver, Color.Gold, Color.Silver,
                Color.Silver, Color.Silver, Color.Brown }; // array of colours to draw the appropriate denomination.

            //draws total amount at the top.
            window.AddText(cashVal.ToString("C"), 24, 400, 50, 0, 0, Color.Yellow);
            
            //loops to draw each denomination.
            for (int i = 0, x = 100, y = 100; i < currVals.GetLength(1); ++i)
            {
                //checks to make sure drawing won't go off screen. If it might, resets it to the top and
                //offsets to the right.
                if (y > 500 && x != 500)
                {
                    y = 100;
                    x = 500;
                }

                //checks to make sure current denomination type is being used.
                if (currVals[1, i] > 0)
                {
                    //draws large bills.
                    if (i <= 3)
                    {
                        window.AddRectangle(x, y, 180, 80, colours[i], 1, Color.Beige);
                        window.AddText((currVals[0,i].ToString("C")) + " x " + currVals[1, i].ToString(), 14, x + 90, y + 40, 0, 0, Color.Black);
                        y += 90;
                    }
                    //draws coins.
                    else
                    {
                        window.AddEllipse(x + 45, y, 85, 85, colours[i], 1, Color.Beige);
                        if (currVals[0, i] == 2) window.AddEllipse(x + 63, y + 20, 47, 47, Color.Gold);
                        window.AddText((currVals[0, i].ToString("C")) + " x " + currVals[1, i].ToString(), 14, x + 85, y + 42, 0, 0, Color.Black);
                        y += 90;
                    }

                }
            }
        }

    }
}
