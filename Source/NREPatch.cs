using HarmonyLib;
using JetBrains.Annotations;
using Verse;

namespace NREPatch
{
    [StaticConstructorOnStartup]
    [UsedImplicitly]
    internal static class NREPatch
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
                prefix: typeof(DubsCentralHeatingPatches).GetMethod("RefreshInternetsOnTile"),
                finalizer: typeof(DubsCentralHeatingPatches).GetMethod("RefreshInternetsOnTile_ExceptionCatcher")
            );

            // handy code to find all patchers of an original method
            /*
            var patches = Harmony.GetPatchInfo(typeof(Section).GetMethod("RegenerateLayers"));
            foreach (var patcher in patches.Owners)
            {
                NREPLog.Debug($"{patcher} is patching Verse.Section.RegenerateLayers");
            }
            */

            SafeTools.Cleanup();
        }
    }
}