using System.Linq;
using GeneSeed.Organs;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GeneSeed
{
    public class GeneSeedHediffWithComps : HediffWithComps
    {
        private int ticks = 0;

        private IntVec3 where;
        
        public override void Tick()
        {
            ticks++;
            
            OrganFunctions();
            
            GeneSeedMutation();

            RestoreOrgan();

            ticks %= 20000000;
        }

        private void OrganFunctions()
        {
            if (ticks%250==0)
            {
                foreach (BodyPartDef astarteBodyPart in Constants.AstarteBodyParts)
                {
                    foreach (var part in pawn.def.race.body.GetPartsWithDef(astarteBodyPart))
                    {
                        if (part == null || pawn.health.hediffSet.PartIsMissing(part)) continue;

                        GeneSeedOrganHelper.apply(pawn, part, this);
                        
                        break;
                    }
                }
            }
        }

        private void GeneSeedMutation()
        {
            if (ticks%1000==0) //20x daily
            {
                if (Rand.MTBEventOccurs(0.5f, 60000f, 1000f)) //if is rare occurance
                {
                    //Mutate
                    PawnHelper.mutate(pawn);
                }
            }
        }

        private void RestoreOrgan()
        {
            if (ticks%1000!=0 || this.Severity < .11f) return;
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

            if (didOne)
            {
                this.Severity *= 0.666f;
            }
        }

        public override void PostAdd(DamageInfo? dinfo)
        {
            if(pawn.RaceProps.Humanlike && pawn.def != Constants.Astarte)
                TransformPawn();

            base.PostAdd(dinfo);
        }

        private void TransformPawn()
        {
//Body change to Astarte

            //only gaining parts
            
            
            var map = pawn.Map;
            var where = pawn.Position;

            if (where == IntVec3.Zero || where == IntVec3.Invalid)
            {
                where = map.Center;
            }
            pawn.DeSpawn();
            RegionListersUpdater.DeregisterInRegions(pawn, map);
            
            pawn.Position = where;
            
            
            if (Constants.Astarte != null)
            {
                pawn.def = Constants.Astarte;
            }


            pawn.SpawnSetup(map, true);
            
            RegionListersUpdater.RegisterInRegions(pawn, map);
            

            map.mapPawns.UpdateRegistryForPawn(pawn);

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

                    //Log.Message("Blowing Off a Part : " + pawn + "'s "+part);
                }

                foreach (var hediff in pawn.health.hediffSet.GetHediffsTendable())
                {
                    hediff.Tended(1f);
                }
            }
            
            //decache graphics
            pawn.Drawer.renderer.graphics.ResolveAllGraphics();
            
            //save the pawn
            pawn.ExposeData();

            pawn.Position = where;

        }

        public override bool ShouldRemove => false;
    }
}