using HarmonyLib;
using MinimumExperienceGain.Settings;
using TaleWorlds.MountAndBlade;

namespace MinimumExperienceGain
{
    public class SubModule : MBSubModuleBase
    {
        public static readonly string ModuleFolderName = "MinimumExperienceGain";
        public static readonly string ModuleName = "Minimum Experience Gain";
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            Harmony harmony = new Harmony("minimum_experience_gain");

            harmony.PatchAll();
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();

        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            MCMConfig.Instance.Settings();

            base.OnBeforeInitialModuleScreenSetAsRoot();
        }
    }
}