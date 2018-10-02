using System;
using RimWorld;
using Verse;

namespace GeneSeed.Organs
{
    public class TheBlackCarapace
    {
        public static void EnhanceArmor(Pawn pawn, BodyPartRecord part, GeneSeedHediffWithComps geneSeedAvailable)
        {
            foreach (var apparel in pawn.apparel.WornApparel)
            {
                if (apparel.WornByCorpse)
                {
                    apparel.Notify_PawnResurrected();//Artifacts are good.
                    apparel.HitPoints = (int) Math.Max(apparel.HitPoints, apparel.MaxHitPoints/1.095f);
                    
                    CompQuality compQuality = apparel.TryGetComp<CompQuality>();
                    compQuality.SetQuality(QualityCategory.Legendary, ArtGenerationContext.Colony);
                }
                
                apparel.HitPoints = (int) Math.Max(apparel.HitPoints, apparel.MaxHitPoints/1.88f);
            }
        }
    }
}