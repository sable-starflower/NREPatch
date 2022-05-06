using System;
using Verse;

namespace NREPatch
{
    /**
     * Extend the Rimworld logger with some nice-to-have features for mods
     *
     * TODO: extract this to its own NuGet package
     */
    public static class NREPLog
    {
        public static bool DEBUG = false;
        
        public static void Debug(String message)
        {
            if (DEBUG)
            {
                Message(message);
            }

        }

        public static void Message(String message)
        {
            Log.Message($"[NREP] {message}");
        }

        public static void Warning(String message)
        {
            Log.Warning($"[NREP] {message}");
        }
    }
}