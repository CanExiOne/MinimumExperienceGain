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

        static bool Prefix(DefaultCharacterDevelopmentModel __instance, ref ExplainedNumber __result,
           int attributeValue, int focusValue, int skillValue, int characterLevel, TextObject attributeName, bool includeDescriptions = false)
        {
            TextObject _skillFocusText = Traverse.Create<DefaultCharacterDevelopmentModel>().Field("_skillFocusText").GetValue<TextObject>();
            TextObject _overLimitText = Traverse.Create<DefaultCharacterDevelopmentModel>().Field("_overLimitText").GetValue<TextObject>();

            ExplainedNumber result = new ExplainedNumber(1.25f, includeDescriptions);
            result.AddFactor(0.4f * (float)attributeValue, attributeName);
            result.AddFactor((float)focusValue * 1f, _skillFocusText);

            var CalculateLearningLimit = AccessTools.Method(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningLimit",
                new Type[] { typeof(int), typeof(int), typeof(TextObject), typeof(bool) });

            ExplainedNumber learningLimit = (ExplainedNumber)CalculateLearningLimit.Invoke(__instance, new object[] { attributeValue, focusValue, null, null });
            int num = TaleWorlds.Library.MathF.Round(learningLimit.ResultNumber);
            if (skillValue > num)
            {
                int num2 = skillValue - num;
                result.AddFactor(-1f - 0.1f * (float)num2, _overLimitText);
            }
            if (skillValue >= maxSkillValue)
            {
                result.LimitMin(0f);
            } else
            {
                result.LimitMin(minLearningRate);
            }

            __result = result;

            return false;

        }
    }

}
