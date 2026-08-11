#nullable enable
using System;
using UnityEngine;

namespace PearPadFinanceRuntime
{
    internal static class FinanceAssets
    {
        private static Texture2D? icon;

        public static Texture2D? GetIcon()
        {
            if (icon != null) return icon;
            try
            {
                string data = string.Concat(IconChunks);
                byte[] bytes = Convert.FromBase64String(data);
                Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                tex.name = "PearPadFinanceIcon";
                tex.filterMode = FilterMode.Bilinear;
                tex.wrapMode = TextureWrapMode.Clamp;
                if (!ImageConversion.LoadImage(tex, bytes, false))
                {
                    UnityEngine.Object.Destroy(tex);
                    return null;
                }
                icon = tex;
                return icon;
            }
            catch (Exception ex)
            {
                Debug.LogWarning("[PearPad: Finance] Icon load failed: " + ex.Message);
                return null;
            }
        }

        private static readonly string[] IconChunks = new string[]
        {
            "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",
            "L6b84NhCJ3S7AAAAAElFTkSuQmCC",
        };
    }
}
