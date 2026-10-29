# GitHub Copilot Instructions for NilchEi Personality Traits (Continued) RimWorld Mod

This document provides guidelines and instructions for developers working on the NilchEi Personality Traits (Continued) RimWorld mod, which adds several personality traits to the game. The mod is compatible with any mods that don't change the Slight and Insult workerClass.

## Mod Overview and Purpose

The NilchEi Personality Traits (Continued) mod is an update to the original NilchEis mod. It adds the following traits to the game:

- **Competitive**: Enhances learning speed and mood based on the pawn's skill comparison with others.
- **Fickle**: Occasionally changes passions.
- **Tsundere**: Alters behavior towards others based on feelings.
- **Determinist**: Randomly changes expectations and work speeds.
- **Repressive**: Ignores minor/major mental breaks and catatonic behavior.
- **Motivated**: Provides mood buffs while inspired.
- **Vehement**: Offers immunity to arrest, damage, and invisibility during mental breaks.

## Key Features and Systems

### Traits
- **Competitive**: Affects learning speed and mood based on skill comparison.
- **Fickle**: Randomly changes passions every 40 days on average.
- **Tsundere**: Changes behavior towards others based on feelings, affecting market value.
- **Determinist**: Randomly changes expectations and work speeds, influenced by Royalty DLC.
- **Repressive**: Ignores minor/major mental breaks, stacking mood penalties.
- **Motivated**: Provides mood buffs during inspiration.
- **Vehement**: Grants immunity to arrest, damage, and invisibility during mental breaks.

### Systems
- **Harmony Patching**: Used to modify existing game mechanics.
- **XML Integration**: Defines new traits, thoughts, and mental states.
- **Custom Thoughts and Mental States**: Custom thought workers and mental state workers.

## Coding Patterns and Conventions

### C# Files
- **HarmonyPatches**: Located in the `HarmonyPatches` directory. Each patch modifies an existing method in the game.
- **MentalStateWorkers**: Located in the `MentalStateWorker` directory. Each worker defines custom mental states.
- **MentalBreakWorkers**: Located in the `MentalBreakWorker` directory. Each worker defines custom mental breaks.
- **PersonalityTraitDefs**: Located in the `PersonalityTraitDefs` directory. Defines new personality traits.
- **ThoughtWorkers**: Located in the `ThoughtWorker` directory. Each worker defines custom thoughts.

### XML Files
- **Defs**: Located in the `Defs` directory. Defines new hediffs, thoughts, mental states, and traits.
- **Patches**: Located in the `Patches` directory. Defines Harmony patches.

## XML Integration

The mod uses XML files to define new game elements such as traits, thoughts, and mental states. Each XML file is located in the `Defs` directory. For example:

- **Hediffs_Local_Misc.xml**: Defines new hediffs.
- **MentalStates_Mood.xml**: Defines new mental states and mood letters.
- **Thoughts_Situation_Traits_NilchEi.xml**: Defines new thoughts.
- **TraitDefs_NilchEi.xml**: Defines new traits.

## Harmony Patching

The mod uses Harmony patching to modify existing game mechanics. Each patch is located in the `HarmonyPatches` directory. For example:

- **ExpectationsUtility_CurrentExpectationFor.cs**: Modifies the `CurrentExpectationFor` method in the `ExpectationsUtility` class.
- **MentalStateWorker_Determinist.cs**: Modifies the `StateCanOccur` method in the `MentalStateWorker` class.
- **MentalBreakWorker_Repressive.cs**: Modifies the `BreakCanOccur` and `TryStart` methods in the `MentalBreakWorker` class.

### Example Patch

csharp
[HarmonyPatch(typeof(ExpectationsUtility), nameof(ExpectationsUtility.CurrentExpectationFor))]
public static class ExpectationsUtility_CurrentExpectationFor
{
    public static void Postfix(Pawn pawn, ref int __result)
    {
        if (pawn.HasTrait(TraitDefOf.Determinist))
        {
            __result = Rand.Range(-3500, 3500);
        }
    }
}


## Suggestions for Copilot

### General Guidelines
- Use descriptive variable and method names.
- Follow the existing coding patterns and conventions.
- Use XML comments to describe new elements and their purpose.
- Use Harmony patches to modify existing game mechanics.

### Example Code Generation
- **Trait Definition**: Generate a new trait definition in XML.
- **Thought Worker**: Generate a new thought worker in C#.
- **Mental State Worker**: Generate a new mental state worker in C#.
- **Harmony Patch**: Generate a new Harmony patch in C#.

### Example XML Generation
xml
<TraitDef>
    <defName>Competitive</defName>
    <label>Competitive</label>
    <description>A highly competitive pawn.</description>
    <degreeDatas>
        <li>
            <degree>Positive</degree>
            <label>Competitive</label>
            <description>Enhances learning speed and mood based on skill comparison.</description>
            <statFactors>
                <li>
                    <stat>LearningSpeed</stat>
                    <factor>1.75</factor>
                </li>
            </statFactors>
        </li>
    </degreeDatas>
</TraitDef>


### Example C# Generation
csharp
public class ThoughtWorker_Motivated : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn p, ThingDef stuff)
    {
        if (p.Inspired)
        {
            return ThoughtState.ActiveAtStage(0);
        }
        return ThoughtState.Inactive;
    }
}


## Conclusion

The NilchEi Personality Traits (Continued) mod provides a variety of new personality traits and systems that enhance the gameplay experience. By following the coding patterns and conventions outlined in this document, developers can effectively contribute to the mod and ensure compatibility with other mods.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

