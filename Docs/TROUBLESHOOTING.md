# Troubleshooting

## My app does not appear

Check the Unity Console and `Player.log`.

Make sure:

- PearDock Core is installed.
- Your `.asmdef` references `PearDock`.
- Your mod class has `[assembly: RegisterModClass(...)]`.
- Your class has `[ModEntryOnInitializationLoad]`.
- your app id is unique.
- `PearDockAppRegistry.Register(...)` is called once.
- `defaultPinned` is `true` if you expect it to appear in the sidebar by default.

If it is not pinned in the sidebar, open the PearDock app library and pin it there.

## Unity cannot resolve PearDockRuntime

PearDock Core is not present in the same Unity project, or your `.asmdef` does not reference the `PearDock` assembly.

## Duplicate app id

PearDock rejects duplicate ids. Use a unique and stable app id.

## My content is clipped

PearDock windows have a fixed size. Increase the registered `windowSize` or put long content inside a `ScrollView`.

Do not add runtime resize logic to work around this.

## I accidentally built another title bar

Do not create an outer window, title bar, Pin/Unpin button or X button inside the add-on UI. PearDock Core creates those. Your `Build` method receives only the content root.

## Opening another app closes mine

That is normal for a pinned/normal app. PearDock shows one normal app at a time.

Use the window's **UNPIN** control if the user should keep that app open as a floating window while opening another app.

## Home closes my app

That is expected. Home closes the current normal app. Unpinned floating windows remain independent and can be closed with X.

## I need game data PearDock does not expose

Your add-on can read Big Ambitions data itself like any other mod. PearDock's API is responsible for app/window integration, not for exposing every game system.
