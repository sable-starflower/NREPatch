using System.Linq;
using Verse;
using DubsCentralHeating;

namespace NREPatch
{
    
    public static class DubsCentralHeatingPatches
    {
        public static bool RefreshInternetsOnTile(int tile)
        {
            NREPLog.Debug($"Checking RefreshInternetsOnTile for tile {tile}");
            if (Find.Maps.Count(x => x.Tile == tile) < 2)
                return false;
            var list = PlumbingNet.AllTileNets(tile)?.ToList();
            if (list == null)
            {
                NREPLog.Message($"Null plumbing nets for tile {tile}");
                return false;
            }
            foreach (var plumbingNet in list)
            {
                if (plumbingNet == null)
                {
                    NREPLog.Message($"A plumbing net in the list was null. {list.ToStringSafeEnumerable()}");
                }

                return false;
            }

            NREPLog.Debug($"Couldn't find any issue with plumbing nets on tile {tile}, running unpatched code");

            return true;
        }
    }
}