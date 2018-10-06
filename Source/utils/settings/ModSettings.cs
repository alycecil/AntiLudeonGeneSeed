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
            WoohooSettingHelper.latest = this.settings;
        }


        public override string SettingsCategory() => "40k GeneSeed!";

        public override void DoSettingsWindowContents(Rect inRect)
        {
                
                
            this.settings.astartePunchingFactor = Widgets.HorizontalSlider(inRect.TopHalf().TopHalf().TopHalf().ContractedBy(4),
                this.settings.astartePunchingFactor, 0f, 5f, true,
                "Astarte Punching Power : " + this.settings.astartePunchingFactor*100 +
                "% Range : ["+15f*this.settings.astartePunchingFactor+","+80f*this.settings.astartePunchingFactor+"]\nDefault possible in single attack (Punch 15, Caustic Spit 80 at 100%)"
                , "0%", "500%");

            Widgets.Label(inRect.BottomHalf().BottomHalf().BottomHalf(), "That's all, restart before playing to ensure your change is there. -Alice.\nSource Code Available at https://github.com/alycecil");
            this.settings.Write();
        }
    }
    
    class GeneSeedSettings : ModSettings
    {
        public float astartePunchingFactor = base_astartePunchingFactor;
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref this.astartePunchingFactor, "astartePunchingFactor", base_astartePunchingFactor);
        }


        static readonly float base_astartePunchingFactor = 80f;

        public void update()
        {
            foreach (var astarteTool in Constants.Astarte.tools.Union(Constants.Custodes.tools))
            {
                astarteTool.power *= astartePunchingFactor;
            }
        }
    }
}