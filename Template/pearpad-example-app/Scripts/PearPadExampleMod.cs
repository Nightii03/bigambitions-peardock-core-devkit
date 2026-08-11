#nullable enable
using System;
using System.Threading.Tasks;
using BAModAPI;
using PearPadRuntime;
using UnityEngine;

[assembly: RegisterModClass(typeof(PearPadExampleMod))]

[ModEntryOnInitializationLoad]
public sealed class PearPadExampleMod : IModBigAmbitions
{
    public string[] RelativeAssetBundlePaths =>
        Array.Empty<string>();

    private bool registered;

    public Task OnLoadAsync(ModContext context)
    {
        try
        {
            registered = PearPadAppRegistry.Register(
                new PearPadAppDefinition(
                    id: "example-app",
                    name: "Example App",
                    icon: PearPadExampleRuntime.ExampleAssets.GetIcon(),
                    build: PearPadExampleRuntime.ExampleApp.Build,
                    showInLauncher: true,
                    canHide: true,
                    sourceName: "PearPad Example"));

            Debug.Log(
                registered
                    ? "[PearPad Example] Registered."
                    : "[PearPad Example] Registration skipped.");
        }
        catch (Exception ex)
        {
            Debug.LogError(
                "[PearPad Example] Registration failed: " +
                ex);
        }

        return Task.CompletedTask;
    }

    public Task OnUnloadAsync()
    {
        if (registered)
        {
            PearPadAppRegistry.Unregister("example-app");
            registered = false;
        }

        return Task.CompletedTask;
    }
}
