using RimWorld;
using Verse;
using Verse.AI;

namespace PersonalityTrait;

internal class MentalState_Determinist : MentalState
{
    public override RandomSocialMode SocialModeMax()
    {
        return RandomSocialMode.Off;
    }

    public override void PostEnd()
    {
        base.PostEnd();
        var num = Rand.RangeInclusive(1, 6);
        pawn.health.AddHediff(PersonalityTraitDefOf.FatedExpectation);
        var firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(PersonalityTraitDefOf.FatedExpectation);
        firstHediffOfDef.Severity = num;
    }
}