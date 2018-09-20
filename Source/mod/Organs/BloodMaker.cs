using System.Linq;
using RimWorld;
using Verse;

namespace GeneSeed.Organs
{
    static internal class BloodMaker
    {
        public static void doHaemastamen(Pawn pawn, BodyPartRecord part)
        {
            if (pawn.health.hediffSet.BleedRateTotal > .0001f)
            {
                var bloodLoss = HediffDefOf.BloodLoss;
                foreach (var hediff in pawn.health.hediffSet.hediffs.Where(x => x.def == bloodLoss))
                {
                    HealthUtility.AdjustSeverity(pawn, bloodLoss, hediff.Severity * 0.8f);
                    return; //done
                }

                //pawn.BodySize
            }
        }
    }
}