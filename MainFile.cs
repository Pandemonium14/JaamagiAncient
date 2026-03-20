using Godot;
using HarmonyLib;
using JaamagiAncient.Relics;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace JaamagiAncient;

[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node
{
    public const string ModId = "JaamagiAncient"; //At the moment, this is used only for the Logger and harmony names.

    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } =
        new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    public static void Initialize()
    {
        Harmony harmony = new(ModId);

        harmony.PatchAll();
        
        SavedPropertiesTypeCache.InjectTypeIntoCache(typeof(FreezingFlame));
    }
    
    /*TODO assets
     *Cold Spark, Ice Tempered Ingot, Solid Blizzard
     *Icy icon
     */
}