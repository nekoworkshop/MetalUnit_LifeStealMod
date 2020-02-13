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
    static class Main
    {

        public static bool enabled;
        public static ConfigManager configManager;
        public static UnityModManager.ModEntry mod;

        public static int accumulatedDamage = 0;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            mod = modEntry;

            var harmony = HarmonyInstance.Create(mod.Info.Id);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            configManager = ConfigManager.Load<ConfigManager>(mod);

            mod.OnGUI = OnGUI;
            mod.OnSaveGUI = OnSaveGUI;
            mod.Logger.Log("Current game version: " + Application.version);
            mod.Logger.Log("Mod folder: " + mod.Path);

            return true;
        }

        static void OnGUI (UnityModManager.ModEntry modEntry)
        {
            configManager.Draw(modEntry);
        }
        static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            configManager.Save(modEntry);
        }
    }
}
