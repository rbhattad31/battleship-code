using Bradsol.Bradsol.BattleShip.UI;
using System;
using System.IO;

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

                string logsDirectory = Path.Combine(Environment.CurrentDirectory, "logs");
                if (!Directory.Exists(@"C:/temp/"))
                {
                    Directory.CreateDirectory(@"C:/temp/");
                }
                if (fileExists)
                    File.AppendAllText(errorLogPath, $"{ Environment.NewLine } { Environment.NewLine }{ constructLog }");
                else
                    File.AppendAllText(errorLogPath, constructLog);
            }
        }
    }
}
