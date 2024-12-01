using RimWorld;
using Verse;

namespace PersonalityTrait;

public class ThoughtWorker_Vehement : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn p)
    {
        if (!p.IsColonist || !p.mindState.mentalStateHandler.InMentalState)
        {
            if (!p.health.hediffSet.HasHediff(HediffDef.Named("VehementBuff")))
            {
                return false;
            }

            var hediff = p.health.hediffSet.hediffs.Find(x => x.def.defName.Equals("PsychicInvisibility"));
            p.health.RemoveHediff(hediff);
            var hediff2 = p.health.hediffSet.hediffs.Find(x => x.def.defName.Equals("VehementBuff"));
            p.health.RemoveHediff(hediff2);

            return false;
        }

        if (p.health.hediffSet.HasHediff(HediffDef.Named("VehementBuff")))
        {
            return false;
        }

        var hediff3 = HediffMaker.MakeHediff(HediffDef.Named("PsychicInvisibility"), p);
        var hediffComp_Disappears = hediff3.TryGetComp<HediffComp_Disappears>();
        if (hediffComp_Disappears != null)
        {
            hediffComp_Disappears.ticksToDisappear = 900000;
        }

        p.health.AddHediff(hediff3);
        p.health.AddHediff(PersonalityTraitDefOf.VehementBuff);

        return false;
    }
}