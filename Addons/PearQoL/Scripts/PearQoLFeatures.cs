#nullable enable
using UnityEngine;

namespace PearQoLRuntime
{
    internal static class PearQoLFeatures
    {
        public static void ApplyAll()
        {
            Apply("employee-demands", PearQoLSettings.EmployeeDemands);
            Apply("parking-tickets", PearQoLSettings.ParkingTickets);
            Apply("parking-fees", PearQoLSettings.ParkingFees);
            Apply("vehicle-damage", PearQoLSettings.VehicleDamage);
            Apply("home-requirement", PearQoLSettings.HomeRequirement);
            ApplyWalkSpeed(PearQoLSettings.WalkSpeedMultiplier);
        }

        public static void Apply(string featureId, bool vanillaEnabled)
        {
            // The UI/config layer is complete. Each game hook gets implemented here
            // once its target class/method is verified against the current game build.
            Debug.Log("[PearQoL] " + featureId + " => " + (vanillaEnabled ? "vanilla" : "qol override"));
        }

        public static void ApplyWalkSpeed(float multiplier)
        {
            Debug.Log("[PearQoL] walk-speed => " + multiplier.ToString("0.##") + "x");
        }
    }
}
