

using DubsCentralHeating;
using HarmonyLib;

namespace NREPatch
{
    [HarmonyPatch(typeof(HygienePipeMapComp), nameof(HygienePipeMapComp.RefreshInternetsOnTile))]
    static class DubsCentralHeating
    {
        static void Prefix(int tile)
        {
            
        }
    }
}