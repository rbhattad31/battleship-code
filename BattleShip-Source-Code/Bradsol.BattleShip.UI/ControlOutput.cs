﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bradsol.BattleShip.BL.GameLogic;
using Bradsol.BattleShip.BL.Requests;
using Bradsol.BattleShip.BL.Responses;
using Bradsol.BattleShip.BL.Ships;
using System.Threading;
namespace Bradsol.BattleShip.UI
{
    class ControlOutput
    {
        static int counttime = 0;

        public static void ShowFlashScreen()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Welcome to The Battleship Game!!!");
            Console.Write($"{ Environment.NewLine }================================={ Environment.NewLine }");
            Console.ForegroundColor = ConsoleColor.White;

            Timer t = new Timer(ClearFlashScreen, null, 0, 1000);
            Thread.Sleep(2100); // Simulating other work (10 seconds)
            t.Dispose(); // Cancel the timer now
        }

        private static void ClearFlashScreen(Object state)
        {
            if (counttime < 2)
                counttime += 1;
            else
            {
                Console.Clear();
                counttime = 0;
            }
        }

        public static void ShowHeader()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("THE BATTLESHIP GAME!!!");
            Console.Write($"{ Environment.NewLine }======================{ Environment.NewLine }");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ShowAllPlayer(Player[] player)
        {
            string str = "Player 1: " + player[0].Name + "(Win: " + player[0].Win + ")\t Player 2: " + player[1].Name + "(win: " + player[1].Win + ")";

            Console.WriteLine(str);
            Console.WriteLine("");
        }

        public static void ShowWhoseTurn(Player player)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(player.Name + " turn... ");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;

        }

        static string GetLetterFromNumber(int number)
        {
            string result = "";
            switch (number)
            {
                case 1:
                    result = "A";
                    break;
                case 2:
                    result = "B";
                    break;
                case 3:
                    result = "C";
                    break;
                case 4:
                    result = "D";
                    break;
                case 5:
                    result = "E";
                    break;
                case 6:
                    result = "F";
                    break;
                case 7:
                    result = "G";
                    break;
                case 8:
                    result = "H";
                    break;
                case 9:
                    result = "I";
                    break;
                case 10:
                    result = "J";
                    break;
                default:
                    break;
            }
            return result;
        }

        public static void DrawHistory(Player player)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("  ");
            for (int y = 1; y <= 10; y++)
            {
                Console.Write(y);
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            for (int x = 1; x <= 10; x++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(GetLetterFromNumber(x) + " ");
                Console.ForegroundColor = ConsoleColor.White;
                for (int y = 1; y <= 10; y++)
                {
                    //Console.Write(y);
                    ShotHistory history = player.PlayerBoard.CheckCoordinate(new Coordinate(x, y));
                    switch (history)
                    {
                        case ShotHistory.Hit:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("H");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case ShotHistory.Miss:
                            Console.Write("M");
                            break;
                        case ShotHistory.Unknown:
                            Console.Write(" ");
                            break;
                    }
                    Console.Write("|");
                }
                Console.WriteLine();
            }
            Console.WriteLine("");
        }

        public static void ShowShotResult(FireShotResponse shotresponse, Coordinate c, string playername)
        {
            String str = "";
            switch (shotresponse.ShotStatus)
            {
                case ShotStatus.Duplicate:
                    Console.ForegroundColor = ConsoleColor.Red;
                    str = "Shot location: " + GetLetterFromNumber(c.XCoordinate) + c.YCoordinate.ToString() + "\t result: Duplicate shot location!";
                    break;
                case ShotStatus.Hit:
                    Console.ForegroundColor = ConsoleColor.Green;
                    str = "Shot location: " + GetLetterFromNumber(c.XCoordinate) + c.YCoordinate.ToString() + "\t result: Hit!";
                    break;
                case ShotStatus.HitAndSunk:
                    Console.ForegroundColor = ConsoleColor.Green;
                    str = "Shot location: " + GetLetterFromNumber(c.XCoordinate) + c.YCoordinate.ToString() + "\t result: Hit and Sunk, " + shotresponse.ShipImpacted + "!";
                    break;
                case ShotStatus.Invalid:
                    Console.ForegroundColor = ConsoleColor.Red;
                    str = "Shot location: " + GetLetterFromNumber(c.XCoordinate) + c.YCoordinate.ToString() + "\t result: Invalid hit location!";
                    break;
                case ShotStatus.Miss:
                    Console.ForegroundColor = ConsoleColor.White;
                    str = "Shot location: " + GetLetterFromNumber(c.XCoordinate) + c.YCoordinate.ToString() + "\t result: Miss!";
                    break;
                case ShotStatus.Victory:
                    Console.ForegroundColor = ConsoleColor.Green;
                    str = "Shot location: " + GetLetterFromNumber(c.XCoordinate) + c.YCoordinate.ToString() + "\t result: Hit and Sunk, " + shotresponse.ShipImpacted + "! \n\n";
                    str += "       ******\n";
                    str += "       ******\n";
                    str += "        **** \n";
                    str += "         **  \n";
                    str += "         **  \n";
                    str += "       ******\n";
                    str +="Game Over, " + playername + " wins!";                    
                    break;
            }
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
        }

        static void test()
        {
            List <object> obj= new List<object>();
            Player player1 = new Player();
            Board board1 = new Board();
            obj.Add(board1);
            obj.Add(player1);
        }

        public static void ResetScreen(Player[] player)
        {
            Console.Clear();
            ControlOutput.ShowHeader();
            ControlOutput.ShowAllPlayer(player);
        }
    }
}
