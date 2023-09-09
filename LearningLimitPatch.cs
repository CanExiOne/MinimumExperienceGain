using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Localization;

public class LearningLimitPatch
{
    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel))]
    [HarmonyPatch("CalculateLearningRate")]
    [HarmonyPatch(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(TextObject), typeof(bool) })]
    private static void Postfix(ref ExplainedNumber __result)
    {
        __result.LimitMin(1f);
    }
}
