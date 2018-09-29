using RimWorld;
using Verse;

namespace GeneSeed.Organs
{
    public class ToxicFilter
    {
        public static void doClense(Pawn pawn, BodyPartRecord part)
        {
            PawnHelper.ClenseBad(pawn, HediffDefOf.ToxicBuildup);
            PawnHelper.ClenseBad(pawn, HediffDefOf.Carcinoma);
        }
    }
}