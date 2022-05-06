using HarmonyLib;
using JetBrains.Annotations;
using Verse;

namespace NREPatch
{
    [StaticConstructorOnStartup]
    [UsedImplicitly]
    static class NREPatch
    {
        static NREPatch()
        {
            //Harmony.DEBUG = true;
            //NREPLog.DEBUG = true;
            var harmony = new Harmony("com.extropicstudios.nrepatch");

            harmony.ConditionalPatch(
                assemblyName: "DubsCentralHeating",
                className: "HygienePipeMapComp",
                methodName: "RefreshInternetsOnTile",
                patchClass: typeof(DubsCentralHeatingPatches),
                prefix: typeof(DubsCentralHeatingPatches).GetMethod("RefreshInternetsOnTile")
            );

            var patches = Harmony.GetPatchInfo(typeof(Section).GetMethod("RegenerateLayers"));
            foreach (var patcher in patches.Owners)
            {
                NREPLog.Debug($"{patcher} is patching Verse.Section.RegenerateLayers");
            }

            SafeTools.Cleanup();
        }
    }
}