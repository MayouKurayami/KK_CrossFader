using BepInEx;
using Harmony;
using System;
using UnityEngine;


namespace Bero.CrossFader
{
	[BepInPlugin("bero.crossfader", "CrossFader", Version)]
	public class CrossFader : BaseUnityPlugin
	{
		public const string Version = "0.7";
		internal static HFlag flags;
		internal static ChaControl female;

		internal static bool dataPathVR;

		public void Awake()
		{
			try
			{
				HarmonyInstance harmonyInstance = HarmonyInstance.Create("bero.crossfader");
				harmonyInstance.PatchAll(typeof(Hooks));

				if (dataPathVR = Application.dataPath.EndsWith("KoikatuVR_Data"))
					harmonyInstance.PatchAll(typeof(VR_Hooks));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
