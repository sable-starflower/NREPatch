
### project infrastructure

- [ ] create template for new rimworld mods https://www.jetbrains.com/help/rider/Creating_and_Opening_Projects_and_Solutions.html#create-custom-project-template
- [ ] use loadfolders from VEF https://github.com/AndroidQuazar/VanillaExpandedFramework/wiki/Toggable-patches
- [ ] github actions for build automation
  - [ ] generate release artifacts
  - [ ] bump version numbers in about.xml and manifest.xml
  - [ ] push tagged releases to steam

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
mmap(PROT_NONE) failed
Caught fatal signal - signo:6 code:-6 errno:0 addr:0x3e90005c37b
Obtained 33 stack frames.
#0  0x007fc4026343c0 in (Unknown)
#1  0x007fc40247103b in (Unknown)
#2  0x007fc402450859 in (Unknown)
#3  0x007fc346e48312 in (Unknown)
#4  0x007fc346e4837f in (Unknown)
#5  0x007fc346e4b27f in (Unknown)
#6  0x007fc346e4b4e8 in (Unknown)
#7  0x007fc346e4b956 in (Unknown)
#8  0x007fc346e4c788 in (Unknown)
#9  0x007fc346e4c8ac in (Unknown)
#10 0x007fc346e228fb in (Unknown)
#11 0x007fc346dd7de0 in (Unknown)
#12 0x007fc346dd7f57 in (Unknown)
#13 0x007fc346dd7f9f in (Unknown)
#14 0x00000040132f07 in (wrapper managed-to-native) object:__icall_wrapper_ves_icall_object_new_specific (intptr)
#15 0x007fc2c77dfdd5 in (wrapper dynamic-method) Verse.Section:Verse.Section.RegenerateLayers_Patch1 (Verse.Section,Verse.MapMeshFlag)
#16 0x00000041317ee8 in Verse.MapDrawer:TryUpdateSection (Verse.Section)
#17 0x007fc0c1af62bb in (wrapper dynamic-method) Verse.Map:Verse.Map.MapUpdate_Patch3 (Verse.Map)
#18 0x007fc0c1371f6b in (wrapper dynamic-method) Verse.Game:Verse.Game.UpdatePlay_Patch2 (Verse.Game)
#19 0x0000004013f5aa in (wrapper runtime-invoke) object:runtime_invoke_void__this__ (object,intptr,intptr,intptr)
#20 0x007fc346c667c5 in (Unknown)
#21 0x007fc346dd6390 in (Unknown)
#22 0x007fc346dd723d in (Unknown)
#23 0x007fc4032c4252 in (Unknown)
#24 0x007fc4032c25ea in (Unknown)
#25 0x007fc4032a8447 in (Unknown)
#26 0x007fc40300f96d in (Unknown)
#27 0x007fc40317dfbe in (Unknown)
#28 0x007fc40317e001 in (Unknown)
#29 0x007fc40317e53a in (Unknown)
#30 0x007fc40331c074 in (Unknown)
#31 0x007fc4024520b3 in (Unknown)
#32 0x00000000400569 in (Unknown)
```