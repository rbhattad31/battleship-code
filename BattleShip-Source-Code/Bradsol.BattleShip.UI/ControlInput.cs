using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bradsol.BattleShip.BL.GameLogic;
using Bradsol.BattleShip.BL.Requests;
using Bradsol.BattleShip.BL.Responses;
using Bradsol.BattleShip.BL.Ships;

namespace Bradsol.BattleShip.UI
{
    public class ControlInput
    {
        /// <summary>
        /// Gets the names entered by the players
        /// </summary>
        /// <returns></returns>
        public static string[] GetNameFromUser()
        {
            string player1 = "";
            string player2 = "";

            do
            {
                Console.Write("Input player 1 name: ");
                player1 = Console.ReadLine();
            } while (player1.Trim() == "");

            do
            {
                Console.Write("Input player 2 name: ");
                player2 = Console.ReadLine();
            } while (player2.Trim() == "");

            return new string[] { player1, player2 };
        }

        /// <summary>
        /// Gets the direction selected by the player
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static ShipDirection getDirection(string direction)
        {
            switch (direction.ToLower())
            {
                case "l":
                    return ShipDirection.Left;
                case "r":
                    return ShipDirection.Right;
                case "u":
                    return ShipDirection.Up;
                default:
                    return ShipDirection.Down;
            }

        }

        /// <summary>
        /// Gets the location selected by the user
        /// </summary>
        /// <param name="ShipType"></param>
        /// <returns></returns>
        public static PlaceShipRequest GetLocationFromUser(string ShipType)
        {
            PlaceShipRequest result = null;

            try
            {
                do
                {
                    Console.Write("- " + ShipType + ": ");
                    result = GetLocation(Console.ReadLine());
                    if (result is null) ;
                    else return result;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please input location and direction. Ex:) a2, r");
                    Console.ForegroundColor = ConsoleColor.White;
                } while (result is null);
            }
            catch (Exception)
            {
                throw;
            }
            
            return result;
        }

        public static PlaceShipRequest GetLocation(string location)
        {
            string strX;
            string strY;
            string strDirection;
            int x;
            int y;

            try
            {
                if (location.Split(',').Length == 2)
                {
                    if (location.Split(',')[0].Trim().Length > 1)
                    {
                        strX = location.Split(',')[0].Trim().Substring(0, 1);
                        strY = location.Split(',')[0].Trim().Substring(1);
                        strDirection = location.Split(',')[1].ToLower().Trim();

                        x = GetNumberFromLetter(strX);
                        if (x > 0 && x < 11 && int.TryParse(strY, out y) && y > 0 && y < 11
                            && (strDirection == "l"
                            || strDirection == "r"
                            || strDirection == "u"
                            || strDirection == "d"))
                        {
                            PlaceShipRequest ShipToPlace = new PlaceShipRequest();
                            ShipToPlace.Direction = getDirection(strDirection);
                            ShipToPlace.Coordinate = new Coordinate(x, y);
                            return ShipToPlace;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }

        /// <summary>
        /// Gets the location of the shot fired from the player
        /// </summary>
        /// <returns></returns>
        public static Coordinate GetShotLocationFromUser()
        {
            string result = "";
            int x;
            int y;
            try
            {
                while (true)
                {
                    Console.Write("Which location do you want to shot? ");
                    result = Console.ReadLine();
                    if (result.Trim().Length > 1)
                    {
                        x = GetNumberFromLetter(result.Substring(0, 1));
                        if (x > 0 && int.TryParse(result.Substring(1), out y))
                        {
                            return new Coordinate(x, y);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the location of the short fired
        /// </summary>
        /// <param name="victimboard"></param>
        /// <returns></returns>
        static Coordinate GetRightLocationToShot(Board victimboard)
        {
            try
            {
                List<Coordinate> tmpList = new List<Coordinate> { };
                for (int i = 0; i < victimboard.Ships.Length; i++)
                {
                    Ship tmpShip = victimboard.Ships[i];
                    for (int j = 0; j < tmpShip.BoardPositions.Length; j++)
                    {
                        if (victimboard.CheckCoordinate(tmpShip.BoardPositions[j]) == ShotHistory.Unknown)
                            tmpList.Add(tmpShip.BoardPositions[j]);
                    }
                }

                return tmpList[GetRandom.r.Next(0, tmpList.Count - 1)];
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Takes Letter and returns Number
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>

        static int GetNumberFromLetter(string letter)
        {
            int result = -1;
            switch (letter.ToLower())
            {
                case "a":
                    result = 1;
                    break;
                case "b":
                    result = 2;
                    break;
                case "c":
                    result = 3;
                    break;
                case "d":
                    result = 4;
                    break;
                case "e":
                    result = 5;
                    break;
                case "f":
                    result = 6;
                    break;
                case "g":
                    result = 7;
                    break;
                case "h":
                    result = 8;
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// Display Thank you message.
        /// </summary>
        /// <returns></returns>
        public static bool CheckQuit()
        {
            Console.WriteLine("Thank you.!..");
            return Console.ReadKey().Key == ConsoleKey.F5;
        }
    }
}
