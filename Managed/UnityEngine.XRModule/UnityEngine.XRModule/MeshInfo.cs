using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200002B RID: 43
	[UsedByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	public struct MeshInfo : IEquatable<MeshInfo>
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000488C File Offset: 0x00002A8C
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00004894 File Offset: 0x00002A94
		public MeshId MeshId { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000489D File Offset: 0x00002A9D
		// (set) Token: 0x0600013C RID: 316 RVA: 0x000048A5 File Offset: 0x00002AA5
		public MeshChangeState ChangeState { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000048AE File Offset: 0x00002AAE
		// (set) Token: 0x0600013E RID: 318 RVA: 0x000048B6 File Offset: 0x00002AB6
		public int PriorityHint { get; set; }

		// Token: 0x0600013F RID: 319 RVA: 0x000048C0 File Offset: 0x00002AC0
		public override bool Equals(object obj)
		{
			bool flag = !(obj is MeshInfo);
			return !flag && this.Equals((MeshInfo)obj);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000048F4 File Offset: 0x00002AF4
		public bool Equals(MeshInfo other)
		{
			return this.MeshId.Equals(other.MeshId) && this.ChangeState.Equals(other.ChangeState) && this.PriorityHint.Equals(other.PriorityHint);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004958 File Offset: 0x00002B58
		public static bool operator ==(MeshInfo lhs, MeshInfo rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004974 File Offset: 0x00002B74
		public static bool operator !=(MeshInfo lhs, MeshInfo rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004994 File Offset: 0x00002B94
		public override int GetHashCode()
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(this.MeshId.GetHashCode(), this.ChangeState.GetHashCode()), this.PriorityHint);
		}
	}
}
