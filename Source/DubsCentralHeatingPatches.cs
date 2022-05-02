using System.Linq;
using Verse;
using DubsCentralHeating;

namespace NREPatch
{
    
    static class DubsCentralHeatingPatches
    {
        static bool RefreshInternetsOnTile(int tile)
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
            foreach (var plumbingNet in list)
            {
                if (plumbingNet == null)
                {
                    Log.Message($"[NREP] A plumbing net was null. {list.ToStringSafeEnumerable()}");
                }

                return false;
            }

            return true;
        }
    }
}