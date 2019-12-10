using System;
using Harmony;
using UnityEngine;

namespace Bero.CrossFader
{
	public static class Hooks
	{
		[HarmonyPatch(typeof(CrossFade), "FadeStart", new Type[]
		{
			typeof(float)
		}, null)]
		[HarmonyPrefix]
		public static bool FadeStartHook()
		{
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
			bool result;
			if (__instance.animBody == null)
			{
				__result = false;
				result = false;
			}
			else
			{
				__instance.animBody.CrossFadeInFixedTime(_strAnmName, UnityEngine.Random.Range(0.5f, 1f), _nLayer);
				__result = true;
				result = false;
			}
			return result;
		}

		private static bool InTransition(HActionBase ab)
		{
			ChaInfo value = Traverse.Create(ab).Field("female").GetValue<ChaControl>();
			HFlag value2 = Traverse.Create(ab).Field("flags").GetValue<HFlag>();
			return !value.animBody.GetCurrentAnimatorStateInfo(0).IsName(value2.nowAnimStateName);
		}

		[HarmonyPatch(typeof(HAibu), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool HAibuProcHook(HAibu __instance, ref bool __result)
		{
			bool result;
			if (Hooks.InTransition(__instance))
			{
				__result = false;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		[HarmonyPatch(typeof(HHoushi), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool HHoushiProcHook(HHoushi __instance, ref bool __result)
		{
			bool result;
			if (Hooks.InTransition(__instance))
			{
				__result = false;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		[HarmonyPatch(typeof(HSonyu), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool HSounyuProcHook(HSonyu __instance, ref bool __result)
		{
			bool result;
			if (Hooks.InTransition(__instance))
			{
				__result = false;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		[HarmonyPatch(typeof(HMasturbation), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool HMasturbationHook(HMasturbation __instance, ref bool __result)
		{
			bool result;
			if (Hooks.InTransition(__instance))
			{
				__result = false;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		[HarmonyPatch(typeof(HPeeping), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool HPeeping(HPeeping __instance, ref bool __result)
		{
			bool result;
			if (Hooks.InTransition(__instance))
			{
				__result = false;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		[HarmonyPatch(typeof(HLesbian), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool HLesbianProcHook(HSonyu __instance, ref bool __result)
		{
			bool result;
			if (Hooks.InTransition(__instance))
			{
				__result = false;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		[HarmonyPatch(typeof(H3PHoushi), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool H3PHoushiProcHook(H3PHoushi __instance, ref bool __result)
		{
			bool result;
			if (Hooks.InTransition(__instance))
			{
				__result = false;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		[HarmonyPatch(typeof(H3PSonyu), "Proc", null, null)]
		[HarmonyPrefix]
		public static bool H3PSonyuProcHook(H3PSonyu __instance, ref bool __result)
		{
			bool result;
			if (Hooks.InTransition(__instance))
			{
				__result = false;
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}
	}
}
