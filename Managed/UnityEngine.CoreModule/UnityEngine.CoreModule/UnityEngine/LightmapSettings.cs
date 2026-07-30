using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000E4 RID: 228
	[NativeHeader("Runtime/Graphics/LightmapSettings.h")]
	[StaticAccessor("GetLightmapSettings()")]
	public sealed class LightmapSettings : Object
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		private LightmapSettings()
		{
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000784 RID: 1924
		// (set) Token: 0x06000785 RID: 1925
		public static extern LightmapData[] lightmaps
		{
			[FreeFunction]
			[MethodImpl(4096)]
			get;
			[FreeFunction(ThrowsException = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000786 RID: 1926
		// (set) Token: 0x06000787 RID: 1927
		public static extern LightmapsMode lightmapsMode
		{
			[MethodImpl(4096)]
			get;
			[FreeFunction(ThrowsException = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000788 RID: 1928
		// (set) Token: 0x06000789 RID: 1929
		public static extern LightProbes lightProbes
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600078A RID: 1930
		[NativeName("ResetAndAwakeFromLoad")]
		[MethodImpl(4096)]
		internal static extern void Reset();

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0000BF08 File Offset: 0x0000A108
		// (set) Token: 0x0600078C RID: 1932 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Obsolete("Use lightmapsMode instead.", false)]
		public static LightmapsModeLegacy lightmapsModeLegacy
		{
			get
			{
				return LightmapsModeLegacy.Single;
			}
			set
			{
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x0000BF1C File Offset: 0x0000A11C
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Obsolete("Use QualitySettings.desiredColorSpace instead.", false)]
		public static ColorSpace bakedColorSpace
		{
			get
			{
				return QualitySettings.desiredColorSpace;
			}
			set
			{
			}
		}
	}
}
