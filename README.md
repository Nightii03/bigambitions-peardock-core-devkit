# PearDock Core Developer Kit

Developer resources for creating third-party PearDock apps for Big Ambitions.

**Target:** PearDock Core / App Registry API v1

## PearDock concept

PearDock Core is a lightweight window manager and app launcher. Add-ons register an app and only build the content inside the window.

PearDock Core handles:

- TAB / left-side dock
- Home action
- app window title bar
- close button
- pin / unpin window mode
- floating windows
- window movement and saved position
- sidebar pinning and ordering
- fixed per-app window sizes

There is intentionally **no runtime window resize**. Each app chooses a fixed size that fits its UI.

## Quick Start

1. Install/reference PearDock Core in your Big Ambitions modding Unity project.
2. Copy `Template/peardock-example-app` into your project.
3. Rename the namespace, assembly and manifest.
4. Build your content in `ExampleApp.cs`.
5. Register it using `PearDockAppRegistry`.

Full guide: [Docs/GETTING_STARTED.md](Docs/GETTING_STARTED.md)

## Basic registration

```csharp
PearDockAppRegistry.Register(
    new PearDockAppDefinition(
        id: "my-app",
        name: "My App",
        icon: MyAssets.GetIcon(),
        windowSize: new Vector2(520f, 420f),
        build: MyApp.Build,
        defaultPinned: true,
        sourceName: "My PearDock Add-on"));
```

Your `Build` method receives the **content area only**. Do not create your own outer window, title bar or close button.

## Requirements

- Big Ambitions modding project
- Unity 2022.3.62f2 project setup used by PearDock Core
- PearDock assembly referenced by the add-on `.asmdef`

See [Docs/PEARDOCK_ADDON_API.md](Docs/PEARDOCK_ADDON_API.md) for the API contract.
