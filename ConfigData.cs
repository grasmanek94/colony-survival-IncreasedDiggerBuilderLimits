namespace grasmanek94.IncreasedDiggerBuilderLimits
{
    public class ConfigData
    {
        public ColonySettings Colony;
        public BannerSettings Banner;
        public NPCSettings NPCs;
        public AuthorData grasmanek94;

        public class NPCSettings
        {
            public float BuilderCooldownMultiplierSeconds;
            public float DiggerCooldownMultiplierSeconds;
        }

        public class ColonySettings
        {
            public int ExclusiveRadius;
        }

        public class BannerSettings
        {
            public int LoadedChunksRadius;
            public int SafeRadiusMinimum;
            public int SafeRadiusMaximum;
            public int MaximumZombieSpawnRadius;
        }

        public class AuthorData
        {
            public IncreasedDiggerBuilderLimitsData IncreasedDiggerBuilderLimits;

            public class IncreasedDiggerBuilderLimitsData
            {
                public int Factor;
            }
        }
    }
}
