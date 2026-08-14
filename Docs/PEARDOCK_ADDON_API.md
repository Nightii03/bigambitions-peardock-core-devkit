# PearDock Add-on API v1

Namespace:

```csharp
using PearDockRuntime;
```

## PearDockAppRegistry

Register an app once during mod startup:

```csharp
bool registered = PearDockAppRegistry.Register(
    new PearDockAppDefinition(
        id: "my-app",
        name: "My App",
        icon: iconTexture,
        windowSize: new Vector2(520f, 420f),
        build: Build,
        defaultPinned: true,
        sourceName: "My Add-on"));
```

Unregister on unload:

```csharp
PearDockAppRegistry.Unregister("my-app");
```

API version:

```csharp
PearDockAppRegistry.ApiVersion
```

Current value: `1`.

## PearDockAppDefinition

Constructor:

```csharp
new PearDockAppDefinition(
    string id,
    string name,
    Texture2D? icon,
    Vector2 windowSize,
    Action<VisualElement, PearDockAppContext> build,
    bool defaultPinned = true,
    string sourceName = "Add-on")
```

### id

Unique stable app id. It is normalized to lowercase and spaces become `-`.

### name

Display name shown by PearDock.

### icon

Optional `Texture2D` used in the dock. `null` is allowed; PearDock will use initials.

### windowSize

Fixed app-window size. Runtime resizing is intentionally not supported. Minimum size is 280 x 220.

### build

Called by PearDock to populate the app content area.

```csharp
private static void Build(VisualElement root, PearDockAppContext context)
{
    root.Clear();
    // Add UI Toolkit elements to root.
}
```

The root is only the content area. PearDock owns the outer window, title bar, Pin/Unpin control and X close button.

### defaultPinned

Whether the app should initially appear in the PearDock sidebar. Users can change sidebar pinning later.

### sourceName

Human-readable source/add-on name used for diagnostics.

## PearDockAppContext

```csharp
context.Close();
context.BringToFront();
```

### Close()

Closes the app window through PearDock Core.

### BringToFront()

Moves the app window in front of other floating PearDock windows.

## Window behavior

PearDock distinguishes between the normal active app and deliberately unpinned floating windows.

- Opening a normal app closes the previous normal app.
- Home closes the current normal app.
- X always closes that specific app.
- Unpin turns the current app into a floating window.
- Floating windows remain open while another app is opened.
- Floating windows can be closed with X.
- Pin returns a floating window to normal single-app behavior.

Add-ons should not implement this behavior themselves.

## Add-on responsibilities

Your add-on owns:

- app-specific UI
- app-specific game logic
- app-specific state/save data
- icon creation/loading
- choosing a sensible fixed window size

PearDock Core owns:

- dock/sidebar
- Home
- TAB behavior
- window creation
- title bar
- Pin/Unpin
- X close button
- window movement
- saved positions
- window stacking
- normal-vs-floating window rules
