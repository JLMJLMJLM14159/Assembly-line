using Newtonsoft.Json;

namespace Assembly_line.Constants.Stuff
{
    [method: JsonConstructor]
    public readonly struct Building(string id, string name, string abbr, TimeSpan timeToBuild, int storage)
    {
        public readonly string Id = id;
        public readonly string Name = name;
        public readonly string Abbreviation = abbr;
        public readonly TimeSpan TimeToBuild = timeToBuild;
        public readonly int StorageSpaceProvided = storage;
        private static Dictionary<string, Building> _all = [];
        public static Dictionary<string, Building> All => _all;

        public static bool operator ==(Building a, Building b) => a.Id == b.Id;
        public static bool operator !=(Building a, Building b) => !(a == b);
        public override bool Equals(object? obj) => obj is Building other && this == other;
        public override int GetHashCode() => Id.GetHashCode();

        public static void Load()
        {
            string file = File.ReadAllText("buildings.json");
            List<Building> list = JsonConvert.DeserializeObject<List<Building>>(file) ?? throw new JsonException();
            _all = list.ToDictionary(m => m.Id);
        }
    }
}