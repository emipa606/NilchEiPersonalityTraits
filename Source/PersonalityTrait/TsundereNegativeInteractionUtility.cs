using RimWorld;
using Verse;

namespace PersonalityTrait;

public static class TsundereNegativeInteractionUtility
{
    private const float AbrasiveSelectionChanceFactor = 2.3f;

    private static readonly SimpleCurve compatibilityFactorCurve =
    [
        new CurvePoint(-2.5f, 4f),
        new CurvePoint(-1.5f, 3f),
        new CurvePoint(-0.5f, 2f),
        new CurvePoint(0.5f, 1f),
        new CurvePoint(1f, 0.75f),
        new CurvePoint(2f, 0.5f),
        new CurvePoint(3f, 0.4f)
    ];

    private static readonly SimpleCurve opinionFactorCurve =
    [
        new CurvePoint(-100f, 6f),
        new CurvePoint(-50f, 4f),
        new CurvePoint(-25f, 2f),
        new CurvePoint(0f, 1f),
        new CurvePoint(50f, 0.1f),
        new CurvePoint(100f, 0f)
    ];

    private static readonly SimpleCurve tsundereCompatibilityFactorCurve =
    [
        new CurvePoint(-2.5f, 0.4f),
        new CurvePoint(-1.5f, 0.5f),
        new CurvePoint(-0.5f, 0.75f),
        new CurvePoint(0.5f, 1f),
        new CurvePoint(1.5f, 1.5f),
        new CurvePoint(2.5f, 2f),
        new CurvePoint(3.5f, 2.5f)
    ];

    private static readonly SimpleCurve tsundereOpinionFactorCurve =
    [
        new CurvePoint(-100f, 0f),
        new CurvePoint(-50f, 0.1f),
        new CurvePoint(-25f, 0.5f),
        new CurvePoint(0f, 1f),
        new CurvePoint(50f, 2f),
        new CurvePoint(100f, 6f)
    ];

    public static float NegativeInteractionChanceFactor(Pawn initiator, Pawn recipient)
    {
        if (initiator.story.traits.HasTrait(TraitDefOf.Kind))
        {
            return 0f;
        }

        var num = 1f;
        if (initiator.story.traits.HasTrait(PersonalityTraitDefOf.Tsundere) &&
            !LovePartnerRelationUtility.LovePartnerRelationExists(initiator, recipient))
        {
            num *= tsundereOpinionFactorCurve.Evaluate(initiator.relations.OpinionOf(recipient));
            num *= tsundereCompatibilityFactorCurve.Evaluate(initiator.relations.CompatibilityWith(recipient));
        }
        else
        {
            num *= opinionFactorCurve.Evaluate(initiator.relations.OpinionOf(recipient));
            num *= compatibilityFactorCurve.Evaluate(initiator.relations.CompatibilityWith(recipient));
        }

        if (initiator.story.traits.HasTrait(TraitDefOf.Abrasive))
        {
            num *= AbrasiveSelectionChanceFactor;
        }

        return num;
    }
}