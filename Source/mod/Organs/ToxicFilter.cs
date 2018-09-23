using RimWorld;
using Verse;

namespace GeneSeed.Organs
{
    public class ToxicFilter
    {
        public static void doClense(Pawn pawn, BodyPartRecord part)
        {
            var toxic = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.ToxicBuildup);
            if (toxic != null)
            {
                pawn.health.RemoveHediff(toxic);
            }
        }
    }
}