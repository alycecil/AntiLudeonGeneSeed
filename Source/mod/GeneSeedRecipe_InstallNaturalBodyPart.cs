using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace GeneSeed
{
    public class GeneSeedRecipe_InstallNaturalBodyPart
    : Recipe_InstallNaturalBodyPart
    {
        // Token: 0x06001434 RID: 5172 RVA: 0x0009BA6C File Offset: 0x00099E6C
        public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
        {
            foreach (var recipePart in recipe.appliedOnFixedBodyParts)
            {
                IEnumerable<BodyPartRecord> missingParts = pawn.RaceProps.body.AllParts.Where(x=>pawn.health.hediffSet.PartIsMissing(x));
                foreach (var record in missingParts)
                {
                    if (record.def == recipePart)
                    {
                        
                        yield return record;
                    }
                }
            }

            yield break;
        }
    }
}