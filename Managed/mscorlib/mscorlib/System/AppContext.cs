using System;
using System.Collections.Generic;

namespace System
{
	// Token: 0x020000F9 RID: 249
	public static class AppContext
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00030ED8 File Offset: 0x0002F0D8
		public static string BaseDirectory
		{
			get
			{
				return ((string)AppDomain.CurrentDomain.GetData("APP_CONTEXT_BASE_DIRECTORY")) ?? AppDomain.CurrentDomain.BaseDirectory;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00030EFC File Offset: 0x0002F0FC
		public static string TargetFrameworkName
		{
			get
			{
				return AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName;
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00030F0D File Offset: 0x0002F10D
		public static object GetData(string name)
		{
			return AppDomain.CurrentDomain.GetData(name);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00030F1C File Offset: 0x0002F11C
		private static void InitializeDefaultSwitchValues()
		{
			Dictionary<string, AppContext.SwitchValueState> dictionary = AppContext.s_switchMap;
			lock (dictionary)
			{
				if (!AppContext.s_defaultsInitialized)
				{
					AppContextDefaultValues.PopulateDefaultValues();
					AppContext.s_defaultsInitialized = true;
				}
			}
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00030F6C File Offset: 0x0002F16C
		public static bool TryGetSwitch(string switchName, out bool isEnabled)
		{
			if (switchName == null)
			{
				throw new ArgumentNullException("switchName");
			}
			if (switchName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Empty name is not legal."), "switchName");
			}
			if (!AppContext.s_defaultsInitialized)
			{
				AppContext.InitializeDefaultSwitchValues();
			}
			isEnabled = false;
			Dictionary<string, AppContext.SwitchValueState> dictionary = AppContext.s_switchMap;
			lock (dictionary)
			{
				AppContext.SwitchValueState switchValueState;
				if (AppContext.s_switchMap.TryGetValue(switchName, out switchValueState))
				{
					if (switchValueState == AppContext.SwitchValueState.UnknownValue)
					{
						isEnabled = false;
						return false;
					}
					isEnabled = (switchValueState & AppContext.SwitchValueState.HasTrueValue) == AppContext.SwitchValueState.HasTrueValue;
					if ((switchValueState & AppContext.SwitchValueState.HasLookedForOverride) == AppContext.SwitchValueState.HasLookedForOverride)
					{
						return true;
					}
					bool flag2;
					if (AppContextDefaultValues.TryGetSwitchOverride(switchName, out flag2))
					{
						isEnabled = flag2;
					}
					AppContext.s_switchMap[switchName] = (isEnabled ? AppContext.SwitchValueState.HasTrueValue : AppContext.SwitchValueState.HasFalseValue) | AppContext.SwitchValueState.HasLookedForOverride;
					return true;
				}
				else
				{
					bool flag3;
					if (AppContextDefaultValues.TryGetSwitchOverride(switchName, out flag3))
					{
						isEnabled = flag3;
						AppContext.s_switchMap[switchName] = (isEnabled ? AppContext.SwitchValueState.HasTrueValue : AppContext.SwitchValueState.HasFalseValue) | AppContext.SwitchValueState.HasLookedForOverride;
						return true;
					}
					AppContext.s_switchMap[switchName] = AppContext.SwitchValueState.UnknownValue;
				}
			}
			return false;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00031070 File Offset: 0x0002F270
		public static void SetSwitch(string switchName, bool isEnabled)
		{
			if (switchName == null)
			{
				throw new ArgumentNullException("switchName");
			}
			if (switchName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Empty name is not legal."), "switchName");
			}
			if (!AppContext.s_defaultsInitialized)
			{
				AppContext.InitializeDefaultSwitchValues();
			}
			AppContext.SwitchValueState switchValueState = (isEnabled ? AppContext.SwitchValueState.HasTrueValue : AppContext.SwitchValueState.HasFalseValue) | AppContext.SwitchValueState.HasLookedForOverride;
			Dictionary<string, AppContext.SwitchValueState> dictionary = AppContext.s_switchMap;
			lock (dictionary)
			{
				AppContext.s_switchMap[switchName] = switchValueState;
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x000310F8 File Offset: 0x0002F2F8
		internal static void DefineSwitchDefault(string switchName, bool isEnabled)
		{
			AppContext.s_switchMap[switchName] = (isEnabled ? AppContext.SwitchValueState.HasTrueValue : AppContext.SwitchValueState.HasFalseValue);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0003110C File Offset: 0x0002F30C
		internal static void DefineSwitchOverride(string switchName, bool isEnabled)
		{
			AppContext.s_switchMap[switchName] = (isEnabled ? AppContext.SwitchValueState.HasTrueValue : AppContext.SwitchValueState.HasFalseValue) | AppContext.SwitchValueState.HasLookedForOverride;
		}

		// Token: 0x040006FF RID: 1791
		private static readonly Dictionary<string, AppContext.SwitchValueState> s_switchMap = new Dictionary<string, AppContext.SwitchValueState>();

		// Token: 0x04000700 RID: 1792
		private static volatile bool s_defaultsInitialized = false;

		// Token: 0x020000FA RID: 250
		[Flags]
		private enum SwitchValueState
		{
			// Token: 0x04000702 RID: 1794
			HasFalseValue = 1,
			// Token: 0x04000703 RID: 1795
			HasTrueValue = 2,
			// Token: 0x04000704 RID: 1796
			HasLookedForOverride = 4,
			// Token: 0x04000705 RID: 1797
			UnknownValue = 8
		}
	}
}
