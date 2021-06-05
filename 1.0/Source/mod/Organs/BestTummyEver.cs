using RimWorld;
using Verse;

namespace GeneSeed.Organs
{
    //Preomnor
    public class BestTummyEver
    {
        public static void doClense(Pawn pawn, BodyPartRecord part)
        {
           PawnHelper.ClenseBad(pawn, HediffDefOf.FoodPoisoning);
        }
    }
}