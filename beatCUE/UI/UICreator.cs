using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;

namespace beatRGB.UI
{
    class UICreator
    {
        public static beatRGBFlowCoordinator beatRGBFlowCoordinator;
        public static bool Created;

        public static void CreateMenu()
        {
            if (!Created)
            {
                MenuButton menuButton = new MenuButton("beatRGB", "Manage lighting integration", ShowFlow);
                MenuButtons.instance.RegisterButton(menuButton);
                Created = true;
            }
        }


        public static void ShowFlow()
        {
            if (beatRGBFlowCoordinator == null)
                beatRGBFlowCoordinator = BeatSaberUI.CreateFlowCoordinator<beatRGBFlowCoordinator>();
            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(beatRGBFlowCoordinator);
        }
    }
}
