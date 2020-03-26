using BepInEx;
using Harmony;
using System;

namespace Bero.CrossFader
{
	[BepInPlugin("bero.crossfader", "CrossFader", Version)]
	public class CrossFader : BaseUnityPlugin
	{
		public const string Version = "0.3";

		public void Awake()
		{
			try
			{
				HarmonyInstance harmonyInstance = HarmonyInstance.Create("bero.crossfader");
				harmonyInstance.PatchAll(typeof(Hooks));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
