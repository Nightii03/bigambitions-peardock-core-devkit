#nullable enable
using UnityEngine;

namespace PearDockExampleRuntime
{
    internal static class ExampleAssets
    {
        private static Texture2D? icon;

        public static Texture2D GetIcon()
        {
            if (icon != null) return icon;

            icon = new Texture2D(64, 64, TextureFormat.RGBA32, false)
            {
                name = "PearDock Example Icon",
                filterMode = FilterMode.Bilinear,
                wrapMode = TextureWrapMode.Clamp
            };

            Color32 background = new Color32(58, 72, 108, 255);
            Color32 foreground = new Color32(238, 242, 250, 255);
            Color32[] pixels = new Color32[64 * 64];

            for (int y = 0; y < 64; y++)
            {
                for (int x = 0; x < 64; x++)
                {
                    bool mark = (x >= 18 && x <= 45 && y >= 29 && y <= 34) ||
                                (y >= 18 && y <= 45 && x >= 29 && x <= 34);
                    pixels[y * 64 + x] = mark ? foreground : background;
                }
            }

            icon.SetPixels32(pixels);
            icon.Apply(false, true);
            return icon;
        }
    }
}
