using System;
using System.Linq;
using Harmony;
using Verse;

namespace GeneSeed
{
    static class PawnHelper
    {
        public static bool is_human(Pawn pawn)
        {
            return pawn.RaceProps.Humanlike; //||pawn.kindDef.race == ThingDefOf.Human
        }


        public static void mutate(Pawn pawn)
        {
            foreach (var bodyPartRecord in pawn.health.hediffSet.GetNotMissingParts().Where(x => Rand.Value < 0.1f))
            {
                var diff = GetHediff(pawn, Constants.Mutated, bodyPartRecord);
                if (diff == null)
                {
                    diff = pawn.health.AddHediff(Constants.Mutated, bodyPartRecord);
                    diff.Severity = Rand.Value;
                }
                else
                {
                    diff.Severity = Math.Min(diff.Severity + Rand.Value, 1f);
                }
            }
        }


        public static Hediff GetHediff(Pawn pawn, HediffDef def, BodyPartRecord bodyPart, bool mustBeVisible = false)
        {
            var hediffs = pawn.health.hediffSet.hediffs;
            for (int index = 0; index < hediffs.Count; ++index)
            {
                if (hediffs[index].def == def && hediffs[index].Part == bodyPart &&
                    (!mustBeVisible || hediffs[index].Visible))
                    return hediffs[index];
            }

            return null;
        }
    }
}