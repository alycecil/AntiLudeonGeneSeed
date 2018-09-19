using System.Collections.Generic;
using Verse;

namespace GeneSeed
{
    static class Constants
    {
        public static readonly ThingDef GeneSeed = DefDatabase<ThingDef>.GetNamed("GeneSeed");
        public static HediffDef Mutated = HediffDef.Named("GeneSeedMutated");
        
        
        //Order Matters inorder to be like the history
        public static List<BodyPartDef> AstarteBodyParts = new List<BodyPartDef>()
        {
            DefDatabase<BodyPartDef>.GetNamed("SecondaryHeart"),
            DefDatabase<BodyPartDef>.GetNamed("Ossmodula"),
            DefDatabase<BodyPartDef>.GetNamed("Biscopea"),
            DefDatabase<BodyPartDef>.GetNamed("Haemastamen"),
            DefDatabase<BodyPartDef>.GetNamed("LarramansOrgan"),
            
            DefDatabase<BodyPartDef>.GetNamed("CatalepseanNode"),
            DefDatabase<BodyPartDef>.GetNamed("Preomnor"),
            DefDatabase<BodyPartDef>.GetNamed("Omophagea"),
            DefDatabase<BodyPartDef>.GetNamed("MultiLung"),
            DefDatabase<BodyPartDef>.GetNamed("Occulobe"),
            
            DefDatabase<BodyPartDef>.GetNamed("LymansEar"),
            DefDatabase<BodyPartDef>.GetNamed("SusanMembrane"),
            DefDatabase<BodyPartDef>.GetNamed("Melanochrome"),
            DefDatabase<BodyPartDef>.GetNamed("OoliticKidney"),
            DefDatabase<BodyPartDef>.GetNamed("Neuroglottis"),
            
            DefDatabase<BodyPartDef>.GetNamed("Mucranoid"),
            DefDatabase<BodyPartDef>.GetNamed("BetchersGland"),
            DefDatabase<BodyPartDef>.GetNamed("ProgenoidGlands"),
            DefDatabase<BodyPartDef>.GetNamed("TheBlackCarapace"),
            
        };

        public static ThingDef Astarte = ThingDef.Named("AstarteSpaceMarine");
    }
}