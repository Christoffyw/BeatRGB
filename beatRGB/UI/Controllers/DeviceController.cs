using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using beatRGB.Lighting;
using OpenRGB.NET.Models;
using BeatSaberMarkupLanguage.Components.Settings;

namespace beatRGB.UI.Controllers
{
    [ViewDefinition("beatRGB.UI.Views.deviceManagement.bsml")]
    [HotReload("./../Views/deviceManagement.bsml")]
    class DeviceController : BSMLAutomaticViewController
    {
        Device[] devices = Plugin.client.GetAllControllerData();

        [UIComponent("device-list")] internal CustomListTableData DeviceList = new CustomListTableData();
        [UIValue("lighting-events")]
        private List<object> options = new object[] { "Back Lasers", "Ring Lights", "Left Rotating Lasers", "Right Rotating Lasers", "Center Lights", "Boost Light Colors", "Interscope Left", "Interscope Right" }.ToList();
        [UIAction("#post-parse")]
        internal void Setup()
        {
            DeviceList.data.Clear();
            foreach (var device in devices)
            {
                DeviceList.data.Add(new CustomListTableData.CustomCellInfo(device.Name, device.Type.ToString()));
                Plugin.Log.Notice(device.Name);
            }
            DeviceList.tableView.ReloadData();
        }

        [UIParams]
        BSMLParserParams parserParams;
        private string dn = "DEVICE NAME";
        private string dt = "DEVICE TYPE";
        [UIValue("device-name")]
        internal string DeviceName
        {
            get => dn;
            set
            {
                dn = value;
                NotifyPropertyChanged();
            }
        }

        [UIValue("device-type")]
        internal string DeviceType
        {
            get => dt;
            set
            {
                dt = value;
                NotifyPropertyChanged();
            }
        }

        [UIValue("light-setting")] string LightSetting
        {
            get => GetLightSetting(dn);
            set
            {
                Configuration.PluginConfig.Instance.devices[dn] = value;
            }
        }

        [UIAction("device-select")]
        public void DeviceSelect(TableView _, int row)
        {
            DeviceName = devices[row].Name;
            DeviceType = devices[row].Type.ToString();
            LightSetting = GetLightSetting(dn); 
            transform.GetChild(0).GetChild(1).GetChild(1).GetChild(2).GetChild(1).gameObject.GetComponent<DropDownListSetting>().Value = GetLightSetting(dn);
            parserParams.EmitEvent("device-modal");
        }

        string GetLightSetting(string deviceName)
        {
            string value;
            if (Configuration.PluginConfig.Instance.devices.TryGetValue(deviceName, out value))
            {
                Plugin.Log.Info(deviceName + " has " + value);
                return value;
            }
            else
            {
                Plugin.Log.Info(deviceName + " has NO VALUE");
                return "NO VALUE";
            }
        }
    }
}
