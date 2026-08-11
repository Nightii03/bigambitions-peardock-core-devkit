# PearPad Add-on API — v1

PearPad 1.02 contains a runtime app registry.

Goal:
PearPad stays the Core mod. Other mods can register their own PearPad apps, for example:
- PearPad: Finance
- PearBay
- PearPad: Investments

## Register an app

Add-on mods reference the PearPad assembly/API and register once during their startup.

```csharp
using PearPadRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public static class FinancePearPadApp
{
    public static void Register(Texture2D icon)
    {
        PearPadAppRegistry.Register(
            new PearPadAppDefinition(
                id: "finance-plus",
                name: "Finance",
                icon: icon,
                build: Build,
                showInLauncher: true,
                canHide: true,
                sourceName: "PearPad: Finance"));
    }

    private static void Build(
        VisualElement root,
        PearPadAppContext context)
    {
        root.Add(new Label("Finance"));
    }
}
```

PearPad Core automatically handles:
- launcher placement
- iOS-style app icon tile
- saved app order
- hide/show
- appearance themes around the app workspace
- navigation
- physical Home button
- newly installed apps being appended to the launcher
- app removal disappearing from the launcher on the next session

## App context

`PearPadAppContext` provides:
- `OpenApp(string id)`
- `GoHome()`
- `ShowToast(string message, Color color)`
- current PearPad colors: Accent, Background, Surface, Surface2, Text, Muted

## Rules

App IDs must be unique and stable.
Do not use PearPad Core IDs:
- motors
- garage
- casino
- browser
- settings
- finance

Register once at mod startup. PearPad does not scan every frame.


## Optional Finance data API

PearPad Core also exposes read-only finance snapshot types for add-ons:

```csharp
PearFinanceSnapshotData snapshot = PearFinanceService.GetSnapshot();
```

This exposes:
- current cash text
- current game day
- today income / expenses / net
- week income / expenses / net
- previous week net
- daily aggregates
- recent transactions
- detected loans

The app UI still belongs to the add-on. Core only exposes the data snapshot.
