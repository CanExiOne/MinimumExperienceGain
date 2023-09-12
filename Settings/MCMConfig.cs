using MCM.Abstractions.Base.Global;
using MCM.Abstractions.FluentBuilder;
using System;
using MCM.Common;
using TaleWorlds.Library;

namespace MinimumExperienceGain.Settings
{
    internal class MCMConfig : IDisposable
    {
        public float minLearningRate { get; set; }
        public bool firstRunDone { get; set; }

        private static MCMConfig _instance;

        private FluentGlobalSettings globalSettings;

        public static MCMConfig Instance
        {
            get 
            {
                if(_instance == null)
                {
                    _instance = new MCMConfig();
                }
                return _instance;
            }
        }

        public void Settings()
        {
            var builder = BaseSettingsBuilder.Create(SubModule.ModuleName, String.Format("{0} Settings", SubModule.ModuleName))!
            .SetFormat("xml")
            .SetFolderName(SubModule.ModuleFolderName)
                .SetSubFolder(SubModule.ModuleName)
                .CreateGroup("General Settings", groupBuilder => groupBuilder
                    .AddFloatingInteger("MinimumLearningRate", "Minimum Learning Rate", 0, 100, new ProxyRef<float>(() => minLearningRate, o => minLearningRate = o), floatingBuilder => floatingBuilder
                        .SetHintText("Change Minimum Learning Rate for Skills")
                        .SetRequireRestart(true))
                    )
                .CreateGroup("Reset Settings", groupBuilder => groupBuilder
                    .AddBool("firstrun", "Reset Settings", new ProxyRef<bool>(() => firstRunDone, o => firstRunDone = o), boolBuilder => boolBuilder
                    .SetHintText("Uncheck to reset settings")
                    .SetRequireRestart(true))); ;

            globalSettings = builder.BuildAsGlobal();
            globalSettings.Register();

            if (!firstRunDone)
            {
                Perform_First_Time_Setup();
            }


        }

        private void Perform_First_Time_Setup()
        {
            InformationManager.DisplayMessage(new InformationMessage("Settings Updated"));
            Instance.minLearningRate = 0.75f;
            Instance.firstRunDone = true;
        }

        public void Dispose()
        {
            //MCMConfig.Unregister();
        }
    }
}
