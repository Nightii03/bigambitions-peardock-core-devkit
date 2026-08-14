# PearDock Example App Template

Minimal add-on template for PearDock Core.

## Change before using

- rename the folder
- rename `PearDock.Example.asmdef`
- change its assembly name/root namespace
- edit `ModManifest.asset`
- change app `id`, `name`, `windowSize` and `sourceName`
- replace the generated example icon if desired

## Important

The app builds only the content area. PearDock Core supplies the outer window, title bar, Pin/Unpin button and X close button.

Do not add runtime resize logic. Pick a fixed `windowSize` that fits your UI.
