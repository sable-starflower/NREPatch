using System;
using System.Linq;
using System.Reflection;
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

            harmony.ConditionalPatch(
                className: "DubsCentralHeating.HygienePipeMapComp",
                methodName: "RefreshInternetsOnTile",
                prefix: new HarmonyMethod(typeof(DubsCentralHeatingPatches).GetMethod("RefreshInternetsOnTile"))
                );
        }

        private static MethodInfo? ConditionalPatch(
            this Harmony harmony,
            String className,
            String methodName,
            HarmonyMethod? prefix = null)
        {
            var originalMethod = FindClass(className)?.GetMethod(methodName);
            if (originalMethod == null)
            {
                return null;
            }

            return harmony.Patch(originalMethod, prefix: prefix);
        }

        public static Type? FindClass(String className)
        {
            return (from asm in AppDomain.CurrentDomain.GetAssemblies()
                from type in asm.GetTypes()
                where type.IsClass && type.Name == className
                select type).Single();
        }
    }
}