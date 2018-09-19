using System.Linq;
using RimWorld;
using Verse;

namespace GeneSeed
{
    public class GeneSeedHediffWithComps : HediffWithComps
    {
        public override void Tick()
        {
            if (this.pawn.IsHashIntervalTick(1000)) //20x daily
            {
                if (Rand.MTBEventOccurs(0.5f, 60000f, 1000f)) //if is rare occurance
                {
                    //Mutate
                    PawnHelper.mutate(pawn);
                }
            }
        }

        public override void PostAdd(DamageInfo? dinfo)
        {
            //Body change to Astarte

            //only gaining parts

            var map = pawn.Map;
            RegionListersUpdater.DeregisterInRegions(pawn, map);
            
            if (Constants.Astarte != null)
            {
                pawn.def = Constants.Astarte;
            }
            
            RegionListersUpdater.RegisterInRegions(pawn, map);
            

            base.PostAdd(dinfo);
        }
    }
}