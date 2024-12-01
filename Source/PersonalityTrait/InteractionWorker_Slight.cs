using RimWorld;
using Verse;

namespace PersonalityTrait;

public class InteractionWorker_Slight : InteractionWorker
{
    private const float BaseSelectionWeight = 0.02f;

    public override float RandomSelectionWeight(Pawn initiator, Pawn recipient)
    {
        return BaseSelectionWeight *
               TsundereNegativeInteractionUtility.NegativeInteractionChanceFactor(initiator, recipient);
    }
}