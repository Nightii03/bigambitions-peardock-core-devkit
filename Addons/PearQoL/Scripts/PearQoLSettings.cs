#nullable enable
using UnityEngine;

namespace PearQoLRuntime
{
    internal static class PearQoLSettings
    {
        private const string Prefix = "PearQoL.";

        public static bool EmployeeDemands
        {
            get => PlayerPrefs.GetInt(Prefix + "EmployeeDemands", 1) != 0;
            set => SaveBool("EmployeeDemands", value);
        }

        public static bool ParkingTickets
        {
            get => PlayerPrefs.GetInt(Prefix + "ParkingTickets", 1) != 0;
            set => SaveBool("ParkingTickets", value);
        }

        public static bool ParkingFees
        {
            get => PlayerPrefs.GetInt(Prefix + "ParkingFees", 1) != 0;
            set => SaveBool("ParkingFees", value);
        }

        public static bool VehicleDamage
        {
            get => PlayerPrefs.GetInt(Prefix + "VehicleDamage", 1) != 0;
            set => SaveBool("VehicleDamage", value);
        }

        public static bool HomeRequirement
        {
            get => PlayerPrefs.GetInt(Prefix + "HomeRequirement", 1) != 0;
            set => SaveBool("HomeRequirement", value);
        }

        public static float WalkSpeedMultiplier
        {
            get => PlayerPrefs.GetFloat(Prefix + "WalkSpeedMultiplier", 1f);
            set
            {
                PlayerPrefs.SetFloat(Prefix + "WalkSpeedMultiplier", Mathf.Clamp(value, 1f, 2f));
                PlayerPrefs.Save();
            }
        }

        private static void SaveBool(string key, bool value)
        {
            PlayerPrefs.SetInt(Prefix + key, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}
