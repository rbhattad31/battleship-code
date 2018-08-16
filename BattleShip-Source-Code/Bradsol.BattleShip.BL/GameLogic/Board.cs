using System;
using System.Collections.Generic;
using System.Linq;
using Bradsol.BattleShip.BL.Requests;
using Bradsol.BattleShip.BL.Responses;
using Bradsol.BattleShip.BL.Ships;

namespace Bradsol.BattleShip.BL.GameLogic
{
    /// <summary>
    /// This class contains designing board and ship placement
    /// </summary>
    public class Board
    {
        public const int xCoordinator = 8;
        public const int yCoordinator = 8;
        private Dictionary<Coordinate, ShotHistory> ShotHistory;
        private int _currentShipIndex;

        public Ship[] Ships { get; private set; }

        public Board()
        {
            ShotHistory = new Dictionary<Coordinate, ShotHistory>();
            Ships = new Ship[1];
            _currentShipIndex = 0;
        }
        /// <summary>
        /// Method gives Shot Responce
        /// </summary>
        public FireShotResponse FireShot(Coordinate coordinate)
        {
            var response = new FireShotResponse();

            try
            {
                // is this coordinate on the board?
                if (!IsValidCoordinate(coordinate))
                {
                    response.ShotStatus = ShotStatus.Invalid;
                    return response;
                }

                // did they already try this position?
                if (ShotHistory.ContainsKey(coordinate))
                {
                    response.ShotStatus = ShotStatus.Duplicate;
                    return response;
                }

                CheckShipsForHit(coordinate, response);
                CheckForVictory(response);
            }
            catch (Exception)
            {
                throw;
            }

            return response;            
        }

        /// <summary>
        /// Method check the Coordinate
        /// </summary>
        public ShotHistory CheckCoordinate(Coordinate coordinate)
        {
            try
            {
                if (ShotHistory.ContainsKey(coordinate))
                {
                    return ShotHistory[coordinate];
                }
                else
                {
                    return BL.Responses.ShotHistory.Unknown;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Mehtod places the Ship on Board
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ShipPlacement PlaceShip(PlaceShipRequest request)
        {
            try
            {
                if (_currentShipIndex > 4)
                    throw new Exception("You can not add another ship, 5 is the limit!");

                if (!IsValidCoordinate(request.Coordinate))
                    return ShipPlacement.NotEnoughSpace;

                Ship newShip = ShipCreator.CreateShip(request.ShipType);
                switch (request.Direction)
                {
                    case ShipDirection.Down:
                        return PlaceShipDown(request.Coordinate, newShip);
                    case ShipDirection.Up:
                        return PlaceShipUp(request.Coordinate, newShip);
                    case ShipDirection.Left:
                        return PlaceShipLeft(request.Coordinate, newShip);
                    default:
                        return PlaceShipRight(request.Coordinate, newShip);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// checks victory
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private void CheckForVictory(FireShotResponse response)
        {
            if (response.ShotStatus == ShotStatus.HitAndSunk)
            {
                // did they win?
                if (Ships.All(s => s.IsSunk))
                    response.ShotStatus = ShotStatus.Victory;
            }
        }
        /// <summary>
        /// Check if a ship is hit
        /// </summary>
        /// <param name="coordinate"></param>
        /// /// <param name="response"></param>
        private void CheckShipsForHit(Coordinate coordinate, FireShotResponse response)
        {
            response.ShotStatus = ShotStatus.Miss;
            ShotStatus status = Ships[0].FireAtShip(coordinate);
            switch (status)
            {
                case ShotStatus.HitAndSunk:
                    response.ShotStatus = ShotStatus.HitAndSunk;
                    response.ShipImpacted = Ships[0].ShipName;
                    ShotHistory.Add(coordinate, Responses.ShotHistory.Hit);
                    break;
                case ShotStatus.Hit:
                    response.ShotStatus = ShotStatus.Hit;
                    response.ShipImpacted = Ships[0].ShipName;
                    ShotHistory.Add(coordinate, Responses.ShotHistory.Hit);
                    break;
            }

            if (response.ShotStatus == ShotStatus.Miss)
            {
                ShotHistory.Add(coordinate, Responses.ShotHistory.Miss);
            }
        }

        /// <summary>
        /// Method checks for Valid Coordinate
        /// </summary>
        /// <param name="coordinate"></param>
        private bool IsValidCoordinate(Coordinate coordinate)
        {
            return coordinate.XCoordinate >= 1 && coordinate.XCoordinate <= xCoordinator &&
            coordinate.YCoordinate >= 1 && coordinate.YCoordinate <= yCoordinator;
        }

        /// <summary>
        /// Places Ship to right on board
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="newShip"></param>
        private ShipPlacement PlaceShipRight(Coordinate coordinate, Ship newShip)
        {
            // y coordinate gets bigger
            int positionIndex = 0;
            int maxY = coordinate.YCoordinate + newShip.BoardPositions.Length;

            for (int i = coordinate.YCoordinate; i < maxY; i++)
            {
                var currentCoordinate = new Coordinate(coordinate.XCoordinate, i);
                if (!IsValidCoordinate(currentCoordinate))
                    return ShipPlacement.NotEnoughSpace;

                newShip.BoardPositions[positionIndex] = currentCoordinate;
                positionIndex++;
            }

            AddShipToBoard(newShip);
            return ShipPlacement.Ok;
        }

        /// <summary>
        /// Places Ship to left on board
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="newShip"></param>
        private ShipPlacement PlaceShipLeft(Coordinate coordinate, Ship newShip)
        {
            // y coordinate gets smaller
            int positionIndex = 0;
            int minY = coordinate.YCoordinate - newShip.BoardPositions.Length;

            for (int i = coordinate.YCoordinate; i > minY; i--)
            {
                var currentCoordinate = new Coordinate(coordinate.XCoordinate, i);

                if (!IsValidCoordinate(currentCoordinate))
                    return ShipPlacement.NotEnoughSpace;

                newShip.BoardPositions[positionIndex] = currentCoordinate;
                positionIndex++;
            }

            AddShipToBoard(newShip);
            return ShipPlacement.Ok;
        }

        /// <summary>
        /// Places Ship to up on board
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="newShip"></param>
        private ShipPlacement PlaceShipUp(Coordinate coordinate, Ship newShip)
        {
            // x coordinate gets smaller
            int positionIndex = 0;
            int minX = coordinate.XCoordinate - newShip.BoardPositions.Length;

            for (int i = coordinate.XCoordinate; i > minX; i--)
            {
                var currentCoordinate = new Coordinate(i, coordinate.YCoordinate);

                if (!IsValidCoordinate(currentCoordinate))
                    return ShipPlacement.NotEnoughSpace;

                newShip.BoardPositions[positionIndex] = currentCoordinate;
                positionIndex++;
            }

            AddShipToBoard(newShip);
            return ShipPlacement.Ok;
        }

        /// <summary>
        /// Places Ship to down on board
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="newShip"></param>
        private ShipPlacement PlaceShipDown(Coordinate coordinate, Ship newShip)
        {
            // y coordinate gets bigger
            int positionIndex = 0;
            int maxX = coordinate.XCoordinate + newShip.BoardPositions.Length;

            for (int i = coordinate.XCoordinate; i < maxX; i++)
            {
                var currentCoordinate = new Coordinate(i, coordinate.YCoordinate);

                if (!IsValidCoordinate(currentCoordinate))
                    return ShipPlacement.NotEnoughSpace;

                newShip.BoardPositions[positionIndex] = currentCoordinate;
                positionIndex++;
            }

            AddShipToBoard(newShip);
            return ShipPlacement.Ok;
        }

        private void AddShipToBoard(Ship newShip)
        {
            Ships[_currentShipIndex] = newShip;
            _currentShipIndex++;
        }

    }
}
