using System;
using HMUI;
using beatRGB.UI.Controllers;
using BeatSaberMarkupLanguage;

namespace beatRGB.UI
{
    class beatRGBFlowCoordinator : FlowCoordinator
    {
        private static DeviceController _deviceController;
        public void Awake()
        {
            if (!_deviceController)
                _deviceController = BeatSaberUI.CreateViewController<DeviceController>();
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            try
            {
                if (firstActivation)
                {
                    SetTitle("beatRGB settings");
                    showBackButton = true;
                    ProvideInitialViewControllers(_deviceController);
                }
            }
            catch (Exception e)
            {
                Plugin.Log.Error(e);
            }
        }
        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
        }
    }
}
