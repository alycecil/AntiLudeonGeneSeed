using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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
//            HarmonyInstance.DEBUG = true;
            var harmony = HarmonyInstance.Create("acecil.rimworld.geneseeds");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
        
        
        [HarmonyPatch(typeof(GenRecipe), "MakeRecipeProducts", null)]
        public static class GenRecipe_MakeRecipeProducts_Patch
        {
            [HarmonyPostfix]
            public static void Postfix(ref IEnumerable<Thing> __result, RecipeDef recipeDef, Pawn worker, List<Thing> ingredients)
            {
                List<Thing> list = (__result as List<Thing>) ?? __result.ToList<Thing>();
                
                foreach (Corpse corpse in ingredients.OfType<Corpse>()
                    .Where(corpse=>corpse.InnerPawn != null && PawnHelper.is_human(corpse.InnerPawn)))
                {

                    Thing var = ThingMaker.MakeThing(Constants.GeneSeed);
                    

                    if (var != null ) //add a rarity roll here 10%
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
                    Log.Message("[GeneSeed:Transpiler] WorkGiver_GatherAnimalBodyResources works on all things with races instead of just animals.");
                else
                    yield return code;
            }
        }
    }
    }
}