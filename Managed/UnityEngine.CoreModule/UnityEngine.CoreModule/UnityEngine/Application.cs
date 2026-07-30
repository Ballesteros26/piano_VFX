using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using UnityEngine.Bindings;
using UnityEngine.Diagnostics;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200009C RID: 156
	[NativeHeader("Runtime/Logging/LogSystem.h")]
	[NativeHeader("Runtime/Network/NetworkUtility.h")]
	[NativeHeader("Runtime/Application/AdsIdHandler.h")]
	[NativeHeader("Runtime/PreloadManager/LoadSceneOperation.h")]
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	[NativeHeader("Runtime/Misc/Player.h")]
	[NativeHeader("Runtime/Misc/BuildSettings.h")]
	[NativeHeader("Runtime/Input/InputManager.h")]
	[NativeHeader("Runtime/Input/GetInput.h")]
	[NativeHeader("Runtime/File/ApplicationSpecificPersistentDataPath.h")]
	[NativeHeader("Runtime/Export/Application/Application.bindings.h")]
	[NativeHeader("Runtime/BaseClasses/IsPlaying.h")]
	[NativeHeader("Runtime/Application/ApplicationInfo.h")]
	[NativeHeader("Runtime/Input/TargetFrameRate.h")]
	[NativeHeader("Runtime/PreloadManager/PreloadManager.h")]
	[NativeHeader("Runtime/Utilities/Argv.h")]
	[NativeHeader("Runtime/Utilities/URLUtility.h")]
	[NativeHeader("Runtime/Misc/SystemInfo.h")]
	public class Application
	{
		// Token: 0x0600020C RID: 524
		[FreeFunction("GetInputManager().QuitApplication")]
		[MethodImpl(4096)]
		public static extern void Quit(int exitCode);

		// Token: 0x0600020D RID: 525 RVA: 0x000046E9 File Offset: 0x000028E9
		public static void Quit()
		{
			Application.Quit(0);
		}

		// Token: 0x0600020E RID: 526
		[FreeFunction("GetInputManager().CancelQuitApplication")]
		[Obsolete("CancelQuit is deprecated. Use the wantsToQuit event instead.")]
		[MethodImpl(4096)]
		public static extern void CancelQuit();

		// Token: 0x0600020F RID: 527
		[FreeFunction("Application_Bindings::Unload")]
		[MethodImpl(4096)]
		public static extern void Unload();

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000210 RID: 528
		[Obsolete("This property is deprecated, please use LoadLevelAsync to detect if a specific scene is currently loading.")]
		public static extern bool isLoadingLevel
		{
			[FreeFunction("GetPreloadManager().IsLoadingOrQueued")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000046F4 File Offset: 0x000028F4
		[Obsolete("Streaming was a Unity Web Player feature, and is removed. This function is deprecated and always returns 1.0 for valid level indices.")]
		public static float GetStreamProgressForLevel(int levelIndex)
		{
			bool flag = levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings;
			float num;
			if (flag)
			{
				num = 1f;
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00004728 File Offset: 0x00002928
		[Obsolete("Streaming was a Unity Web Player feature, and is removed. This function is deprecated and always returns 1.0.")]
		public static float GetStreamProgressForLevel(string levelName)
		{
			return 1f;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00004740 File Offset: 0x00002940
		[Obsolete("Streaming was a Unity Web Player feature, and is removed. This property is deprecated and always returns 0.")]
		public static int streamedBytes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00004754 File Offset: 0x00002954
		[EditorBrowsable(1)]
		[Obsolete("Application.webSecurityEnabled is no longer supported, since the Unity Web Player is no longer supported by Unity", true)]
		public static bool webSecurityEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00004768 File Offset: 0x00002968
		public static bool CanStreamedLevelBeLoaded(int levelIndex)
		{
			return levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings;
		}

		// Token: 0x06000216 RID: 534
		[FreeFunction("Application_Bindings::CanStreamedLevelBeLoaded")]
		[MethodImpl(4096)]
		public static extern bool CanStreamedLevelBeLoaded(string levelName);

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000217 RID: 535
		public static extern bool isPlaying
		{
			[FreeFunction("IsWorldPlaying")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000218 RID: 536
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern bool IsPlaying(Object obj);

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000219 RID: 537
		public static extern bool isFocused
		{
			[FreeFunction("IsPlayerFocused")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600021A RID: 538
		[FreeFunction("GetBuildSettings().GetBuildTags")]
		[MethodImpl(4096)]
		public static extern string[] GetBuildTags();

		// Token: 0x0600021B RID: 539
		[FreeFunction("GetBuildSettings().SetBuildTags")]
		[MethodImpl(4096)]
		public static extern void SetBuildTags(string[] buildTags);

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600021C RID: 540
		public static extern string buildGUID
		{
			[FreeFunction("Application_Bindings::GetBuildGUID")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600021D RID: 541
		// (set) Token: 0x0600021E RID: 542
		public static extern bool runInBackground
		{
			[FreeFunction("GetPlayerSettingsRunInBackground")]
			[MethodImpl(4096)]
			get;
			[FreeFunction("SetPlayerSettingsRunInBackground")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600021F RID: 543
		[FreeFunction("GetBuildSettings().GetHasPROVersion")]
		[MethodImpl(4096)]
		public static extern bool HasProLicense();

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000220 RID: 544
		public static extern bool isBatchMode
		{
			[FreeFunction("::IsBatchmode")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000221 RID: 545
		internal static extern bool isTestRun
		{
			[FreeFunction("::IsTestRun")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000222 RID: 546
		internal static extern bool isHumanControllingUs
		{
			[FreeFunction("::IsHumanControllingUs")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000223 RID: 547
		[FreeFunction("HasARGV")]
		[MethodImpl(4096)]
		internal static extern bool HasARGV(string name);

		// Token: 0x06000224 RID: 548
		[FreeFunction("GetFirstValueForARGV")]
		[MethodImpl(4096)]
		internal static extern string GetValueForARGV(string name);

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000225 RID: 549
		public static extern string dataPath
		{
			[FreeFunction("GetAppDataPath")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000226 RID: 550
		public static extern string streamingAssetsPath
		{
			[FreeFunction("GetStreamingAssetsPath", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000227 RID: 551
		[SecurityCritical]
		public static extern string persistentDataPath
		{
			[FreeFunction("GetPersistentDataPathApplicationSpecific")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000228 RID: 552
		public static extern string temporaryCachePath
		{
			[FreeFunction("GetTemporaryCachePathApplicationSpecific")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000229 RID: 553
		public static extern string absoluteURL
		{
			[FreeFunction("GetPlayerSettings().GetAbsoluteURL")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000478C File Offset: 0x0000298C
		[Obsolete("Application.ExternalEval is deprecated. See https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting.html for alternatives.")]
		public static void ExternalEval(string script)
		{
			bool flag = script.Length > 0 && script.get_Chars(script.Length - 1) != ';';
			if (flag)
			{
				script += ";";
			}
			Application.Internal_ExternalCall(script);
		}

		// Token: 0x0600022B RID: 555
		[FreeFunction("Application_Bindings::ExternalCall")]
		[MethodImpl(4096)]
		private static extern void Internal_ExternalCall(string script);

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600022C RID: 556
		public static extern string unityVersion
		{
			[FreeFunction("Application_Bindings::GetUnityVersion", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600022D RID: 557
		public static extern string version
		{
			[FreeFunction("GetApplicationInfo().GetVersion")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600022E RID: 558
		public static extern string installerName
		{
			[FreeFunction("GetApplicationInfo().GetInstallerName")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600022F RID: 559
		public static extern string identifier
		{
			[FreeFunction("GetApplicationInfo().GetApplicationIdentifier")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000230 RID: 560
		public static extern ApplicationInstallMode installMode
		{
			[FreeFunction("GetApplicationInfo().GetInstallMode")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000231 RID: 561
		public static extern ApplicationSandboxType sandboxType
		{
			[FreeFunction("GetApplicationInfo().GetSandboxType")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000232 RID: 562
		public static extern string productName
		{
			[FreeFunction("GetPlayerSettings().GetProductName")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000233 RID: 563
		public static extern string companyName
		{
			[FreeFunction("GetPlayerSettings().GetCompanyName")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000234 RID: 564
		public static extern string cloudProjectId
		{
			[FreeFunction("GetPlayerSettings().GetCloudProjectId")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000235 RID: 565
		[FreeFunction("GetAdsIdHandler().RequestAdsIdAsync")]
		[MethodImpl(4096)]
		public static extern bool RequestAdvertisingIdentifierAsync(Application.AdvertisingIdentifierCallback delegateMethod);

		// Token: 0x06000236 RID: 566
		[FreeFunction("OpenURL")]
		[MethodImpl(4096)]
		public static extern void OpenURL(string url);

		// Token: 0x06000237 RID: 567 RVA: 0x000047D3 File Offset: 0x000029D3
		[Obsolete("Use UnityEngine.Diagnostics.Utils.ForceCrash")]
		public static void ForceCrash(int mode)
		{
			Utils.ForceCrash((ForcedCrashCategory)mode);
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000238 RID: 568
		// (set) Token: 0x06000239 RID: 569
		public static extern int targetFrameRate
		{
			[FreeFunction("GetTargetFrameRate")]
			[MethodImpl(4096)]
			get;
			[FreeFunction("SetTargetFrameRate")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600023A RID: 570
		[FreeFunction("Application_Bindings::SetLogCallbackDefined")]
		[MethodImpl(4096)]
		private static extern void SetLogCallbackDefined(bool defined);

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600023B RID: 571
		// (set) Token: 0x0600023C RID: 572
		[Obsolete("Use SetStackTraceLogType/GetStackTraceLogType instead")]
		public static extern StackTraceLogType stackTraceLogType
		{
			[FreeFunction("Application_Bindings::GetStackTraceLogType")]
			[MethodImpl(4096)]
			get;
			[FreeFunction("Application_Bindings::SetStackTraceLogType")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600023D RID: 573
		[FreeFunction("GetStackTraceLogType")]
		[MethodImpl(4096)]
		public static extern StackTraceLogType GetStackTraceLogType(LogType logType);

		// Token: 0x0600023E RID: 574
		[FreeFunction("SetStackTraceLogType")]
		[MethodImpl(4096)]
		public static extern void SetStackTraceLogType(LogType logType, StackTraceLogType stackTraceType);

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600023F RID: 575
		public static extern string consoleLogPath
		{
			[FreeFunction("GetConsoleLogPath")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000240 RID: 576
		// (set) Token: 0x06000241 RID: 577
		public static extern ThreadPriority backgroundLoadingPriority
		{
			[FreeFunction("GetPreloadManager().GetThreadPriority")]
			[MethodImpl(4096)]
			get;
			[FreeFunction("GetPreloadManager().SetThreadPriority")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000242 RID: 578
		public static extern bool genuine
		{
			[FreeFunction("IsApplicationGenuine")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000243 RID: 579
		public static extern bool genuineCheckAvailable
		{
			[FreeFunction("IsApplicationGenuineAvailable")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000244 RID: 580
		[FreeFunction("Application_Bindings::RequestUserAuthorization")]
		[MethodImpl(4096)]
		public static extern AsyncOperation RequestUserAuthorization(UserAuthorization mode);

		// Token: 0x06000245 RID: 581
		[FreeFunction("Application_Bindings::HasUserAuthorization")]
		[MethodImpl(4096)]
		public static extern bool HasUserAuthorization(UserAuthorization mode);

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000246 RID: 582
		internal static extern bool submitAnalytics
		{
			[FreeFunction("GetPlayerSettings().GetSubmitAnalytics")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000047E0 File Offset: 0x000029E0
		[Obsolete("This property is deprecated, please use SplashScreen.isFinished instead")]
		public static bool isShowingSplashScreen
		{
			get
			{
				return !SplashScreen.isFinished;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000248 RID: 584
		public static extern RuntimePlatform platform
		{
			[FreeFunction("systeminfo::GetRuntimePlatform", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000249 RID: 585 RVA: 0x000047FC File Offset: 0x000029FC
		public static bool isMobilePlatform
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.Android || (platform - RuntimePlatform.MetroPlayerX86 <= 2 && SystemInfo.deviceType == DeviceType.Handheld);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000483C File Offset: 0x00002A3C
		public static bool isConsolePlatform
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return platform == RuntimePlatform.PS4 || platform == RuntimePlatform.XboxOne;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600024B RID: 587
		public static extern SystemLanguage systemLanguage
		{
			[FreeFunction("(SystemLanguage)systeminfo::GetSystemLanguage")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600024C RID: 588
		public static extern NetworkReachability internetReachability
		{
			[FreeFunction("GetInternetReachability")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600024D RID: 589 RVA: 0x00004864 File Offset: 0x00002A64
		// (remove) Token: 0x0600024E RID: 590 RVA: 0x00004898 File Offset: 0x00002A98
		[field: DebuggerBrowsable(0)]
		public static event Application.LowMemoryCallback lowMemory;

		// Token: 0x0600024F RID: 591 RVA: 0x000048CC File Offset: 0x00002ACC
		[RequiredByNativeCode]
		internal static void CallLowMemory()
		{
			Application.LowMemoryCallback lowMemoryCallback = Application.lowMemory;
			bool flag = lowMemoryCallback != null;
			if (flag)
			{
				lowMemoryCallback();
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000250 RID: 592 RVA: 0x000048EF File Offset: 0x00002AEF
		// (remove) Token: 0x06000251 RID: 593 RVA: 0x0000490E File Offset: 0x00002B0E
		public static event Application.LogCallback logMessageReceived
		{
			add
			{
				Application.s_LogCallbackHandler = (Application.LogCallback)Delegate.Combine(Application.s_LogCallbackHandler, value);
				Application.SetLogCallbackDefined(true);
			}
			remove
			{
				Application.s_LogCallbackHandler = (Application.LogCallback)Delegate.Remove(Application.s_LogCallbackHandler, value);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000252 RID: 594 RVA: 0x00004926 File Offset: 0x00002B26
		// (remove) Token: 0x06000253 RID: 595 RVA: 0x00004945 File Offset: 0x00002B45
		public static event Application.LogCallback logMessageReceivedThreaded
		{
			add
			{
				Application.s_LogCallbackHandlerThreaded = (Application.LogCallback)Delegate.Combine(Application.s_LogCallbackHandlerThreaded, value);
				Application.SetLogCallbackDefined(true);
			}
			remove
			{
				Application.s_LogCallbackHandlerThreaded = (Application.LogCallback)Delegate.Remove(Application.s_LogCallbackHandlerThreaded, value);
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00004960 File Offset: 0x00002B60
		[RequiredByNativeCode]
		private static void CallLogCallback(string logString, string stackTrace, LogType type, bool invokedOnMainThread)
		{
			if (invokedOnMainThread)
			{
				Application.LogCallback logCallback = Application.s_LogCallbackHandler;
				bool flag = logCallback != null;
				if (flag)
				{
					logCallback(logString, stackTrace, type);
				}
			}
			Application.LogCallback logCallback2 = Application.s_LogCallbackHandlerThreaded;
			bool flag2 = logCallback2 != null;
			if (flag2)
			{
				logCallback2(logString, stackTrace, type);
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000049A8 File Offset: 0x00002BA8
		internal static void InvokeOnAdvertisingIdentifierCallback(string advertisingId, bool trackingEnabled)
		{
			bool flag = Application.OnAdvertisingIdentifierCallback != null;
			if (flag)
			{
				Application.OnAdvertisingIdentifierCallback(advertisingId, trackingEnabled, string.Empty);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000049D4 File Offset: 0x00002BD4
		private static string ObjectToJSString(object o)
		{
			bool flag = o == null;
			string text;
			if (flag)
			{
				text = "null";
			}
			else
			{
				bool flag2 = o is string;
				if (flag2)
				{
					string text2 = o.ToString().Replace("\\", "\\\\");
					text2 = text2.Replace("\"", "\\\"");
					text2 = text2.Replace("\n", "\\n");
					text2 = text2.Replace("\r", "\\r");
					text2 = text2.Replace("\0", "");
					text2 = text2.Replace("\u2028", "");
					text2 = text2.Replace("\u2029", "");
					text = "\"" + text2 + "\"";
				}
				else
				{
					bool flag3 = o is int || o is short || o is uint || o is ushort || o is byte;
					if (flag3)
					{
						text = o.ToString();
					}
					else
					{
						bool flag4 = o is float;
						if (flag4)
						{
							NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
							text = ((float)o).ToString(numberFormat);
						}
						else
						{
							bool flag5 = o is double;
							if (flag5)
							{
								NumberFormatInfo numberFormat2 = CultureInfo.InvariantCulture.NumberFormat;
								text = ((double)o).ToString(numberFormat2);
							}
							else
							{
								bool flag6 = o is char;
								if (flag6)
								{
									bool flag7 = (char)o == '"';
									if (flag7)
									{
										text = "\"\\\"\"";
									}
									else
									{
										text = "\"" + o.ToString() + "\"";
									}
								}
								else
								{
									bool flag8 = o is IList;
									if (flag8)
									{
										IList list = (IList)o;
										StringBuilder stringBuilder = new StringBuilder();
										stringBuilder.Append("new Array(");
										int count = list.Count;
										for (int i = 0; i < count; i++)
										{
											bool flag9 = i != 0;
											if (flag9)
											{
												stringBuilder.Append(", ");
											}
											stringBuilder.Append(Application.ObjectToJSString(list[i]));
										}
										stringBuilder.Append(")");
										text = stringBuilder.ToString();
									}
									else
									{
										text = Application.ObjectToJSString(o.ToString());
									}
								}
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00004C2A File Offset: 0x00002E2A
		[Obsolete("Application.ExternalCall is deprecated. See https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting.html for alternatives.")]
		public static void ExternalCall(string functionName, params object[] args)
		{
			Application.Internal_ExternalCall(Application.BuildInvocationForArguments(functionName, args));
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00004C3C File Offset: 0x00002E3C
		private static string BuildInvocationForArguments(string functionName, params object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(functionName);
			stringBuilder.Append('(');
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = i != 0;
				if (flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(Application.ObjectToJSString(args[i]));
			}
			stringBuilder.Append(')');
			stringBuilder.Append(';');
			return stringBuilder.ToString();
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00004CBC File Offset: 0x00002EBC
		[Obsolete("use Application.isEditor instead")]
		public static bool isPlayer
		{
			get
			{
				return !Application.isEditor;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00004CD8 File Offset: 0x00002ED8
		[Obsolete("Use Object.DontDestroyOnLoad instead")]
		public static void DontDestroyOnLoad(Object o)
		{
			bool flag = o != null;
			if (flag)
			{
				Object.DontDestroyOnLoad(o);
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00004CF8 File Offset: 0x00002EF8
		[Obsolete("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead (UnityUpgradable) -> [UnityEngine] UnityEngine.ScreenCapture.CaptureScreenshot(*)", true)]
		public static void CaptureScreenshot(string filename, int superSize)
		{
			throw new NotSupportedException("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead.");
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00004CF8 File Offset: 0x00002EF8
		[Obsolete("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead (UnityUpgradable) -> [UnityEngine] UnityEngine.ScreenCapture.CaptureScreenshot(*)", true)]
		public static void CaptureScreenshot(string filename)
		{
			throw new NotSupportedException("Application.CaptureScreenshot is obsolete. Use ScreenCapture.CaptureScreenshot instead.");
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600025D RID: 605 RVA: 0x00004D05 File Offset: 0x00002F05
		// (remove) Token: 0x0600025E RID: 606 RVA: 0x00004D0F File Offset: 0x00002F0F
		public static event UnityAction onBeforeRender
		{
			add
			{
				BeforeRenderHelper.RegisterCallback(value);
			}
			remove
			{
				BeforeRenderHelper.UnregisterCallback(value);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600025F RID: 607 RVA: 0x00004D1C File Offset: 0x00002F1C
		// (remove) Token: 0x06000260 RID: 608 RVA: 0x00004D50 File Offset: 0x00002F50
		[field: DebuggerBrowsable(0)]
		public static event Action<bool> focusChanged;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000261 RID: 609 RVA: 0x00004D84 File Offset: 0x00002F84
		// (remove) Token: 0x06000262 RID: 610 RVA: 0x00004DB8 File Offset: 0x00002FB8
		[field: DebuggerBrowsable(0)]
		public static event Action<string> deepLinkActivated;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000263 RID: 611 RVA: 0x00004DEC File Offset: 0x00002FEC
		// (remove) Token: 0x06000264 RID: 612 RVA: 0x00004E20 File Offset: 0x00003020
		[field: DebuggerBrowsable(0)]
		public static event Func<bool> wantsToQuit;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000265 RID: 613 RVA: 0x00004E54 File Offset: 0x00003054
		// (remove) Token: 0x06000266 RID: 614 RVA: 0x00004E88 File Offset: 0x00003088
		[field: DebuggerBrowsable(0)]
		public static event Action quitting;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000267 RID: 615 RVA: 0x00004EBC File Offset: 0x000030BC
		// (remove) Token: 0x06000268 RID: 616 RVA: 0x00004EF0 File Offset: 0x000030F0
		[field: DebuggerBrowsable(0)]
		public static event Action unloading;

		// Token: 0x06000269 RID: 617 RVA: 0x00004F24 File Offset: 0x00003124
		[RequiredByNativeCode]
		private static bool Internal_ApplicationWantsToQuit()
		{
			bool flag = Application.wantsToQuit != null;
			if (flag)
			{
				foreach (Func<bool> func in Application.wantsToQuit.GetInvocationList())
				{
					try
					{
						bool flag2 = !func.Invoke();
						if (flag2)
						{
							return false;
						}
					}
					catch (Exception ex)
					{
						Debug.LogException(ex);
					}
				}
			}
			return true;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00004FA4 File Offset: 0x000031A4
		[RequiredByNativeCode]
		private static void Internal_ApplicationQuit()
		{
			bool flag = Application.quitting != null;
			if (flag)
			{
				Application.quitting.Invoke();
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00004FCC File Offset: 0x000031CC
		[RequiredByNativeCode]
		private static void Internal_ApplicationUnload()
		{
			bool flag = Application.unloading != null;
			if (flag)
			{
				Application.unloading.Invoke();
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00004FF1 File Offset: 0x000031F1
		[RequiredByNativeCode]
		internal static void InvokeOnBeforeRender()
		{
			BeforeRenderHelper.Invoke();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00004FFC File Offset: 0x000031FC
		[RequiredByNativeCode]
		internal static void InvokeFocusChanged(bool focus)
		{
			bool flag = Application.focusChanged != null;
			if (flag)
			{
				Application.focusChanged.Invoke(focus);
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00005024 File Offset: 0x00003224
		[RequiredByNativeCode]
		internal static void InvokeDeepLinkActivated(string url)
		{
			bool flag = Application.deepLinkActivated != null;
			if (flag)
			{
				Application.deepLinkActivated.Invoke(url);
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000504A File Offset: 0x0000324A
		[Obsolete("Application.RegisterLogCallback is deprecated. Use Application.logMessageReceived instead.")]
		public static void RegisterLogCallback(Application.LogCallback handler)
		{
			Application.RegisterLogCallback(handler, false);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00005055 File Offset: 0x00003255
		[Obsolete("Application.RegisterLogCallbackThreaded is deprecated. Use Application.logMessageReceivedThreaded instead.")]
		public static void RegisterLogCallbackThreaded(Application.LogCallback handler)
		{
			Application.RegisterLogCallback(handler, true);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00005060 File Offset: 0x00003260
		private static void RegisterLogCallback(Application.LogCallback handler, bool threaded)
		{
			bool flag = Application.s_RegisterLogCallbackDeprecated != null;
			if (flag)
			{
				Application.logMessageReceived -= Application.s_RegisterLogCallbackDeprecated;
				Application.logMessageReceivedThreaded -= Application.s_RegisterLogCallbackDeprecated;
			}
			Application.s_RegisterLogCallbackDeprecated = handler;
			bool flag2 = handler != null;
			if (flag2)
			{
				if (threaded)
				{
					Application.logMessageReceivedThreaded += handler;
				}
				else
				{
					Application.logMessageReceived += handler;
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000272 RID: 626 RVA: 0x000050C0 File Offset: 0x000032C0
		[Obsolete("Use SceneManager.sceneCountInBuildSettings")]
		public static int levelCount
		{
			get
			{
				return SceneManager.sceneCountInBuildSettings;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000273 RID: 627 RVA: 0x000050D8 File Offset: 0x000032D8
		[Obsolete("Use SceneManager to determine what scenes have been loaded")]
		public static int loadedLevel
		{
			get
			{
				return SceneManager.GetActiveScene().buildIndex;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000050F8 File Offset: 0x000032F8
		[Obsolete("Use SceneManager to determine what scenes have been loaded")]
		public static string loadedLevelName
		{
			get
			{
				return SceneManager.GetActiveScene().name;
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00005117 File Offset: 0x00003317
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevel(int index)
		{
			SceneManager.LoadScene(index, LoadSceneMode.Single);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00005122 File Offset: 0x00003322
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevel(string name)
		{
			SceneManager.LoadScene(name, LoadSceneMode.Single);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000512D File Offset: 0x0000332D
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevelAdditive(int index)
		{
			SceneManager.LoadScene(index, LoadSceneMode.Additive);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00005138 File Offset: 0x00003338
		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevelAdditive(string name)
		{
			SceneManager.LoadScene(name, LoadSceneMode.Additive);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00005144 File Offset: 0x00003344
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAsync(int index)
		{
			return SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00005160 File Offset: 0x00003360
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAsync(string levelName)
		{
			return SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000517C File Offset: 0x0000337C
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAdditiveAsync(int index)
		{
			return SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00005198 File Offset: 0x00003398
		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAdditiveAsync(string levelName)
		{
			return SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000051B4 File Offset: 0x000033B4
		[Obsolete("Use SceneManager.UnloadScene")]
		public static bool UnloadLevel(int index)
		{
			return SceneManager.UnloadScene(index);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000051CC File Offset: 0x000033CC
		[Obsolete("Use SceneManager.UnloadScene")]
		public static bool UnloadLevel(string scenePath)
		{
			return SceneManager.UnloadScene(scenePath);
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600027F RID: 639 RVA: 0x000051E4 File Offset: 0x000033E4
		public static bool isEditor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040001C3 RID: 451
		private static Application.LogCallback s_LogCallbackHandler;

		// Token: 0x040001C4 RID: 452
		private static Application.LogCallback s_LogCallbackHandlerThreaded;

		// Token: 0x040001C5 RID: 453
		internal static Application.AdvertisingIdentifierCallback OnAdvertisingIdentifierCallback;

		// Token: 0x040001CB RID: 459
		private static volatile Application.LogCallback s_RegisterLogCallbackDeprecated;

		// Token: 0x0200009D RID: 157
		// (Invoke) Token: 0x06000282 RID: 642
		public delegate void AdvertisingIdentifierCallback(string advertisingId, bool trackingEnabled, string errorMsg);

		// Token: 0x0200009E RID: 158
		// (Invoke) Token: 0x06000286 RID: 646
		public delegate void LowMemoryCallback();

		// Token: 0x0200009F RID: 159
		// (Invoke) Token: 0x0600028A RID: 650
		public delegate void LogCallback(string condition, string stackTrace, LogType type);
	}
}
