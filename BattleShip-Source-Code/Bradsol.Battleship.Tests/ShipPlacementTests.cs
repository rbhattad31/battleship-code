﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bradsol.BattleShip.BL.GameLogic;
using Bradsol.BattleShip.BL.Requests;
using Bradsol.BattleShip.BL.Responses;
using Bradsol.BattleShip.BL.Ships;
using NUnit.Framework;

namespace Bradsol.Battleship.Tests
{
    [TestFixture]
    public class ShipPlacementTests
    {
        [Test]
        public void CanNotPlaceShipOffBoard()
        {
            Board board = new Board();
            PlaceShipRequest request = new PlaceShipRequest()
            {
                Coordinate = new Coordinate(15, 10),
                Direction = ShipDirection.Up,
                ShipType = ShipType.Battleship
            };

            var response = board.PlaceShip(request);

            Assert.AreEqual(ShipPlacement.NotEnoughSpace, response);
        }

        [Test]
        public void CanNotPlaceShipPartiallyOnBoard()
        {
            Board board = new Board();
            PlaceShipRequest request = new PlaceShipRequest()
            {
                Coordinate = new Coordinate(7, 7),
                Direction = ShipDirection.Right,
                ShipType = ShipType.Battleship
            };

            var response = board.PlaceShip(request);
            Assert.AreEqual(ShipPlacement.NotEnoughSpace, response);
        }
    }
}
