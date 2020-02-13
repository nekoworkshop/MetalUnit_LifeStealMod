using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Harmony12;
using UnityModManagerNet;

namespace LifeSteal
{
    class ConfigManager : UnityModManager.ModSettings, IDrawable
    {

        [Draw("Heal up by damaging the enemies!", Collapsible = false)] public LifeStealSettings lifeStealSettings = new LifeStealSettings();

        public void OnChange()
        {
            throw new NotImplementedException();
        }

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }
    }


    [DrawFields(DrawFieldMask.Public)]
    public class LifeStealSettings
    {
        [Draw("Enabled")] public bool enabled = true;
        [Draw("Amount of Damage to heal one HP")] public int damagePerHealth = 200;
        [Draw("Will only heal up to this amount of health (default=15)")] public int healCap = 15;
        [Draw("Enable Lifesteal for melee weapons")] public bool meleeLifeSteal = true;
        [Draw("Enable Lifesteal for ranged weapons")] public bool rangedLifeSteal = false;
        [Draw("Enable Lifesteal for sub-weapons")] public bool subWeaponLifeSteal = false;
        [Draw("Enable Lifesteal for ultimates")] public bool ultiLifeSteal = true;
    }
}
