using System.Collections.Generic;
using GeneSeed.Organs;
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
             GeneSeedOrganHelper.SecondaryHeart,
             GeneSeedOrganHelper.Ossmodula,
             GeneSeedOrganHelper.Biscopea,
             GeneSeedOrganHelper.Haemastamen,
             GeneSeedOrganHelper.LarramansOrgan,
            
             GeneSeedOrganHelper.CatalepseanNode,
             GeneSeedOrganHelper.Preomnor,
             GeneSeedOrganHelper.Omophagea,
             GeneSeedOrganHelper.MultiLung,
             GeneSeedOrganHelper.Occulobe,
            
             GeneSeedOrganHelper.LymansEar,
             GeneSeedOrganHelper.SusanMembrane,
             GeneSeedOrganHelper.Melanochrome,
             GeneSeedOrganHelper.OoliticKidney,
             GeneSeedOrganHelper.Neuroglottis,
            
             GeneSeedOrganHelper.Mucranoid,
             GeneSeedOrganHelper.BetchersGland,
             GeneSeedOrganHelper.ProgenoidGlands,
             GeneSeedOrganHelper.TheBlackCarapace,
            
        };

        public static ThingDef Astarte = ThingDef.Named("AstarteSpaceMarine");
        public static ThingDef Custodes = ThingDef.Named("AdaptusCustodes");
    }
}