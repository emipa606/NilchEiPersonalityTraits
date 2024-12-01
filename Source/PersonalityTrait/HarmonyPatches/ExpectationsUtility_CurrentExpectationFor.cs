using HarmonyLib;
using RimWorld;
using Verse;

namespace PersonalityTrait;

[HarmonyPatch(typeof(ExpectationsUtility), nameof(ExpectationsUtility.CurrentExpectationFor), typeof(Pawn))]
internal class ExpectationsUtility_CurrentExpectationFor
{
    public static void Postfix(Pawn p, ref ExpectationDef __result)
    {
        if (!p.story.traits.HasTrait(PersonalityTraitDefOf.Determinist) || p.MapHeld == null)
        {
            return;
        }

        var firstHediffOfDef = p.health.hediffSet.GetFirstHediffOfDef(PersonalityTraitDefOf.FatedExpectation);
        if (firstHediffOfDef == null)
        {
            return;
        }

        switch (firstHediffOfDef.Severity)
        {
            case >= 5f:
                __result = ExpectationDefOf.High;
                break;
            case 4f:
                __result = ExpectationDefOf.Moderate;
                break;
            case 3f:
                __result = ExpectationDefOf.Low;
                break;
            case 2f:
                __result = ExpectationDefOf.VeryLow;
                break;
            default:
                __result = ExpectationDefOf.ExtremelyLow;
                break;
        }
    }
}