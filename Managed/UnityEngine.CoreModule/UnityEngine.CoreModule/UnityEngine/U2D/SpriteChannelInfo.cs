using System;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x0200020C RID: 524
	[VisibleToOtherModules]
	internal struct SpriteChannelInfo
	{
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x00025C04 File Offset: 0x00023E04
		// (set) Token: 0x06001743 RID: 5955 RVA: 0x00025C21 File Offset: 0x00023E21
		public unsafe void* buffer
		{
			get
			{
				return (void*)this.m_Buffer;
			}
			set
			{
				this.m_Buffer = (IntPtr)value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x00025C30 File Offset: 0x00023E30
		// (set) Token: 0x06001745 RID: 5957 RVA: 0x00025C48 File Offset: 0x00023E48
		public int count
		{
			get
			{
				return this.m_Count;
			}
			set
			{
				this.m_Count = value;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x00025C54 File Offset: 0x00023E54
		// (set) Token: 0x06001747 RID: 5959 RVA: 0x00025C6C File Offset: 0x00023E6C
		public int offset
		{
			get
			{
				return this.m_Offset;
			}
			set
			{
				this.m_Offset = value;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x00025C78 File Offset: 0x00023E78
		// (set) Token: 0x06001749 RID: 5961 RVA: 0x00025C90 File Offset: 0x00023E90
		public int stride
		{
			get
			{
				return this.m_Stride;
			}
			set
			{
				this.m_Stride = value;
			}
		}

		// Token: 0x04000735 RID: 1845
		[NativeName("buffer")]
		private IntPtr m_Buffer;

		// Token: 0x04000736 RID: 1846
		[NativeName("count")]
		private int m_Count;

		// Token: 0x04000737 RID: 1847
		[NativeName("offset")]
		private int m_Offset;

		// Token: 0x04000738 RID: 1848
		[NativeName("stride")]
		private int m_Stride;
	}
}
