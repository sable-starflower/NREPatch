using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace NREPatch
{
    /**
     * Tools to perform Harmony patches conditionally only if the assembly to patch is loaded.
     *
     * @TODO: Extract to a separate NuGet package
     */
    public static class SafeTools
    {
        public static void Cleanup()
        {
            Lookup.Free();
        }

        /**
         * Get info on a method if the assembly containing it is loaded.
         */
        public static MethodInfo? GetMethod(string assemblyName, string className, string methodName)
        {
            return FindClass(assemblyName, className)?.GetMethod(methodName);
        }
        
        public static Type? FindClass(string assemblyName, string className)
        {
            NREPLog.Debug($"Looking for class {className} in assembly {assemblyName}");
            var asm = Lookup.Assembly(assemblyName);
            if (asm == null)
            {
                NREPLog.Debug($"Could not find assembly {assemblyName}");
                return null;
            }

            NREPLog.Debug($"Found assembly {asm.FullName} for patching, looking for class {className}");
            // TODO: lazy initialize dictionary lookup
            foreach (var type in asm.GetTypes())
            {
                if (type.IsClass)
                {
                    NREPLog.Debug($"scanning class {type.Name} (fullName={type.FullName})");
                }

                if (type.IsClass && type.Name == className)
                {
                    return type;
                }
            }

            return null;
        }

        /**
         * Extend Harmony class with a conditional patch method
         */
        public static MethodInfo? ConditionalPatch(
            this Harmony harmony,
            string assemblyName,
            string className,
            string methodName,
            MethodInfo? prefix = null,
            MethodInfo? transpiler = null,
            MethodInfo? postfix = null,
            MethodInfo? finalizer = null
            )
        {
            NREPLog.Debug($"Attempting to patch {className}.{methodName}");

            var originalMethod = GetMethod(assemblyName, className, methodName);
            if (originalMethod == null)
            {
                NREPLog.Debug($"{className}.{methodName} not found, skipping");
                return null;
            }

            var methodInfo = harmony.Patch(
                original: originalMethod,
                prefix: prefix != null ? new HarmonyMethod(prefix) : null,
                transpiler: transpiler != null ? new HarmonyMethod(transpiler) : null,
                postfix: postfix != null ? new HarmonyMethod(postfix) : null,
                finalizer: finalizer != null ? new HarmonyMethod(finalizer) : null
                );

            NREPLog.Message($"{className}.{methodName} patched.");

            return methodInfo;
        }
    }

    /**
     * Internal private class used to store cache dictionaries
     */
    internal static class Lookup
    {
        public static void Free()
        {
            _assemblyDict = null;
        }

        public static Assembly? Assembly(string assemblyName)
        {
            _assemblyDict ??= InitializeAssemblyDict();
            return _assemblyDict.GetValueOrDefault(assemblyName);
        }

        private static Dictionary<string, Assembly> InitializeAssemblyDict()
        {
            NREPLog.Debug($"Initializing assembly lookup dictionary");
            var assemblyDict = new Dictionary<string, Assembly>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asm in assemblies)
            {
                if (asm != null)
                {
                    NREPLog.Debug($"Adding assembly {asm.GetName().Name} to lookup dictionary (fullname={asm.FullName})");
                    assemblyDict.AddDistinct(asm.GetName().Name, asm);
                }
            }

            return assemblyDict;
        }

        private static Dictionary<string, Assembly>? _assemblyDict;
    }
}