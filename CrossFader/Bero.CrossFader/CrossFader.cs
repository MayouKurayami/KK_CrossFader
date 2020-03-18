using BepInEx;
using Harmony;
using System;

namespace Bero.CrossFader
{
	[BepInPlugin("bero.crossfader", "CrossFader", "0.1")]
	public class CrossFader : BaseUnityPlugin
	{
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
