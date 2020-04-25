using Harmony;
using System;

namespace Bero.CrossFader
{
	public static class Hooks
	{
		//This should hook to a method that loads as late as possible in the loading phase
		//Hooking method "MapSameObjectDisable" because: "Something that happens at the end of H scene loading, good enough place to hook" - DeathWeasel1337/Anon11
		//https://github.com/DeathWeasel1337/KK_Plugins/blob/master/KK_EyeShaking/KK.EyeShaking.Hooks.cs#L20
		[HarmonyPostfix]
		[HarmonyPatch(typeof(HSceneProc), "MapSameObjectDisable")]
		public static void HSceneProcLoadPost(HSceneProc __instance)
		{
			CrossFader.flags = __instance.flags;
		}

		[HarmonyPatch(typeof(CrossFade), "FadeStart", new Type[]{ typeof(float) }, null)]
		[HarmonyPrefix]
		public static bool FadeStartHook()
		{
			//Skip hook if in sonyu3P mode and not in VR, since it is not patched as a workaround to prevent conflict with the modified mono.dll for debudding
			if (CrossFader.flags?.mode == HFlag.EMode.sonyu3P && !CrossFader.dataPathVR)
				return true;
			else
				return false;
		}

		[HarmonyPatch(typeof(ChaControl), "setPlay", new Type[]
		{
			typeof(string),
			typeof(int)
		}, null)]
		[HarmonyPrefix]
		public static bool SetPlayHook(string _strAnmName, int _nLayer, ChaControl __instance, ref bool __result)
		{
			//Skip hook if in sonyu3P mode and not in VR, since it is not patched as a workaround to prevent conflict with the modified mono.dll for debudding
			if (CrossFader.flags?.mode == HFlag.EMode.sonyu3P && !CrossFader.dataPathVR)
				return true;

			if (CrossFader.flags?.mode == HFlag.EMode.peeping)
			{
				__instance.animBody.CrossFadeInFixedTime(_strAnmName, 0f, _nLayer);
				__result = true;
				return false;
			}
			if (__instance.animBody == null)
			{
				__result = false;
				return false;
			}
			__instance.animBody.CrossFadeInFixedTime(_strAnmName, UnityEngine.Random.Range(0.5f, 1f), _nLayer);
			__result = true;
			return false;
		}

		internal static bool InTransition(HActionBase ab)
		{
			ChaControl value = Traverse.Create(ab).Field("female").GetValue<ChaControl>();
			HFlag value2 = Traverse.Create(ab).Field("flags").GetValue<HFlag>();
			return !value.animBody.GetCurrentAnimatorStateInfo(0).IsName(value2.nowAnimStateName);
		}

		[HarmonyPatch(typeof(HAibu), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool HAibuProcHook(HAibu __instance, ref bool __result)
		{
			if (InTransition(__instance))
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPatch(typeof(HHoushi), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool HHoushiProcHook(HHoushi __instance, ref bool __result)
		{
			if (InTransition(__instance))
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPatch(typeof(HSonyu), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool HSounyuProcHook(HSonyu __instance, ref bool __result)
		{
			if (InTransition(__instance))
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPatch(typeof(HMasturbation), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool HMasturbationHook(HMasturbation __instance, ref bool __result)
		{
			if (InTransition(__instance))
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPatch(typeof(HPeeping), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool HPeeping(HPeeping __instance, ref bool __result)
		{
			if (InTransition(__instance))
			{
				__result = false;
				return true;
			}
			return true;
		}

		[HarmonyPatch(typeof(HLesbian), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool HLesbianProcHook(HSonyu __instance, ref bool __result)
		{
			if (InTransition(__instance))
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPatch(typeof(H3PHoushi), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool H3PHoushiProcHook(H3PHoushi __instance, ref bool __result)
		{
			if (InTransition(__instance))
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPatch(typeof(H3PDarkSonyu), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool H3PDarkSonyuProcHook(H3PDarkSonyu __instance, ref bool __result)
		{
			if (InTransition(__instance))
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPatch(typeof(H3PDarkHoushi), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool H3PDarkHoushiProcHook(H3PDarkHoushi __instance, ref bool __result)
		{
			if (InTransition(__instance))
			{
				__result = false;
				return false;
			}
			return true;
		}
	}
}
