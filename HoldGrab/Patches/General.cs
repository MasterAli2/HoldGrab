using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace HoldGrab.Patches
{
    public class General
    {
        public static float GrabPostInteractTimer = 1f;

        [HarmonyPatch(typeof(PlayerControllerB), nameof(PlayerControllerB.ClickHoldInteraction))]
        [HarmonyPostfix]
        private static void ClickHoldPostfix(PlayerControllerB __instance)
        {
            GrabPostInteractTimer += Time.deltaTime;

            if (__instance.isHoldingInteract == false)
                return;

            if (__instance.IsOwner && __instance.isPlayerDead && (!__instance.IsServer || __instance.isHostPlayerObject))
            {
                return;
            }
            else
            {
                if (((!__instance.IsOwner || !__instance.isPlayerControlled || (__instance.IsServer && !__instance.isHostPlayerObject)) && !__instance.isTestingPlayer) || false/*(!context.performed)*/ || __instance.timeSinceSwitchingSlots < 0.2f || __instance.inSpecialMenu)
                {
                    return;
                }
                // ShipBuildModeManager.Instance.CancelBuildMode();
                if (!__instance.isGrabbingObjectAnimation && !__instance.isTypingChat && !__instance.inTerminalMenu && !__instance.throwingObject && !__instance.IsInspectingItem && !(__instance.inAnimationWithEnemy != null) && !__instance.jetpackControls && !__instance.disablingJetpackControls && !StartOfRound.Instance.suckingPlayersOutOfShip)
                {
                    if (!__instance.activatingItem && !__instance.waitingToDropItem)
                    {
                        if (GrabPostInteractTimer < 0.25f)
                            return;

                        __instance.BeginGrabObject();
                    }
                    if (!(__instance.hoveringOverTrigger == null) && !__instance.hoveringOverTrigger.holdInteraction && (!__instance.isHoldingObject || __instance.hoveringOverTrigger.oneHandedItemAllowed) && (!__instance.twoHanded || (__instance.hoveringOverTrigger.twoHandedItemAllowed && !__instance.hoveringOverTrigger.specialCharacterAnimation)) && __instance.InteractTriggerUseConditionsMet())
                    {
                        __instance.hoveringOverTrigger.Interact(__instance.thisPlayerBody);
                    }
                }
            }
        }
    }
}
