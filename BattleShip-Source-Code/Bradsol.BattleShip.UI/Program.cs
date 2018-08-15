using Bradsol.Bradsol.BattleShip.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bradsol.BattleShip.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            string errorLogPath = @"C:/temp/BattleShipErrorLog.txt";

            GameFlow flow = new GameFlow();

            try
            {
                // start the game flow
                flow.Start();
            }
            catch (Exception ex)
            {
                bool fileExists = File.Exists(errorLogPath);

                string constructLog = $"Logged on - { DateTime.Now } { Environment.NewLine }" +
                    $"----------------------------{ Environment.NewLine } { ex.GetAllExceptionMessages() } { Environment.NewLine }" +
                    $"======================================================";

                if (fileExists)
                    File.AppendAllText(errorLogPath, $"{ Environment.NewLine } { Environment.NewLine }{ constructLog }");
                else
                    File.AppendAllText(errorLogPath, constructLog);
            }
        }
    }
}
