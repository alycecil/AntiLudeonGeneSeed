using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace GeneSeed
{
    public class GeneSeedRecipe_MakeAwesome
        : Recipe_InstallNaturalBodyPart
    {
        // Token: 0x06001434 RID: 5172 RVA: 0x0009BA6C File Offset: 0x00099E6C
        public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
        {
            var missingParts = BodyPartRecordsMissing(pawn);
            foreach (var record in missingParts)
            {
                yield return record;
                yield break;
            }
        }

        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients,
            Bill bill)
        {
            TaleRecorder.RecordTale(TaleDefOf.DidSurgery, (object) billDoer, (object) pawn);

            var missingParts = BodyPartRecordsMissing(pawn);
            foreach (var record in missingParts)
            {
                MedicalRecipesUtility.RestorePartAndSpawnAllPreviousParts(pawn, record, billDoer.Position,
                    billDoer.Map);
            }
        }

        private static IEnumerable<BodyPartRecord> BodyPartRecordsMissing(Pawn pawn)
        {
            IEnumerable<BodyPartRecord> missingParts = pawn.RaceProps.body.AllParts.Where(x =>
                pawn.health.hediffSet.PartIsMissing(x) && x.body == Constants.Astarte.race.body);
            return missingParts;
        }
    }
}