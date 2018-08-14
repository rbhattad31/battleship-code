using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bradsol.BattleShip.BL.Ships;

namespace Bradsol.BattleShip.BL.GameLogic
{
    public class ShipCreator
    {
        public static Ship CreateShip(ShipType type)
        {
            switch (type)
            {
                //case ShipType.Destroyer:
                //    return new Ship(ShipType.Destroyer, 2);
                //case ShipType.Cruiser:
                //    return new Ship(ShipType.Cruiser, 3);
                //case ShipType.Submarine:
                //    return new Ship(ShipType.Submarine, 3);
                //case ShipType.Battleship:
                //    return new Ship(ShipType.Battleship, 3);
                default:
                    return new Ship(ShipType.Battleship, 3);
            }
        }
    }
}
