using System.Linq;
using Verse;

namespace GeneSeed.Organs
{
    public class ClotOrganHelper
    {
        public static void doOrgan(Pawn pawn, BodyPartRecord part)
        {
            foreach (var hediff in pawn.health.hediffSet.hediffs.Where(x=> x.Bleeding))
            {
                hediff.Tended(Rand.Value);
            }
        }
    }
}