using RimWorld;

namespace LessSadNightOwls2;

[DefOf]
public static class MyTraitDefOf
{
    public static TraitDef QuickSleeper;

    static MyTraitDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(MyTraitDefOf));
    }
}
