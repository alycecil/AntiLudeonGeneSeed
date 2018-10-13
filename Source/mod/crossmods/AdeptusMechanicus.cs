using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GeneSeed.crossmods
{
    public static class AdeptusMechanicus_Patch
    {
        public static HediffDef RadiationPoisioning = DefDatabase<HediffDef>.GetNamedSilentFail("RadiationPoisioning");
        
        public static void DealWithMissingWargearParts(ThingDef thingDef)
        {
            try
            {
                AddPartToThing(thingDef, AdeptusMechanicus_BodyPartDef.OGTorsoWargear1,
                    AdeptusMechanicus_BodyPartGroupDef.OGTorsoWargear1, "wargear slot 1");


                AddPartToThing(thingDef, AdeptusMechanicus_BodyPartDef.OGTorsoWargear2,
                    AdeptusMechanicus_BodyPartGroupDef.OGTorsoWargear2, "wargear slot 2");

                AddPartToThing(thingDef, AdeptusMechanicus_BodyPartDef.OGTorsoWargear3,
                    AdeptusMechanicus_BodyPartGroupDef.OGTorsoWargear3, "wargear slot 3");

                AddPartToThing(thingDef, AdeptusMechanicus_BodyPartDef.OGNeuralLink,
                    AdeptusMechanicus_BodyPartGroupDef.OGNeuralLink, "neural link");
                //thingDef.race.body.ResolveReferences();
            }catch(Exception){}
        }

        private static void AddPartToThing(ThingDef thingDef, BodyPartDef warGear, BodyPartGroupDef bodyPartGroupDef, string customLabel)
        {
            if (thingDef == null || warGear == null || bodyPartGroupDef == null) return;

            thingDef.race.body.corePart.parts.Add(new BodyPartRecord
            {
                customLabel = customLabel,
                coverage = 0f,
                def = warGear,
                depth = BodyPartDepth.Undefined,
                groups = new List<BodyPartGroupDef>(new[] {bodyPartGroupDef}),
                height = BodyPartHeight.Middle
            });
        }
    }
    
    public static class AdeptusMechanicus_BodyPartDef
    {
        public static BodyPartDef OGTorsoWargear1 = DefDatabase<BodyPartDef>.GetNamedSilentFail("OGTorsoWargear1");
        public static BodyPartDef OGTorsoWargear2 = DefDatabase<BodyPartDef>.GetNamedSilentFail("OGTorsoWargear2");
        public static BodyPartDef OGTorsoWargear3 = DefDatabase<BodyPartDef>.GetNamedSilentFail("OGTorsoWargear3");
        public static BodyPartDef OGNeuralLink = DefDatabase<BodyPartDef>.GetNamedSilentFail("OGNeuralLink");
    }
    
    public static class AdeptusMechanicus_BodyPartGroupDef
    {
        public static BodyPartGroupDef OGTorsoWargear1 = DefDatabase<BodyPartGroupDef>.GetNamedSilentFail("OGTorsoWargear1");
        public static BodyPartGroupDef OGTorsoWargear2 = DefDatabase<BodyPartGroupDef>.GetNamedSilentFail("OGTorsoWargear2");
        public static BodyPartGroupDef OGTorsoWargear3 = DefDatabase<BodyPartGroupDef>.GetNamedSilentFail("OGTorsoWargear3");
        public static BodyPartGroupDef OGNeuralLink = DefDatabase<BodyPartGroupDef>.GetNamedSilentFail("OGNeuralLink");
    }
}