﻿using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace ConvenientChests.Framework {
    public abstract class Module {
        public bool IsActive { get; protected set; } = false;
        public ModEntry ModEntry { get; }
        public ModConfig ModConfig => ModEntry.Config;
        public IMonitor Monitor => ModEntry.Monitor;
        public IModEvents Events => ModEntry.Helper.Events;

        public Module(ModEntry modEntry) => ModEntry = modEntry;

        public abstract void Activate();
        public abstract void Deactivate();
    }
}
