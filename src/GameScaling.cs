using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

// If you're here, i guess you shouldn't. Yes, the color fade ansi takes more space than the mod itself lmao

[BepInPlugin("com.river.GameScaling", "GameScaling", "1.0.0")]
public class GameScaling : BaseUnityPlugin
{
    public static ConfigEntry<float> MultiplierPerLevel;
    public static ConfigEntry<float> ShopBaseMultiplier;
    public static ConfigEntry<float> ShopMultiplierPerLevel;

    public static ManualLogSource Log; // <-- Static logger

    private void Awake()
    {
        Log = Logger; // <-- Save instance logger for use in patches

        // Config for valuable item scaling
        MultiplierPerLevel = Config.Bind(
            "Valuables",
            "MultiplierPerLevel",
            0.2f,
            "Value multiplier added per level after the first. (e.g. 0.2 = +20% per level)"
        );

        // Config for shop base cost
        ShopBaseMultiplier = Config.Bind(
            "Shop",
            "BaseMultiplier",
            4f,
            "Base multiplier applied to shop item value (applied to valueMax / 1000)"
        );

        // Config for shop scaling per level
        ShopMultiplierPerLevel = Config.Bind(
            "Shop",
            "MultiplierPerLevel",
            0.2f,
            "Additional shop price multiplier per level after the first (0.2 = +20% per level)"
        );

        var harmony = new Harmony("com.river.GameScaling");
        harmony.PatchAll();

        Log.LogInfo("[GameScaling] Mod loaded successfully.");
    }
}

[HarmonyPatch(typeof(ValuableObject), "DollarValueSetLogic")]
public static class Patch_ValuableObject_DollarValue
{
    static void Postfix(ValuableObject __instance)
    {
        string objName = __instance.gameObject.name.ToLower();

        GameScaling.Log.LogInfo("[ValuablePatch] Processing: " + objName);

        if (objName.Contains("surplus valuable"))
        {
            GameScaling.Log.LogInfo("[ValuablePatch] Skipped scaling for: " + objName);
            return;
        }

        int levelIndex = RunManager.instance.levelsCompleted + 1;
        if (levelIndex < 1) levelIndex = 1;

        float multiplier = 1f + GameScaling.MultiplierPerLevel.Value * (levelIndex - 1);

        // Use reflection since field is internal
        var fieldOrig = AccessTools.Field(__instance.GetType(), "dollarValueOriginal");
        var fieldCurr = AccessTools.Field(__instance.GetType(), "dollarValueCurrent");

        if (fieldOrig != null && fieldCurr != null)
        {
            float originalValue = (float)fieldOrig.GetValue(__instance);
            float newValue = originalValue * multiplier;
            fieldCurr.SetValue(__instance, newValue);

            GameScaling.Log.LogInfo("[ValuablePatch] Scaled " + objName + " to " + newValue + " (original: " + originalValue + ")");
        }
        else
        {
            GameScaling.Log.LogWarning("[ValuablePatch] Could not find dollarValue fields on " + objName);
        }
    }
}

[HarmonyPatch(typeof(ShopManager), "GetAllItemsFromStatsManager")]
public class Patch_ShopPriceScaling
{
    static void Prefix(ShopManager __instance)
    {
        int level = RunManager.instance.levelsCompleted + 1;
        float levelMultiplier = 1f + GameScaling.ShopMultiplierPerLevel.Value * (level - 1);
        float finalMultiplier = GameScaling.ShopBaseMultiplier.Value * levelMultiplier;

        int playerCount = GameDirector.instance.PlayerList.Count;
        if (playerCount > 1)
        {
            float discount = 1f - (0.05f * (playerCount - 1));
            finalMultiplier *= discount;
        }

        AccessTools.Field(typeof(ShopManager), "itemValueMultiplier").SetValue(__instance, finalMultiplier);
    }
}