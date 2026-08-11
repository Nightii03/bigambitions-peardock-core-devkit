#nullable enable
using PearPadRuntime;
using UnityEngine;
using UnityEngine.UIElements;

namespace PearPadExampleRuntime
{
    internal static class ExampleApp
    {
        public static void Build(
            VisualElement root,
            PearPadAppContext context)
        {
            VisualElement card = new VisualElement();
            card.style.flexGrow = 1;
            card.style.paddingLeft = 22;
            card.style.paddingRight = 22;
            card.style.paddingTop = 20;
            card.style.paddingBottom = 20;
            card.style.backgroundColor = context.Surface;
            card.style.borderTopLeftRadius = 14;
            card.style.borderTopRightRadius = 14;
            card.style.borderBottomLeftRadius = 14;
            card.style.borderBottomRightRadius = 14;
            root.Add(card);

            Label title = new Label("My PearPad App");
            title.style.fontSize = 24;
            title.style.unityFontStyleAndWeight =
                FontStyle.Bold;
            title.style.color = context.Text;
            card.Add(title);

            Label text = new Label(
                "This page comes from a separate mod.");
            text.style.fontSize = 12;
            text.style.color = context.Muted;
            text.style.marginTop = 8;
            card.Add(text);

            VisualElement button = new VisualElement();
            button.style.width = 150;
            button.style.height = 40;
            button.style.marginTop = 18;
            button.style.alignItems = Align.Center;
            button.style.justifyContent = Justify.Center;
            button.style.backgroundColor = context.Accent;
            button.style.borderTopLeftRadius = 9;
            button.style.borderTopRightRadius = 9;
            button.style.borderBottomLeftRadius = 9;
            button.style.borderBottomRightRadius = 9;
            button.pickingMode = PickingMode.Position;
            card.Add(button);

            Label buttonText = new Label("TEST TOAST");
            buttonText.style.fontSize = 11;
            buttonText.style.unityFontStyleAndWeight =
                FontStyle.Bold;
            buttonText.style.color = context.Text;
            button.Add(buttonText);

            button.AddManipulator(
                new Clickable(() =>
                {
                    context.ShowToast(
                        "Hello from the add-on.",
                        context.Accent);
                }));
        }
    }
}
