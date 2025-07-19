using HarmonyLib;
using RimWorld;
using Verse;

namespace LessSadNightOwls2;

[StaticConstructorOnStartup]
public class LessSadNightOwls2
{
    static LessSadNightOwls2()
    {
        Harmony harmony = new("NachoToast.LessSadNightOwls2");

        harmony.Patch(
            original: AccessTools.Method(
                type: typeof(ThoughtWorker_IsDayForNightOwl),
                name: "CurrentStateInternal"),
            postfix: new HarmonyMethod(
                methodType: typeof(LessSadNightOwls2),
                methodName: nameof(CurrentStateInteral_Postfix)));
    }

    private static void CurrentStateInteral_Postfix(Pawn p, ref ThoughtState __result)
    {
        if (!__result.Active)
        {
            // already inactive
            return;
        }

        if (p.IsPrisoner)
        {
            // prisoner - suppress
            __result = false;
            return;
        }

        int maxHourChecked =
            p.story.traits.HasTrait(MyTraitDefOf.QuickSleeper)
            ? 14
            : 18;

        for (int i = 11; i <= maxHourChecked; i++)
        {
            if (p.timetable.GetAssignment(i) != TimeAssignmentDefOf.Sleep)
            {
                // not set as sleep, don't suppress
                return;
            }
        }

        __result = false;
    }
}
