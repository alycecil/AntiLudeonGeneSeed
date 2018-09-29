using System.Collections.Generic;
using System.Linq;
using GeneSeed.Organs;
using RimWorld;
using Verse;

namespace GeneSeed
{
    public class GeneSeedHediffWithComps : HediffWithComps
    {
        private int ticks = 0;

        public override void Tick()
        {
            ticks++;

            OrganFunctions();

            GeneSeedMutation();

            RestoreOrgan();

            ticks %= 3010349; //my favorite prime
        }

        private void OrganFunctions()
        {
            if (ticks % 250 != 0) return;

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

        private void GeneSeedMutation()
        {
            if (ticks % 1000 != 0) return;
            if (!Rand.MTBEventOccurs(0.5f, 60000f, 1000f)) return;
            //Mutate
            PawnHelper.mutate(pawn);
        }

        private void RestoreOrgan()
        {
            if (ticks % 1000 != 0 || this.Severity < .11f) return;
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

            if (!didOne) return;
            checkAllThatInsidesForGunk();
            this.Severity -= 0.1f;
        }

        public override void PostAdd(DamageInfo? dinfo)
        {
            if (pawn.RaceProps.Humanlike && pawn.def == ThingDefOf.Human)
                TransformPawn();

            base.PostAdd(dinfo);
        }

        private void TransformPawn()
        {
            //Body change to Astarte
            var where = pawn.Position;

            var map = pawn.Map;
            pawn.DestroyOrPassToWorld();
            pawn.DeSpawn();
            RegionListersUpdater.DeregisterInRegions(pawn, map);


            if (Constants.Astarte != null)
            {
                pawn.def = Constants.Astarte;
            }


            pawn.SpawnSetup(map, true);

            RegionListersUpdater.RegisterInRegions(pawn, map);


            map.mapPawns.UpdateRegistryForPawn(pawn);

            //remove the 19

            RemoveAstarteParts();

            //decache graphics
            pawn.Drawer.renderer.graphics.ResolveAllGraphics();

            //save the pawn
            pawn.ExposeData();

            pawn.Position = map.Center;
        }

        private void RemoveAstarteParts()
        {
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
        }


        private void checkAllThatInsidesForGunk()
        {
            bool ohHellNahWeAMutant = false;
            foreach (BodyPartDef astarteBodyPart in Constants.AstarteBodyParts)
            {
                IEnumerable<BodyPartRecord> bodyPartRecords = pawn.def.race.body.GetPartsWithDef(astarteBodyPart);
                if (bodyPartRecords.Count() <= 1) continue;
                ohHellNahWeAMutant = true;
                Log.Message(
                    "Uh Oh this Astarte is a damn mutant, they got more than one copy of the astarte organs. Gotta fix our organs, something is helping our bodies too much.");
                FilthMaker.MakeFilth(pawn.Position, pawn.Map, ThingDefOf.Filth_Blood, 6);
                break;
            }

            if (!ohHellNahWeAMutant) return;
            DeDuper.DuplicatesByType(pawn.def);
            DeDuper.DuplicatesByLabel(pawn.def);
        }


        public override bool ShouldRemove => false;
    }
}