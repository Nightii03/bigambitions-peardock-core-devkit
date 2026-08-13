#nullable enable
using System;
using PearPadRuntime;
using UnityEngine;
using UnityEngine.UIElements;

namespace PearQoLRuntime
{
    internal static class PearQoLApp
    {
        public static void Build(VisualElement root, PearPadAppContext context)
        {
            root.style.flexGrow = 1;
            root.style.paddingLeft = 18;
            root.style.paddingRight = 18;
            root.style.paddingTop = 16;
            root.style.paddingBottom = 16;

            Label title = new Label("PearQoL");
            title.style.fontSize = 26;
            title.style.unityFontStyleAndWeight = FontStyle.Bold;
            title.style.color = context.Text;
            root.Add(title);

            Label subtitle = new Label("Small quality-of-life tweaks for Big Ambitions.");
            subtitle.style.fontSize = 12;
            subtitle.style.color = context.Muted;
            subtitle.style.marginBottom = 14;
            root.Add(subtitle);

            AddSection(root, context, "Employees");
            AddToggle(root, context, "Employee Demands", "Keep normal employee demands enabled.",
                () => PearQoLSettings.EmployeeDemands,
                value => PearQoLSettings.EmployeeDemands = value,
                "employee-demands");

            AddSection(root, context, "Vehicles & Parking");
            AddToggle(root, context, "Parking Tickets", "Keep illegal parking penalties enabled.",
                () => PearQoLSettings.ParkingTickets,
                value => PearQoLSettings.ParkingTickets = value,
                "parking-tickets");
            AddToggle(root, context, "Parking Fees", "Keep normal parking fees enabled.",
                () => PearQoLSettings.ParkingFees,
                value => PearQoLSettings.ParkingFees = value,
                "parking-fees");
            AddToggle(root, context, "Vehicle Damage", "Keep vehicle damage and wear enabled.",
                () => PearQoLSettings.VehicleDamage,
                value => PearQoLSettings.VehicleDamage = value,
                "vehicle-damage");

            AddSection(root, context, "Player");
            AddToggle(root, context, "Home Requirement", "Keep the normal home requirement enabled.",
                () => PearQoLSettings.HomeRequirement,
                value => PearQoLSettings.HomeRequirement = value,
                "home-requirement");
            AddWalkSpeed(root, context);

            Label footer = new Label("Settings save instantly. Game hooks are handled by the PearQoL runtime layer.");
            footer.style.fontSize = 10;
            footer.style.color = context.Muted;
            footer.style.marginTop = 14;
            root.Add(footer);
        }

        private static void AddSection(VisualElement root, PearPadAppContext context, string text)
        {
            Label label = new Label(text.ToUpperInvariant());
            label.style.fontSize = 10;
            label.style.unityFontStyleAndWeight = FontStyle.Bold;
            label.style.color = context.Muted;
            label.style.marginTop = 12;
            label.style.marginBottom = 6;
            root.Add(label);
        }

        private static void AddToggle(
            VisualElement root,
            PearPadAppContext context,
            string name,
            string description,
            Func<bool> getter,
            Action<bool> setter,
            string featureId)
        {
            VisualElement card = CreateCard(context);
            root.Add(card);

            VisualElement textArea = new VisualElement();
            textArea.style.flexGrow = 1;
            card.Add(textArea);

            Label nameLabel = new Label(name);
            nameLabel.style.fontSize = 14;
            nameLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
            nameLabel.style.color = context.Text;
            textArea.Add(nameLabel);

            Label descriptionLabel = new Label(description);
            descriptionLabel.style.fontSize = 10;
            descriptionLabel.style.color = context.Muted;
            descriptionLabel.style.marginTop = 3;
            textArea.Add(descriptionLabel);

            Toggle toggle = new Toggle();
            toggle.value = getter();
            toggle.style.width = 42;
            toggle.style.marginLeft = 12;
            toggle.RegisterValueChangedCallback(evt =>
            {
                setter(evt.newValue);
                PearQoLFeatures.Apply(featureId, evt.newValue);
                context.ShowToast(name + (evt.newValue ? " enabled" : " disabled"), context.Accent);
            });
            card.Add(toggle);
        }

        private static VisualElement CreateCard(PearPadAppContext context)
        {
            VisualElement card = new VisualElement();
            card.style.flexDirection = FlexDirection.Row;
            card.style.alignItems = Align.Center;
            card.style.paddingLeft = 14;
            card.style.paddingRight = 14;
            card.style.paddingTop = 12;
            card.style.paddingBottom = 12;
            card.style.marginBottom = 8;
            card.style.backgroundColor = context.Surface;
            card.style.borderTopLeftRadius = 12;
            card.style.borderTopRightRadius = 12;
            card.style.borderBottomLeftRadius = 12;
            card.style.borderBottomRightRadius = 12;
            return card;
        }

        private static void AddWalkSpeed(VisualElement root, PearPadAppContext context)
        {
            VisualElement card = CreateCard(context);
            card.style.flexDirection = FlexDirection.Column;
            card.style.alignItems = Align.Stretch;
            root.Add(card);

            Label name = new Label("Walk Speed");
            name.style.fontSize = 14;
            name.style.unityFontStyleAndWeight = FontStyle.Bold;
            name.style.color = context.Text;
            card.Add(name);

            Label description = new Label("Choose a small player movement boost.");
            description.style.fontSize = 10;
            description.style.color = context.Muted;
            description.style.marginTop = 3;
            card.Add(description);

            VisualElement row = new VisualElement();
            row.style.flexDirection = FlexDirection.Row;
            row.style.marginTop = 10;
            card.Add(row);

            float[] values = { 1f, 1.25f, 1.5f, 2f };
            foreach (float value in values)
            {
                Button button = new Button(() =>
                {
                    PearQoLSettings.WalkSpeedMultiplier = value;
                    PearQoLFeatures.ApplyWalkSpeed(value);
                    context.ShowToast("Walk speed: " + value.ToString("0.##") + "x", context.Accent);
                });
                button.text = value.ToString("0.##") + "x";
                button.style.flexGrow = 1;
                button.style.height = 34;
                button.style.marginRight = 6;
                row.Add(button);
            }
        }
    }
}
