using RimWorld;
using Verse;

namespace PersonalityTrait;

public class ThoughtWorker_Competitive : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn p)
    {
        if (!p.IsColonist)
        {
            return false;
        }

        var list = p.Map.mapPawns.SpawnedPawnsInFaction(Faction.OfPlayer);
        var differentSkillLevels = false;
        foreach (var pawn in list)
        {
            if (!pawn.def.race.Humanlike || pawn.Dead)
            {
                continue;
            }

            var allDefsListForReading = DefDatabase<SkillDef>.AllDefsListForReading;
            var skillLevelOne = 0f;
            var skillLevelTwo = 0f;
            foreach (var skillDef in allDefsListForReading)
            {
                var skill = p.skills.GetSkill(skillDef);
                var skill2 = pawn.skills.GetSkill(skillDef);
                skillLevelOne += skill.Level;
                skillLevelTwo += skill2.Level;
            }

            if (skillLevelOne != skillLevelTwo)
            {
                differentSkillLevels = true;
            }

            if (skillLevelOne < skillLevelTwo)
            {
                return ThoughtState.ActiveAtStage(1);
            }
        }

        return differentSkillLevels ? ThoughtState.ActiveAtStage(0) : false;
    }
}