#nullable enable
using PearDockRuntime;
using UnityEngine;
using UnityEngine.UIElements;

namespace PearDockExampleRuntime
{
    internal static class ExampleApp
    {
        public static void Build(VisualElement root, PearDockAppContext context)
        {
            root.Clear();
            root.style.flexGrow = 1;
            root.style.paddingLeft = 18;
            root.style.paddingRight = 18;
            root.style.paddingTop = 18;
            root.style.paddingBottom = 18;
            root.style.backgroundColor = new Color32(20, 22, 27, 255);

            Label title = new Label("Example App");
            title.style.fontSize = 24;
            title.style.unityFontStyleAndWeight = FontStyle.Bold;
            title.style.color = new Color32(238, 241, 247, 255);
            root.Add(title);

            Label description = new Label("This UI is provided by a separate PearDock add-on. PearDock Core owns the outer window.");
            description.style.whiteSpace = WhiteSpace.Normal;
            description.style.fontSize = 12;
            description.style.color = new Color32(160, 169, 188, 255);
            description.style.marginTop = 8;
            description.style.marginBottom = 18;
            root.Add(description);

            VisualElement card = new VisualElement();
            card.style.paddingLeft = 14;
            card.style.paddingRight = 14;
            card.style.paddingTop = 14;
            card.style.paddingBottom = 14;
            card.style.backgroundColor = new Color32(29, 32, 39, 255);
            card.style.borderTopLeftRadius = 8;
            card.style.borderTopRightRadius = 8;
            card.style.borderBottomLeftRadius = 8;
            card.style.borderBottomRightRadius = 8;
            root.Add(card);

            Label status = new Label("Window size: 520 x 420");
            status.style.fontSize = 12;
            status.style.color = new Color32(220, 225, 236, 255);
            card.Add(status);

            Button front = new Button(context.BringToFront) { text = "Bring to front" };
            front.style.width = 150;
            front.style.height = 38;
            front.style.marginTop = 14;
            card.Add(front);

            Button close = new Button(context.Close) { text = "Close this app" };
            close.style.width = 150;
            close.style.height = 38;
            close.style.marginTop = 8;
            card.Add(close);
        }
    }
}
