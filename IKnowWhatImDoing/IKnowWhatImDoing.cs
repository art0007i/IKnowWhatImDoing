using HarmonyLib;
using ResoniteModLoader;
using FrooxEngine;
using FrooxEngine.Undo;

namespace IKnowWhatImDoing;

public class IKnowWhatImDoing : ResoniteMod
{
    public const string VERSION = "1.0.0";
    public override string Name => "IKnowWhatImDoing";
    public override string Author => "art0007i";
    public override string Version => VERSION;
    public override string Link => "https://github.com/art0007i/IKnowWhatImDoing/";

    static ModConfiguration config;
    [AutoRegisterConfigKey]
    public static ModConfigurationKey<bool> KEY_ENABLED = new("enabled", "When true the mod is enabled", () => true);

    public override void OnEngineInit()
    {
        config = GetConfiguration();
        Harmony harmony = new Harmony("me.art0007i.IKnowWhatImDoing");
        harmony.PatchAll();
    }
    [HarmonyPatch(typeof(WorkerInspector), "OnRemoveComponentPressed")]
    class IKnowWhatImDoingPatch
    {
        public static bool Prefix(Worker worker)
        {
            if (!config.GetValue(KEY_ENABLED)) return true;
            Component obj = worker as Component;
            UserComponent userComponent = worker as UserComponent;
            obj?.UndoableDestroy();
            userComponent?.Destroy();
            return false;
        }
    }
}
