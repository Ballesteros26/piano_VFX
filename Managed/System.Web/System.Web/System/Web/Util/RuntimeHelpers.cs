using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Configuration;

namespace System.Web.Util
{
	// Token: 0x02000144 RID: 324
	internal static class RuntimeHelpers
	{
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00029F6F File Offset: 0x0002816F
		// (set) Token: 0x06000EBD RID: 3773 RVA: 0x00029F76 File Offset: 0x00028176
		public static bool CaseInsensitive { get; private set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00029F80 File Offset: 0x00028180
		public static bool DebuggingEnabled
		{
			get
			{
				CompilationSection compilationSection = WebConfigurationManager.GetSection("system.web/compilation") as CompilationSection;
				return compilationSection != null && compilationSection.Debug;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00029FA8 File Offset: 0x000281A8
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x00029FAF File Offset: 0x000281AF
		public static IEqualityComparer<string> StringEqualityComparer { get; private set; }

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00029FB7 File Offset: 0x000281B7
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x00029FBE File Offset: 0x000281BE
		public static IEqualityComparer<string> StringEqualityComparerCulture { get; private set; }

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00029FC6 File Offset: 0x000281C6
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x00029FCD File Offset: 0x000281CD
		public static bool IsUncShare { get; private set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00029FD5 File Offset: 0x000281D5
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x00029FDC File Offset: 0x000281DC
		public static string MonoVersion { get; private set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00029FE4 File Offset: 0x000281E4
		// (set) Token: 0x06000EC8 RID: 3784 RVA: 0x00029FEB File Offset: 0x000281EB
		public static bool RunningOnWindows { get; private set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00029FF3 File Offset: 0x000281F3
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x00029FFA File Offset: 0x000281FA
		public static StringComparison StringComparison { get; private set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0002A002 File Offset: 0x00028202
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x0002A009 File Offset: 0x00028209
		public static StringComparison StringComparisonCulture { get; private set; }

		// Token: 0x06000ECD RID: 3789 RVA: 0x0002A014 File Offset: 0x00028214
		static RuntimeHelpers()
		{
			PlatformID platform = Environment.OSVersion.Platform;
			RuntimeHelpers.RunningOnWindows = platform != (PlatformID)128 && platform != PlatformID.Unix && platform != PlatformID.MacOSX;
			if (RuntimeHelpers.RunningOnWindows)
			{
				RuntimeHelpers.CaseInsensitive = true;
				string text = AppDomain.CurrentDomain.GetData(".appPath") as string;
				if (string.IsNullOrEmpty(text))
				{
					goto IL_00E1;
				}
				try
				{
					RuntimeHelpers.IsUncShare = new Uri(text).IsUnc;
					goto IL_00E1;
				}
				catch
				{
					goto IL_00E1;
				}
			}
			string environmentVariable = Environment.GetEnvironmentVariable("MONO_IOMAP");
			if (!string.IsNullOrEmpty(environmentVariable))
			{
				if (environmentVariable == "all")
				{
					RuntimeHelpers.CaseInsensitive = true;
				}
				else
				{
					foreach (string text2 in environmentVariable.Split(new char[] { ':' }))
					{
						if (text2 == "all" || text2 == "case")
						{
							RuntimeHelpers.CaseInsensitive = true;
							break;
						}
					}
				}
			}
			IL_00E1:
			if (RuntimeHelpers.CaseInsensitive)
			{
				RuntimeHelpers.StringEqualityComparer = StringComparer.OrdinalIgnoreCase;
				RuntimeHelpers.StringEqualityComparerCulture = StringComparer.CurrentCultureIgnoreCase;
				RuntimeHelpers.StringComparison = StringComparison.OrdinalIgnoreCase;
				RuntimeHelpers.StringComparisonCulture = StringComparison.CurrentCultureIgnoreCase;
			}
			else
			{
				RuntimeHelpers.StringEqualityComparer = StringComparer.Ordinal;
				RuntimeHelpers.StringEqualityComparerCulture = StringComparer.CurrentCulture;
				RuntimeHelpers.StringComparison = StringComparison.Ordinal;
				RuntimeHelpers.StringComparisonCulture = StringComparison.CurrentCulture;
			}
			string text3 = null;
			try
			{
				Type type = Type.GetType("Mono.Runtime", false);
				if (type != null)
				{
					MethodInfo method = type.GetMethod("GetDisplayName", BindingFlags.Static | BindingFlags.NonPublic);
					if (method != null)
					{
						text3 = method.Invoke(null, new object[0]) as string;
					}
				}
			}
			catch
			{
			}
			if (text3 == null)
			{
				text3 = Environment.Version.ToString();
			}
			RuntimeHelpers.MonoVersion = text3;
		}
	}
}
