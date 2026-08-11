# PearPad Developer Kit

Developer resources for creating third-party PearPad apps for Big Ambitions.

**Target:** PearPad Core 1.02 / App Registry API v1

## Quick Start

1. Install PearPad Core in your Big Ambitions modding Unity project.
2. Copy `Template/pearpad-example-app` into `Assets/Mods/`.
3. Rename the project and change the manifest.
4. Build your UI in `ExampleApp.cs`.
5. Register your app with `PearPadAppRegistry`.

Full guide: [Docs/GETTING_STARTED.md](Docs/GETTING_STARTED.md)

## Included

- ready-to-use add-on template
- API documentation
- troubleshooting guide
- Steam Workshop text
- PearPad: Finance source example

## Basic registration

```csharp
PearPadAppRegistry.Register(
    new PearPadAppDefinition(
        id: "my-app",
        name: "My App",
        icon: MyAssets.GetIcon(),
        build: MyApp.Build,
        showInLauncher: true,
        canHide: true,
        sourceName: "My PearPad Add-on"));
```

PearPad Core handles the tablet shell and launcher. Your add-on only provides the app.

## Requirements

- Big Ambitions modding project
- PearPad Core 1.02
- PearPad assembly referenced in the add-on `.asmdef`
