using RimWorld;
using Verse;
using Verse.AI;

namespace PersonalityTrait;

public class MentalStateWorker_Determinist : MentalStateWorker
{
    public override bool StateCanOccur(Pawn pawn)
    {
        if (!base.StateCanOccur(pawn))
        {
            return false;
        }

        if (!ModLister.RoyaltyInstalled)
        {
            return true;
        }

        foreach (var item in Find.FactionManager.AllFactionsListForReading)
        {
            if (!pawn.royalty.HasAnyTitleIn(item))
            {
                continue;
            }

            var currentTitleInFaction = pawn.royalty.GetCurrentTitleInFaction(item);
            if (currentTitleInFaction == null || currentTitleInFaction.def.minExpectation == null)
            {
                continue;
            }

            var firstHediffOfDef =
                pawn.health.hediffSet.GetFirstHediffOfDef(PersonalityTraitDefOf.FatedExpectation);
            if (firstHediffOfDef == null)
            {
                return false;
            }

            Find.LetterStack.ReceiveLetter("LabelRoyalDiv".Translate(),
                "DescRoyalDiv".Translate(pawn.Name.ToStringFull, pawn.Possessive(), pawn.ProSubjCap()),
                LetterDefOf.NeutralEvent, pawn);
            pawn.health.RemoveHediff(firstHediffOfDef);

            return false;
        }

        return true;
    }
}