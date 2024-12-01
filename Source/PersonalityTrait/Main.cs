using HarmonyLib;
using Verse;

namespace PersonalityTrait;

[StaticConstructorOnStartup]
internal static class Main
{
    static Main()
    {
        new Harmony("com.nilch.personalitytrait").PatchAll();
    }
}