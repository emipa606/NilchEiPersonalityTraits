using RimWorld;
using Verse;

namespace PersonalityTrait;

public class ThoughtWorker_Motivated : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn p)
    {
        if (!p.IsColonist || !p.Inspired)
        {
            return false;
        }

        return ThoughtState.ActiveAtStage(0);
    }
}