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

            if (!this.pawn.IsHashIntervalTick(10000) || !Rand.MTBEventOccurs(2f, 60000f, 1000f)) return;
            //time for a bonus organ

            bool didOne = false;

            foreach (BodyPartDef astarteBodyPart in Constants.AstarteBodyParts)
            {
                if (didOne) break;
                foreach (var part in pawn.def.race.body.GetPartsWithDef(astarteBodyPart))
                {
                    if (part == null || !pawn.health.hediffSet.PartIsMissing(part)) continue;
                    pawn.health.RestorePart(part);
                    
                    didOne = true;
                    break;
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

            //remove the 19

            foreach (BodyPartDef astarteBodyPart in Constants.AstarteBodyParts)
            {
                DamageDef surgicalCut = DamageDefOf.SurgicalCut;
                float amount = 99999f;
                float armorPenetration = 999f;
                foreach (var part in pawn.def.race.body.GetPartsWithDef(astarteBodyPart))
                {
                    pawn.TakeDamage(new DamageInfo(surgicalCut, amount, armorPenetration, -1f, null, part, null,
                        DamageInfo.SourceCategory.ThingOrUnknown, null));
                }
                foreach (var hediff in pawn.health.hediffSet.GetHediffsTendable())
                {
                    hediff.Tended(1f);
                }
                
            }

            base.PostAdd(dinfo);
        }
    }
}