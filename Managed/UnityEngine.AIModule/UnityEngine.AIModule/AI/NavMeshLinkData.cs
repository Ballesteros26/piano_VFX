using System;

namespace UnityEngine.AI
{
	// Token: 0x02000010 RID: 16
	public struct NavMeshLinkData
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00002748 File Offset: 0x00000948
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00002760 File Offset: 0x00000960
		public Vector3 startPosition
		{
			get
			{
				return this.m_StartPosition;
			}
			set
			{
				this.m_StartPosition = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000276C File Offset: 0x0000096C
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00002784 File Offset: 0x00000984
		public Vector3 endPosition
		{
			get
			{
				return this.m_EndPosition;
			}
			set
			{
				this.m_EndPosition = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002790 File Offset: 0x00000990
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x000027A8 File Offset: 0x000009A8
		public float costModifier
		{
			get
			{
				return this.m_CostModifier;
			}
			set
			{
				this.m_CostModifier = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000027B4 File Offset: 0x000009B4
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x000027CF File Offset: 0x000009CF
		public bool bidirectional
		{
			get
			{
				return this.m_Bidirectional != 0;
			}
			set
			{
				this.m_Bidirectional = (value ? 1 : 0);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000027E0 File Offset: 0x000009E0
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x000027F8 File Offset: 0x000009F8
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00002804 File Offset: 0x00000A04
		// (set) Token: 0x060000DA RID: 218 RVA: 0x0000281C File Offset: 0x00000A1C
		public int area
		{
			get
			{
				return this.m_Area;
			}
			set
			{
				this.m_Area = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00002828 File Offset: 0x00000A28
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00002840 File Offset: 0x00000A40
		public int agentTypeID
		{
			get
			{
				return this.m_AgentTypeID;
			}
			set
			{
				this.m_AgentTypeID = value;
			}
		}

		// Token: 0x04000023 RID: 35
		private Vector3 m_StartPosition;

		// Token: 0x04000024 RID: 36
		private Vector3 m_EndPosition;

		// Token: 0x04000025 RID: 37
		private float m_CostModifier;

		// Token: 0x04000026 RID: 38
		private int m_Bidirectional;

		// Token: 0x04000027 RID: 39
		private float m_Width;

		// Token: 0x04000028 RID: 40
		private int m_Area;

		// Token: 0x04000029 RID: 41
		private int m_AgentTypeID;
	}
}
