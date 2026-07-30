using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000E7 RID: 231
	[UsedByNativeCode]
	[NativeHeader("Runtime/GfxDevice/HDROutputSettings.h")]
	public class HDROutputSettings
	{
		// Token: 0x060007A1 RID: 1953 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
		internal HDROutputSettings()
		{
			this.m_DisplayIndex = 0;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000C0E5 File Offset: 0x0000A2E5
		internal HDROutputSettings(int displayIndex)
		{
			this.m_DisplayIndex = displayIndex;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		public static HDROutputSettings main
		{
			get
			{
				return HDROutputSettings._mainDisplay;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0000C110 File Offset: 0x0000A310
		public bool active
		{
			get
			{
				return HDROutputSettings.GetActive(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0000C130 File Offset: 0x0000A330
		public bool available
		{
			get
			{
				return HDROutputSettings.GetAvailable(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0000C150 File Offset: 0x0000A350
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0000C16D File Offset: 0x0000A36D
		public bool automaticHDRTonemapping
		{
			get
			{
				return HDROutputSettings.GetAutomaticHDRTonemapping(this.m_DisplayIndex);
			}
			set
			{
				HDROutputSettings.SetAutomaticHDRTonemapping(this.m_DisplayIndex, value);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0000C180 File Offset: 0x0000A380
		public ColorGamut displayColorGamut
		{
			get
			{
				return HDROutputSettings.GetDisplayColorGamut(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0000C1A0 File Offset: 0x0000A3A0
		public RenderTextureFormat format
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(HDROutputSettings.GetGraphicsFormat(this.m_DisplayIndex));
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0000C1C4 File Offset: 0x0000A3C4
		public GraphicsFormat graphicsFormat
		{
			get
			{
				return HDROutputSettings.GetGraphicsFormat(this.m_DisplayIndex);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x0000C201 File Offset: 0x0000A401
		public float paperWhiteNits
		{
			get
			{
				return HDROutputSettings.GetPaperWhiteNits(this.m_DisplayIndex);
			}
			set
			{
				HDROutputSettings.SetPaperWhiteNits(this.m_DisplayIndex, value);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0000C214 File Offset: 0x0000A414
		public int maxFullFrameToneMapLuminance
		{
			get
			{
				return HDROutputSettings.GetMaxFullFrameToneMapLuminance(this.m_DisplayIndex);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0000C234 File Offset: 0x0000A434
		public int maxToneMapLuminance
		{
			get
			{
				return HDROutputSettings.GetMaxToneMapLuminance(this.m_DisplayIndex);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0000C254 File Offset: 0x0000A454
		public int minToneMapLuminance
		{
			get
			{
				return HDROutputSettings.GetMinToneMapLuminance(this.m_DisplayIndex);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0000C274 File Offset: 0x0000A474
		public bool HDRModeChangeRequested
		{
			get
			{
				return HDROutputSettings.GetHDRModeChangeRequested(this.m_DisplayIndex);
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0000C291 File Offset: 0x0000A491
		public void RequestHDRModeChange(bool enabled)
		{
			HDROutputSettings.RequestHDRModeChangeInternal(this.m_DisplayIndex, enabled);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
		[Obsolete("SetPaperWhiteInNits is deprecated, please use paperWhiteNits instead.")]
		public static void SetPaperWhiteInNits(float paperWhite)
		{
			int num = 0;
			bool available = HDROutputSettings.GetAvailable(num);
			if (available)
			{
				HDROutputSettings.SetPaperWhiteNits(num, paperWhite);
			}
		}

		// Token: 0x060007B3 RID: 1971
		[FreeFunction("HDROutputSettingsBindings::GetActive", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern bool GetActive(int displayIndex);

		// Token: 0x060007B4 RID: 1972
		[FreeFunction("HDROutputSettingsBindings::GetAvailable", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern bool GetAvailable(int displayIndex);

		// Token: 0x060007B5 RID: 1973
		[FreeFunction("HDROutputSettingsBindings::GetAutomaticHDRTonemapping", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern bool GetAutomaticHDRTonemapping(int displayIndex);

		// Token: 0x060007B6 RID: 1974
		[FreeFunction("HDROutputSettingsBindings::SetAutomaticHDRTonemapping", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void SetAutomaticHDRTonemapping(int displayIndex, bool scripted);

		// Token: 0x060007B7 RID: 1975
		[FreeFunction("HDROutputSettingsBindings::GetDisplayColorGamut", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern ColorGamut GetDisplayColorGamut(int displayIndex);

		// Token: 0x060007B8 RID: 1976
		[FreeFunction("HDROutputSettingsBindings::GetGraphicsFormat", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern GraphicsFormat GetGraphicsFormat(int displayIndex);

		// Token: 0x060007B9 RID: 1977
		[FreeFunction("HDROutputSettingsBindings::GetPaperWhiteNits", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern float GetPaperWhiteNits(int displayIndex);

		// Token: 0x060007BA RID: 1978
		[FreeFunction("HDROutputSettingsBindings::SetPaperWhiteNits", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void SetPaperWhiteNits(int displayIndex, float paperWhite);

		// Token: 0x060007BB RID: 1979
		[FreeFunction("HDROutputSettingsBindings::GetMaxFullFrameToneMapLuminance", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern int GetMaxFullFrameToneMapLuminance(int displayIndex);

		// Token: 0x060007BC RID: 1980
		[FreeFunction("HDROutputSettingsBindings::GetMaxToneMapLuminance", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern int GetMaxToneMapLuminance(int displayIndex);

		// Token: 0x060007BD RID: 1981
		[FreeFunction("HDROutputSettingsBindings::GetMinToneMapLuminance", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern int GetMinToneMapLuminance(int displayIndex);

		// Token: 0x060007BE RID: 1982
		[FreeFunction("HDROutputSettingsBindings::GetHDRModeChangeRequested", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern bool GetHDRModeChangeRequested(int displayIndex);

		// Token: 0x060007BF RID: 1983
		[FreeFunction("HDROutputSettingsBindings::RequestHDRModeChange", HasExplicitThis = false, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void RequestHDRModeChangeInternal(int displayIndex, bool enabled);

		// Token: 0x04000281 RID: 641
		private int m_DisplayIndex;

		// Token: 0x04000282 RID: 642
		public static HDROutputSettings[] displays = new HDROutputSettings[]
		{
			new HDROutputSettings()
		};

		// Token: 0x04000283 RID: 643
		private static HDROutputSettings _mainDisplay = HDROutputSettings.displays[0];
	}
}
