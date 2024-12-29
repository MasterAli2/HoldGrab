using GameNetcodeStuff;
using HarmonyLib;

namespace HoldGrab.Patches
{
    public class General
    {
        [HarmonyPatch(typeof(PlayerControllerB), nameof(PlayerControllerB.ClickHoldInteraction))]
        [HarmonyPostfix]
        private static void ClickHoldPostfix(PlayerControllerB __instance)
        {
            if (!IngamePlayerSettings.Instance.playerInput.actions.FindAction("Interact").IsPressed())
                return;

            if (__instance.IsOwner && __instance.isPlayerDead && (!__instance.IsServer || __instance.isHostPlayerObject))
                return;

            if (((!__instance.IsOwner || !__instance.isPlayerControlled || (__instance.IsServer && !__instance.isHostPlayerObject)) && !__instance.isTestingPlayer) || false /*!context.performed*/ || __instance.timeSinceSwitchingSlots < (0.2f) || __instance.inSpecialMenu)
                return;

            if (!__instance.isGrabbingObjectAnimation && !__instance.isTypingChat && !__instance.inTerminalMenu && !__instance.throwingObject && !__instance.IsInspectingItem && !(__instance.inAnimationWithEnemy != null) && !__instance.jetpackControls && !__instance.disablingJetpackControls && !StartOfRound.Instance.suckingPlayersOutOfShip)
                if (!__instance.activatingItem && !__instance.waitingToDropItem)
                    __instance.BeginGrabObject();
        }
    }
}
