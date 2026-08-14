#nullable enable
using System;
using System.Threading.Tasks;
using BAModAPI;
using PearDockRuntime;
using UnityEngine;

[assembly: RegisterModClass(typeof(PearDockExampleRuntime.PearDockExampleMod))]

namespace PearDockExampleRuntime
{
    [ModEntryOnInitializationLoad]
    public sealed class PearDockExampleMod : IModBigAmbitions
    {
        public string[] RelativeAssetBundlePaths => Array.Empty<string>();
        private bool registered;

        public Task OnLoadAsync(ModContext context)
        {
            registered = PearDockAppRegistry.Register(
                new PearDockAppDefinition(
                    id: "example-app",
                    name: "Example App",
                    icon: ExampleAssets.GetIcon(),
                    windowSize: new Vector2(520f, 420f),
                    build: ExampleApp.Build,
                    defaultPinned: true,
                    sourceName: "PearDock Example"));

            Debug.Log(registered
                ? "[PearDock Example] Registered."
                : "[PearDock Example] Registration skipped.");

            return Task.CompletedTask;
        }

        public Task OnUnloadAsync()
        {
            if (registered)
            {
                PearDockAppRegistry.Unregister("example-app");
                registered = false;
            }

            return Task.CompletedTask;
        }
    }
}
