using System;
using System.Collections.Generic;
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
            //Harmony.DEBUG = true;
            //NREPLog.DEBUG = true;
            var harmony = new Harmony("com.extropicstudios.nrepatch");

            harmony.ConditionalPatch(
                assemblyName: "DubsCentralHeating",
                className: "HygienePipeMapComp",
                methodName: "RefreshInternetsOnTile",
                patchClass: typeof(DubsCentralHeatingPatches),
                prefix: typeof(DubsCentralHeatingPatches).GetMethod("RefreshInternetsOnTile")
            );

            Lookup.Free();
        }

        private static MethodInfo? ConditionalPatch(
            this Harmony harmony,
            String assemblyName,
            String className,
            String methodName,
            Type? patchClass,
            MethodInfo? prefix = null)
        {
            NREPLog.Debug($"Attempting to patch {className}.{methodName}");

            if (patchClass == null)
            {
                NREPLog.Debug($"Could not find patch class for {className}");
                return null;
            }

            HarmonyMethod? prefixMethod = null;
            if (prefix != null)
            {
                var prefixMethodInfo = patchClass.GetMethod(methodName);
                if (prefixMethodInfo == null)
                {
                    NREPLog.Warning($"Could not find expected method {methodName} to patch in {patchClass.Name}");
                    return null;
                }

                prefixMethod = new HarmonyMethod(patchClass.GetMethod(methodName));
            }

            var originalMethod = FindClass(assemblyName, className)?.GetMethod(methodName);
            if (originalMethod == null)
            {
                NREPLog.Debug($"{className}.{methodName} not found, skipping");
                return null;
            }

            var methodInfo = harmony.Patch(originalMethod, prefix: prefixMethod);

            NREPLog.Message($"{className}.{methodName} patched.");

            return methodInfo;
        }

        public static Type? FindClass(String assemblyName, String className)
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
    }

    static class Lookup
    {
        public static void Free()
        {
            _assemblyDict = null;
        }

        public static Assembly? Assembly(String assemblyName)
        {
            if (_assemblyDict == null)
            {
                _assemblyDict = InitializeAssemblyDict();
            }
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