using System.Diagnostics;
using Newtonsoft.Json;
using Vertex;

namespace Assembly_line
{
    public static class Program
    {
        public static void Main()
        {
            Material.Load();
            Building.Load();
            Craft.Load();
        }

        public static void MainLoop()
        {
            bool shouldContinueRunning = true;
            while (shouldContinueRunning)
            {
                
            }
        }
    }

    public struct Game()
    {
        public Grid<System> Systems { get; set; } = new(((-10, -10), (11, 11)));
    }

    public struct System() { }

    public struct Base()
    {
        public List<Building> Buildings { get; set; } = [];
        public List<Craft> BaseCraftingQueue = [];
        private readonly Stopwatch _craftingProgress = new();
        public readonly Stopwatch CraftingProgress => _craftingProgress;

        public readonly void AddToBaseCraftingQueue(Craft craftToAdd)
        {
            BaseCraftingQueue.Add(craftToAdd);
            _craftingProgress.Start();
        }

        public readonly void TryFinishCurrentCraft()
        {
            if (BaseCraftingQueue.Count == 0) { return; }

            int amICooked = 
                Convert.ToInt32(BaseCraftingQueue.Count > 0) + 
                Convert.ToInt32(CraftingProgress != null) + 
                Convert.ToInt32(BaseCraftingQueue != null)
            ;
            if (amICooked == 3 && CraftingProgress!.Elapsed >= BaseCraftingQueue![0].TimeToCraft)
            {
                CraftingProgress.Stop();
                CraftingProgress.Reset();
                BaseCraftingQueue.RemoveAt(0);

                if (BaseCraftingQueue.Count > 0) { CraftingProgress.Start(); }
            }
            else if (amICooked > 0) { throw new("Ok something's gone really wrong with the crafting stopwatch. DEBUG NOW"); }
        }
    }

    public struct BaseCraftingQueue()
    {
        public List<Craft> CraftsQueue { get; private set; } = [];
        public Stopwatch CraftingProgress { get; private set; } = new();
        public TimeSpan? WhenCraftingIsDone { get; private set; }

        public void AddToQueue(Craft craftToAdd)
        {
            CraftsQueue.Add(craftToAdd);
            WhenCraftingIsDone = craftToAdd.TimeToCraft;
            CraftingProgress.Start();
        }

        public void TryFinishCurrentCraft()
        {
            int amICooked = 
                Convert.ToInt32(CraftsQueue.Count > 0) + 
                Convert.ToInt32(CraftingProgress != null) + 
                Convert.ToInt32(WhenCraftingIsDone != null)
            ;
            if (amICooked == 3 && CraftingProgress!.Elapsed >= WhenCraftingIsDone)
            {
                CraftingProgress.Stop();
                CraftingProgress.Reset();
                CraftsQueue.RemoveAt(0);

                if (CraftsQueue.Count > 0)
                {
                    WhenCraftingIsDone = CraftsQueue[0].TimeToCraft;
                    CraftingProgress.Start();
                }
            }
            else if (amICooked > 0) { throw new("Ok something's gone really wrong with the crafting stopwatch. DEBUG NOW"); }
        }
    }

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