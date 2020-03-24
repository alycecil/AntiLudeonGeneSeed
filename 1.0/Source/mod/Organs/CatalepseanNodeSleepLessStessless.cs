using RimWorld;
using Verse;

namespace GeneSeed.Organs
{
    public class CatalepseanNodeSleepLessStessless
    {
        //omg this is painful as hell on the poor children.

        public static void wakeUpCryBaby(Pawn pawn, BodyPartRecord part, GeneSeedHediffWithComps geneSeedAvailable)
        {
            if (pawn.Drafted && pawn.needs.rest.CurCategory <= RestCategory.Exhausted && geneSeedAvailable.Severity > .2f)//need to be a full + 1, shh~! its a super power 
            {
                pawn.needs.rest.TickResting(Rand.Value);//war is basically rest for the wicked

                if (!PawnHelper.is_bloodlust(pawn)) return;
                //bloodravens love this cause of the red haze or whatever you wanna call it.
                pawn.skills.Learn(SkillDefOf.Melee, 1, true);
                

            }
        }
    }
}