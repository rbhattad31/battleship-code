using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bradsol.BattleShip.BL.Ships;

namespace Bradsol.BattleShip.BL.Requests
{
    public class PlaceShipRequest
    {
        public Coordinate Coordinate { get; set; }
        public ShipDirection Direction { get; set; }
        public ShipType ShipType { get; set; }
    }
}
