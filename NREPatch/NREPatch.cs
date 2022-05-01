using HarmonyLib;
using Verse;

namespace NREPatch
{
    [StaticConstructorOnStartup]
    static class NREPatch
    {
        static NREPatch()
        {
            var harmony = new Harmony("com.extropicstudios.nrepatch");
            harmony.PatchAll();
        }
    }
}