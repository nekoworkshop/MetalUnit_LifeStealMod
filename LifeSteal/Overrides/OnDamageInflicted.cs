using System;
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
    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch("OnDamageInflicted")]
    class OnDamageInflicted
    {
        static void Postfix(int damage, ItemInfoData item, Actor target, Player __instance)
        {
            if (Main.configManager.lifeStealSettings.enabled == false)
            {
                return;
            }
            if (__instance.GetCurrentHp() >= Main.configManager.lifeStealSettings.healCap)
            {
                return;
            }
            if ((item.itemType == ItemType.MeleeWeapon) && !(Main.configManager.lifeStealSettings.meleeLifeSteal))
            {
                return;
            }
            if ((item.itemType == ItemType.RangedWeapon) && !(Main.configManager.lifeStealSettings.rangedLifeSteal))
            {
                return;
            }
            if ((item.itemType == ItemType.SubWeapon) && !(Main.configManager.lifeStealSettings.subWeaponLifeSteal))
            {
                return;
            }

            if ((item.itemType == ItemType.Ultimate) && !(Main.configManager.lifeStealSettings.ultiLifeSteal))
            {
                return;
            }
            if (!__instance.IsAlive())
            {
                return;
            }
            Main.accumulatedDamage += damage;
            if (Main.accumulatedDamage >= Main.configManager.lifeStealSettings.damagePerHealth)
            {
                //Calculate the amount of health point to heal
                int toHeal = Main.accumulatedDamage / Main.configManager.lifeStealSettings.damagePerHealth;

                //Update accumulatedDamage 
                Main.accumulatedDamage = Main.accumulatedDamage % Main.configManager.lifeStealSettings.damagePerHealth;

                //Prevent healing over heal cap.
                if (toHeal + __instance.GetCurrentHp() > Main.configManager.lifeStealSettings.healCap)
                {
                    toHeal = Main.configManager.lifeStealSettings.healCap - __instance.GetCurrentHp();
                }
                //Inflict healing
                __instance.OnHeal(toHeal);
            }
        }
    }
}
