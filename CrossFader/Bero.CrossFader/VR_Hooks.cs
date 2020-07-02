using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;

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
			CrossFader.female = Traverse.Create(__instance).Field("lstFemale").GetValue<List<ChaControl>>().FirstOrDefault<ChaControl>();
		}
	}
}
