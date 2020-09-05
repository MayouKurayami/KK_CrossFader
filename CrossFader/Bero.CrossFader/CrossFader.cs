using BepInEx;
using BepInEx.Harmony;
using BepInEx.Configuration;
using System;
using UnityEngine;
using System.ComponentModel;


namespace Bero.CrossFader
{
	[BepInPlugin("bero.crossfader", PluginName, Version)]
	public class CrossFader : BaseUnityPlugin
	{
		public const string PluginName = "CrossFader";
		public const string Version = "0.10";
		internal static HFlag flags;
		internal static ChaControl female;

		internal static bool dataPathVR;
		internal static bool Sonyu3PPatched;


		public static ConfigEntry<Mode> Enabled { get; private set; }

		public static ConfigEntry<bool> DebugFix { get; private set; }


		public void Awake()
		{
			Enabled = Config.Bind("", "Enable Crossfade", Mode.On, "Enable smooth fade between animations. \nChoose \"VR only\" if you only want it enabled in official VR");

			DebugFix = Config.Bind("Advanced", "Debugger Crash Workaround", true, new ConfigDescription(
					"Disable crossfade in non-VR 3P intercourse to prevent conflict with the mono.dll used for dnSpy debugging." +
					"\nKeep this enabled unless you're sure that you do not have the modified dll in place." +
					"\nRequires restarting the game to take effect.", 
					null, new ConfigurationManagerAttributes { IsAdvanced = true, Order = 0}));

			try
			{
				HarmonyWrapper.PatchAll(typeof(Hooks));

				if (dataPathVR = Application.dataPath.EndsWith("KoikatuVR_Data"))
				{
					HarmonyWrapper.PatchAll(typeof(Hooks_VR));
					HarmonyWrapper.PatchAll(typeof(Hooks_H3PSonyu));
					Sonyu3PPatched = true;
				}
				else if (!DebugFix.Value)
				{
					HarmonyWrapper.PatchAll(typeof(Hooks_H3PSonyu));
					Sonyu3PPatched = true;
				}
					
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		public enum Mode
		{
			On,
			[Description("VR Only")]
			VR,
			Off
		}
	}
}
