using System.Diagnostics;
using Assembly_line.Constants.Stuff;

namespace Assembly_line.Constants.Map
{
    public readonly struct Base()
    {
        public readonly List<Building> Buildings = [];
        private readonly List<Craft> BaseCraftingQueue = [];
        private readonly Stopwatch CraftingProgress = new();

        public readonly void AddToBaseCraftingQueue(Craft craftToAdd)
        {
            BaseCraftingQueue.Add(craftToAdd);
            CraftingProgress.Start();
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
}