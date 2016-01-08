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
            double userMoney;
            CDrawer window = new CDrawer();
            do
            {
                double[,] cashAndCoins;
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
        static public double getMoney()
        {
            double retVal = 0;
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
                    retVal = double.Parse(userVal);
                }
                catch
                {
                    error = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nError. Please enter a valid currency amount.");
                    Console.ResetColor();
                }
                if (retVal < 0)
                {
                    error = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nError. Please enter a valid currency amount.");
                    Console.ResetColor();
                }
                retVal = Math.Round(retVal, 2);
            } while (error);
            return retVal;
        }
        static public double[,] calcMoney(double cashVal)
        {
            double[,] currVals = new double[2, 10];
            currVals[0, 0] = 50;
            currVals[0, 1] = 20;
            currVals[0, 2] = 10;
            currVals[0, 3] = 5;
            currVals[0, 4] = 2;
            currVals[0, 5] = 1;
            currVals[0, 6] = 0.25;
            currVals[0, 7] = 0.1;
            currVals[0, 8] = 0.05;
            currVals[0, 9] = 0.01;
            currVals[1, 0] = (int)cashVal / 50;
            currVals[1, 1] = (int)cashVal % 50 / 20;
            currVals[1, 2] = (int)cashVal % 50 % 20 / 10;
            currVals[1, 3] = (int)cashVal % 50 % 20 % 10 / 5;
            currVals[1, 4] = (int)cashVal % 50 % 20 % 10 % 5 / 2;
            currVals[1, 5] = (int)cashVal % 50 % 20 % 10 % 5 % 2 / 1;
            currVals[1, 6] = Math.Floor(cashVal % 50 % 20 % 10 % 5 % 2 % 1 / 0.25);
            currVals[1, 7] = Math.Floor(cashVal % 50 % 20 % 10 % 5 % 2 % 1 % 0.25 / 0.10);
            currVals[1, 8] = Math.Floor(cashVal % 50 % 20 % 10 % 5 % 2 % 1 % 0.25 % 0.10 / 0.05);
            currVals[1, 9] = Math.Floor(cashVal % 50 % 20 % 10 % 5 % 2 % 1 % 0.25 % 0.10 % 0.05 / 0.01);
            return currVals;
        }
        static public void drawMoney(CDrawer window, double cashVal, double[,] currVals)
        {
            window.AddText("$" + cashVal.ToString(), 24, 400, 50, 0, 0, Color.Yellow);
            for (int i = 0, x = 100, y = 100; i < currVals.GetLength(1); ++i)
            {
                if (y > 450 && x != 500)
                {
                    y = 100;
                    x = 500;
                }
                if (currVals[1, i] > 0)
                {
                    switch (currVals[0, i].ToString())
                    {
                        case "50":
                            if (currVals[0, i] > 0)
                                window.AddRectangle(x, y, 150, 70, Color.Red, 0, null);
                            window.AddText("$50 x " + currVals[1, i].ToString(), 16, x + 35, y + 25, 0, 0, Color.Black);
                            break;
                        case "20":
                            window.AddRectangle(x, y, 150, 70, Color.Green, 0, null);
                            window.AddText("$20 x " + currVals[1, i].ToString(), 16, x + 35, y + 25, 0, 0, Color.Black);
                            break;
                        case "10":
                            window.AddRectangle(x, y, 150, 70, Color.Purple, 0, null);
                            window.AddText("$10 x " + currVals[1, i].ToString(), 16, x + 35, y + 25, 0, 0, Color.Black);
                            break;
                        case "5":
                            window.AddRectangle(x, y, 150, 70, Color.LightSkyBlue, 0, null);
                            window.AddText("$5 x " + currVals[1, i].ToString(), 16, x + 35, y + 25, 0, 0, Color.Black);
                            break;
                        case "2":
                            window.AddEllipse(x, y, 150, 70, Color.Beige, 0, null);
                            window.AddText("$2 x " + currVals[1, i].ToString(), 16, x + 15, y + 15, 0, 0, Color.Black);
                            break;
                        case "1":
                            window.AddEllipse(x, y, 70, 70, Color.Gold, 0, null);
                            window.AddText("$1 x " + currVals[1, i].ToString(), 16, x + 15, y + 15, 0, 0, Color.Black);
                            break;
                        case "0.25":
                            window.AddEllipse(x, y, 70, 70, Color.Silver, 0, null);
                            window.AddText("$0.25 x " + currVals[1, i].ToString(), 16, x + 15, y + 15, 0, 0, Color.Black);
                            break;
                        case "0.1":
                            window.AddEllipse(x, y, 70, 70, Color.Silver, 0, null);
                            window.AddText("$0.10 x " + currVals[1, i].ToString(), 16, x + 15, y + 15, 0, 0, Color.Black);
                            break;
                        case "0.05":
                            window.AddEllipse(x, y, 70, 70, Color.Silver, 0, null);
                            window.AddText("$0.05 x " + currVals[1, i].ToString(), 16, x + 15, y + 15, 0, 0, Color.Black);
                            break;
                        case "0.01":
                            window.AddEllipse(x, y, 70, 70, Color.Brown, 0, null);
                            window.AddText("$0.01 x " + currVals[1, i].ToString(), 16, x + 15, y + 15, 0, 0, Color.Black);
                            break;
                    }
                    y += 85;
                }
            }
        }

    }
}