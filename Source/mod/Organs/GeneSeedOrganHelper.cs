using System.Linq;
using RimWorld;
using Verse;

namespace GeneSeed.Organs
{
    public static class GeneSeedOrganHelper
    {
        public static BodyPartDef SecondaryHeart = DefDatabase<BodyPartDef>.GetNamed("SecondaryHeart");
        public static BodyPartDef Ossmodula = DefDatabase<BodyPartDef>.GetNamed("Ossmodula");
        public static BodyPartDef Biscopea = DefDatabase<BodyPartDef>.GetNamed("Biscopea");
        public static BodyPartDef Haemastamen = DefDatabase<BodyPartDef>.GetNamed("Haemastamen");
        public static BodyPartDef LarramansOrgan = DefDatabase<BodyPartDef>.GetNamed("LarramansOrgan");
            
        public static BodyPartDef CatalepseanNode = DefDatabase<BodyPartDef>.GetNamed("CatalepseanNode");
        public static BodyPartDef Preomnor = DefDatabase<BodyPartDef>.GetNamed("Preomnor");
        public static BodyPartDef Omophagea = DefDatabase<BodyPartDef>.GetNamed("Omophagea");
        public static BodyPartDef MultiLung = DefDatabase<BodyPartDef>.GetNamed("MultiLung");
        public static BodyPartDef Occulobe = DefDatabase<BodyPartDef>.GetNamed("Occulobe");
            
        public static BodyPartDef LymansEar = DefDatabase<BodyPartDef>.GetNamed("LymansEar");
        public static BodyPartDef SusanMembrane = DefDatabase<BodyPartDef>.GetNamed("SusanMembrane");
        public static BodyPartDef Melanochrome = DefDatabase<BodyPartDef>.GetNamed("Melanochrome");
        public static BodyPartDef OoliticKidney = DefDatabase<BodyPartDef>.GetNamed("OoliticKidney");
        public static BodyPartDef Neuroglottis = DefDatabase<BodyPartDef>.GetNamed("Neuroglottis");
            
        public static BodyPartDef Mucranoid = DefDatabase<BodyPartDef>.GetNamed("Mucranoid");
        public static BodyPartDef BetchersGland = DefDatabase<BodyPartDef>.GetNamed("BetchersGland");
        public static BodyPartDef ProgenoidGlands = DefDatabase<BodyPartDef>.GetNamed("ProgenoidGlands");
        public static BodyPartDef TheBlackCarapace = DefDatabase<BodyPartDef>.GetNamed("TheBlackCarapace");
               
        public static void apply(Pawn pawn, BodyPartRecord part, GeneSeedHediffWithComps geneSeedAvailable)
        {
            if(pawn == null || part?.def==null) return;

            if (part.def == Haemastamen)
            {
                BloodMaker.doHaemastamen(pawn, part);
            }else if (part.def == LarramansOrgan)
            {
                ClotOrganHelper.doOrgan(pawn, part);

            }else if (part.def == Preomnor || part.def == OoliticKidney)
            {
                ToxicFilter.doClense(pawn, part);
            }else if (part.def == ProgenoidGlands)
            {
                geneSeedAvailable.Severity += .0005f;
            }
            
            //TODO ProgenoidGlands
        }
    }
}