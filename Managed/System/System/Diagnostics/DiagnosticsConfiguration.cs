using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020001AF RID: 431
	internal static class DiagnosticsConfiguration
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0003DC5C File Offset: 0x0003BE5C
		internal static SwitchElementsCollection SwitchSettings
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.Switches;
				}
				return null;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0003DC84 File Offset: 0x0003BE84
		internal static bool AssertUIEnabled
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection == null || systemDiagnosticsSection.Assert == null || systemDiagnosticsSection.Assert.AssertUIEnabled;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0003DCB8 File Offset: 0x0003BEB8
		internal static string ConfigFilePath
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.ElementInformation.Source;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0003DCE8 File Offset: 0x0003BEE8
		internal static string LogFileName
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Assert != null)
				{
					return systemDiagnosticsSection.Assert.LogFileName;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0003DD20 File Offset: 0x0003BF20
		internal static bool AutoFlush
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection != null && systemDiagnosticsSection.Trace != null && systemDiagnosticsSection.Trace.AutoFlush;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0003DD54 File Offset: 0x0003BF54
		internal static bool UseGlobalLock
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection == null || systemDiagnosticsSection.Trace == null || systemDiagnosticsSection.Trace.UseGlobalLock;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0003DD88 File Offset: 0x0003BF88
		internal static int IndentSize
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Trace != null)
				{
					return systemDiagnosticsSection.Trace.IndentSize;
				}
				return 4;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0003DDBC File Offset: 0x0003BFBC
		internal static ListenerElementsCollection SharedListeners
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.SharedListeners;
				}
				return null;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0003DDE4 File Offset: 0x0003BFE4
		internal static SourceElementsCollection Sources
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Sources != null)
				{
					return systemDiagnosticsSection.Sources;
				}
				return null;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0003DE11 File Offset: 0x0003C011
		internal static SystemDiagnosticsSection SystemDiagnosticsSection
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				return DiagnosticsConfiguration.configSection;
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0003DE20 File Offset: 0x0003C020
		private static SystemDiagnosticsSection GetConfigSection()
		{
			object section = PrivilegedConfigurationManager.GetSection("system.diagnostics");
			if (section is SystemDiagnosticsSection)
			{
				return (SystemDiagnosticsSection)section;
			}
			return null;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0003DE48 File Offset: 0x0003C048
		internal static bool IsInitializing()
		{
			return DiagnosticsConfiguration.initState == InitState.Initializing;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0003DE54 File Offset: 0x0003C054
		internal static bool IsInitialized()
		{
			return DiagnosticsConfiguration.initState == InitState.Initialized;
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0003DE60 File Offset: 0x0003C060
		internal static bool CanInitialize()
		{
			return DiagnosticsConfiguration.initState != InitState.Initializing && !ConfigurationManagerInternalFactory.Instance.SetConfigurationSystemInProgress;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0003DE78 File Offset: 0x0003C078
		internal static void Initialize()
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				if (DiagnosticsConfiguration.initState == InitState.NotInitialized && !ConfigurationManagerInternalFactory.Instance.SetConfigurationSystemInProgress)
				{
					DiagnosticsConfiguration.initState = InitState.Initializing;
					try
					{
						DiagnosticsConfiguration.configSection = DiagnosticsConfiguration.GetConfigSection();
					}
					finally
					{
						DiagnosticsConfiguration.initState = InitState.Initialized;
					}
				}
			}
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0003DEEC File Offset: 0x0003C0EC
		internal static void Refresh()
		{
			ConfigurationManager.RefreshSection("system.diagnostics");
			SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
			if (systemDiagnosticsSection != null)
			{
				if (systemDiagnosticsSection.Switches != null)
				{
					foreach (object obj in systemDiagnosticsSection.Switches)
					{
						((SwitchElement)obj).ResetProperties();
					}
				}
				if (systemDiagnosticsSection.SharedListeners != null)
				{
					foreach (object obj2 in systemDiagnosticsSection.SharedListeners)
					{
						((ListenerElement)obj2).ResetProperties();
					}
				}
				if (systemDiagnosticsSection.Sources != null)
				{
					foreach (object obj3 in systemDiagnosticsSection.Sources)
					{
						((SourceElement)obj3).ResetProperties();
					}
				}
			}
			DiagnosticsConfiguration.configSection = null;
			DiagnosticsConfiguration.initState = InitState.NotInitialized;
			DiagnosticsConfiguration.Initialize();
		}

		// Token: 0x04001015 RID: 4117
		private static volatile SystemDiagnosticsSection configSection;

		// Token: 0x04001016 RID: 4118
		private static volatile InitState initState;
	}
}
