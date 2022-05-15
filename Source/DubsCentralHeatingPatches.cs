using System;
using System.Linq;
using Verse;
using DubsCentralHeating;
using JetBrains.Annotations;

namespace NREPatch
{
    
    public static class DubsCentralHeatingPatches
    {
        [UsedImplicitly]
        public static Exception RefreshInternetsOnTile_ExceptionCatcher(Exception __exception)
        {
            return __exception;
        }

        [UsedImplicitly]
        public static bool RefreshInternetsOnTile(ref int ___MasterInternetID,  int tile)
        {
            NREPLog.Debug($"Checking RefreshInternetsOnTile for tile {tile} (MasterInternetID={___MasterInternetID})");
            if (Find.Maps.Count<Map>((Func<Map, bool>) (x =>
                {
                    if (x != null) return x.Tile == tile;
                    NREPLog.Debug("Avoiding NRE for null map returned from Verse.");
                    return false;
                })) < 2)
                return false;
            var list = PlumbingNet.AllTileNets(tile)?.ToList<PlumbingNet>();
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
                    continue;
                }
                plumbingNet.IP = -1;
                plumbingNet.slave = false;
            }
            for (var net = list.FirstOrDefault((Func<PlumbingNet, bool>) (x => x.IP == -1)); net != null; net = list.FirstOrDefault((Func<PlumbingNet, bool>) (x => x.IP == -1)))
            {
                ++___MasterInternetID;
                net.IP = ___MasterInternetID;
                net.slave = true;
                foreach (var overnet in list)
                {
                    if (overnet == net)
                        continue;
                    if (overnet.PipeComp == null)
                    {
                        NREPLog.Message($"PlumbingNet {overnet.NetID} (NetType={overnet.NetType}) had a null PipeComp");
                        continue;
                    }
                    if (net.cells.Any((Func<IntVec3, bool>) (cell => cell.InBounds(overnet.PipeComp.map) && overnet.PipeComp.PerfectMatch(cell, (PipeType) net.NetType, overnet.NetID))))
                    {
                        if (overnet.IP != -1)
                        {
                            foreach (var plumbingNet2 in list.Where((Func<PlumbingNet, bool>) (x => x.IP == overnet.IP)))
                                plumbingNet2.IP = net.IP;
                        }
                        overnet.IP = net.IP;
                    }
                }
            }
            foreach (var source in list.GroupBy((Func<PlumbingNet, int>) (x => x.IP)))
            {
                var plumbingNet3 = source.First();
                plumbingNet3.slave = false;
                foreach (var plumbingNet4 in source)
                {
                    foreach (var pipedThing in plumbingNet4.PipedThings)
                        plumbingNet3.PipedThings.Add(pipedThing);
                }
                plumbingNet3.InitNet();
                foreach (var plumbingNet5 in source)
                {
                    foreach (var pipedThing in plumbingNet3.PipedThings)
                        plumbingNet5.PipedThings.Add(pipedThing);
                    plumbingNet5.InitNet();
                }
            }
            return false;
        }
    }
}