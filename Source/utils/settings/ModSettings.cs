using System.Linq;
using System.Reflection;
using Harmony;
using UnityEngine;
using Verse;

namespace GeneSeed.settings
{
    class WoohooMod : Mod
    {
        private GeneSeedSettings settings;

        public WoohooMod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<GeneSeedSettings>();
            SettingsHelper.latest = this.settings;
        }


        public override string SettingsCategory() => "40k GeneSeed!";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            this.settings.astartePunchingFactor = Widgets.HorizontalSlider(
                inRect.TopHalf().TopHalf().TopHalf().ContractedBy(4),
                this.settings.astartePunchingFactor, 0f, 5f, true,
                "Astarte Punching Power : " + this.settings.astartePunchingFactor * 100 +
                "% : [" + 15f * this.settings.astartePunchingFactor+ "]\nDefault possible in single attack (Punch 15 at 100%)"
                , "0%", "500%");

            this.settings.astarteSplitFactor = Widgets.HorizontalSlider(
                inRect.TopHalf().TopHalf().BottomHalf().ContractedBy(4),
                this.settings.astarteSplitFactor, 0f, 5f, true,
                "Astarte Spit Power : " + this.settings.astarteSplitFactor * 100 +
                "% : [" + 80f * this.settings.astarteSplitFactor +
                "]\nDefault possible in single attack (Caustic Spit 80 at 100%)"
                , "0%", "500%");

            this.settings.scale = Widgets.HorizontalSlider(inRect.TopHalf().BottomHalf().TopHalf().ContractedBy(4),
                this.settings.scale, 0f, 2f, true,
                "Astarte Size Scaler: " + this.settings.astartePunchingFactor * 100 +
                "% for size of " + 3f * this.settings.scale
                , "0%", "200%");

            Widgets.Label(inRect.BottomHalf().BottomHalf().BottomHalf(),
                "That's all, restart before playing to ensure your change is there. -Alice.\nSource Code Available at https://github.com/alycecil");
            this.settings.Write();
        }
    }

    class GeneSeedSettings : ModSettings
    {
        public float astartePunchingFactor = 1f, astarteSplitFactor = 1f, scale = 1f;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref this.astartePunchingFactor, "astartePunchingFactor", 1f);
            Scribe_Values.Look(ref this.astarteSplitFactor, "astarteSplitFactor", 1f);
            Scribe_Values.Look(ref this.scale, "scale", 1f);
        }


        public void update()
        {
            foreach (var astarteTool in Constants.Astarte.tools.Union(Constants.Custodes.tools))
            {
                if (astarteTool.linkedBodyPartsGroup.defName == "HeadAttackTool")
                {
                    astarteTool.power *= astarteSplitFactor;
                }
                else
                    astarteTool.power *= astartePunchingFactor;
            }

            Constants.Astarte.race.baseBodySize *= scale;
            Constants.Custodes.race.baseBodySize *= scale;
        }
    }
}