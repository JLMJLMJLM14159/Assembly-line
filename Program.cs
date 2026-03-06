using Assembly_line.Constants.Map;
using Assembly_line.Constants.Stuff;
using System.Numerics;

namespace Assembly_line
{
    public static class Program
    {
        public static void Main()
        {
            Material.Load();
            Building.Load();
            Craft.Load();

            MainLoop();
        }

        public static void MainLoop()
        {
            bool shouldContinueRunning = true;
            while (shouldContinueRunning)
            {
                
            }
        }
    }

    public struct Game
    {
        public Dictionary<Vector3, SolarSystem> SolarSystems { get; set; }

        public Game()
        {
            Random random = new();   
            SolarSystems = [];

            for (int i = 0; i < 100; i++)
            {
                SolarSystems.Add(
                    new(
                        (float)Math.Round(random.NextDouble(), 4, MidpointRounding.AwayFromZero) * 10,
                        (float)Math.Round(random.NextDouble(), 4, MidpointRounding.AwayFromZero) * 10,
                        (float)Math.Round(random.NextDouble(), 4, MidpointRounding.AwayFromZero)
                    ),
                    new()
                );
            }
        }

        public Game(Dictionary<Vector3, SolarSystem> solarSystems)
        {
            SolarSystems = solarSystems;
        }
    }
}