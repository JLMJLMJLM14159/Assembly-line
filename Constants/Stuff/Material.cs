using Newtonsoft.Json;

namespace Assembly_line.Constants.Stuff
{
    [method: JsonConstructor]
    public readonly struct Material(string id, string name, string abbreviation, string description)
    {
        public readonly string Id = id;
        public readonly string Name = name;
        public readonly string Abbreviation = abbreviation;
        public readonly string Description = description;
        private static Dictionary<string, Material> _all = [];
        public static Dictionary<string, Material> All => _all;

        public static bool operator ==(Material a, Material b) => a.Id == b.Id;
        public static bool operator !=(Material a, Material b) => !(a == b);
        public override readonly bool Equals(object? obj) => obj is Material other && this == other;
        public override readonly int GetHashCode() => Id.GetHashCode();

        public static void Load()
        {
            string file = File.ReadAllText("materials.json");
            List<Material> list = JsonConvert.DeserializeObject<List<Material>>(file) ?? throw new JsonException();
            _all = list.ToDictionary(m => m.Id);
        }
    }
}