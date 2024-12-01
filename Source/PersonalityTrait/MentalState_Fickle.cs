using RimWorld;
using Verse;
using Verse.AI;

namespace PersonalityTrait;

internal class MentalState_Fickle : MentalState
{
    public override RandomSocialMode SocialModeMax()
    {
        return RandomSocialMode.Off;
    }

    public override void PostEnd()
    {
        base.PostEnd();
        var allDefsListForReading = DefDatabase<SkillDef>.AllDefsListForReading;
        foreach (var skillDef in allDefsListForReading)
        {
            var skill = pawn.skills.GetSkill(skillDef);
            var level = skill.Level;
            if (skill.TotallyDisabled)
            {
                continue;
            }

            var num = level * 0.11f;
            var value = Rand.Value;
            if (value < num)
            {
                skill.passion = value < num * 0.2f ? Passion.Major : Passion.Minor;
            }
            else
            {
                skill.passion = Passion.None;
            }
        }
    }
}