using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000028 RID: 40
	[RequiredByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	public struct MeshGenerationResult : IEquatable<MeshGenerationResult>
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000046E3 File Offset: 0x000028E3
		public MeshId MeshId { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000046EB File Offset: 0x000028EB
		public Mesh Mesh { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000046F3 File Offset: 0x000028F3
		public MeshCollider MeshCollider { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000046FB File Offset: 0x000028FB
		public MeshGenerationStatus Status { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00004703 File Offset: 0x00002903
		public MeshVertexAttributes Attributes { get; }

		// Token: 0x06000134 RID: 308 RVA: 0x0000470C File Offset: 0x0000290C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is MeshGenerationResult);
			return !flag && this.Equals((MeshGenerationResult)obj);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004740 File Offset: 0x00002940
		public bool Equals(MeshGenerationResult other)
		{
			return this.MeshId.Equals(other.MeshId) && this.Mesh.Equals(other.Mesh) && this.MeshCollider.Equals(other.MeshCollider) && this.Status.Equals(other.Status) && this.Attributes.Equals(other.Attributes);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000047D8 File Offset: 0x000029D8
		public static bool operator ==(MeshGenerationResult lhs, MeshGenerationResult rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000047F4 File Offset: 0x000029F4
		public static bool operator !=(MeshGenerationResult lhs, MeshGenerationResult rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004814 File Offset: 0x00002A14
		public override int GetHashCode()
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(HashCodeHelper.Combine(HashCodeHelper.Combine(this.MeshId.GetHashCode(), this.Mesh.GetHashCode()), this.MeshCollider.GetHashCode()), this.Status.GetHashCode()), this.Attributes.GetHashCode());
		}
	}
}
