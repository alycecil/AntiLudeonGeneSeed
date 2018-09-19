using System;
using Verse;

namespace GeneSeed
{
    public class GeneSeedMutationHediffWithComps : HediffWithComps
    {
        public override void Tick()
        {
            if (this.pawn.IsHashIntervalTick(1000)) //20x daily
            {
                if ( Rand.Value < 0.05f )
                {
                    //geneseed instability
                    if(Rand.Bool)
                        this.Severity /= 2f;
                    else
                    {
                        //better
                        this.Severity = Math.Min(this.Severity * 2, 1f);
                    }
                }
            }    
        }
        
        
        
    }
}