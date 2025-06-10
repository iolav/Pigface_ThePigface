using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Pigface_ThePigface
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string PluginGUID = "iolav.Pigface_ThePigface";
        private const string PluginName = "The Pigface";
        private const string PluginVersion = "1.0.0";

        private readonly Harmony MyHarmony = new Harmony(PluginGUID);

        public static string AssetsFolderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets");
        public static AssetBundle Bundle = AssetBundle.LoadFromFile(Path.Combine(Plugin.AssetsFolderPath, "assets"));

        public static ManualLogSource? Log;

        private void Awake()
        {
            Log = Logger;
            Logger.LogInfo($"Loaded {PluginGUID}");

            MyHarmony.PatchAll();
        }
    }
}