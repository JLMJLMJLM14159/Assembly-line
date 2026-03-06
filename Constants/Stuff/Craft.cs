using Newtonsoft.Json;

namespace Assembly_line.Constants.Stuff

{
    [method: JsonConstructor]
    public readonly struct Craft(string id, List<(Material, int)> inputs, List<(Material, int)> outputs, Building place, TimeSpan time)
    {
        public readonly string Id = id;
        public readonly List<(Material, int)> Inputs = inputs;
        public readonly List<(Material, int)> Outputs = outputs;
        public readonly Building PlaceToCraft = place;
        public readonly TimeSpan TimeToCraft = time;
        private static Dictionary<string, Craft> _all = [];
        public static Dictionary<string, Craft> All => _all;

        public static bool operator ==(Craft a, Craft b) => a.Id == b.Id;
        public static bool operator !=(Craft a, Craft b) => !(a == b);
        public override bool Equals(object? obj) => obj is Craft other && this == other;
        public override int GetHashCode() => Id.GetHashCode();

        public static void Load()
        {
            string file = File.ReadAllText("crafts.json");
            List<Craft> list = JsonConvert.DeserializeObject<List<Craft>>(file) ?? throw new JsonException();
            _all = list.ToDictionary(m => m.Id);
        }
    }
}