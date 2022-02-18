using System.Collections.Generic;
using static BeatmapSaveData;
using beatRGB;

namespace beatRGB.Lighting
{
    static class Extensions
    {

        public static string ToNamed(this BeatmapEventType ev)
        {
            switch(ev){
                case BeatmapEventType.Event0:
                    return "Back Lasers";
                case BeatmapEventType.Event1:
                    return "Ring Lights";
                case BeatmapEventType.Event2:
                    return "Left Rotating Lasers";
                case BeatmapEventType.Event3:
                    return "Right Rotating Lasers";
                case BeatmapEventType.Event4:
                    return "Center Lights";
                case BeatmapEventType.Event5:
                    return "Boost Light Colors";
                case BeatmapEventType.Event6:
                    return "Interscope Left";
                case BeatmapEventType.Event7:
                    return "Interscope Right";
                default:
                    return "";
            }
        }
        public static BeatmapEventType FromNamed(this string named)
        {
            switch (named)
            {
                case "Back Lasers":
                    return BeatmapEventType.Event0;
                case "Ring Lights":
                    return BeatmapEventType.Event1;
                case "Left Rotating Lasers":
                    return BeatmapEventType.Event2;
                case "Right Rotating Lasers":
                    return BeatmapEventType.Event3;
                case "Center Lights":
                    return BeatmapEventType.Event4;
                case "Boost Light Colors":
                    return BeatmapEventType.Event5;
                case "Interscope Left":
                    return BeatmapEventType.Event6;
                case "Interscope Right":
                    return BeatmapEventType.Event7;
                default:
                    return BeatmapEventType.Event11;
                    //11 is unused
            }
        }
        /// <summary>
        /// takes an RGBA color and turns the alpha channel into the brightness
        /// </summary>
        /// <returns>modified color</returns>
        public static UnityEngine.Color Ify(this UnityEngine.Color color)
        {
            float H = 0;
            float S = 0;
            float V = 0;
            UnityEngine.Color.RGBToHSV(color, out H, out S, out V);
            V = color.a * 255;
            return UnityEngine.Color.HSVToRGB(H, S, V);
        }

        public static OpenRGB.NET.Models.Color FromUnity(this UnityEngine.Color color)
        {
            var hc = new OpenRGB.NET.Models.Color((byte) color.r, (byte) color.g, (byte) color.b);
 
            return hc;
        }
    }
}
