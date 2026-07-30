using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000053 RID: 83
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/ParticleSystem/ParticleSystemRenderer.h")]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemRendererScriptBindings.h")]
	public sealed class ParticleSystemRenderer : Renderer
	{
		// Token: 0x06000681 RID: 1665 RVA: 0x00005ACF File Offset: 0x00003CCF
		[Obsolete("EnableVertexStreams is deprecated.Use SetActiveVertexStreams instead.", false)]
		public void EnableVertexStreams(ParticleSystemVertexStreams streams)
		{
			this.Internal_SetVertexStreams(streams, true);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00005ADB File Offset: 0x00003CDB
		[Obsolete("DisableVertexStreams is deprecated.Use SetActiveVertexStreams instead.", false)]
		public void DisableVertexStreams(ParticleSystemVertexStreams streams)
		{
			this.Internal_SetVertexStreams(streams, false);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00005AE8 File Offset: 0x00003CE8
		[Obsolete("AreVertexStreamsEnabled is deprecated.Use GetActiveVertexStreams instead.", false)]
		public bool AreVertexStreamsEnabled(ParticleSystemVertexStreams streams)
		{
			return this.Internal_GetEnabledVertexStreams(streams) == streams;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00005B04 File Offset: 0x00003D04
		[Obsolete("GetEnabledVertexStreams is deprecated.Use GetActiveVertexStreams instead.", false)]
		public ParticleSystemVertexStreams GetEnabledVertexStreams(ParticleSystemVertexStreams streams)
		{
			return this.Internal_GetEnabledVertexStreams(streams);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00005B20 File Offset: 0x00003D20
		[Obsolete("Internal_SetVertexStreams is deprecated.Use SetActiveVertexStreams instead.", false)]
		internal void Internal_SetVertexStreams(ParticleSystemVertexStreams streams, bool enabled)
		{
			List<ParticleSystemVertexStream> list = new List<ParticleSystemVertexStream>(this.activeVertexStreamsCount);
			this.GetActiveVertexStreams(list);
			if (enabled)
			{
				bool flag = (streams & ParticleSystemVertexStreams.Position) > ParticleSystemVertexStreams.None;
				if (flag)
				{
					bool flag2 = !list.Contains(ParticleSystemVertexStream.Position);
					if (flag2)
					{
						list.Add(ParticleSystemVertexStream.Position);
					}
				}
				bool flag3 = (streams & ParticleSystemVertexStreams.Normal) > ParticleSystemVertexStreams.None;
				if (flag3)
				{
					bool flag4 = !list.Contains(ParticleSystemVertexStream.Normal);
					if (flag4)
					{
						list.Add(ParticleSystemVertexStream.Normal);
					}
				}
				bool flag5 = (streams & ParticleSystemVertexStreams.Tangent) > ParticleSystemVertexStreams.None;
				if (flag5)
				{
					bool flag6 = !list.Contains(ParticleSystemVertexStream.Tangent);
					if (flag6)
					{
						list.Add(ParticleSystemVertexStream.Tangent);
					}
				}
				bool flag7 = (streams & ParticleSystemVertexStreams.Color) > ParticleSystemVertexStreams.None;
				if (flag7)
				{
					bool flag8 = !list.Contains(ParticleSystemVertexStream.Color);
					if (flag8)
					{
						list.Add(ParticleSystemVertexStream.Color);
					}
				}
				bool flag9 = (streams & ParticleSystemVertexStreams.UV) > ParticleSystemVertexStreams.None;
				if (flag9)
				{
					bool flag10 = !list.Contains(ParticleSystemVertexStream.UV);
					if (flag10)
					{
						list.Add(ParticleSystemVertexStream.UV);
					}
				}
				bool flag11 = (streams & ParticleSystemVertexStreams.UV2BlendAndFrame) > ParticleSystemVertexStreams.None;
				if (flag11)
				{
					bool flag12 = !list.Contains(ParticleSystemVertexStream.UV2);
					if (flag12)
					{
						list.Add(ParticleSystemVertexStream.UV2);
						list.Add(ParticleSystemVertexStream.AnimBlend);
						list.Add(ParticleSystemVertexStream.AnimFrame);
					}
				}
				bool flag13 = (streams & ParticleSystemVertexStreams.CenterAndVertexID) > ParticleSystemVertexStreams.None;
				if (flag13)
				{
					bool flag14 = !list.Contains(ParticleSystemVertexStream.Center);
					if (flag14)
					{
						list.Add(ParticleSystemVertexStream.Center);
						list.Add(ParticleSystemVertexStream.VertexID);
					}
				}
				bool flag15 = (streams & ParticleSystemVertexStreams.Size) > ParticleSystemVertexStreams.None;
				if (flag15)
				{
					bool flag16 = !list.Contains(ParticleSystemVertexStream.SizeXYZ);
					if (flag16)
					{
						list.Add(ParticleSystemVertexStream.SizeXYZ);
					}
				}
				bool flag17 = (streams & ParticleSystemVertexStreams.Rotation) > ParticleSystemVertexStreams.None;
				if (flag17)
				{
					bool flag18 = !list.Contains(ParticleSystemVertexStream.Rotation3D);
					if (flag18)
					{
						list.Add(ParticleSystemVertexStream.Rotation3D);
					}
				}
				bool flag19 = (streams & ParticleSystemVertexStreams.Velocity) > ParticleSystemVertexStreams.None;
				if (flag19)
				{
					bool flag20 = !list.Contains(ParticleSystemVertexStream.Velocity);
					if (flag20)
					{
						list.Add(ParticleSystemVertexStream.Velocity);
					}
				}
				bool flag21 = (streams & ParticleSystemVertexStreams.Lifetime) > ParticleSystemVertexStreams.None;
				if (flag21)
				{
					bool flag22 = !list.Contains(ParticleSystemVertexStream.AgePercent);
					if (flag22)
					{
						list.Add(ParticleSystemVertexStream.AgePercent);
						list.Add(ParticleSystemVertexStream.InvStartLifetime);
					}
				}
				bool flag23 = (streams & ParticleSystemVertexStreams.Custom1) > ParticleSystemVertexStreams.None;
				if (flag23)
				{
					bool flag24 = !list.Contains(ParticleSystemVertexStream.Custom1XYZW);
					if (flag24)
					{
						list.Add(ParticleSystemVertexStream.Custom1XYZW);
					}
				}
				bool flag25 = (streams & ParticleSystemVertexStreams.Custom2) > ParticleSystemVertexStreams.None;
				if (flag25)
				{
					bool flag26 = !list.Contains(ParticleSystemVertexStream.Custom2XYZW);
					if (flag26)
					{
						list.Add(ParticleSystemVertexStream.Custom2XYZW);
					}
				}
				bool flag27 = (streams & ParticleSystemVertexStreams.Random) > ParticleSystemVertexStreams.None;
				if (flag27)
				{
					bool flag28 = !list.Contains(ParticleSystemVertexStream.StableRandomXYZ);
					if (flag28)
					{
						list.Add(ParticleSystemVertexStream.StableRandomXYZ);
						list.Add(ParticleSystemVertexStream.VaryingRandomX);
					}
				}
			}
			else
			{
				bool flag29 = (streams & ParticleSystemVertexStreams.Position) > ParticleSystemVertexStreams.None;
				if (flag29)
				{
					list.Remove(ParticleSystemVertexStream.Position);
				}
				bool flag30 = (streams & ParticleSystemVertexStreams.Normal) > ParticleSystemVertexStreams.None;
				if (flag30)
				{
					list.Remove(ParticleSystemVertexStream.Normal);
				}
				bool flag31 = (streams & ParticleSystemVertexStreams.Tangent) > ParticleSystemVertexStreams.None;
				if (flag31)
				{
					list.Remove(ParticleSystemVertexStream.Tangent);
				}
				bool flag32 = (streams & ParticleSystemVertexStreams.Color) > ParticleSystemVertexStreams.None;
				if (flag32)
				{
					list.Remove(ParticleSystemVertexStream.Color);
				}
				bool flag33 = (streams & ParticleSystemVertexStreams.UV) > ParticleSystemVertexStreams.None;
				if (flag33)
				{
					list.Remove(ParticleSystemVertexStream.UV);
				}
				bool flag34 = (streams & ParticleSystemVertexStreams.UV2BlendAndFrame) > ParticleSystemVertexStreams.None;
				if (flag34)
				{
					list.Remove(ParticleSystemVertexStream.UV2);
					list.Remove(ParticleSystemVertexStream.AnimBlend);
					list.Remove(ParticleSystemVertexStream.AnimFrame);
				}
				bool flag35 = (streams & ParticleSystemVertexStreams.CenterAndVertexID) > ParticleSystemVertexStreams.None;
				if (flag35)
				{
					list.Remove(ParticleSystemVertexStream.Center);
					list.Remove(ParticleSystemVertexStream.VertexID);
				}
				bool flag36 = (streams & ParticleSystemVertexStreams.Size) > ParticleSystemVertexStreams.None;
				if (flag36)
				{
					list.Remove(ParticleSystemVertexStream.SizeXYZ);
				}
				bool flag37 = (streams & ParticleSystemVertexStreams.Rotation) > ParticleSystemVertexStreams.None;
				if (flag37)
				{
					list.Remove(ParticleSystemVertexStream.Rotation3D);
				}
				bool flag38 = (streams & ParticleSystemVertexStreams.Velocity) > ParticleSystemVertexStreams.None;
				if (flag38)
				{
					list.Remove(ParticleSystemVertexStream.Velocity);
				}
				bool flag39 = (streams & ParticleSystemVertexStreams.Lifetime) > ParticleSystemVertexStreams.None;
				if (flag39)
				{
					list.Remove(ParticleSystemVertexStream.AgePercent);
					list.Remove(ParticleSystemVertexStream.InvStartLifetime);
				}
				bool flag40 = (streams & ParticleSystemVertexStreams.Custom1) > ParticleSystemVertexStreams.None;
				if (flag40)
				{
					list.Remove(ParticleSystemVertexStream.Custom1XYZW);
				}
				bool flag41 = (streams & ParticleSystemVertexStreams.Custom2) > ParticleSystemVertexStreams.None;
				if (flag41)
				{
					list.Remove(ParticleSystemVertexStream.Custom2XYZW);
				}
				bool flag42 = (streams & ParticleSystemVertexStreams.Random) > ParticleSystemVertexStreams.None;
				if (flag42)
				{
					list.Remove(ParticleSystemVertexStream.StableRandomXYZW);
					list.Remove(ParticleSystemVertexStream.VaryingRandomX);
				}
			}
			this.SetActiveVertexStreams(list);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00005F6C File Offset: 0x0000416C
		[Obsolete("Internal_GetVertexStreams is deprecated.Use GetActiveVertexStreams instead.", false)]
		internal ParticleSystemVertexStreams Internal_GetEnabledVertexStreams(ParticleSystemVertexStreams streams)
		{
			List<ParticleSystemVertexStream> list = new List<ParticleSystemVertexStream>(this.activeVertexStreamsCount);
			this.GetActiveVertexStreams(list);
			ParticleSystemVertexStreams particleSystemVertexStreams = ParticleSystemVertexStreams.None;
			bool flag = list.Contains(ParticleSystemVertexStream.Position);
			if (flag)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Position;
			}
			bool flag2 = list.Contains(ParticleSystemVertexStream.Normal);
			if (flag2)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Normal;
			}
			bool flag3 = list.Contains(ParticleSystemVertexStream.Tangent);
			if (flag3)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Tangent;
			}
			bool flag4 = list.Contains(ParticleSystemVertexStream.Color);
			if (flag4)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Color;
			}
			bool flag5 = list.Contains(ParticleSystemVertexStream.UV);
			if (flag5)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.UV;
			}
			bool flag6 = list.Contains(ParticleSystemVertexStream.UV2);
			if (flag6)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.UV2BlendAndFrame;
			}
			bool flag7 = list.Contains(ParticleSystemVertexStream.Center);
			if (flag7)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.CenterAndVertexID;
			}
			bool flag8 = list.Contains(ParticleSystemVertexStream.SizeXYZ);
			if (flag8)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Size;
			}
			bool flag9 = list.Contains(ParticleSystemVertexStream.Rotation3D);
			if (flag9)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Rotation;
			}
			bool flag10 = list.Contains(ParticleSystemVertexStream.Velocity);
			if (flag10)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Velocity;
			}
			bool flag11 = list.Contains(ParticleSystemVertexStream.AgePercent);
			if (flag11)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Lifetime;
			}
			bool flag12 = list.Contains(ParticleSystemVertexStream.Custom1XYZW);
			if (flag12)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Custom1;
			}
			bool flag13 = list.Contains(ParticleSystemVertexStream.Custom2XYZW);
			if (flag13)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Custom2;
			}
			bool flag14 = list.Contains(ParticleSystemVertexStream.StableRandomXYZ);
			if (flag14)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Random;
			}
			return particleSystemVertexStreams & streams;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000687 RID: 1671
		// (set) Token: 0x06000688 RID: 1672
		[NativeName("RenderAlignment")]
		public extern ParticleSystemRenderSpace alignment
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000689 RID: 1673
		// (set) Token: 0x0600068A RID: 1674
		public extern ParticleSystemRenderMode renderMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600068B RID: 1675
		// (set) Token: 0x0600068C RID: 1676
		public extern ParticleSystemSortMode sortMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600068D RID: 1677
		// (set) Token: 0x0600068E RID: 1678
		public extern float lengthScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600068F RID: 1679
		// (set) Token: 0x06000690 RID: 1680
		public extern float velocityScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000691 RID: 1681
		// (set) Token: 0x06000692 RID: 1682
		public extern float cameraVelocityScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000693 RID: 1683
		// (set) Token: 0x06000694 RID: 1684
		public extern float normalDirection
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000695 RID: 1685
		// (set) Token: 0x06000696 RID: 1686
		public extern float shadowBias
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000697 RID: 1687
		// (set) Token: 0x06000698 RID: 1688
		public extern float sortingFudge
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000699 RID: 1689
		// (set) Token: 0x0600069A RID: 1690
		public extern float minParticleSize
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600069B RID: 1691
		// (set) Token: 0x0600069C RID: 1692
		public extern float maxParticleSize
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x000060AC File Offset: 0x000042AC
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x000060C2 File Offset: 0x000042C2
		public Vector3 pivot
		{
			get
			{
				Vector3 vector;
				this.get_pivot_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_pivot_Injected(ref value);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x000060CC File Offset: 0x000042CC
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x000060E2 File Offset: 0x000042E2
		public Vector3 flip
		{
			get
			{
				Vector3 vector;
				this.get_flip_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_flip_Injected(ref value);
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006A1 RID: 1697
		// (set) Token: 0x060006A2 RID: 1698
		public extern SpriteMaskInteraction maskInteraction
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006A3 RID: 1699
		// (set) Token: 0x060006A4 RID: 1700
		public extern Material trailMaterial
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006A5 RID: 1701
		// (set) Token: 0x060006A6 RID: 1702
		public extern bool enableGPUInstancing
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006A7 RID: 1703
		// (set) Token: 0x060006A8 RID: 1704
		public extern bool allowRoll
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006A9 RID: 1705
		// (set) Token: 0x060006AA RID: 1706
		public extern bool freeformStretching
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006AB RID: 1707
		// (set) Token: 0x060006AC RID: 1708
		public extern bool rotateWithStretchDirection
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060006AD RID: 1709
		// (set) Token: 0x060006AE RID: 1710
		public extern Mesh mesh
		{
			[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetMesh", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetMesh", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060006AF RID: 1711
		[RequiredByNativeCode]
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetMeshes", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern int GetMeshes([NotNull] [Out] Mesh[] meshes);

		// Token: 0x060006B0 RID: 1712
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetMeshes", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetMeshes([NotNull] Mesh[] meshes, int size);

		// Token: 0x060006B1 RID: 1713 RVA: 0x000060EC File Offset: 0x000042EC
		public void SetMeshes(Mesh[] meshes)
		{
			this.SetMeshes(meshes, meshes.Length);
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060006B2 RID: 1714
		public extern int meshCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x000060FA File Offset: 0x000042FA
		public void BakeMesh(Mesh mesh, bool useTransform = false)
		{
			this.BakeMesh(mesh, Camera.main, useTransform);
		}

		// Token: 0x060006B4 RID: 1716
		[MethodImpl(4096)]
		public extern void BakeMesh([NotNull] Mesh mesh, [NotNull] Camera camera, bool useTransform = false);

		// Token: 0x060006B5 RID: 1717 RVA: 0x0000610B File Offset: 0x0000430B
		public void BakeTrailsMesh(Mesh mesh, bool useTransform = false)
		{
			this.BakeTrailsMesh(mesh, Camera.main, useTransform);
		}

		// Token: 0x060006B6 RID: 1718
		[MethodImpl(4096)]
		public extern void BakeTrailsMesh([NotNull] Mesh mesh, [NotNull] Camera camera, bool useTransform = false);

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060006B7 RID: 1719
		public extern int activeVertexStreamsCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060006B8 RID: 1720
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetActiveVertexStreams", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetActiveVertexStreams([NotNull] List<ParticleSystemVertexStream> streams);

		// Token: 0x060006B9 RID: 1721
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetActiveVertexStreams", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetActiveVertexStreams([NotNull] List<ParticleSystemVertexStream> streams);

		// Token: 0x060006BB RID: 1723
		[MethodImpl(4096)]
		private extern void get_pivot_Injected(out Vector3 ret);

		// Token: 0x060006BC RID: 1724
		[MethodImpl(4096)]
		private extern void set_pivot_Injected(ref Vector3 value);

		// Token: 0x060006BD RID: 1725
		[MethodImpl(4096)]
		private extern void get_flip_Injected(out Vector3 ret);

		// Token: 0x060006BE RID: 1726
		[MethodImpl(4096)]
		private extern void set_flip_Injected(ref Vector3 value);
	}
}
