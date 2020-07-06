using System;
using HarmonyLib;


namespace Bero.CrossFader
{
	public static class Hooks_H3PSonyu
	{
		//This should only be patched in VR or if DebugFix is disabled because of a conflict with the modified mono.dll for debugging the non-VR version of the game
		[HarmonyPatch(typeof(H3PSonyu), "Proc")]
		[HarmonyPrefix]
		public static bool H3PSonyuProcHook(ref bool __result)
		{
			if (Hooks.InTransition())
			{
				__result = false;
				return false;
			}
			return true;
		}
	}
}
