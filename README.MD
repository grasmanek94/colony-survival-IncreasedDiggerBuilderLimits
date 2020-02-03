This Colony Survival mod increases the Builder and Digger area limits by a factor 10 (default, customizable in `factor.txt`).

Mod compatibility:   

- `0.7.1`
- `0.7.2`

Notes:

Don't forget to update your `gamedata/settings/server.json`:

- `Banner.LoadedChunksRadius` to prevent chunk unloading
- `NPCs.BuilderCooldownMultiplierSeconds` to decrease the time it takes to build (lower number)
- `NPCs.DiggerCooldownMultiplierSeconds` to decrease the time it takes to dig (lower number)
- `Colony.ExclusiveRadius` to be able to use the new (bigger) area sizes