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
    public class GameSetup
    {
        Players _players;
        public GameSetup(Players players)
        {
            _players = players;
        }

        public void Setup()
        {
            Console.ForegroundColor = ConsoleColor.White;
            ControlOutput.ShowFlashScreen();
            ControlOutput.ShowHeader();

            string[] userSetUp = ControlInput.GetNameFromUser();

            _players.Player1.Name = userSetUp[0];
            _players.Player1.Win = 0;

            _players.Player2.Name = userSetUp[1];
            _players.Player2.Win = 0;
        }

        public void SetBoard()
        {
            ControlOutput.ResetScreen(new Player[] { _players.Player1, _players.Player2 });

            _players.IsPlayer1 = BL.Responses.GetRandom.WhoseFirst();

            _players.Player1.PlayerBoard = new Board();
            PlaceShipOnBoard(_players.Player1);
            ControlOutput.ResetScreen(new Player[] { _players.Player1, _players.Player2 });

            _players.Player2.PlayerBoard = new Board();
            PlaceShipOnBoard(_players.Player2);
            Console.WriteLine("All ship were placed successfull! Press any key to continue...");
            Console.ReadKey();
        }

        public void PlaceShipOnBoard(Player player)
        {
            ControlOutput.ShowWhoseTurn(player);
            Console.WriteLine("Input the location and direction(l, r, u, d) of the ships. Ex:) a2, r:");

            PlaceShipRequest ShipToPlace = new PlaceShipRequest();
            ShipPlacement result;
            do
            {
                ShipToPlace = ControlInput.GetLocationFromUser(ShipType.Battleship.ToString());
                ShipToPlace.ShipType = ShipType.Battleship;
                result = player.PlayerBoard.PlaceShip(ShipToPlace);
                if (result == ShipPlacement.NotEnoughSpace)
                    Console.WriteLine("Not Enough Space!");
                else if (result == ShipPlacement.Overlap)
                    Console.WriteLine("Overlap placement!");

            } while (result != ShipPlacement.Ok);
        }
    }
}
