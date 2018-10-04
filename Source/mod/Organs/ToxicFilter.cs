using GeneSeed.crossmods;
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
            PawnHelper.ClenseBad(pawn, AdeptusMechanicus_Patch.RadiationPoisioning);
            PawnHelper.ClenseBad(pawn, ImperialGuard.hediff_unConsciousness);
            PawnHelper.ClenseBad(pawn, ImperialGuard.hediff_TYPlague);
        }
    }
}