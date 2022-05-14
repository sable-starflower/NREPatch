
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

### tracking down OOM issue

- fix existing known NPEs in case one of them is the root issue
  - `RimWorld.Pawn_StyleObserverTracker.UpdateStyleDominanceThoughtIndex (System.Single styleDominance, System.Single pointsThreshold, System.Int32 lastIndex) [0x0002b] in <9472cef786a241bbb917e810bc0a1328>:0`
- narrow down which mod(s) are causing it
  1. first, graph memory usage and make sure it's actually going up until it runs out
  2. make a copy of the save and remove mods one by one until i don't see the memory usage climbing anymore