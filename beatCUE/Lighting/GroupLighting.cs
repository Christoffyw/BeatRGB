using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BeatmapSaveData;
using OpenRGB.NET;
using OpenRGB.NET.Models;

namespace beatRGB.Lighting
{
    static class GroupLighting
    {
        public static UnityEngine.Color previousColor = new UnityEngine.Color(0,0,0);

        public static void GroupLight(UnityEngine.Color color, BeatmapEventType eventType)
        {
            if (color == previousColor)
                return;
            previousColor = color;
            var devices = Plugin.client.GetAllControllerData();

            Color newColor = color.FromUnity();
            //Plugin.Log.Info("Setting device from R: " + color.r + " G: " + color.g + " B: " + color.b + " to R: " + newColor.R + " G: " + newColor.G + " B: " + newColor.B);

            for (int i = 0; i < devices.Length; i++)
            {
                if (eventType != Configuration.PluginConfig.Instance.devices[devices[i].Name].FromNamed())
                    continue;
                var leds = Enumerable.Range(0, devices[i].Colors.Length)
                    .Select(_ => newColor)
                    .ToArray();
                Plugin.client.UpdateLeds(i, leds);
            }
        }
    }
}
