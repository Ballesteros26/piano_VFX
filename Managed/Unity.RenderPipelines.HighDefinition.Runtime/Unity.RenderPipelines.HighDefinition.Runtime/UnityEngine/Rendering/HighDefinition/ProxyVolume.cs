using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000083 RID: 131
	[Serializable]
	public class ProxyVolume : IVersionable<ProxyVolume.Version>, ISerializationCallbackReceiver
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0002D824 File Offset: 0x0002BA24
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0002D82C File Offset: 0x0002BA2C
		ProxyVolume.Version IVersionable<ProxyVolume.Version>.version
		{
			get
			{
				return this.m_CSVersion;
			}
			set
			{
				this.m_CSVersion = value;
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00002646 File Offset: 0x00000846
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0002D838 File Offset: 0x0002BA38
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			ProxyVolume.k_Migration.Migrate(this);
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0002D854 File Offset: 0x0002BA54
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0002D85C File Offset: 0x0002BA5C
		public ProxyShape shape
		{
			get
			{
				return this.m_Shape;
			}
			private set
			{
				this.m_Shape = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0002D865 File Offset: 0x0002BA65
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0002D86D File Offset: 0x0002BA6D
		public Vector3 boxSize
		{
			get
			{
				return this.m_BoxSize;
			}
			set
			{
				this.m_BoxSize = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0002D876 File Offset: 0x0002BA76
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0002D87E File Offset: 0x0002BA7E
		public float sphereRadius
		{
			get
			{
				return this.m_SphereRadius;
			}
			set
			{
				this.m_SphereRadius = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0002D887 File Offset: 0x0002BA87
		internal Vector3 extents
		{
			get
			{
				return this.GetExtents(this.shape);
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0002D898 File Offset: 0x0002BA98
		internal Hash128 ComputeHash()
		{
			Hash128 hash = default(Hash128);
			Hash128 hash2 = default(Hash128);
			HashUtilities.ComputeHash128<ProxyShape>(ref this.m_Shape, ref hash);
			HashUtilities.ComputeHash128<Vector3>(ref this.m_BoxSize, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			HashUtilities.ComputeHash128<float>(ref this.m_SphereRadius, ref hash2);
			return hash;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0002D8E6 File Offset: 0x0002BAE6
		private Vector3 GetExtents(ProxyShape shape)
		{
			if (shape == ProxyShape.Box)
			{
				return this.m_BoxSize * 0.5f;
			}
			if (shape != ProxyShape.Sphere)
			{
				return Vector3.one;
			}
			return Vector3.one * this.m_SphereRadius;
		}

		// Token: 0x04000571 RID: 1393
		private static readonly MigrationDescription<ProxyVolume.Version, ProxyVolume> k_Migration = MigrationDescription.New<ProxyVolume.Version, ProxyVolume>(new MigrationStep<ProxyVolume.Version, ProxyVolume>[] { MigrationStep.New<ProxyVolume.Version, ProxyVolume>(ProxyVolume.Version.InfiniteProjectionInShape, delegate(ProxyVolume p)
		{
			if ((p.shape == ProxyShape.Sphere && p.m_ObsoleteSphereInfiniteProjection) || (p.shape == ProxyShape.Box && p.m_ObsoleteBoxInfiniteProjection))
			{
				p.shape = ProxyShape.Infinite;
			}
		}) });

		// Token: 0x04000572 RID: 1394
		[SerializeField]
		private ProxyVolume.Version m_CSVersion = MigrationDescription.LastVersion<ProxyVolume.Version>();

		// Token: 0x04000573 RID: 1395
		[SerializeField]
		[FormerlySerializedAs("m_SphereInfiniteProjection")]
		[Obsolete("For data migration")]
		private bool m_ObsoleteSphereInfiniteProjection;

		// Token: 0x04000574 RID: 1396
		[SerializeField]
		[FormerlySerializedAs("m_BoxInfiniteProjection")]
		[Obsolete("Kept only for compatibility. Use m_Shape instead")]
		private bool m_ObsoleteBoxInfiniteProjection;

		// Token: 0x04000575 RID: 1397
		[SerializeField]
		[FormerlySerializedAs("m_ShapeType")]
		private ProxyShape m_Shape;

		// Token: 0x04000576 RID: 1398
		[SerializeField]
		private Vector3 m_BoxSize = Vector3.one;

		// Token: 0x04000577 RID: 1399
		[SerializeField]
		private float m_SphereRadius = 1f;

		// Token: 0x0200020C RID: 524
		private enum Version
		{
			// Token: 0x04001393 RID: 5011
			Initial,
			// Token: 0x04001394 RID: 5012
			InfiniteProjectionInShape
		}
	}
}
