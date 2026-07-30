using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000E3 RID: 227
	[NativeHeader("Runtime/Graphics/LightmapData.h")]
	[UsedByNativeCode]
	[StructLayout(0)]
	public sealed class LightmapData
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0000BE7C File Offset: 0x0000A07C
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x0000BE94 File Offset: 0x0000A094
		[Obsolete("Use lightmapColor property (UnityUpgradable) -> lightmapColor", false)]
		public Texture2D lightmapLight
		{
			get
			{
				return this.m_Light;
			}
			set
			{
				this.m_Light = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x0000BE94 File Offset: 0x0000A094
		public Texture2D lightmapColor
		{
			get
			{
				return this.m_Light;
			}
			set
			{
				this.m_Light = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x0000BED0 File Offset: 0x0000A0D0
		public Texture2D lightmapDir
		{
			get
			{
				return this.m_Dir;
			}
			set
			{
				this.m_Dir = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		public Texture2D shadowMask
		{
			get
			{
				return this.m_ShadowMask;
			}
			set
			{
				this.m_ShadowMask = value;
			}
		}

		// Token: 0x0400027B RID: 635
		internal Texture2D m_Light;

		// Token: 0x0400027C RID: 636
		internal Texture2D m_Dir;

		// Token: 0x0400027D RID: 637
		internal Texture2D m_ShadowMask;
	}
}
