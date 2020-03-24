using System;
using System.Collections.Generic;
using System.Linq;
using GeneSeed.crossmods;
using GeneSeed.Organs;
using GeneSeed.settings;
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

            base.Tick();
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

            MoreBetterHediffs();
        }

        private void MoreBetterHediffs()
        {
            if (ticks % 2500 != 0) return;
            foreach (Pair<BodyPartDef, HediffDef> pair in ImperialGuard.AstarteBodyParts)
            {
                if (pair.Second == null || pair.First == null) continue;

                if (pawn.health.hediffSet.HasHediff(pair.Second)) continue;

                foreach (var part in pawn.def.race.body.GetPartsWithDef(pair.First))
                {
                    if (part == null || pawn.health.hediffSet.PartIsMissing(part)) continue;

                    var diff = pawn.health.AddHediff(pair.Second, part);
                    diff.Severity = Rand.Value;
                }
            }

            if (SettingsHelper.latest.n17Rimhammer) return;

            foreach (Pair<BodyPartDef, HediffDef> pair in GeneSeedOrganHelper.AstarteBodyParts)
            {
                if (pair.Second == null || pair.First == null) continue;

                try
                {
                    if (pawn.health.hediffSet.HasHediff(pair.Second)) continue;

                    foreach (var part in pawn.def.race.body.GetPartsWithDef(pair.First))
                    {
                        if (part == null || pawn.health.hediffSet.PartIsMissing(part)) continue;

                        var diff = pawn.health.AddHediff(pair.Second, part);
                        diff.Severity = Rand.Value;
                    }
                }
                catch (Exception e)
                {
                    Log.Message("Failed to put something somewhere" + e.StackTrace);
                }
            }
        }

        protected virtual void GeneSeedMutation()
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
                }
            }

            if (!didOne) return;
            //checkAllThatInsidesForGunk();
            this.Severity -= 0.05f;
        }

        public override void PostAdd(DamageInfo? dinfo)
        {
            if (pawn.RaceProps.Humanlike && (pawn.def == ThingDefOf.Human ||
                                             ThingDefOf.Human.race.body.defName == pawn.def.race.body.defName ||
                                             SettingsHelper.latest.n17Rimhammer &&
                                             (pawn.def == N17Rimhammer.HumanAlt || pawn.def == N17Rimhammer.HumanAlt2 ||
                                              pawn.def == N17Rimhammer.HumanAlt3))
            )
                TransformPawn();

            base.PostAdd(dinfo);
        }

        private void TransformPawn(bool changeDef = true, bool keep = false)
        {
            //Body change to Astarte
            var where = pawn.Position;

            var map = pawn.Map;
        //   pawn.DestroyOrPassToWorld();
        //    pawn.DeSpawn();
            RegionListersUpdater.DeregisterInRegions(pawn, map);

            var thingDef = PawnThingDef();
            if (changeDef && thingDef != null)
            {
                HediffSet set = pawn.health.hediffSet;
                pawn.def = thingDef;
                pawn.health = new Pawn_HealthTracker(pawn);
                pawn.health.hediffSet.DirtyCache();
                pawn.health.hediffSet = set;
            }

        //    pawn.SpawnSetup(map, true);

            RegionListersUpdater.RegisterInRegions(pawn, map);


            map.mapPawns.UpdateRegistryForPawn(pawn);

            //remove the 19

            if (BlowOffParts(keep)) RemoveAstarteParts();

            //decache graphics
            pawn.Drawer.renderer.graphics.ResolveAllGraphics();

            //save the pawn
            pawn.ExposeData();

        //    pawn.Position = map.Center;
        }

        protected virtual ThingDef PawnThingDef()
        {
            if (SettingsHelper.latest.n17Rimhammer && N17Rimhammer.AstartesAlt != null) return N17Rimhammer.AstartesAlt;

            return Constants.Astarte;
        }

        protected virtual bool BlowOffParts(bool keep)
        {
            if (SettingsHelper.latest.instantTransform) return false;
            return !keep;
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
                    break;
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
                IEnumerable<BodyPartRecord> bodyPartRecords =
                    pawn.def.race.body.GetPartsWithDef(astarteBodyPart).ToList();
                if (bodyPartRecords.Count() <= 1) continue;
                ohHellNahWeAMutant = true;
                Log.Message(
                    "Uh Oh this Astarte is a damn mutant, they got more than one copy of the astarte organs [" +
                    bodyPartRecords.First() + "]");
                FilthMaker.TryMakeFilth(pawn.Position, pawn.Map, ThingDefOf.Filth_Blood, 6);
                break;
            }

            if (!ohHellNahWeAMutant) return;
        }


        public override bool ShouldRemove => false;
    }
}