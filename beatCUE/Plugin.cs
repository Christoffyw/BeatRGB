using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IPA;
using IPA.Utilities;
using IPA.Config;
using IPA.Config.Stores;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using System.IO;
using System.Reflection;
using beatRGB.Harmony_Patches;
using HarmonyLib;
using BeatSaberMarkupLanguage;
using Color = UnityEngine.Color;
using OpenRGB.NET;

namespace beatRGB
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static Harmony Harmony { get; private set; }


    //internal static RGBSurface surface = RGBSurface.Instance;

    //internal static List<IRGBDevice> keyboards = new List<IRGBDevice>();
    //internal static List<IRGBDevice> mice = new List<IRGBDevice>();
    //internal static List<IRGBDevice> headsets = new List<IRGBDevice>();
    //internal static List<IRGBDevice> memorymodules = new List<IRGBDevice>();
    internal static OpenRGBClient client = new OpenRGBClient(name: "My OpenRGB Client", autoconnect: true, timeout: 1000);

        public void MenuLoaded()
        {
            client.LoadProfile("beforeBeatsaber");
        }

        public void WriteResourceToFile(string resourceName, string fileName)
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                }
            }
        }


        [Init]
        public void Init(IPALogger logger)
        {

            var openRGBPath = Path.Combine(UnityGame.InstallPath, "Libs", "OpenRGB.NET.dll");


            if (!File.Exists(openRGBPath))
                WriteResourceToFile("BeatRGB.Libs.OpenRGB.NET.dll", openRGBPath);
            Instance = this;
            Log = logger;
            Harmony = new Harmony("com.christoffyw.beatrgb");
            Harmony.PatchAll(Assembly.GetExecutingAssembly());

            //surface.LoadDevices(CorsairDeviceProvider.Instance);
            //surface.LoadDevices(CoolerMasterDeviceProvider.Instance);
            client.SaveProfile("beforeBeatsaber");

            BS_Utils.Utilities.BSEvents.menuSceneActive += MenuLoaded;

            UI.UICreator.CreateMenu();
        }

        
        [Init]
        public void InitWithConfig(IPA.Config.Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            for (int i = 0; i < client.GetAllControllerData().Length; i++)
            {
                var device = client.GetAllControllerData()[i];
                if (!Configuration.PluginConfig.Instance.devices.ContainsKey(device.Name))
                {
                    Log.Info("Couldn't find " + device.Name + " key, Adding to dictionary");
                    Configuration.PluginConfig.Instance.devices.Add(device.Name, "Back Lasers");
                }
            }
            for (int i = 0; i < Configuration.PluginConfig.Instance.devices.Count; i++)
            {
                Log.Info("Key: " + Configuration.PluginConfig.Instance.devices.Keys.ToArray()[i] + " Value: " + Configuration.PluginConfig.Instance.devices.Values.ToArray()[i]);
            }
        }
        
        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            beatRGBController go = new GameObject("beatRGBController").AddComponent<beatRGBController>();
        }

        [OnExit]
        public void OnApplicationExit()
        {
            client.LoadProfile("beforeBeatsaber");
            client.DeleteProfile("beforeBeatsaber");
        }
    }
}
