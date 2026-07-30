using System;

namespace UnityEngine
{
	// Token: 0x020000B5 RID: 181
	public struct CullingGroupEvent
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x000061E0 File Offset: 0x000043E0
		public int index
		{
			get
			{
				return this.m_Index;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x000061F8 File Offset: 0x000043F8
		public bool isVisible
		{
			get
			{
				return (this.m_ThisState & 128) > 0;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000621C File Offset: 0x0000441C
		public bool wasVisible
		{
			get
			{
				return (this.m_PrevState & 128) > 0;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00006240 File Offset: 0x00004440
		public bool hasBecomeVisible
		{
			get
			{
				return this.isVisible && !this.wasVisible;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00006268 File Offset: 0x00004468
		public bool hasBecomeInvisible
		{
			get
			{
				return !this.isVisible && this.wasVisible;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000628C File Offset: 0x0000448C
		public int currentDistance
		{
			get
			{
				return (int)(this.m_ThisState & 127);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x000062A8 File Offset: 0x000044A8
		public int previousDistance
		{
			get
			{
				return (int)(this.m_PrevState & 127);
			}
		}

		// Token: 0x04000217 RID: 535
		private int m_Index;

		// Token: 0x04000218 RID: 536
		private byte m_PrevState;

		// Token: 0x04000219 RID: 537
		private byte m_ThisState;

		// Token: 0x0400021A RID: 538
		private const byte kIsVisibleMask = 128;

		// Token: 0x0400021B RID: 539
		private const byte kDistanceMask = 127;
	}
}
