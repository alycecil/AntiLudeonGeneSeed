using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GeneSeed.crossmods;
using GeneSeed.settings;
using Harmony;
using RimWorld;
using Verse;

namespace GeneSeed
{
    public class GeneSeed
        : Mod
    {
        public GeneSeed(ModContentPack content) : base(content)
        {
            var harmony = HarmonyInstance.Create("acecil.rimworld.geneseeds");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public static void DealWithAstarteMissingParts(BodyDef raceBody)
        {
            foreach (BodyPartDef warGear in Constants.AstarteBodyParts)
            {
                bool hadParts = false;
                foreach (var part in raceBody.GetPartsWithDef(warGear))
                {
                    hadParts = true;
                    break;
                }
                
                raceBody.corePart.parts.Add(new BodyPartRecord
                {
                    coverage = 0f,
                    def = warGear,
                    depth = BodyPartDepth.Undefined,
                    groups = new List<BodyPartGroupDef>(new[] {BodyPartGroupDefOf.Torso}),
                    height = BodyPartHeight.Middle
                });
            }

            //raceBody.ResolveReferences();
        }
    }

    [HarmonyPatch(typeof(GenRecipe), "MakeRecipeProducts", null)]
    public static class GenRecipe_MakeRecipeProducts_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(ref IEnumerable<Thing> __result, RecipeDef recipeDef, Pawn worker,
            List<Thing> ingredients)
        {
            List<Thing> list = (__result as List<Thing>) ?? __result.ToList<Thing>();

            foreach (Corpse corpse in ingredients.OfType<Corpse>()
                .Where(corpse => corpse.InnerPawn != null && PawnHelper.is_human(corpse.InnerPawn)))
            {
                Thing var = ThingMaker.MakeThing(Constants.GeneSeed);


                if (var != null)
                {
                    //if astarte more.
                    var.stackCount = corpse.InnerPawn.def == Constants.Astarte ? 20 : 1;

                    list.Add(var);
                }
                else
                {
                    Log.Error("Failed to make GeneSeed");
                }
            }

            __result = list;
            return;
        }
    }


    [HarmonyPatch(typeof(WorkGiver_GatherAnimalBodyResources), "HasJobOnThing")]
    public static class WorkGiver_GatherAnimalBodyResources_Transpiler_Patch
    {
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            foreach (var code in codes)
            {
                if ("Boolean get_Animal()".Equals(code.operand?.ToString()))
                    Log.Message(
                        "[GeneSeed:Transpiler] WorkGiver_GatherAnimalBodyResources works on all things with races instead of just animals.");
                else
                    yield return code;
            }
        }
    }
    
    
    [HarmonyPatch(typeof(MassUtility), "Capacity", null)]
    public static class GeneSeed_MassUtility_Capacity
    {
        [HarmonyPostfix]
        public static void Postfix(ref float __result, Pawn p)
        {

            if (p.health.hediffSet.hediffs.Any(x => x.def.defName.StartsWith("GeenSeedAdaption")))
            {
                __result += 1.2f * p.GetStatValue(StatDefOf.Mass);
            }
        }
    }
    

    [HarmonyPatch(typeof(DefGenerator), "GenerateImpliedDefs_PreResolve")]
    public static class DefGenerator_GenerateImpliedDefs_PreResolve
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            Log.Message("Adding Adaptus Recipe");
            var thingDef = DefDatabase<ThingDef>.GetNamed("AstarteAdaptusCore");


            foreach (var recipe in ThingDefOf.Human.recipes)
            {
                Log.Message("Adding : " + recipe);
                thingDef.recipes.Add(recipe);
            }


            AdeptusMechanicus_Patch.DealWithMissingWargearParts(thingDef);
            
            N17Rimhammer.PatchBody();

            SettingsHelper.latest.update();
        }
    }
}