using System;
using Verse;

namespace GeneSeed.crossmods
{
    //1513943591
    public static class N17Rimhammer
    {
        public static ThingDef HumanAlt = DefDatabase<ThingDef>.GetNamedSilentFail("Alien_Terran");
        public static ThingDef HumanAlt2 = DefDatabase<ThingDef>.GetNamedSilentFail("Alien_Valhallan");
        public static ThingDef HumanAlt3 = DefDatabase<ThingDef>.GetNamedSilentFail("Alien_Krieg");        
        public static ThingDef AstartesAlt = DefDatabase<ThingDef>.GetNamedSilentFail("Alien_Astartes");
        
        private static BodyDef AstartesBody = DefDatabase<BodyDef>.GetNamedSilentFail("AstartesBody");
        
        private static bool didIt = false;
        
        public static void PatchBody()
        {
            if (!didIt && AstartesBody != null)
            {

                didIt = true;
                
                GeneSeed.DealWithAstarteMissingParts(AstartesBody);
                
                Log.Message("Patched GeneSeed for N17 Rimhammer");

            }
        }
    }
}