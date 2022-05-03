using System;
using Verse;

namespace NREPatch
{
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