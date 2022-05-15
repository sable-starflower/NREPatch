
### project infrastructure

- [ ] create template for new rimworld mods https://www.jetbrains.com/help/rider/Creating_and_Opening_Projects_and_Solutions.html#create-custom-project-template
- [ ] use loadfolders from VEF https://github.com/AndroidQuazar/VanillaExpandedFramework/wiki/Toggable-patches
- [ ] github actions for build automation
  - [ ] generate release artifacts
  - [ ] bump version numbers in about.xml and manifest.xml
  - [ ] push tagged releases to steam
- [ ] convert patches to Finalizers

### NREs seen in the wild

```
Exception ticking L7-A6 (at (221, 0, 116)). Suppressing further errors. Exception: System.NullReferenceException: Object reference not set to an instance of an object
  at RimWorld.Pawn_StyleObserverTracker.UpdateStyleDominanceThoughtIndex (System.Single styleDominance, System.Single pointsThreshold, System.Int32 lastIndex) [0x0002b] in <9472cef786a241bbb917e810bc0a1328>:0
  at RimWorld.Pawn_StyleObserverTracker.StyleObserverTick () [0x000fc] in <9472cef786a241bbb917e810bc0a1328>:0
  at (wrapper dynamic-method) Verse.Pawn.Verse.Pawn.Tick_Patch4(Verse.Pawn)
  at (wrapper dynamic-method) Verse.TickList.Verse.TickList.Tick_Patch0(Verse.TickList)
UnityEngine.StackTraceUtility:ExtractStackTrace ()
(wrapper dynamic-method) Verse.Log:Verse.Log.Error_Patch4 (string)
Verse.Log:ErrorOnce (string,int)
(wrapper dynamic-method) Verse.TickList:Verse.TickList.Tick_Patch0 (Verse.TickList)
(wrapper dynamic-method) Verse.TickManager:Verse.TickManager.DoSingleTick_Patch4 (Verse.TickManager)
Verse.TickManager:TickManagerUpdate ()
(wrapper dynamic-method) Verse.Game:Verse.Game.UpdatePlay_Patch2 (Verse.Game)
(wrapper dynamic-method) Verse.Root_Play:Verse.Root_Play.Update_Patch0 (Verse.Root_Play)
```

```
Exception processing alert Z_MoreAlerts.Alert_AsceticBedroomQuality: System.NullReferenceException: Object reference not set to an instance of an object
  at Z_MoreAlerts.Alert_AsceticBedroomQuality+<AffectedPawns>d__3.MoveNext () [0x00092] in <e0b73d8c298a4052b4355d4c5b382db7>:0
  at System.Collections.Generic.List`1[T]..ctor (System.Collections.Generic.IEnumerable`1[T] collection) [0x00077] in <eae584ce26bc40229c1b1aa476bfa589>:0
  at System.Linq.Enumerable.ToList[TSource] (System.Collections.Generic.IEnumerable`1[T] source) [0x00018] in <351e49e2a5bf4fd6beabb458ce2255f3>:0
  at Z_MoreAlerts.Alert_AsceticBedroomQuality.GetReport () [0x0001c] in <e0b73d8c298a4052b4355d4c5b382db7>:0
  at RimWorld.Alert.Recalculate () [0x00000] in <9472cef786a241bbb917e810bc0a1328>:0
  at (wrapper dynamic-method) RimWorld.AlertsReadout.RimWorld.AlertsReadout.CheckAddOrRemoveAlert_Patch1(RimWorld.AlertsReadout,RimWorld.Alert,bool)
UnityEngine.StackTraceUtility:ExtractStackTrace ()
(wrapper dynamic-method) Verse.Log:Verse.Log.Error_Patch4 (string)
Verse.Log:ErrorOnce (string,int)
(wrapper dynamic-method) RimWorld.AlertsReadout:RimWorld.AlertsReadout.CheckAddOrRemoveAlert_Patch1 (RimWorld.AlertsReadout,RimWorld.Alert,bool)
RimWorld.AlertsReadout:AlertsReadoutUpdate ()
(wrapper dynamic-method) RimWorld.UIRoot_Play:RimWorld.UIRoot_Play.UIRootUpdate_Patch1 (RimWorld.UIRoot_Play)
(wrapper dynamic-method) Verse.Root:Verse.Root.Update_Patch1 (Verse.Root)
(wrapper dynamic-method) Verse.Root_Play:Verse.Root_Play.Update_Patch1 (Verse.Root_Play)
```

```
Got ThingsListAt out of bounds: (244, 0, 282)
UnityEngine.StackTraceUtility:ExtractStackTrace ()
(wrapper dynamic-method) Verse.Log:Verse.Log.Error_Patch4 (string)
Verse.Log:ErrorOnce (string,int)
(wrapper dynamic-method) Verse.ThingGrid:Verse.ThingGrid.ThingsListAt_Patch1 (Verse.ThingGrid,Verse.IntVec3)
VUIE.OverlayWorker_Chairs/<>c:<.ctor>b__2_2 (Verse.IntVec3)
System.Linq.Enumerable:Any<Verse.IntVec3> (System.Collections.Generic.IEnumerable`1<Verse.IntVec3>,System.Func`2<Verse.IntVec3, bool>)
VUIE.OverlayWorker_Chairs/<>c:<.ctor>b__2_1 (Verse.Thing)
VUIE.OverlayWorker_Chairs:Notify_BuildingChanged (Verse.Thing)
VUIE.CoverageOverlays:BuildingCheck (Verse.Thing)
(wrapper dynamic-method) Verse.Thing:Verse.Thing.SpawnSetup_Patch2 (Verse.Thing,Verse.Map,bool)
(wrapper dynamic-method) Verse.ThingWithComps:Verse.ThingWithComps.SpawnSetup_Patch2 (Verse.ThingWithComps,Verse.Map,bool)
(wrapper dynamic-method) Verse.Building:Verse.Building.SpawnSetup_Patch1 (Verse.Building,Verse.Map,bool)
(wrapper dynamic-method) Verse.GenSpawn:Verse.GenSpawn.Spawn_Patch2 (Verse.Thing,Verse.IntVec3,Verse.Map,Verse.Rot4,Verse.WipeMode,bool)
Verse.GenSpawn:SpawnBuildingAsPossible (Verse.Building,Verse.Map,bool)
(wrapper dynamic-method) Verse.Map:Verse.Map.FinalizeLoading_Patch1 (Verse.Map)
(wrapper dynamic-method) Verse.Game:Verse.Game.LoadGame_Patch6 (Verse.Game)
Verse.SavedGameLoaderNow:LoadGameFromSaveFileNow (string)
Verse.Root_Play/<>c:<Start>b__1_1 ()
Verse.LongEventHandler:RunEventFromAnotherThread (System.Action)
Verse.LongEventHandler/<>c:<UpdateCurrentAsynchronousEvent>b__27_0 ()
System.Threading.ThreadHelper:ThreadStart_Context (object)
System.Threading.ExecutionContext:RunInternal (System.Threading.ExecutionContext,System.Threading.ContextCallback,object,bool)
System.Threading.ExecutionContext:Run (System.Threading.ExecutionContext,System.Threading.ContextCallback,object,bool)
System.Threading.ExecutionContext:Run (System.Threading.ExecutionContext,System.Threading.ContextCallback,object)
System.Threading.ThreadHelper:ThreadStart ()
```

### tracking down OOM issue

- fix existing known NPEs in case one of them is the root issue
  - `RimWorld.Pawn_StyleObserverTracker.UpdateStyleDominanceThoughtIndex (System.Single styleDominance, System.Single pointsThreshold, System.Int32 lastIndex) [0x0002b] in <9472cef786a241bbb917e810bc0a1328>:0`
- narrow down which mod(s) are causing it
  1. first, graph memory usage and make sure it's actually going up until it runs out
  2. make a copy of the save and remove mods one by one until i don't see the memory usage climbing anymore