using Assembly_line.Constants.Stuff;

namespace Assembly_line.Constants.Map
{
    public readonly struct Planet
    {
        public enum Habitabilities
        {
            sterile = 100,
            mostly_sterile = 80,
            somewhat_sterile = 60,
            somewhat_fertile = 40,
            mostly_fertile = 20,
            fertile = 0,
        }

        public readonly Habitabilities Habitability;
        public readonly List<(Material, float)> ExtractableMaterials;
        public readonly List<Base> Bases;

        public Planet()
        {
            Random random = new();

            Bases = [];
            ExtractableMaterials = [];

            Habitability = Enum.GetValues<Habitabilities>()[random.Next(Enum.GetNames<Habitabilities>().Length)];

            for (int _ = 0; _ < 5; _++)
            { ExtractableMaterials.Add((Material.All.ToList()[random.Next(Material.All.Count)].Value, new Func<float>(() => { float toCube = random.NextSingle(); return MathF.Round(toCube * toCube * toCube, 2, MidpointRounding.AwayFromZero); })())); }
        }

        public Planet(Habitabilities habitability, List<(Material, float)> extractableMaterials, List<Base> bases)
        {
            Habitability = habitability;
            ExtractableMaterials = extractableMaterials;
            Bases = bases;
        }
    }
}