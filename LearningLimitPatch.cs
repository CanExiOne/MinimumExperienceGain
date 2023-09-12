using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Localization;
using MinimumExperienceGain.Settings;

namespace MinimumExperienceGain
{

    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningRate")]
    [HarmonyPatch(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(TextObject), typeof(bool) })]
    public class LearningLimitPatch
    {
        public static float minLearningRate = MCMConfig.Instance.minLearningRate;
        public static int maxSkillValue = MCMConfig.Instance.maxSkillValue;

        static void Postfix(ref ExplainedNumber __result, int skillValue)
        {
            if (skillValue >= maxSkillValue)
            {
                __result.LimitMin(0f);
            } else
            {
                __result.LimitMin(minLearningRate);
            }
        }
    }

}
