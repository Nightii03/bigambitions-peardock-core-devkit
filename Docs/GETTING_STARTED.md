# PearPad Developer Kit

Build your own PearPad app as a separate Big Ambitions mod.

PearPad Core stays installed as the main tablet mod. Your add-on only registers an app and draws its own UI inside the PearPad workspace.

## What you need

1. A working Big Ambitions modding Unity project.
2. PearPad Core 1.02 inside the same Unity project.
3. This Developer Kit.

Recommended Unity layout:

```text
Assets/
└─ Mods/
   ├─ pearpad/
   └─ pearpad-my-app/
```

Your add-on assembly references the `PearPad` assembly. PearPad Core must therefore be present in the Unity project while you develop the add-on.

## Fast start

1. Copy `Template/pearpad-example-app` into `Assets/Mods/`.
2. Rename the folder.
3. Open `PearPad.Example.asmdef` and change the assembly name.
4. Open `ModManifest.asset` and change:
   - `ModId`
   - `DisplayName`
   - `Author`
   - `Version`
5. Rename `PearPadExampleMod.cs` and update the class name if you want.
6. Change the app id, name and source name in the registration code.
7. Replace `UIAssets/icon.png`.
8. Build your page in `ExampleApp.cs`.
9. Start the game with PearPad Core installed.
10. Your app should appear automatically on the PearPad Home screen.

## The important part

An add-on registers itself once:

```csharp
PearPadAppRegistry.Register(
    new PearPadAppDefinition(
        id: "my-app",
        name: "My App",
        icon: MyAssets.GetIcon(),
        build: MyApp.Build,
        showInLauncher: true,
        canHide: true,
        sourceName: "PearPad: My App"));
```

PearPad Core then handles the tablet shell and app launcher.

## What PearPad handles for you

- Home-screen app placement
- PearPad system shell
- app navigation
- Back/Home behavior
- theme colors
- app icon display
- registering/unregistering the app
- future add-on compatibility through the app registry

## What your add-on handles

- your own data
- your own game logic
- your own UI Toolkit page
- your own icon
- your own save data if required

## PearPadAppContext

Your page receives a `PearPadAppContext`.

Useful members:

```csharp
context.GoHome();
context.OpenApp("another-app-id");
context.ShowToast("Saved.", context.Accent);

Color accent = context.Accent;
Color background = context.Background;
Color surface = context.Surface;
Color surface2 = context.Surface2;
Color text = context.Text;
Color muted = context.Muted;
```

Use these colors instead of hard-coded colors when possible. Your app will then fit the active PearPad theme.

## App IDs

Choose one unique and stable id.

Good:
```text
myname-stocktracker
pearbay
companyname-business-tools
```

Do not use PearPad Core ids:

```text
motors
garage
casino
browser
settings
finance
```

## Loading

Register once during your mod startup. Do not scan or register every frame.

Unregister your app in `OnUnloadAsync()`.

## Example project

`Examples/PearPad-Finance` is a real separate PearPad add-on. It shows:

- a separate Big Ambitions mod
- its own assembly
- its own manifest
- its own icon
- app registration
- UI Toolkit pages
- reading a public PearPad Core service
- tabs and transaction UI

Use it as a larger reference after the basic template works.

## PearPad Core version

This kit targets:

```text
PearPad Core 1.02
PearPad App Registry API v1
```
