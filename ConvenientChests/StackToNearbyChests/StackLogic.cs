﻿using System;
using System.Collections.Generic;
using System.Linq;
using ConvenientChests.CategorizeChests.Framework;
using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Locations;
using StardewValley.Objects;
using Object = StardewValley.Object;

namespace ConvenientChests.StackToNearbyChests {
    public static class StackLogic {
        internal static Func<Chest, Item, bool> AcceptingFunction
            => ModEntry.CategorizeChests.IsActive
                   ? (chest, i) => ModEntry.CategorizeChests.ChestAcceptsItem(chest, i) || chest.ContainsItem(i)
                   : (Func<Chest, Item, bool>) ((chest, i) => chest.ContainsItem(i));

        public static IEnumerable<Chest> GetNearbyChests(this Farmer farmer, int radius)
            => GetNearbyChests(farmer.currentLocation, farmer.getTileLocation(), radius);

        public static void StashToChest(Chest chest) {
            ModEntry.Log("Stash to current chest");

            var inventory = Game1.player.Items.Where(i => i != null).ToList();
            var toBeMoved = inventory.Where(i => AcceptingFunction(chest, i)).ToList();

            if (toBeMoved.Any() && chest.DumpItemsToChest(toBeMoved).Any())
                Game1.playSound(Game1.soundBank.GetCue("pickUpItem").Name);
        }

        public static void StashToNearbyChests(int radius) {
            ModEntry.Log("Stash to nearby chests");

            var farmer = Game1.player;
            var items = farmer.Items
                              .Where(i => i != null)
                              .ToList();


            var movedAtLeastOne = false;

            foreach (var chest in farmer.GetNearbyChests(radius)) {
                var moveItems = items.Where(i => AcceptingFunction(chest, i)).ToList();

                if (!moveItems.Any())
                    continue;

                if (chest.DumpItemsToChest(moveItems).Any())
                    movedAtLeastOne = true;
            }

            if (!movedAtLeastOne)
                return;

            // List of sounds: https://gist.github.com/gasolinewaltz/46b1473415d412e220a21cb84dd9aad6
            Game1.playSound(Game1.soundBank.GetCue("pickUpItem").Name);
        }

        private static IEnumerable<Chest> GetNearbyChests(GameLocation location, Vector2 point, int radius) {
            // chests
            foreach (Chest c in GetNearbyObjects<Chest>(location, point, radius))
                yield return c;


            switch (location) {
                // fridge
                case FarmHouse farmHouse when farmHouse.upgradeLevel > 0:
                    if (InRadius(radius, point, farmHouse.getKitchenStandingSpot().X + 1, farmHouse.getKitchenStandingSpot().Y - 2))
                        yield return farmHouse.fridge.Value;
                    break;

                // buildings
                case BuildableGameLocation l:
                    foreach (var building in l.buildings.Where(b => InRadius(radius, point, b.tileX.Value, b.tileY.Value)))
                        if (building is JunimoHut junimoHut)
                            yield return junimoHut.output.Value;

                        else if (building is Mill mill)
                            yield return mill.output.Value;
                    break;
            }
        }

        private static IEnumerable<T> GetNearbyObjects<T>(GameLocation location, Vector2 point, int radius) where T : Object =>
            location.Objects.Pairs
                    .Where(p => p.Value is T && InRadius(radius, point, p.Key))
                    .Select(p => (T) p.Value);

        private static bool InRadius(int radius, Vector2 a, Vector2 b)        => Math.Abs(a.X - b.X) < radius && Math.Abs(a.Y - b.Y) < radius;
        private static bool InRadius(int radius, Vector2 a, int     x, int y) => Math.Abs(a.X - x)   < radius && Math.Abs(a.Y - y)   < radius;
    }
}