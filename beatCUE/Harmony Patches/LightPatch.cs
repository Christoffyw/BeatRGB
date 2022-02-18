using UnityEngine;
using static BeatmapSaveData;
using beatRGB.Lighting;
using HarmonyLib;

namespace beatRGB.Harmony_Patches
{
    [HarmonyPatch(typeof(LightSwitchEventEffect), nameof(LightSwitchEventEffect.SetColor))]
    public class LightPatch
    {
        static void Postfix(BeatmapEventType ____event, Color color)
        {
            GroupLighting.GroupLight(color.Ify(), ____event);
        }
    }
}
