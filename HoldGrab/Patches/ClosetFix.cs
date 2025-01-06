using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoldGrab.Patches
{
    public class ClosetFix
    {
        [HarmonyPatch(typeof(PlaceableObjectsSurface), nameof(PlaceableObjectsSurface.PlaceObject))]
        [HarmonyPrefix]
        static void fixPatch1(PlayerControllerB playerWhoTriggered)
        {
            if (playerWhoTriggered == null)
            {
                HoldGrab.Logger.LogWarning("player is null in fixPatch1()");
                return;
            }

            if (!playerWhoTriggered.isHoldingObject || playerWhoTriggered.isGrabbingObjectAnimation || !(playerWhoTriggered.currentlyHeldObjectServer != null))
            {
                return;
            }

            General.GrabPostInteractTimer = 0f;
        }

    }
}
