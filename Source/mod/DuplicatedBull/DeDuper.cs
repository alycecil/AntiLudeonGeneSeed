using System.Collections.Generic;
using System.Linq;
using Verse;

namespace GeneSeed
{
    [StaticConstructorOnStartup]
    public static class DeDuper
    {
        static DeDuper()
        {
            DuplicatesByType(Constants.Astarte);
            DuplicatesByLabel(Constants.Astarte);
        }

        public static void DuplicatesByType(ThingDef thingDef)
        {
            IEnumerable<BodyPartRecord> removeMe = new List<BodyPartRecord>();


            IEnumerable<IGrouping<BodyPartDef, BodyPartRecord>> partsByType = thingDef.race.body.AllParts
                .Where(x => x.customLabel.NullOrEmpty())
                .GroupBy(x => x.def)
                .Where(x => x.Count() > 1);

            foreach (IGrouping<BodyPartDef, BodyPartRecord> bodyPartRecords in partsByType)
            {
                int duplicates = bodyPartRecords.Count(), index = 0;
                IEnumerable<BodyPartRecord> partRecords = bodyPartRecords.Where(x => (index++) <= duplicates / 2);

                removeMe = removeMe.Union(partRecords);
            }

            RipOutParts(thingDef, removeMe);
        }

        private static void RipOutParts(ThingDef thingDef, IEnumerable<BodyPartRecord> removeMe)
        {
            foreach (var bodyPartRecord in removeMe.Distinct())
            {
                Log.Message("Freaking out and kinda like removing body parts this time its a " + bodyPartRecord);
                thingDef.race.body.AllParts.Remove(bodyPartRecord);
            }
        }

        public static void DuplicatesByLabel(ThingDef thingDef)
        {
            IEnumerable<BodyPartRecord> removeMe = new List<BodyPartRecord>();
            
            var partsByLabel = thingDef.race.body.AllParts
                .Where(x=>!x.customLabel.NullOrEmpty())
                .GroupBy(x => x.customLabel)
                .Where(x=>x.Count()>1);
                
            foreach (var bodyPartRecords in partsByLabel)
            {
                int duplicates = bodyPartRecords.Count(), index = 0;
                
                Log.Message("Really we got ["+duplicates+"] bonus ["+bodyPartRecords.Key+"], lets just cut that off.");
                
                removeMe = removeMe.Union(bodyPartRecords.Where(x => (index++) < duplicates / 2));
            }
            
            RipOutParts(thingDef , removeMe);
        }
    }
}