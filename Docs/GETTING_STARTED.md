# PearDock Developer Kit

Build a PearDock app as a separate Big Ambitions mod.

PearDock Core provides the dock, window manager and window chrome. Your add-on registers one or more apps and builds only the content inside each app window.

## What you need

1. A working Big Ambitions modding Unity project.
2. PearDock Core in the same Unity project while developing.
3. This Developer Kit.

Recommended layout:

```text
Assets/
├─ PearDock/
└─ Mods/
   └─ my-peardock-app/
```

Your add-on assembly references the `PearDock` assembly.

## Fast start

1. Copy `Template/peardock-example-app` into your Unity project.
2. Rename the folder, namespace and assembly.
3. Edit `ModManifest.asset` (`ModId`, `DisplayName`, `Author`, `Version`).
4. Choose a unique app id.
5. Choose a fixed `windowSize` that fits the UI.
6. Build your content in `ExampleApp.cs`.
7. Register once in `OnLoadAsync()`.
8. Unregister in `OnUnloadAsync()`.
9. Start Big Ambitions with PearDock Core installed.

## Registration

```csharp
registered = PearDockAppRegistry.Register(
    new PearDockAppDefinition(
        id: "my-app",
        name: "My App",
        icon: ExampleAssets.GetIcon(),
        windowSize: new Vector2(520f, 420f),
        build: ExampleApp.Build,
        defaultPinned: true,
        sourceName: "My PearDock Add-on"));
```

## Fixed window size

PearDock does not resize app windows at runtime. Pick a size that fits the app.

Examples:

```text
Small utility:   420 x 360
Settings/QoL:    480 x 560
Medium app:      700 x 620
Large dashboard: 1050 x 760
```

These are suggestions, not hard limits. PearDock enforces a minimum of 280 x 220.

## What PearDock handles

- TAB / left-side dock
- opening/focusing apps
- one normal active app at a time
- automatically replacing the current normal app when another is opened
- Home = close the normal active app
- title bar and X close button
- Pin/Unpin window behavior
- floating windows after Unpin
- dragging windows
- saved window positions
- sidebar pin/unpin and ordering

## Window behavior

A newly opened app starts as the normal active app.

- Open another app: the previous normal app closes.
- Press Home: the normal app closes and no normal app is shown.
- Press X: that app closes.
- Press UNPIN: the app becomes a floating window and is no longer replaced when another app opens.
- Floating apps can also be closed with X.
- PIN returns a floating app to normal single-app behavior.

## App content

`Build` receives the content area only:

```csharp
public static void Build(VisualElement root, PearDockAppContext context)
{
    root.Clear();
    // Build the app UI here.
}
```

Do not create your own outer window/title bar/X button. PearDock Core does that.

## PearDockAppContext

Available helpers:

```csharp
context.Close();
context.BringToFront();
```

## App IDs

Use a unique stable id, for example:

```text
pearqol
pearbank
myname-stocktracker
company-tools
```

## API version

```text
PearDock App Registry API v1
```

See `Docs/PEARDOCK_ADDON_API.md` for the full contract.
