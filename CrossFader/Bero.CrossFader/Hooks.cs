using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using static Bero.CrossFader.CrossFader;

namespace Bero.CrossFader
{
	public static class Hooks
	{
		internal static bool IsEnabled()
		{
			if (Enabled.Value == Mode.Off || !flags)
				return false;
			else if (dataPathVR)
				return true;
			else if (Enabled.Value == Mode.On && (flags?.mode == HFlag.EMode.sonyu3P ? Sonyu3PPatched : true))
				return true;
			else
				return false;
		}

		//This should hook to a method that loads as late as possible in the loading phase
		//Hooking method "MapSameObjectDisable" because: "Something that happens at the end of H scene loading, good enough place to hook" - DeathWeasel1337/Anon11
		//https://github.com/DeathWeasel1337/KK_Plugins/blob/master/KK_EyeShaking/KK.EyeShaking.Hooks.cs#L20
		[HarmonyPostfix]
		[HarmonyPatch(typeof(HSceneProc), "MapSameObjectDisable")]
		public static void HSceneProcLoadPost(HSceneProc __instance)
		{
			flags = __instance.flags;
			female = Traverse.Create(__instance).Field("lstFemale").GetValue<List<ChaControl>>().FirstOrDefault<ChaControl>();
		}

		[HarmonyPatch(typeof(CrossFade), "FadeStart", new Type[]{ typeof(float) }, null)]
		[HarmonyPrefix]
		public static bool FadeStartHook()
		{
			return !IsEnabled();
		}

		[HarmonyPatch(typeof(ChaControl), "setPlay", new Type[] { typeof(string), typeof(int)}, null)]
		[HarmonyPrefix]
		public static bool SetPlayHook(string _strAnmName, int _nLayer, ChaControl __instance, ref bool __result)
		{
			if (!IsEnabled() || __instance.animBody == null || !flags)
				return true;

			if (flags.mode == HFlag.EMode.peeping)
			{
				__instance.animBody.CrossFadeInFixedTime(_strAnmName, 0f, _nLayer);
				__result = true;
				return false;
			}

			if ((flags.mode == HFlag.EMode.houshi || flags.mode == HFlag.EMode.houshi3P || flags.mode == HFlag.EMode.houshi3PMMF) 
				&& (_strAnmName == "Oral_Idle_IN" || _strAnmName == "M_OUT_Start"))
			{
				__instance.animBody.CrossFadeInFixedTime(_strAnmName, 0.2f, _nLayer);
				__result = true;
				return false;
			}

			if (_strAnmName == "M_Idle" && (__instance.animBody.GetCurrentAnimatorStateInfo(0).IsName("M_Touch"))
				|| (_strAnmName == "A_Idle" && __instance.animBody.GetCurrentAnimatorStateInfo(0).IsName("A_Touch"))
				|| (_strAnmName == "S_Idle" && __instance.animBody.GetCurrentAnimatorStateInfo(0).IsName("S_Touch")))
			{
				return true;
			}		
			
			__instance.animBody.CrossFadeInFixedTime(_strAnmName, UnityEngine.Random.Range(0.5f, 1f), _nLayer);
			__result = true;
			return false;
		}

		internal static bool InTransition()
		{
			if (!flags)
				return false;
			
			return !female?.animBody?.GetCurrentAnimatorStateInfo(0).IsName(flags.nowAnimStateName) ?? false;
		}

		[HarmonyPatch(typeof(HAibu), "Proc")]
		[HarmonyPrefix]
		public static bool HAibuProcHook(ref bool __result)
		{
			if (InTransition())
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPatch(typeof(HHoushi), "Proc")]
		[HarmonyPrefix]
		public static bool HHoushiProcHook(ref bool __result)
		{
			if (InTransition())
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPatch(typeof(HSonyu), "Proc")]
		[HarmonyPrefix]
		public static bool HSounyuProcHook(ref bool __result)
		{
			if (InTransition())
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPatch(typeof(HMasturbation), "Proc")]
		[HarmonyPrefix]
		public static bool HMasturbationHook(ref bool __result)
		{
			if (InTransition())
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPatch(typeof(HPeeping), "Proc")]
		[HarmonyPrefix]
		public static bool HPeeping(ref bool __result)
		{
			if (InTransition())
			{
				__result = false;
				return true;
			}
			return true;
		}

		[HarmonyPatch(typeof(HLesbian), "Proc")]
		[HarmonyPrefix]
		public static bool HLesbianProcHook(ref bool __result)
		{
			if (InTransition())
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPatch(typeof(H3PHoushi), "Proc")]
		[HarmonyPrefix]
		public static bool H3PHoushiProcHook(ref bool __result)
		{
			if (InTransition())
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPatch(typeof(H3PDarkSonyu), "Proc")]
		[HarmonyPrefix]
		public static bool H3PDarkSonyuProcHook(ref bool __result)
		{
			if (InTransition())
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPatch(typeof(H3PDarkHoushi), "Proc")]
		[HarmonyPrefix]
		public static bool H3PDarkHoushiProcHook(ref bool __result)
		{
			if (InTransition())
			{
				__result = false;
				return false;
			}
			return true;
		}

		/// <summary>
		/// Disable menu actions during transition to prevent menus being disabled incorrectly
		/// </summary>
		[HarmonyPatch(typeof(HSprite), nameof(HSprite.IsSpriteAciotn))]
		[HarmonyPrefix]
		public static bool IsSpriteActionOverride(ref bool __result)
		{
			if (InTransition())
			{
				__result = false;
				return false;
			}
			return true;
		}
	}
}
