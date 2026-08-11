#nullable enable
using System;
using System.Threading.Tasks;
using BAModAPI;
using PearPadRuntime;
using UnityEngine;

[assembly: RegisterModClass(typeof(PearPadFinanceMod))]

[ModEntryOnInitializationLoad]
public sealed class PearPadFinanceMod : IModBigAmbitions
{
    public string[] RelativeAssetBundlePaths => Array.Empty<string>();

    private bool registered;

    public Task OnLoadAsync(ModContext context)
    {
        try
        {
            registered = PearPadAppRegistry.Register(
                new PearPadAppDefinition(
                    id: "pear-finance",
                    name: "Finance",
                    icon: PearPadFinanceRuntime.FinanceAssets.GetIcon(),
                    build: PearPadFinanceRuntime.FinanceApp.Build,
                    showInLauncher: true,
                    canHide: true,
                    sourceName: "PearPad: Finance"));

            Debug.Log(
                registered
                    ? "[PearPad: Finance] Registered with PearPad Core."
                    : "[PearPad: Finance] App was already registered.");
        }
        catch (Exception ex)
        {
            Debug.LogError(
                "[PearPad: Finance] Could not register app: " + ex);
        }

        return Task.CompletedTask;
    }

    public Task OnUnloadAsync()
    {
        if (registered)
        {
            PearPadAppRegistry.Unregister("pear-finance");
            registered = false;
        }

        return Task.CompletedTask;
    }
}
