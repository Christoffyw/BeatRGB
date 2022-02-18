using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace beatRGB.Harmony_Patches
{
    [HarmonyPatch(typeof(AudioTimeSyncController), nameof(AudioTimeSyncController.StartSong))]
    public class SongStart
    {
        static void Postfix(float startTimeOffset)
        {
            var devices = Plugin.client.GetAllControllerData();
            for (int i = 0; i < devices.Length; i++)
            {
                var leds = Enumerable.Range(0, devices[i].Colors.Length)
                    .Select(_ => new OpenRGB.NET.Models.Color(0,0,0))
                    .ToArray();
                Plugin.client.UpdateLeds(i, leds);
            }
        }
    }
}
