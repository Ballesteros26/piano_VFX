using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000025 RID: 37
	[UsedByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	public struct MeshId : IEquatable<MeshId>
	{
		// Token: 0x06000126 RID: 294 RVA: 0x00004574 File Offset: 0x00002774
		public override string ToString()
		{
			return string.Format("{0}-{1}", this.m_SubId1.ToString("X16"), this.m_SubId2.ToString("X16"));
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000045B0 File Offset: 0x000027B0
		public override int GetHashCode()
		{
			return this.m_SubId1.GetHashCode() ^ this.m_SubId2.GetHashCode();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000045DC File Offset: 0x000027DC
		public override bool Equals(object obj)
		{
			return obj is MeshId && this.Equals((MeshId)obj);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004608 File Offset: 0x00002808
		public bool Equals(MeshId other)
		{
			return this.m_SubId1 == other.m_SubId1 && this.m_SubId2 == other.m_SubId2;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000463C File Offset: 0x0000283C
		public static bool operator ==(MeshId id1, MeshId id2)
		{
			return id1.m_SubId1 == id2.m_SubId1 && id1.m_SubId2 == id2.m_SubId2;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004670 File Offset: 0x00002870
		public static bool operator !=(MeshId id1, MeshId id2)
		{
			return id1.m_SubId1 != id2.m_SubId1 || id1.m_SubId2 != id2.m_SubId2;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000046A4 File Offset: 0x000028A4
		public static MeshId InvalidId
		{
			get
			{
				return MeshId.s_InvalidId;
			}
		}

		// Token: 0x040000DD RID: 221
		private static MeshId s_InvalidId = default(MeshId);

		// Token: 0x040000DE RID: 222
		private ulong m_SubId1;

		// Token: 0x040000DF RID: 223
		private ulong m_SubId2;
	}
}
