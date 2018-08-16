using Bradsol.BattleShip.BL.GameLogic;
using Bradsol.BattleShip.BL.Requests;
using Bradsol.BattleShip.BL.Responses;
using Bradsol.BattleShip.BL.Ships;
using NUnit.Framework;

namespace Bradsol.Battleship.Tests
{
    [TestFixture]
    public class FireShotTests
    {
        #region "Board Setup"
        private Board SetupBoard()
        {
            Board board = new Board();
            PlaceBattleship(board);
            return board;
        }

        private void PlaceBattleship(Board board)
        {
            var request = new PlaceShipRequest()
            {
                Coordinate = new Coordinate(1, 3),
                Direction = ShipDirection.Down,
                ShipType = ShipType.Battleship
            };

            board.PlaceShip(request);
        }
        #endregion

        [Test]
        public void CoordinateEquality()
        {
            var c1 = new Coordinate(5, 10);
            var c2 = new Coordinate(5, 10);

            Assert.AreEqual(c1, c2);
        }

        [Test]
        public void CanNotFireOffBoard()
        {
            var board = SetupBoard();

            var coordinate = new Coordinate(15, 20);

            var response = board.FireShot(coordinate);

            Assert.AreEqual(ShotStatus.Invalid, response.ShotStatus);
        }

        [Test]
        public void CanNotFireDuplicateShot()
        {
            var board = SetupBoard();

            var coordinate = new Coordinate(5, 5);
            var response = board.FireShot(coordinate);

            Assert.AreEqual(ShotStatus.Miss, response.ShotStatus);

            // fire same shot
            response = board.FireShot(coordinate);
            Assert.AreEqual(ShotStatus.Duplicate, response.ShotStatus);
        }

        [Test]
        public void CanMissShip()
        {
            var board = SetupBoard();
            var coordinate = new Coordinate(5, 5);
            var response = board.FireShot(coordinate);
            Assert.AreEqual(ShotStatus.Miss, response.ShotStatus);
        }

        [Test]
        public void CanHitShip()
        {
            var board = SetupBoard();

            var coordinate = new Coordinate(1, 3);
            var response = board.FireShot(coordinate);
            Assert.AreEqual(ShotStatus.Hit, response.ShotStatus);
            Assert.AreEqual("Battleship", response.ShipImpacted);
        }

        [Test]
        public void CanSinkShip()
        {
            var board = SetupBoard();

            var coordinate = new Coordinate(1, 3);
            var response = board.FireShot(coordinate);

            Assert.AreEqual(ShotStatus.Hit, response.ShotStatus);
            Assert.AreEqual("Battleship", response.ShipImpacted);

            coordinate = new Coordinate(2, 3);
            response = board.FireShot(coordinate);

            Assert.AreEqual(ShotStatus.Hit, response.ShotStatus);
            Assert.AreEqual("Battleship", response.ShipImpacted);

            coordinate = new Coordinate(3, 3);
            response = board.FireShot(coordinate);

            Assert.AreEqual(ShotStatus.Victory, response.ShotStatus);
            Assert.AreEqual("Battleship", response.ShipImpacted);
        }
    }
}
