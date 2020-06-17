using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;

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
			CrossFader.female = Traverse.Create(__instance).Field("lstFemale").GetValue<List<ChaControl>>().FirstOrDefault<ChaControl>();
		}

		[HarmonyPatch(typeof(CrossFade), "FadeStart", new Type[]{ typeof(float) }, null)]
		[HarmonyPrefix]
		public static bool FadeStartHook()
		{
#if DEBUG_FIX
			//Skip hook if in sonyu3P mode and not in VR, since it is not patched as a workaround to prevent conflict with the modified mono.dll for debudding
			if (CrossFader.flags?.mode == HFlag.EMode.sonyu3P && !CrossFader.dataPathVR)
				return true;
			else
#endif
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
#if DEBUG_FIX
			//Skip hook if in sonyu3P mode and not in VR, since it is not patched as a workaround to prevent conflict with the modified mono.dll for debudding
			if (CrossFader.flags?.mode == HFlag.EMode.sonyu3P && !CrossFader.dataPathVR)
				return true;
#endif
			if (__instance.animBody == null)
			{
				__result = false;
				return false;
			}

			if (CrossFader.flags?.mode == HFlag.EMode.peeping)
			{
				__instance.animBody.CrossFadeInFixedTime(_strAnmName, 0f, _nLayer);
				__result = true;
				return false;
			}

			if ((__instance.animBody.GetCurrentAnimatorStateInfo(0).IsName("M_Touch") && _strAnmName == "M_Idle")
				|| (__instance.animBody.GetCurrentAnimatorStateInfo(0).IsName("A_Touch") && _strAnmName == "A_Idle")
				|| (__instance.animBody.GetCurrentAnimatorStateInfo(0).IsName("S_Touch") && _strAnmName == "S_Idle"))
			{
				return true;
			}		
			
			__instance.animBody.CrossFadeInFixedTime(_strAnmName, UnityEngine.Random.Range(0.5f, 1f), _nLayer);
			__result = true;
			return false;
		}

		internal static bool InTransition()
		{
			return (!CrossFader.female?.animBody.GetCurrentAnimatorStateInfo(0).IsName(CrossFader.flags?.nowAnimStateName)) ?? false;
		}

		[HarmonyPatch(typeof(HAibu), "Proc", null, null)]
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

		[HarmonyPatch(typeof(HHoushi), "Proc", null, null)]
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

		[HarmonyPatch(typeof(HSonyu), "Proc", null, null)]
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

		[HarmonyPatch(typeof(HMasturbation), "Proc", null, null)]
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

		[HarmonyPatch(typeof(HPeeping), "Proc", null, null)]
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

		[HarmonyPatch(typeof(HLesbian), "Proc", null, null)]
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

		[HarmonyPatch(typeof(H3PHoushi), "Proc", null, null)]
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

		[HarmonyPatch(typeof(H3PDarkSonyu), "Proc", null, null)]
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

		[HarmonyPatch(typeof(H3PDarkHoushi), "Proc", null, null)]
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

#if !DEBUG_FIX
		//If DEBUG_FIX is true, this hook should only be patched in VR, as defined in VR_Hooks
		[HarmonyPatch(typeof(H3PSonyu), "Proc", null, null)]
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
#endif
	}
}
