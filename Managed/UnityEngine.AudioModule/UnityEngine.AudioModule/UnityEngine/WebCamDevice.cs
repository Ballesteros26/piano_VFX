using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	[UsedByNativeCode]
	public struct WebCamDevice
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public bool isFrontFacing
		{
			get
			{
				return (this.m_Flags & 1) != 0;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public WebCamKind kind
		{
			get
			{
				return this.m_Kind;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public string depthCameraName
		{
			get
			{
				return (this.m_DepthCameraName == "") ? null : this.m_DepthCameraName;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00002D28 File Offset: 0x00000F28
		public bool isAutoFocusPointSupported
		{
			get
			{
				return (this.m_Flags & 2) != 0;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00002D48 File Offset: 0x00000F48
		public Resolution[] availableResolutions
		{
			get
			{
				return this.m_Resolutions;
			}
		}

		// Token: 0x04000060 RID: 96
		[NativeName("name")]
		internal string m_Name;

		// Token: 0x04000061 RID: 97
		[NativeName("depthCameraName")]
		internal string m_DepthCameraName;

		// Token: 0x04000062 RID: 98
		[NativeName("flags")]
		internal int m_Flags;

		// Token: 0x04000063 RID: 99
		[NativeName("kind")]
		internal WebCamKind m_Kind;

		// Token: 0x04000064 RID: 100
		[NativeName("resolutions")]
		internal Resolution[] m_Resolutions;
	}
}
