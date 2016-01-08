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
            bool runProg = true;
            decimal userMoney;
            CDrawer window = new CDrawer();
            do
            {
                decimal[,] cashAndCoins;
                window.Clear();
                Console.Clear();
                Console.WriteLine("\t\tCMPE 1700 Money Calculator\n\n");
                userMoney = getMoney();
                cashAndCoins = calcMoney(userMoney);
                Console.WriteLine("Now displaying {0:C}", userMoney);
                drawMoney(window, userMoney, cashAndCoins);
                for (int i = 0; i < cashAndCoins.GetLength(1); ++i)
                    Console.WriteLine(cashAndCoins[1, i]);
                Console.Write("Press Enter to run again, or any other key to exit.");
                if (!(Console.ReadKey().Key.Equals(ConsoleKey.Enter)))
                    runProg = false;
            } while (runProg);

        }
        static public decimal getMoney()
        {
            decimal retVal = 0;
            string userVal;
            bool error = false;

            do
            {
                retVal = 0;
                Console.Write("Enter the amount of currency to calculate: ");
                userVal = Console.ReadLine().Replace('$', ' ').Trim();
                error = false;
                try
                {
                    retVal = decimal.Parse(userVal);
                }
                catch
                {
                    error = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nError. Please enter a valid currency amount.");
                    Console.ResetColor();
                }
                if (retVal < 0 || retVal > 9999999)
                {
                    error = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nError. Please enter a valid currency amount.");
                    Console.ResetColor();
                }
            } while (error);
            return retVal;
        }
        static public decimal[,] calcMoney(decimal cashVal)
        {
            decimal[,] currVals = new decimal[2, 10];
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
            return currVals;
        }
        static public void drawMoney(CDrawer window, decimal cashVal, decimal[,] currVals)
        {
            string cashTotal = string.Format("{0:C}", cashVal);
            window.AddText(cashTotal, 24, 400, 50, 0, 0, Color.Yellow);
            Color[] colours = { Color.Firebrick, Color.LightGreen, Color.Magenta, Color.LightSkyBlue, Color.Silver, Color.Gold, Color.Silver, Color.Silver, Color.Silver, Color.Brown};
            for (int i = 0, x = 100, y = 100; i < currVals.GetLength(1); ++i)
            {
                if (y > 500 && x != 500)
                {
                    y = 100;
                    x = 500;
                }
                if (currVals[1, i] > 0)
                {
                    if (i <= 3)
                    {
                        window.AddRectangle(x, y, 180, 80, colours[i], 1, Color.Beige);
                        window.AddText("$" + ((currVals[0,i] == 0.1m) ? "0.10" : currVals[0,i].ToString()) + " x " + currVals[1, i].ToString(), 14, x + 90, y + 40, 0, 0, Color.Black);
                        y += 90;
                    }
                    else
                    {
                        window.AddEllipse(x + 45, y, 85, 85, colours[i], 1, Color.Beige);
                        if (currVals[0, i] == 2) window.AddEllipse(x + 63, y+20, 47, 47, Color.Gold);
                        window.AddText("$" + ((currVals[0,i] == 0.1m) ? "0.10" : currVals[0,i].ToString()) + " x " + currVals[1, i].ToString(), 14, x + 85, y + 42, 0, 0, Color.Black);
                        y += 90;
                    }
                    
                }
            }
        }

    }
}