#nullable enable
using System;
using System.Threading.Tasks;
using BAModAPI;
using PearPadRuntime;
using UnityEngine;

[assembly: RegisterModClass(typeof(PearQoLMod))]

[ModEntryOnInitializationLoad]
public sealed class PearQoLMod : IModBigAmbitions
{
    public string[] RelativeAssetBundlePaths => Array.Empty<string>();

    private bool registered;

    public Task OnLoadAsync(ModContext context)
    {
        try
        {
            registered = PearPadAppRegistry.Register(
                new PearPadAppDefinition(
                    id: "pearqol",
                    name: "PearQoL",
                    icon: null,
                    build: PearQoLRuntime.PearQoLApp.Build,
                    showInLauncher: true,
                    canHide: true,
                    sourceName: "PearQoL"));

            PearQoLRuntime.PearQoLFeatures.ApplyAll();

            Debug.Log(registered
                ? "[PearQoL] Registered with PearPad."
                : "[PearQoL] Registration skipped.");
        }
        catch (Exception ex)
        {
            Debug.LogError("[PearQoL] Load failed: " + ex);
        }

        return Task.CompletedTask;
    }

    public Task OnUnloadAsync()
    {
        if (registered)
        {
            PearPadAppRegistry.Unregister("pearqol");
            registered = false;
        }

        return Task.CompletedTask;
    }
}
