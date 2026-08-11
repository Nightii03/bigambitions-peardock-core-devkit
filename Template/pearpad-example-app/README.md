# PearPad Example App

Copy this whole folder into:

```text
Assets/Mods/
```

Make sure PearPad Core is also present:

```text
Assets/Mods/pearpad
Assets/Mods/pearpad-example-app
```

Then customize:

1. `PearPad.Example.asmdef`
2. `ModManifest.asset`
3. `Scripts/PearPadExampleMod.cs`
4. `Scripts/ExampleApp.cs`
5. `UIAssets/icon.png`

Important:

- Keep a unique app id.
- Keep the `"PearPad"` assembly reference in the `.asmdef`.
- Register once in `OnLoadAsync`.
- Unregister in `OnUnloadAsync`.
- Use `PearPadAppContext` colors where possible.
