#nullable enable
using System;
using UnityEngine;

namespace PearPadExampleRuntime
{
    internal static class ExampleAssets
    {
        private static Texture2D? icon;

        public static Texture2D? GetIcon()
        {
            if (icon != null)
                return icon;

            try
            {
                byte[] bytes = Convert.FromBase64String(
                    string.Concat(IconChunks));

                Texture2D texture = new Texture2D(
                    2, 2, TextureFormat.RGBA32, false);

                texture.name = "PearPadExampleIcon";
                texture.filterMode = FilterMode.Bilinear;
                texture.wrapMode = TextureWrapMode.Clamp;

                if (!ImageConversion.LoadImage(
                    texture, bytes, false))
                {
                    UnityEngine.Object.Destroy(texture);
                    return null;
                }

                icon = texture;
                return icon;
            }
            catch (Exception ex)
            {
                Debug.LogWarning(
                    "[PearPad Example] Icon load failed: " +
                    ex.Message);
                return null;
            }
        }

        private static readonly string[] IconChunks =
            new string[]
        {
            "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",
            "MzSSXwAAAABJRU5ErkJggg==",
        };
    }
}
