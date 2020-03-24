using System.Collections.Generic;
using GeneSeed.Organs;
using Verse;

namespace GeneSeed.crossmods
{
    public class ImperialGuard
    {
        public static HediffDef hediff_unConsciousness = DefDatabase<HediffDef>.GetNamedSilentFail("hediff_unConsciousness");
        public static HediffDef hediff_TYPlague = DefDatabase<HediffDef>.GetNamedSilentFail("hediff_TYPlague");

        public static HediffDef Omophagea = DefDatabase<HediffDef>.GetNamedSilentFail("IG_HediffDef_OMOP");
        public static HediffDef Catalepsean = DefDatabase<HediffDef>.GetNamedSilentFail("IG_HediffDef_CATA");
        public static HediffDef SecondaryHeart = DefDatabase<HediffDef>.GetNamedSilentFail("IG_HediffDef_SECHRT");
        public static HediffDef Ossmodula = DefDatabase<HediffDef>.GetNamedSilentFail("IG_HediffDef_OSSM");
        public static HediffDef BlackCarapace = DefDatabase<HediffDef>.GetNamedSilentFail("IG_HediffDef_BCARA");
        public static HediffDef LymanEarAugmentation = DefDatabase<HediffDef>.GetNamedSilentFail("IG_HediffDef_LYMAN");
        public static HediffDef Occulobe = DefDatabase<HediffDef>.GetNamedSilentFail("IG_HediffDef_OCCU");


        public static List<Pair<BodyPartDef, HediffDef>> AstarteBodyParts = new List<Pair<BodyPartDef, HediffDef>>()
        {
            new Pair<BodyPartDef, HediffDef>(GeneSeedOrganHelper.Omophagea, Omophagea),
            new Pair<BodyPartDef, HediffDef>(GeneSeedOrganHelper.CatalepseanNode, Catalepsean),
            new Pair<BodyPartDef, HediffDef>(GeneSeedOrganHelper.SecondaryHeart, SecondaryHeart),

            new Pair<BodyPartDef, HediffDef>(GeneSeedOrganHelper.Ossmodula, Ossmodula),
            new Pair<BodyPartDef, HediffDef>(GeneSeedOrganHelper.TheBlackCarapace, BlackCarapace),
            new Pair<BodyPartDef, HediffDef>(GeneSeedOrganHelper.LymansEar, LymanEarAugmentation),
            new Pair<BodyPartDef, HediffDef>(GeneSeedOrganHelper.Occulobe, Occulobe)
        };
    }
}