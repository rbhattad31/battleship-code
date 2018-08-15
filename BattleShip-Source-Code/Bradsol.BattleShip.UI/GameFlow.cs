using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bradsol.BattleShip.BL.GameLogic;
using Bradsol.BattleShip.BL.Requests;
using Bradsol.BattleShip.BL.Responses;
using Bradsol.BattleShip.BL.Ships;
using Bradsol.BattleShip.UI;

namespace Bradsol.Bradsol.BattleShip.UI
{
    class GameFlow
    {
        Players players;

        public GameFlow()
        {
            players = new Players() { IsPlayer1 = false, Player1 = new Player(), Player2 = new Player() };
        }
        
        /// <summary>
        /// Starts the Game Play
        /// </summary>
        public void Start()
        {
            try
            {
                // register players and setup the game
                GameSetup GameSetup = new GameSetup(players);
                GameSetup.Setup();

                do
                {
                    // setup the board
                    GameSetup.SetBoard();
                    FireShotResponse shotresponse;
                    do
                    {
                        ControlOutput.ResetScreen(new Player[] { players.Player1, players.Player2 });
                        ControlOutput.ShowWhoseTurn(players.IsPlayer1 ? players.Player1 : players.Player2);
                        ControlOutput.DrawHistory(players.IsPlayer1 ? players.Player2 : players.Player1);
                        Coordinate ShotPoint = new Coordinate(1, 1);
                        shotresponse = Shot(players.IsPlayer1 ? players.Player2 : players.Player1, players.IsPlayer1 ? players.Player1 : players.Player2, out ShotPoint);

                        ControlOutput.ResetScreen(new Player[] { players.Player1, players.Player2 });
                        ControlOutput.ShowWhoseTurn(players.IsPlayer1 ? players.Player1 : players.Player2);
                        ControlOutput.DrawHistory(players.IsPlayer1 ? players.Player2 : players.Player1);
                        ControlOutput.ShowShotResult(shotresponse, ShotPoint, players.IsPlayer1 ? players.Player1.Name : players.Player2.Name);
                        if (shotresponse.ShotStatus != ShotStatus.Victory)
                        {
                            Console.WriteLine("Press any key to continue to switch to " + (players.IsPlayer1 ? players.Player2.Name : players.Player1.Name));
                            players.IsPlayer1 = !players.IsPlayer1;
                            Console.ReadKey();
                        }
                    } while (shotresponse.ShotStatus != ShotStatus.Victory);

                } while (ControlInput.CheckQuit());
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Fires a shot
        /// </summary>
        /// <param name="victim"></param>
        /// <param name="Shoter"></param>
        /// <param name="ShotPoint"></param>
        /// <returns></returns>
        private FireShotResponse Shot(Player victim, Player Shoter, out Coordinate ShotPoint)
        {
            FireShotResponse fire;
            Coordinate WhereToShot;

            try
            {
                do
                {
                    WhereToShot = ControlInput.GetShotLocationFromUser();
                    fire = victim.PlayerBoard.FireShot(WhereToShot);
                    if (fire.ShotStatus == ShotStatus.Invalid || fire.ShotStatus == ShotStatus.Duplicate)
                        ControlOutput.ShowShotResult(fire, WhereToShot, "");

                    if (fire.ShotStatus == ShotStatus.Victory)
                    {
                        if (players.IsPlayer1) players.Player1.Win += 1;
                        else players.Player2.Win += 1;
                    }
                } while (fire.ShotStatus == ShotStatus.Duplicate || fire.ShotStatus == ShotStatus.Invalid);
                ShotPoint = WhereToShot;
            }
            catch (Exception)
            {
                throw;
            }

            return fire;
        }
    }
}
