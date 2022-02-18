using UnityEngine;
using static BeatmapSaveData;
using beatRGB.Lighting;
using HarmonyLib;

namespace beatRGB.Harmony_Patches
{
    [HarmonyPatch(typeof(LightSwitchEventEffect), nameof(LightSwitchEventEffect.SetColor))]
    public class LightPatch
    {
        [HarmonyAfter(new string[] { "com.noodle.BeatSaber.Chroma", "com.noodle.BeatSaber.ChromaCore", "com.noodle.BeatSaber.Technicolor" })]
        static void Postfix(BeatmapEventType ____event, Color color)
        {
            GroupLighting.GroupLight(color.Ify(), ____event);
        }
    }
}
