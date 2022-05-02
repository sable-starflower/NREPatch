using System.Linq;
using DubsCentralHeating;
using HarmonyLib;
using Verse;

namespace NREPatch
{
    
    static class DubsCentralHeating
    {
        [HarmonyPatch(typeof(HygienePipeMapComp), nameof(HygienePipeMapComp.RefreshInternetsOnTile))]
        [HarmonyPrefix]
        static bool Prefix(int tile)
        {
            Log.Message($"[NREP] Checking RefreshInternetsOnTile for tile {tile}");
            if (Find.Maps.Count(x => x.Tile == tile) < 2)
                return false;
            var list = PlumbingNet.AllTileNets(tile)?.ToList();
            if (list == null)
            {
                Log.Message("[NREP] Null plumbing nets for tile");
                return false;
            }
            foreach (PlumbingNet plumbingNet in list)
            {
                if (plumbingNet == null)
                {
                    Log.Message($"null plumbing net");
                }

                return false;
            }

            return true;
        }
    }
}