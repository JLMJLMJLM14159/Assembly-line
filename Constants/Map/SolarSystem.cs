namespace Assembly_line.Constants.Map
{
    public readonly struct SolarSystem
    {
        public readonly List<Planet> Planets;

        public SolarSystem()
        {
            Random random = new();

            Planets = [];

            for (int _ = 0; _ < 7; _++) { if (Convert.ToBoolean(random.Next(2))) { Planets.Add(new()); } }
        }

        public SolarSystem(List<Planet> planets)
        {
            Planets = planets;
        }
    }
}