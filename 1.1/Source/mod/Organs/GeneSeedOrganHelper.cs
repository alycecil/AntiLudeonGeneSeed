using System;
using System.Collections.Generic;
using UnityEngine;
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
            if (pawn == null || part?.def == null) return;

            try
            {
                if (part.def == LarramansOrgan || part.def == Haemastamen)
                {
                    ClotOrganHelper.doOrgan(pawn, part);
                }
                else if (part.def == OoliticKidney)
                {
                    ToxicFilter.doClense(pawn, part);
                }
                else if (part.def == ProgenoidGlands)
                {
                    geneSeedAvailable.Severity += .0001f;
                }
                else if (part.def == Preomnor)
                {
                    BestTummyEver.doClense(pawn, part);
                }
                else if (part.def == CatalepseanNode)
                {
                    CatalepseanNodeSleepLessStessless.wakeUpCryBaby(pawn, part, geneSeedAvailable);
                }
                else if (part.def == TheBlackCarapace)
                {
                    Organs.TheBlackCarapace.EnhanceArmor(pawn, part, geneSeedAvailable);
                }
                else if (part.def == Biscopea || part.def == Ossmodula)
                {

                }
            }
            catch (Exception e)
            {
                Log.Error("Well butt failed at organ functioning:"+ e.StackTrace);
            }

        }
        
        
        public static HediffDef AwesomeOrgans_SecondaryHeart = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_SecondaryHeart");
        public static HediffDef AwesomeOrgans_Ossmodula = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_Ossmodula");
        public static HediffDef AwesomeOrgans_Biscopea = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_Biscopea");
        public static HediffDef AwesomeOrgans_Haemastamen = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_Haemastamen");
        public static HediffDef AwesomeOrgans_LarramansOrgan = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_LarramansOrgan");

        public static HediffDef AwesomeOrgans_Catalepsean = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_Catalepsean");
        public static HediffDef AwesomeOrgans_Preomnor = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_Preomnor");
        public static HediffDef AwesomeOrgans_Omophagea = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_Omophagea");
        public static HediffDef AwesomeOrgans_MultiLung = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_MultiLung");
        public static HediffDef AwesomeOrgans_Occulobe = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_Occulobe");

        public static HediffDef AwesomeOrgans_LymansEar = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_LymansEar");
        public static HediffDef AwesomeOrgans_SusanMembrane = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_SusanMembrane");
        public static HediffDef AwesomeOrgans_Melanochrome = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_Melanochrome");
        public static HediffDef AwesomeOrgans_OoliticKidney = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_OoliticKidney");
        public static HediffDef AwesomeOrgans_Neuroglottis = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_Neuroglottis");

        public static HediffDef AwesomeOrgans_Mucranoid = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_Mucranoid");
        public static HediffDef AwesomeOrgans_BetchersGland = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_BetchersGland");
        //public static HediffDef AwesomeOrgans_ProgenoidGlands = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_ProgenoidGlands");
        public static HediffDef AwesomeOrgans_TheBlackCarapace = DefDatabase<HediffDef>.GetNamedSilentFail("AwesomeOrgans_TheBlackCarapace");



        public static List<Pair<BodyPartDef, HediffDef>> AstarteBodyParts = new List<Pair<BodyPartDef, HediffDef>>
        {
            
            
            
            new Pair<BodyPartDef, HediffDef>(SecondaryHeart, AwesomeOrgans_SecondaryHeart),
            new Pair<BodyPartDef, HediffDef>(Ossmodula, AwesomeOrgans_Ossmodula),
            new Pair<BodyPartDef, HediffDef>(Biscopea, AwesomeOrgans_Biscopea),
            new Pair<BodyPartDef, HediffDef>(Haemastamen, AwesomeOrgans_Haemastamen),
            new Pair<BodyPartDef, HediffDef>(LarramansOrgan, AwesomeOrgans_LarramansOrgan),

            new Pair<BodyPartDef, HediffDef>(CatalepseanNode, AwesomeOrgans_Catalepsean),
            new Pair<BodyPartDef, HediffDef>(Preomnor, AwesomeOrgans_Preomnor),
            new Pair<BodyPartDef, HediffDef>(Omophagea, AwesomeOrgans_Omophagea),
            new Pair<BodyPartDef, HediffDef>(MultiLung, AwesomeOrgans_MultiLung),
            new Pair<BodyPartDef, HediffDef>(Occulobe, AwesomeOrgans_Occulobe),

            new Pair<BodyPartDef, HediffDef>(LymansEar, AwesomeOrgans_LymansEar),
            new Pair<BodyPartDef, HediffDef>(SusanMembrane, AwesomeOrgans_SusanMembrane),
            new Pair<BodyPartDef, HediffDef>(Melanochrome, AwesomeOrgans_Melanochrome),
            new Pair<BodyPartDef, HediffDef>(OoliticKidney, AwesomeOrgans_OoliticKidney),
            new Pair<BodyPartDef, HediffDef>(Neuroglottis, AwesomeOrgans_Neuroglottis),

            new Pair<BodyPartDef, HediffDef>(Mucranoid, AwesomeOrgans_Mucranoid),
            new Pair<BodyPartDef, HediffDef>(BetchersGland, AwesomeOrgans_BetchersGland),
            //new Pair<BodyPartDef, HediffDef>(ProgenoidGlands, AwesomeOrgans_ProgenoidGlands),
            new Pair<BodyPartDef, HediffDef>(TheBlackCarapace, AwesomeOrgans_TheBlackCarapace),
        };
        
    }
}