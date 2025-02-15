using HarmonyLib;

namespace HoldGrab.Patches
{
    [HarmonyPatch(typeof(VehicleController))]
    public class CruiserFix
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(VehicleController.SetPlayerInControlOfVehicleClientRpc))]
        static void PatchTakeControl(VehicleController __instance)
        {
            if (GameNetworkManager.Instance.localPlayerController != __instance.currentDriver
                && GameNetworkManager.Instance.localPlayerController != __instance.currentPassenger)
                return;

            General.GrabPostInteractTimer = 0f;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(VehicleController.SetPassengerInCar))]
        static void PatchSetPassenger(VehicleController __instance)
        {
            if (GameNetworkManager.Instance.localPlayerController != __instance.currentPassenger)
                return;

            General.GrabPostInteractTimer = 0f;
        }
    }
}
