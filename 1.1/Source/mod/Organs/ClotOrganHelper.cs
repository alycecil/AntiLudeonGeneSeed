using System;
using System.Linq;
using Verse;

namespace GeneSeed.Organs
{
    public static class ClotOrganHelper
    {
        public static void doOrgan(Pawn pawn, BodyPartRecord part)
        {
            var i = 5;
            foreach (var hediff in pawn.health.hediffSet.hediffs.Where(x=> x.Bleeding).OrderByDescending(x=>x.BleedRate))
            {
                hediff.Tended(Math.Min(Rand.Value+Rand.Value+Rand.Value, 1f));
                i--;

                if (i <= 0) return;
            }
        }
    }
}