using System.Text;

namespace Assembly_line
{
    public static class Program
    {
        public static void Main()
        {
            
        }
    }
    
    public readonly struct Material
    {
        public readonly string Name;
        public readonly string Abbreviation;
        public readonly string Description;

        private Material(string name, string abbreviation, string description)
        {
            Name = name;
            Abbreviation = abbreviation;
            Description = description;
        }

        public static readonly List<Material> Materials = [
            new("oxygen", "o", "Required for human life. Without this, your crew will lose all working efficiency."),
            new("iron ore", "feo", "Unsmelted iron ore, straight from the ground."),
            new("iron", "fe", "A useful element that can be made into structural elements.")
        ];

        public static Material FindMaterialFromName(string name)
        {
            name = name.ToLower();

            List<Material> foundMaterials = [.. Materials.Where(m => { return name == m.Name; })];
            
            if (foundMaterials.Count != 1) { throw new($"Found multiple or no materials from material list with name {name}."); }

            return foundMaterials[0];
        }

        public static Material FindMaterialFromAbbreviation(string abbreviation)
        {
            abbreviation = abbreviation.ToLower();

            List<Material> foundMaterials = [.. Materials.Where(m => { return abbreviation == m.Abbreviation; })];
            
            if (foundMaterials.Count != 1) { throw new($"Found multiple or no materials from material list with name {abbreviation}."); }

            return foundMaterials[0];
        }
    }

    public readonly struct Craft
    {
        public readonly List<(Material, int)> Inputs;
        public readonly List<(Material, int)> Outputs;
        public readonly Building PlaceToCraft;
        public readonly TimeSpan TimeToCraft;

        private Craft(List<(Material, int)> inputs, List<(Material, int)> outputs, Building placeToCraft, TimeSpan timeToCraft)
        {
            Inputs = inputs;
            Outputs = outputs;
            PlaceToCraft = placeToCraft;
            TimeToCraft = timeToCraft;
        }

        public static readonly List<Craft> Crafts = [
            new([(Material.FindMaterialFromName("iron ore"), 2)], [(Material.FindMaterialFromName("iron"), 1)], Building.FindBuildingFromName("smelter"), new(0, 0, 1))
        ];
    }

    public readonly struct Building
    {
        public readonly string Name;
        public readonly string Abbreviation;
        public readonly TimeSpan TimeToBuild;

        private Building(string name, string abbreviation, TimeSpan timeToBuild)
        {
            Name = name;
            Abbreviation = abbreviation;
            TimeToBuild = timeToBuild;
        }

        public static readonly List<Building> Buildings = [
            new("smelter", "smt", new(0, 0, 10))
        ];

        public static Building FindBuildingFromName(string name)
        {
            name = name.ToLower();

            List<Building> foundBuildings = [.. Buildings.Where(b => { return name == b.Name; })];
            
            if (foundBuildings.Count != 1) { throw new($"Found multiple or no materials from building list with name {name}."); }

            return foundBuildings[0];
        }

        public static Building FindBuildingFromAbbreviation(string abbreviation)
        {
            abbreviation = abbreviation.ToLower();

            List<Building> foundMaterials = [.. Buildings.Where(m => { return abbreviation == m.Abbreviation; })];
            
            if (foundMaterials.Count != 1) { throw new($"Found multiple or no buildings from material list with name {abbreviation}."); }

            return foundMaterials[0];
        }
    }
}