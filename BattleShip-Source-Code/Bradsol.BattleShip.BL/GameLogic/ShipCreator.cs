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

           return new Ship(ShipType.Battleship, 3);
            
        }
    }
}
