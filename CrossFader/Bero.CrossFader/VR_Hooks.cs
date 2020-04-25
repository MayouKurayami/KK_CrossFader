﻿using Harmony;


namespace Bero.CrossFader
{
	public static class VR_Hooks
	{
		//This should hook to a method that loads as late as possible in the loading phase
		//Hooking method "MapSameObjectDisable" because: "Something that happens at the end of H scene loading, good enough place to hook" - DeathWeasel1337/Anon11
		//https://github.com/DeathWeasel1337/KK_Plugins/blob/master/KK_EyeShaking/KK.EyeShaking.Hooks.cs#L20
		[HarmonyPostfix]
		[HarmonyPatch(typeof(VRHScene), "MapSameObjectDisable")]
		public static void HSceneProcLoadPost(VRHScene __instance)
		{
			CrossFader.flags = __instance.flags;
		}

		//This should only be patched in VR because of a conflict with the modified mono.dll for debugging the non-VR version of the game
		[HarmonyPatch(typeof(H3PSonyu), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool H3PSonyuProcHook(H3PSonyu __instance, ref bool __result)
		{
			if (Hooks.InTransition(__instance))
			{
				__result = false;
				return false;
			}
			return true;
		}
	}
}