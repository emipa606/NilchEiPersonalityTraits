using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace PersonalityTrait.HarmonyPatches;

[HarmonyPatch(typeof(GenAI), nameof(GenAI.CanBeArrestedBy))]
internal class GenAI_CanBeArrestedBy
{
    public static void Postfix(Pawn pawn, Pawn arrester, ref bool __result)
    {
        __result = pawn.RaceProps.Humanlike && (!pawn.InAggroMentalState || !pawn.HostileTo(arrester)) &&
                   !pawn.HostileTo(Faction.OfPlayer) &&
                   (!pawn.IsPrisonerOfColony || !pawn.Position.IsInPrisonCell(pawn.MapHeld)) &&
                   !pawn.DevelopmentalStage.Baby() && (!pawn.story.traits.HasTrait(PersonalityTraitDefOf.Vehement) ||
                                                       !pawn.mindState.mentalStateHandler.InMentalState);
    }
}