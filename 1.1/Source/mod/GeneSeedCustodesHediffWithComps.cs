using Verse;

namespace GeneSeed
{
    public class GeneSeedCustodesHediffWithComps : GeneSeedHediffWithComps
    {
        protected override bool BlowOffParts(bool keep)
        {
            foreach (var astarteBodyPart in Constants.AstarteBodyParts)
            {
                foreach (var part in pawn.def.race.body.GetPartsWithDef(astarteBodyPart))
                {
                    PawnHelper.MutatePart(pawn, part);
                }
            }

            return false;
        }

        protected override ThingDef PawnThingDef()
        {
            return Constants.Custodes;
        }
    }
}