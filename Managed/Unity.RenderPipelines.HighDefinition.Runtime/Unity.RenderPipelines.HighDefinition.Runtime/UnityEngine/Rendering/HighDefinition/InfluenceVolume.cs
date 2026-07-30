using System;
using System.Runtime.CompilerServices;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000082 RID: 130
	[Serializable]
	public class InfluenceVolume : IVersionable<InfluenceVolume.Version>, ISerializationCallbackReceiver
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0002CFDF File Offset: 0x0002B1DF
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0002CFE7 File Offset: 0x0002B1E7
		InfluenceVolume.Version IVersionable<InfluenceVolume.Version>.version
		{
			get
			{
				return this.m_Version;
			}
			set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0002CFF0 File Offset: 0x0002B1F0
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x0002CFF8 File Offset: 0x0002B1F8
		[Obsolete("Only used for data migration purpose. Don't use this field.")]
		internal Vector3 obsoleteOffset
		{
			get
			{
				return this.m_ObsoleteOffset;
			}
			set
			{
				this.m_ObsoleteOffset = value;
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00002646 File Offset: 0x00000846
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0002D004 File Offset: 0x0002B204
		public void OnAfterDeserialize()
		{
			InfluenceVolume.k_Migration.Migrate(this);
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0002D020 File Offset: 0x0002B220
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0002D028 File Offset: 0x0002B228
		public InfluenceShape shape
		{
			get
			{
				return this.m_Shape;
			}
			set
			{
				this.m_Shape = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0002D031 File Offset: 0x0002B231
		public Vector3 extents
		{
			get
			{
				return this.GetExtents(this.shape);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0002D03F File Offset: 0x0002B23F
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x0002D047 File Offset: 0x0002B247
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

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0002D050 File Offset: 0x0002B250
		public Vector3 boxBlendOffset
		{
			get
			{
				return (this.boxBlendDistanceNegative - this.boxBlendDistancePositive) * 0.5f;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0002D06D File Offset: 0x0002B26D
		public Vector3 boxBlendSize
		{
			get
			{
				return -(this.boxBlendDistancePositive + this.boxBlendDistanceNegative);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0002D085 File Offset: 0x0002B285
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x0002D08D File Offset: 0x0002B28D
		public Vector3 boxBlendDistancePositive
		{
			get
			{
				return this.m_BoxBlendDistancePositive;
			}
			set
			{
				this.m_BoxBlendDistancePositive = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0002D096 File Offset: 0x0002B296
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0002D09E File Offset: 0x0002B29E
		public Vector3 boxBlendDistanceNegative
		{
			get
			{
				return this.m_BoxBlendDistanceNegative;
			}
			set
			{
				this.m_BoxBlendDistanceNegative = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0002D0A7 File Offset: 0x0002B2A7
		public Vector3 boxBlendNormalOffset
		{
			get
			{
				return (this.boxBlendNormalDistanceNegative - this.boxBlendNormalDistancePositive) * 0.5f;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0002D0C4 File Offset: 0x0002B2C4
		public Vector3 boxBlendNormalSize
		{
			get
			{
				return -(this.boxBlendNormalDistancePositive + this.boxBlendNormalDistanceNegative);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0002D0DC File Offset: 0x0002B2DC
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0002D0E4 File Offset: 0x0002B2E4
		public Vector3 boxBlendNormalDistancePositive
		{
			get
			{
				return this.m_BoxBlendNormalDistancePositive;
			}
			set
			{
				this.m_BoxBlendNormalDistancePositive = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0002D0ED File Offset: 0x0002B2ED
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0002D0F5 File Offset: 0x0002B2F5
		public Vector3 boxBlendNormalDistanceNegative
		{
			get
			{
				return this.m_BoxBlendNormalDistanceNegative;
			}
			set
			{
				this.m_BoxBlendNormalDistanceNegative = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0002D0FE File Offset: 0x0002B2FE
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0002D106 File Offset: 0x0002B306
		public Vector3 boxSideFadePositive
		{
			get
			{
				return this.m_BoxSideFadePositive;
			}
			set
			{
				this.m_BoxSideFadePositive = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0002D10F File Offset: 0x0002B30F
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x0002D117 File Offset: 0x0002B317
		public Vector3 boxSideFadeNegative
		{
			get
			{
				return this.m_BoxSideFadeNegative;
			}
			set
			{
				this.m_BoxSideFadeNegative = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0002D120 File Offset: 0x0002B320
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0002D128 File Offset: 0x0002B328
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0002D131 File Offset: 0x0002B331
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x0002D139 File Offset: 0x0002B339
		public float sphereBlendDistance
		{
			get
			{
				return this.m_SphereBlendDistance;
			}
			set
			{
				this.m_SphereBlendDistance = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0002D142 File Offset: 0x0002B342
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x0002D14A File Offset: 0x0002B34A
		public float sphereBlendNormalDistance
		{
			get
			{
				return this.m_SphereBlendNormalDistance;
			}
			set
			{
				this.m_SphereBlendNormalDistance = value;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0002D154 File Offset: 0x0002B354
		public Hash128 ComputeHash()
		{
			Hash128 hash = default(Hash128);
			Hash128 hash2 = default(Hash128);
			HashUtilities.ComputeHash128<InfluenceShape>(ref this.m_Shape, ref hash);
			HashUtilities.ComputeHash128<Vector3>(ref this.m_ObsoleteOffset, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			HashUtilities.ComputeHash128<Vector3>(ref this.m_BoxBlendDistanceNegative, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			HashUtilities.ComputeHash128<Vector3>(ref this.m_BoxBlendDistancePositive, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			HashUtilities.ComputeHash128<Vector3>(ref this.m_BoxBlendNormalDistanceNegative, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			HashUtilities.ComputeHash128<Vector3>(ref this.m_BoxBlendNormalDistancePositive, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			HashUtilities.ComputeHash128<Vector3>(ref this.m_BoxSideFadeNegative, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			HashUtilities.ComputeHash128<Vector3>(ref this.m_BoxSideFadePositive, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			HashUtilities.ComputeHash128<Vector3>(ref this.m_BoxSize, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			HashUtilities.ComputeHash128<float>(ref this.m_SphereBlendDistance, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			HashUtilities.ComputeHash128<float>(ref this.m_SphereBlendNormalDistance, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			HashUtilities.ComputeHash128<float>(ref this.m_SphereRadius, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			return hash;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0002D274 File Offset: 0x0002B474
		internal BoundingSphere GetBoundingSphereAt(Vector3 position)
		{
			InfluenceShape shape = this.shape;
			if (shape != InfluenceShape.Box)
			{
				return new BoundingSphere(position, this.sphereRadius);
			}
			float num = Mathf.Max(this.boxSize.x, Mathf.Max(this.boxSize.y, this.boxSize.z));
			return new BoundingSphere(position, num);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0002D2D0 File Offset: 0x0002B4D0
		internal Bounds GetBoundsAt(Vector3 position)
		{
			InfluenceShape shape = this.shape;
			if (shape != InfluenceShape.Box)
			{
				return new Bounds(position, Vector3.one * this.sphereRadius);
			}
			return new Bounds(position, this.boxSize);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0002D30E File Offset: 0x0002B50E
		internal Matrix4x4 GetInfluenceToWorld(Transform transform)
		{
			return Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0002D328 File Offset: 0x0002B528
		internal EnvShapeType envShape
		{
			get
			{
				InfluenceShape shape = this.shape;
				if (shape == InfluenceShape.Box || shape != InfluenceShape.Sphere)
				{
					return EnvShapeType.Box;
				}
				return EnvShapeType.Sphere;
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0002D348 File Offset: 0x0002B548
		internal void CopyTo(InfluenceVolume data)
		{
			data.m_Shape = this.m_Shape;
			data.m_ObsoleteOffset = this.m_ObsoleteOffset;
			data.m_BoxSize = this.m_BoxSize;
			data.m_BoxBlendDistancePositive = this.m_BoxBlendDistancePositive;
			data.m_BoxBlendDistanceNegative = this.m_BoxBlendDistanceNegative;
			data.m_BoxBlendNormalDistancePositive = this.m_BoxBlendNormalDistancePositive;
			data.m_BoxBlendNormalDistanceNegative = this.m_BoxBlendNormalDistanceNegative;
			data.m_BoxSideFadePositive = this.m_BoxSideFadePositive;
			data.m_BoxSideFadeNegative = this.m_BoxSideFadeNegative;
			data.m_SphereRadius = this.m_SphereRadius;
			data.m_SphereBlendDistance = this.m_SphereBlendDistance;
			data.m_SphereBlendNormalDistance = this.m_SphereBlendNormalDistance;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002D3E8 File Offset: 0x0002B5E8
		private Vector3 GetExtents(InfluenceShape shape)
		{
			if (shape == InfluenceShape.Box || shape != InfluenceShape.Sphere)
			{
				return Vector3.Max(Vector3.one * 0.0001f, this.boxSize * 0.5f);
			}
			return Mathf.Max(0.0001f, this.sphereRadius) * Vector3.one;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0002D43C File Offset: 0x0002B63C
		public float ComputeFOVAt(Vector3 viewerPositionWS, Vector3 lookAtPositionWS, Matrix4x4 influenceToWorld)
		{
			InfluenceVolume.<>c__DisplayClass84_0 CS$<>8__locals1;
			CS$<>8__locals1.lookAtPositionWS = lookAtPositionWS;
			CS$<>8__locals1.viewerPositionWS = viewerPositionWS;
			float num = 0f;
			EnvShapeType envShape = this.envShape;
			if (envShape != EnvShapeType.Box)
			{
				if (envShape != EnvShapeType.Sphere)
				{
					num = 90f;
				}
				else
				{
					InfluenceVolume.<ComputeFOVAt>g__GrowFOVToInclude|84_0(ref num, influenceToWorld.MultiplyPoint(new Vector3(this.sphereRadius * 2f, 0f, 0f)), ref CS$<>8__locals1);
					InfluenceVolume.<ComputeFOVAt>g__GrowFOVToInclude|84_0(ref num, influenceToWorld.MultiplyPoint(new Vector3(-this.sphereRadius * 2f, 0f, 0f)), ref CS$<>8__locals1);
					InfluenceVolume.<ComputeFOVAt>g__GrowFOVToInclude|84_0(ref num, influenceToWorld.MultiplyPoint(new Vector3(0f, this.sphereRadius * 2f, 0f)), ref CS$<>8__locals1);
					InfluenceVolume.<ComputeFOVAt>g__GrowFOVToInclude|84_0(ref num, influenceToWorld.MultiplyPoint(new Vector3(0f, -this.sphereRadius * 2f, 0f)), ref CS$<>8__locals1);
					InfluenceVolume.<ComputeFOVAt>g__GrowFOVToInclude|84_0(ref num, influenceToWorld.MultiplyPoint(new Vector3(0f, 0f, this.sphereRadius * 2f)), ref CS$<>8__locals1);
					InfluenceVolume.<ComputeFOVAt>g__GrowFOVToInclude|84_0(ref num, influenceToWorld.MultiplyPoint(new Vector3(0f, 0f, -this.sphereRadius * 2f)), ref CS$<>8__locals1);
				}
			}
			else
			{
				InfluenceVolume.<ComputeFOVAt>g__GrowFOVToInclude|84_0(ref num, influenceToWorld.MultiplyPoint(new Vector3(this.boxSize.x, -this.boxSize.y, -this.boxSize.z)), ref CS$<>8__locals1);
				InfluenceVolume.<ComputeFOVAt>g__GrowFOVToInclude|84_0(ref num, influenceToWorld.MultiplyPoint(new Vector3(this.boxSize.x, -this.boxSize.y, this.boxSize.z)), ref CS$<>8__locals1);
				InfluenceVolume.<ComputeFOVAt>g__GrowFOVToInclude|84_0(ref num, influenceToWorld.MultiplyPoint(new Vector3(this.boxSize.x, this.boxSize.y, -this.boxSize.z)), ref CS$<>8__locals1);
				InfluenceVolume.<ComputeFOVAt>g__GrowFOVToInclude|84_0(ref num, influenceToWorld.MultiplyPoint(new Vector3(this.boxSize.x, this.boxSize.y, this.boxSize.z)), ref CS$<>8__locals1);
				InfluenceVolume.<ComputeFOVAt>g__GrowFOVToInclude|84_0(ref num, influenceToWorld.MultiplyPoint(new Vector3(-this.boxSize.x, -this.boxSize.y, -this.boxSize.z)), ref CS$<>8__locals1);
				InfluenceVolume.<ComputeFOVAt>g__GrowFOVToInclude|84_0(ref num, influenceToWorld.MultiplyPoint(new Vector3(-this.boxSize.x, -this.boxSize.y, this.boxSize.z)), ref CS$<>8__locals1);
				InfluenceVolume.<ComputeFOVAt>g__GrowFOVToInclude|84_0(ref num, influenceToWorld.MultiplyPoint(new Vector3(-this.boxSize.x, this.boxSize.y, -this.boxSize.z)), ref CS$<>8__locals1);
				InfluenceVolume.<ComputeFOVAt>g__GrowFOVToInclude|84_0(ref num, influenceToWorld.MultiplyPoint(new Vector3(-this.boxSize.x, this.boxSize.y, this.boxSize.z)), ref CS$<>8__locals1);
			}
			return num;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0002D7E4 File Offset: 0x0002B9E4
		[CompilerGenerated]
		internal static void <ComputeFOVAt>g__GrowFOVToInclude|84_0(ref float fieldOfView, Vector3 positionWS, ref InfluenceVolume.<>c__DisplayClass84_0 A_2)
		{
			float num = Vector3.Angle(A_2.lookAtPositionWS - A_2.viewerPositionWS, positionWS - A_2.viewerPositionWS);
			fieldOfView = Mathf.Max(num * 2f, fieldOfView);
		}

		// Token: 0x04000559 RID: 1369
		[SerializeField]
		[FormerlySerializedAs("editorAdvancedModeBlendDistancePositive")]
		private Vector3 m_EditorAdvancedModeBlendDistancePositive;

		// Token: 0x0400055A RID: 1370
		[SerializeField]
		[FormerlySerializedAs("editorAdvancedModeBlendDistanceNegative")]
		private Vector3 m_EditorAdvancedModeBlendDistanceNegative;

		// Token: 0x0400055B RID: 1371
		[SerializeField]
		[FormerlySerializedAs("editorSimplifiedModeBlendDistance")]
		private float m_EditorSimplifiedModeBlendDistance;

		// Token: 0x0400055C RID: 1372
		[SerializeField]
		[FormerlySerializedAs("editorAdvancedModeBlendNormalDistancePositive")]
		private Vector3 m_EditorAdvancedModeBlendNormalDistancePositive;

		// Token: 0x0400055D RID: 1373
		[SerializeField]
		[FormerlySerializedAs("editorAdvancedModeBlendNormalDistanceNegative")]
		private Vector3 m_EditorAdvancedModeBlendNormalDistanceNegative;

		// Token: 0x0400055E RID: 1374
		[SerializeField]
		[FormerlySerializedAs("editorSimplifiedModeBlendNormalDistance")]
		private float m_EditorSimplifiedModeBlendNormalDistance;

		// Token: 0x0400055F RID: 1375
		[SerializeField]
		[FormerlySerializedAs("editorAdvancedModeEnabled")]
		private bool m_EditorAdvancedModeEnabled;

		// Token: 0x04000560 RID: 1376
		[SerializeField]
		private Vector3 m_EditorAdvancedModeFaceFadePositive = Vector3.one;

		// Token: 0x04000561 RID: 1377
		[SerializeField]
		private Vector3 m_EditorAdvancedModeFaceFadeNegative = Vector3.one;

		// Token: 0x04000562 RID: 1378
		private static readonly MigrationDescription<InfluenceVolume.Version, InfluenceVolume> k_Migration = MigrationDescription.New<InfluenceVolume.Version, InfluenceVolume>(new MigrationStep<InfluenceVolume.Version, InfluenceVolume>[] { MigrationStep.New<InfluenceVolume.Version, InfluenceVolume>(InfluenceVolume.Version.SphereOffset, delegate(InfluenceVolume i)
		{
			if (i.shape == InfluenceShape.Sphere)
			{
				i.m_ObsoleteOffset = i.m_ObsoleteSphereBaseOffset;
			}
		}) });

		// Token: 0x04000563 RID: 1379
		[SerializeField]
		private InfluenceVolume.Version m_Version = MigrationDescription.LastVersion<InfluenceVolume.Version>();

		// Token: 0x04000564 RID: 1380
		[SerializeField]
		[FormerlySerializedAs("m_SphereBaseOffset")]
		[Obsolete("For Data Migration")]
		private Vector3 m_ObsoleteSphereBaseOffset;

		// Token: 0x04000565 RID: 1381
		[SerializeField]
		[FormerlySerializedAs("m_BoxBaseOffset")]
		[FormerlySerializedAs("m_Offset")]
		private Vector3 m_ObsoleteOffset;

		// Token: 0x04000566 RID: 1382
		[SerializeField]
		[FormerlySerializedAs("m_ShapeType")]
		private InfluenceShape m_Shape;

		// Token: 0x04000567 RID: 1383
		[SerializeField]
		[FormerlySerializedAs("m_BoxBaseSize")]
		private Vector3 m_BoxSize = Vector3.one * 10f;

		// Token: 0x04000568 RID: 1384
		[SerializeField]
		[FormerlySerializedAs("m_BoxInfluencePositiveFade")]
		private Vector3 m_BoxBlendDistancePositive;

		// Token: 0x04000569 RID: 1385
		[SerializeField]
		[FormerlySerializedAs("m_BoxInfluenceNegativeFade")]
		private Vector3 m_BoxBlendDistanceNegative;

		// Token: 0x0400056A RID: 1386
		[SerializeField]
		[FormerlySerializedAs("m_BoxInfluenceNormalPositiveFade")]
		private Vector3 m_BoxBlendNormalDistancePositive;

		// Token: 0x0400056B RID: 1387
		[SerializeField]
		[FormerlySerializedAs("m_BoxInfluenceNormalNegativeFade")]
		private Vector3 m_BoxBlendNormalDistanceNegative;

		// Token: 0x0400056C RID: 1388
		[SerializeField]
		[FormerlySerializedAs("m_BoxPositiveFaceFade")]
		private Vector3 m_BoxSideFadePositive = Vector3.one;

		// Token: 0x0400056D RID: 1389
		[SerializeField]
		[FormerlySerializedAs("m_BoxNegativeFaceFade")]
		private Vector3 m_BoxSideFadeNegative = Vector3.one;

		// Token: 0x0400056E RID: 1390
		[SerializeField]
		[FormerlySerializedAs("m_SphereBaseRadius")]
		private float m_SphereRadius = 3f;

		// Token: 0x0400056F RID: 1391
		[SerializeField]
		[FormerlySerializedAs("m_SphereInfluenceFade")]
		private float m_SphereBlendDistance;

		// Token: 0x04000570 RID: 1392
		[SerializeField]
		[FormerlySerializedAs("m_SphereInfluenceNormalFade")]
		private float m_SphereBlendNormalDistance;

		// Token: 0x02000209 RID: 521
		private enum Version
		{
			// Token: 0x0400138D RID: 5005
			Initial,
			// Token: 0x0400138E RID: 5006
			SphereOffset
		}
	}
}
