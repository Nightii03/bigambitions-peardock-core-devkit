# Troubleshooting

## My app does not appear

Check the Unity Console and `Player.log`.

You should see a registration line from your add-on.

Make sure:

- PearPad Core is installed.
- Your `.asmdef` references `PearPad`.
- Your mod class has `[assembly: RegisterModClass(...)]`.
- Your class has `[ModEntryOnInitializationLoad]`.
- Your app id is unique.
- `PearPadAppRegistry.Register(...)` is called once.

## Unity cannot resolve PearPadRuntime

PearPad Core is not present in the same Unity project, or your `.asmdef` does not reference the `PearPad` assembly.

## Duplicate app id

PearPad rejects duplicate ids. Change your add-on app id to a unique value.

## The app opens but looks wrong

Use UI Toolkit `VisualElement`, `Label`, `ScrollView`, etc.

Prefer the colors from `PearPadAppContext` so the app follows PearPad themes.

## The page is larger than the tablet

Put long content inside a `ScrollView`.

Only scroll the part that should move. Keep headings outside the `ScrollView` if they should stay fixed.

## I need game data that PearPad does not expose

Your add-on can read Big Ambitions data itself like a normal mod.

The PearPad API is mainly responsible for app integration and shared PearPad services. It does not need to contain every possible game system.
