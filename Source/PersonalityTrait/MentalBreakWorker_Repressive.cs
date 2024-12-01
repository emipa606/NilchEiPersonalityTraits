using RimWorld;
using Verse;
using Verse.AI;

namespace PersonalityTrait;

public class MentalBreakWorker_Repressive : MentalBreakWorker
{
    public override bool BreakCanOccur(Pawn pawn)
    {
        return pawn.IsColonist && base.BreakCanOccur(pawn);
    }

    public override bool TryStart(Pawn pawn, string reason, bool causedByMood)
    {
        if (pawn.needs.mood.CurLevel > pawn.GetStatValue(StatDefOf.MentalBreakThreshold) * (1f / 7f))
        {
            Find.LetterStack.ReceiveLetter("LabelRepressionMinor".Translate(),
                "DescRepressionMinor".Translate(pawn.Name.ToStringFull, pawn.Possessive(), reason),
                LetterDefOf.NeutralEvent, pawn);
            pawn.needs.mood.thoughts.memories.TryGainMemory(PersonalityTraitDefOf.Repression);
            return false;
        }

        pawn.health.AddHediff(HediffDefOf.CatatonicBreakdown);
        pawn.needs.mood.thoughts.memories.RemoveMemoriesOfDef(PersonalityTraitDefOf.Repression);
        TrySendLetter(pawn, "LetterCatatonicMentalBreak", reason);
        return true;
    }
}