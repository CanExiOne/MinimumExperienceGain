using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Localization;

namespace MinimumExperienceGain
{
    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningRate")]
    [HarmonyPatch(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(TextObject), typeof(bool) })]
    public class LearningLimitPatch
    {
        static void Postfix(ref ExplainedNumber __result, int skillValue)
        {
            if (skillValue >= Settings.Instance.MaxSkillValue)
            {
                __result.LimitMin(0f);
            } 
            else
            {
                __result.LimitMin(Settings.Instance.MinLearningRate);
            }
        }
    }
}