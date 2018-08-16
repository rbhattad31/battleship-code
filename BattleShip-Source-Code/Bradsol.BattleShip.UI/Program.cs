using Bradsol.Bradsol.BattleShip.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bradsol.BattleShip.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            string errorLogPath = Environment.CurrentDirectory;
            errorLogPath = errorLogPath.Replace("\\bin\\Debug", "");
            GameFlow flow = new GameFlow();
            try
            {
                int x = 0;
                int y = 1;
                int z = y / x;
                // start the game flow
                flow.Start();
            }
            catch (Exception ex)
            {
                bool fileExists = File.Exists(errorLogPath);

                string constructLog = $"Logged on - { DateTime.Now } { Environment.NewLine }" +
                    $"----------------------------{ Environment.NewLine } { ex.GetAllExceptionMessages() } { Environment.NewLine }" +
                    $"======================================================";
;
                if (!Directory.Exists(errorLogPath + @"/BattleShipErrorLog.txt/"))
                {
                    Directory.CreateDirectory(errorLogPath + @"/BattleShipErrorLog.txt/");
                }
                if (fileExists)
                    File.AppendAllText(errorLogPath, $"{ Environment.NewLine } { Environment.NewLine }{ constructLog }");
                else
                    File.AppendAllText(errorLogPath, constructLog);
            }
        }
    }
}
