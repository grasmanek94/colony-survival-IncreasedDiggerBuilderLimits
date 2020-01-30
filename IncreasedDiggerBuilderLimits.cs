using System.IO;
using System.Collections.Generic;
using ConstructionManager = Jobs.Implementations.Construction.ConstructionManager;
using Pipliz;

namespace grasmanek94.IncreasedDiggerBuilderLimits
{
    [ModLoader.ModManager]
    public static class IncreasedDiggerBuilderLimits
    {
        [ModLoader.ModCallback(ModLoader.EModCallbackType.OnAssemblyLoaded, "OnAssemblyLoaded")]
        static void OnLoad(string assemblyPath)
        {
            int largestNumber = 0;

            List<string> keys = new List<string>(ConstructionManager.BuilderLimits.Keys);
            foreach (string key in keys)
            {
                largestNumber = Math.Max(largestNumber, ConstructionManager.BuilderLimits[key]);
            }

            // Digger Limits
            keys = new List<string>(ConstructionManager.DiggerLimits.Keys);
            foreach (string key in keys)
            {
                largestNumber = Math.Max(largestNumber, ConstructionManager.DiggerLimits[key]);
            }

            int factor = 10;
            string config = Path.Combine(assemblyPath, "factor.txt");
            if (File.Exists(config))
            {
                string data = File.ReadAllText(config);
                if(!int.TryParse(data, out factor))
                {
                    Log.Write("IncreasedDiggerBuilderLimits: Failed to parse factor.txt");
                } else {
                    factor = Math.Clamp(factor, 1, int.MaxValue / largestNumber);
                    Log.Write("IncreasedDiggerBuilderLimits: custom factor = {0}", factor);
                }             
            }

            // Builder Limits
            keys = new List<string>(ConstructionManager.BuilderLimits.Keys);
            foreach (string key in keys)
            {
                ConstructionManager.BuilderLimits[key] *= factor;
            }

            // Digger Limits
            keys = new List<string>(ConstructionManager.DiggerLimits.Keys);
            foreach (string key in keys)
            {
                ConstructionManager.DiggerLimits[key] *= factor;
            }
        }
    }
}
