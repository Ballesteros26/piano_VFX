using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	// Token: 0x0200020B RID: 523
	[NativeHeader("Runtime/2D/Common/SpriteDataMarshalling.h")]
	[MovedFrom("UnityEngine.Experimental.U2D")]
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	[RequiredByNativeCode]
	[NativeType(CodegenOptions.Custom, "ScriptingSpriteBone")]
	[Serializable]
	public struct SpriteBone
	{
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x00025B50 File Offset: 0x00023D50
		// (set) Token: 0x06001739 RID: 5945 RVA: 0x00025B68 File Offset: 0x00023D68
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x00025B74 File Offset: 0x00023D74
		// (set) Token: 0x0600173B RID: 5947 RVA: 0x00025B8C File Offset: 0x00023D8C
		public Vector3 position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x00025B98 File Offset: 0x00023D98
		// (set) Token: 0x0600173D RID: 5949 RVA: 0x00025BB0 File Offset: 0x00023DB0
		public Quaternion rotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				this.m_Rotation = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x00025BBC File Offset: 0x00023DBC
		// (set) Token: 0x0600173F RID: 5951 RVA: 0x00025BD4 File Offset: 0x00023DD4
		public float length
		{
			get
			{
				return this.m_Length;
			}
			set
			{
				this.m_Length = value;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x00025BE0 File Offset: 0x00023DE0
		// (set) Token: 0x06001741 RID: 5953 RVA: 0x00025BF8 File Offset: 0x00023DF8
		public int parentId
		{
			get
			{
				return this.m_ParentId;
			}
			set
			{
				this.m_ParentId = value;
			}
		}

		// Token: 0x04000730 RID: 1840
		[NativeName("name")]
		[SerializeField]
		private string m_Name;

		// Token: 0x04000731 RID: 1841
		[SerializeField]
		[NativeName("position")]
		private Vector3 m_Position;

		// Token: 0x04000732 RID: 1842
		[NativeName("rotation")]
		[SerializeField]
		private Quaternion m_Rotation;

		// Token: 0x04000733 RID: 1843
		[NativeName("length")]
		[SerializeField]
		private float m_Length;

		// Token: 0x04000734 RID: 1844
		[SerializeField]
		[NativeName("parentId")]
		private int m_ParentId;
	}
}
