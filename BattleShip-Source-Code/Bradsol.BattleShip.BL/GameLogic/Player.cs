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
    public class Player
    {
        public string Name{ get; set;}
        public int Win { get; set; }
        public Board PlayerBoard;
    }
}
