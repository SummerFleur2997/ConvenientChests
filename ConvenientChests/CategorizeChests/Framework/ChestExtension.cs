#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using StardewValley;
using StardewValley.Inventories;
using StardewValley.Locations;
using StardewValley.Objects;

namespace ConvenientChests.Framework.CategorizeChests.Framework {
    internal static class ChestExtension {
        public static Chest? GetFridge(Farmer player) {
            if (Game1.player.IsMainPlayer)
                return StardewValley.Utility.getHomeOfFarmer(player).fridge.Value;

            if (Game1.currentLocation is not FarmHouse f)
                // Can't access other locations
                return null;

            if (f.owner != player)
                ModEntry.Log($"Could not get fridge for player '{player.Name}' (wrong house)");

            return f.fridge.Value;
        }

        public static Chest GetLocalFridge(Farmer player) {
            if (Game1.currentLocation is FarmHouse f)
                return f.fridge.Value;

            if (Game1.player.IsMainPlayer)
                return StardewValley.Utility.getHomeOfFarmer(player).fridge.Value;

            throw new Exception("Cooking from the outside as farmhand?");
        }

        public static bool ContainsItem(this Chest chest, Item i) => chest.Items.Any(i.canStackWith);

        ///  <summary>
        ///  Attempt to move as much as possible of the player's inventory into the given chest
        ///  </summary>
        /// <param name="chest">The chest to put the items in.</param>
        /// <param name="sourceInventory"></param>
        /// <param name="items">Items to put in</param>
        /// <returns>List of Items that were successfully moved into the chest</returns>
        public static IEnumerable<Item> DumpItemsToChest(this Inventory sourceInventory, Chest chest, IEnumerable<Item> items)
            => items.Select(item => sourceInventory.TryMoveItemToChest(chest, item))
                    .OfType<Item>()
                    .ToList();

        ///  <summary>
        ///  Attempt to move as much as possible of the given item stack into the chest.
        ///  </summary>
        ///  <param name="sourceInventory"></param>
        ///  <param name="chest">The chest to put the items in.</param>
        ///  <param name="item">The items to put in the chest.</param>
        ///  <returns>True if at least some of the stack was moved into the chest.</returns>
        public static Item? TryMoveItemToChest(this IInventory sourceInventory, Chest chest, Item item) {
            var original  = item.Stack;
            var remainder = chest.addItem(item);

            // nothing remains -> remove item
            if (remainder == null) {
                var index = sourceInventory.IndexOf(item);
                sourceInventory[index] = null;
                // item.Stack = original;
                return item;
            }

            // nothing changed
            if (remainder.Stack == item.Stack)
                return null;

            // update stack count
            item.Stack = remainder.Stack;

            // return copy for moved item
            var copy = item.Copy();
            copy.Stack = original - remainder.Stack;
            return copy;
        }

        /// <summary>
        /// Check whether the given chest has any completely empty slots.
        /// </summary>
        /// <returns>Whether at least one slot is empty.</returns>
        /// <param name="chest">The chest to check.</param>
        public static bool HasEmptySlots(this Chest chest)
            => chest.Items.Count < Chest.capacity || chest.Items.Any(i => i == null);
    }
}