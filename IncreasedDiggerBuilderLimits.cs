using System.IO;
using System.Collections.Generic;
using ConstructionManager = Jobs.Implementations.Construction.ConstructionManager;
using Pipliz;
using System.Threading.Tasks;

namespace grasmanek94.IncreasedDiggerBuilderLimits
{
    [ModLoader.ModManager]
    public static class IncreasedDiggerBuilderLimits
    {
        static ConfigData configData;
        static bool configLoaded;

        [ModLoader.ModCallback(ModLoader.EModCallbackType.OnAssemblyLoaded, "grasmanek94.IncreasedDiggerBuilderLimits.OnAssemblyLoaded")]
        static void OnAssemblyLoaded(string assemblyPath)
        {
            int largestNumber = 0;
            configLoaded = false;

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
            string config = Path.Combine(Path.GetDirectoryName(assemblyPath), "config.json");
            if (File.Exists(config))
            {
                Task<ConfigData> task = Task.Run(() => JSONHelper.Deserialize<ConfigData>(config));
                task.Wait();
                configData = task.Result;

                factor = Math.Clamp(configData.grasmanek94.IncreasedDiggerBuilderLimits.Factor, 1, int.MaxValue / largestNumber);

                configLoaded = true;
            }

            Log.Write("IncreasedDiggerBuilderLimits: factor = {0}", factor);

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

        [ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "grasmanek94.IncreasedDiggerBuilderLimits.AfterItemTypesDefined")]
        static void AfterItemTypesDefined()
        {
            if(!configLoaded)
            {
                return;
            }

            ServerManager.ServerSettings.Banner.LoadedChunksRadius = configData.Banner.LoadedChunksRadius;
            ServerManager.ServerSettings.Banner.MaximumZombieSpawnRadius = configData.Banner.MaximumZombieSpawnRadius;
            ServerManager.ServerSettings.Banner.SafeRadiusMaximum = configData.Banner.SafeRadiusMaximum;
            ServerManager.ServerSettings.Banner.SafeRadiusMinimum = configData.Banner.SafeRadiusMinimum;
            ServerManager.ServerSettings.Colony.ExclusiveRadius = configData.Colony.ExclusiveRadius;
            ServerManager.ServerSettings.NPCs.BuilderCooldownMultiplierSeconds = configData.NPCs.BuilderCooldownMultiplierSeconds;
            ServerManager.ServerSettings.NPCs.DiggerCooldownMultiplierSeconds = configData.NPCs.DiggerCooldownMultiplierSeconds;
        }
    }
}
