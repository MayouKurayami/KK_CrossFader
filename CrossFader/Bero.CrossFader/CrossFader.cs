using BepInEx;
using Harmony;
using System;
using UnityEngine;
using System.ComponentModel;


namespace Bero.CrossFader
{
	[BepInPlugin("bero.crossfader", PluginName, Version)]
	public class CrossFader : BaseUnityPlugin
	{
		public const string PluginName = "CrossFader";
		public const string Version = "0.7";
		internal static HFlag flags;
		internal static ChaControl female;

		internal static bool dataPathVR;


		[DisplayName("Enabled")]
		[Description("Enable smooth fade between animations. \nChoose VR if you only want it enabled in VR")]
		public static ConfigWrapper<Mode> Enabled { get; private set; }

		[Category("Advanced Settings")]
		[DisplayName("Debugger Crash Workaround")]
		[Description("Disable crossfade in non-VR 3P intercourse to prevent conflict with the mono.dll used for dnSpy debugging." +
			"\nKeep this enabled unless you're sure that you do not have the modified dll in place." +
			"\nRequires restarting the game to take effect.")]
		public static ConfigWrapper<bool> DebugFix { get; private set; }


		public void Awake()
		{
			Enabled = new ConfigWrapper<Mode>(nameof(Enabled), this, Mode.On);
			DebugFix = new ConfigWrapper<bool>(nameof(DebugFix), this, true);

			try
			{
				HarmonyInstance harmonyInstance = HarmonyInstance.Create("bero.crossfader");
				harmonyInstance.PatchAll(typeof(Hooks));

				if (dataPathVR = Application.dataPath.EndsWith("KoikatuVR_Data"))
				{
					harmonyInstance.PatchAll(typeof(VR_Hooks));
					harmonyInstance.PatchAll(typeof(H3PSonyu_Hook));
				}
				else if (!DebugFix.Value)
				{
					harmonyInstance.PatchAll(typeof(H3PSonyu_Hook));
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
