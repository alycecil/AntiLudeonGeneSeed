using System;
using System.Linq;
using Harmony;
using RimWorld;
using Verse;

namespace GeneSeed
{
    static class PawnHelper
    {
        public static bool is_human(Pawn pawn)
        {
            return pawn.RaceProps.Humanlike; //||pawn.kindDef.race == ThingDefOf.Human
        }
        
        
        public static bool is_bloodlust(Pawn pawn)
        {
            return (pawn?.story?.traits != null && pawn.story.traits.HasTrait(TraitDefOf.Bloodlust));
        }

        public static void mutate(Pawn pawn)
        {
            foreach (var bodyPartRecord in pawn.health.hediffSet.GetNotMissingParts().Where(x => Rand.Value < 0.1f && !Constants.AstarteBodyParts.Contains(x.def)))
            {
                MutatePart(pawn, bodyPartRecord);
            }
        }

        public static Hediff MutatePart(Pawn pawn, BodyPartRecord bodyPartRecord)
        {
            Hediff diff = GetHediff(pawn, Constants.Mutated, bodyPartRecord);
            if (diff == null)
            {
                diff = pawn.health.AddHediff(Constants.Mutated, bodyPartRecord);
                diff.Severity = Rand.Value;
            }
            else
            {
                diff.Severity = Math.Min(diff.Severity + Rand.Value, 1f);
            }

            return diff;
        }


        public static Hediff GetHediff(Pawn pawn, HediffDef def, BodyPartRecord bodyPart, bool mustBeVisible = false)
        {
            var heDiffs = pawn.health.hediffSet.hediffs;
            foreach (var t in heDiffs)
            {
                if (t.def == def && t.Part == bodyPart &&
                    (!mustBeVisible || t.Visible))
                    return t;
            }

            return null;
        }

        public static void ClenseBad(Pawn pawn, HediffDef hediffDef)
        {
            var toxic = pawn.health.hediffSet.GetFirstHediffOfDef(hediffDef);
            if (toxic != null)
            {
                pawn.health.RemoveHediff(toxic);
                int outCount = (int) (toxic.Severity * 10);
                //ew, that was a ot of ew, gotta poop. THAT'S LORE BABY.
                if (outCount > 0)
                    FilthMaker.MakeFilth(pawn.Position, pawn.Map, ThingDefOf.Filth_Slime, outCount);
            }
        }
    }
}