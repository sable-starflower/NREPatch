using System;
using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace NREPatch
{
    public static class RimworldPatches
    {
        [HarmonyFinalizer]
        [HarmonyPatch(typeof(Pawn_StyleObserverTracker), "UpdateStyleDominanceThoughtIndex")]
        [UsedImplicitly]
        public static Exception Pawn_StyleObserverTracker_UpdateStyleDominanceThoughtIndex_ExceptionHandler(Exception __exception)
        {
            NREPLog.Warning("Exception caught from Pawn_StyleObserverTracker.UpdateStyleDominanceThoughtIndex. (" +
                            $"data={__exception.Data.ToStringSafeEnumerable()}," +
                            $"trace={__exception.StackTrace}," +
                            $"source={__exception.Source}," +
                            $"innerException={__exception.InnerException.ToStringSafe()}" +
                            ")");
            return __exception;
        }
    }
}