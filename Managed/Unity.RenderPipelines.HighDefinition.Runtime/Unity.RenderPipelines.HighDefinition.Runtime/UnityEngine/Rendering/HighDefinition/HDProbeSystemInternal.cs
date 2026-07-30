using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200007C RID: 124
	internal class HDProbeSystemInternal : IDisposable
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0002BEAD File Offset: 0x0002A0AD
		public IList<HDProbe> bakedProbes
		{
			get
			{
				HDProbeSystemInternal.RemoveDestroyedProbes(this.m_BakedProbes);
				return this.m_BakedProbes;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0002BEC0 File Offset: 0x0002A0C0
		public IList<HDProbe> realtimeViewDependentProbes
		{
			get
			{
				HDProbeSystemInternal.RemoveDestroyedProbes(this.m_RealtimeViewDependentProbes);
				return this.m_RealtimeViewDependentProbes;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0002BED3 File Offset: 0x0002A0D3
		public IList<HDProbe> realtimeViewIndependentProbes
		{
			get
			{
				HDProbeSystemInternal.RemoveDestroyedProbes(this.m_RealtimeViewIndependentProbes);
				return this.m_RealtimeViewIndependentProbes;
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0002BEE6 File Offset: 0x0002A0E6
		public void Dispose()
		{
			this.m_PlanarProbeCullingGroup.Dispose();
			this.m_PlanarProbeCullingGroup = null;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0002BEFC File Offset: 0x0002A0FC
		internal void RegisterProbe(HDProbe probe)
		{
			ProbeSettings settings = probe.settings;
			ProbeSettings.Mode mode = settings.mode;
			ProbeSettings.ProbeType probeType;
			if (mode != ProbeSettings.Mode.Baked)
			{
				if (mode == ProbeSettings.Mode.Realtime)
				{
					probeType = settings.type;
					if (probeType != ProbeSettings.ProbeType.ReflectionProbe)
					{
						if (probeType == ProbeSettings.ProbeType.PlanarProbe && !this.m_RealtimeViewDependentProbes.Contains(probe))
						{
							this.m_RealtimeViewDependentProbes.Add(probe);
						}
					}
					else if (!this.m_RealtimeViewIndependentProbes.Contains(probe))
					{
						this.m_RealtimeViewIndependentProbes.Add(probe);
					}
				}
			}
			else if (!this.m_BakedProbes.Contains(probe))
			{
				this.m_BakedProbes.Add(probe);
			}
			probeType = settings.type;
			if (probeType == ProbeSettings.ProbeType.PlanarProbe && Array.IndexOf<PlanarReflectionProbe>(this.m_PlanarProbes, (PlanarReflectionProbe)probe) == -1)
			{
				if (this.m_PlanarProbeCount == this.m_PlanarProbes.Length)
				{
					Array.Resize<PlanarReflectionProbe>(ref this.m_PlanarProbes, this.m_PlanarProbes.Length * 2);
					Array.Resize<BoundingSphere>(ref this.m_PlanarProbeBounds, this.m_PlanarProbeBounds.Length * 2);
				}
				this.m_PlanarProbes[this.m_PlanarProbeCount] = (PlanarReflectionProbe)probe;
				this.m_PlanarProbeBounds[this.m_PlanarProbeCount] = ((PlanarReflectionProbe)probe).boundingSphere;
				this.m_PlanarProbeCount++;
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0002C01C File Offset: 0x0002A21C
		internal void UnregisterProbe(HDProbe probe)
		{
			this.m_BakedProbes.Remove(probe);
			this.m_RealtimeViewDependentProbes.Remove(probe);
			this.m_RealtimeViewIndependentProbes.Remove(probe);
			HDProbe[] planarProbes = this.m_PlanarProbes;
			int num = Array.IndexOf<HDProbe>(planarProbes, probe);
			if (num != -1)
			{
				if (num < this.m_PlanarProbeCount)
				{
					this.m_PlanarProbes[num] = this.m_PlanarProbes[this.m_PlanarProbeCount - 1];
					this.m_PlanarProbeBounds[num] = this.m_PlanarProbeBounds[this.m_PlanarProbeCount - 1];
					this.m_PlanarProbes[this.m_PlanarProbeCount - 1] = null;
				}
				this.m_PlanarProbeCount--;
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0002C0C0 File Offset: 0x0002A2C0
		internal HDProbeCullState PrepareCull(Camera camera)
		{
			if (this.m_PlanarProbeCullingGroup == null)
			{
				return default(HDProbeCullState);
			}
			HDProbeSystemInternal.RemoveDestroyedProbes(this.m_PlanarProbes, this.m_PlanarProbeBounds, ref this.m_PlanarProbeCount);
			this.m_PlanarProbeCullingGroup.targetCamera = camera;
			this.m_PlanarProbeCullingGroup.SetBoundingSpheres(this.m_PlanarProbeBounds);
			this.m_PlanarProbeCullingGroup.SetBoundingSphereCount(this.m_PlanarProbeCount);
			BoundingSphere[] planarProbeBounds = this.m_PlanarProbeBounds;
			HDProbe[] array = this.m_PlanarProbes;
			Hash128 hash = HDProbeSystemInternal.ComputeStateHashDebug(planarProbeBounds, array, this.m_PlanarProbeCount);
			CullingGroup planarProbeCullingGroup = this.m_PlanarProbeCullingGroup;
			array = this.m_PlanarProbes;
			return new HDProbeCullState(planarProbeCullingGroup, array, hash);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0002C154 File Offset: 0x0002A354
		internal void QueryCullResults(HDProbeCullState state, ref HDProbeCullingResults results)
		{
			BoundingSphere[] planarProbeBounds = this.m_PlanarProbeBounds;
			HDProbe[] planarProbes = this.m_PlanarProbes;
			HDProbeSystemInternal.ComputeStateHashDebug(planarProbeBounds, planarProbes, this.m_PlanarProbeCount);
			results.Reset();
			List<HDProbe> writeableVisibleProbes = results.writeableVisibleProbes;
			Array.Resize<int>(ref this.m_QueryCullResults_Indices, this.Parameters.maxActivePlanarReflectionProbe + this.Parameters.maxActiveReflectionProbe);
			int num = state.cullingGroup.QueryIndices(true, this.m_QueryCullResults_Indices, 0);
			for (int i = 0; i < num; i++)
			{
				writeableVisibleProbes.Add(state.hdProbes[this.m_QueryCullResults_Indices[i]]);
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0002C1E4 File Offset: 0x0002A3E4
		private static void RemoveDestroyedProbes(List<HDProbe> probes)
		{
			for (int i = probes.Count - 1; i >= 0; i--)
			{
				if (probes[i] == null || probes[i].Equals(null))
				{
					probes.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0002C22C File Offset: 0x0002A42C
		private static void RemoveDestroyedProbes(PlanarReflectionProbe[] probes, BoundingSphere[] bounds, ref int count)
		{
			for (int i = 0; i < count; i++)
			{
				if (probes[i] == null || probes[i].Equals(null))
				{
					probes[i] = probes[count - 1];
					bounds[i] = bounds[count - 1];
					probes[count - 1] = null;
					count--;
				}
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0002C284 File Offset: 0x0002A484
		private static Hash128 ComputeStateHashDebug(BoundingSphere[] probeBounds, HDProbe[] probes, int probeCount)
		{
			return default(Hash128);
		}

		// Token: 0x04000528 RID: 1320
		private List<HDProbe> m_BakedProbes = new List<HDProbe>();

		// Token: 0x04000529 RID: 1321
		private List<HDProbe> m_RealtimeViewDependentProbes = new List<HDProbe>();

		// Token: 0x0400052A RID: 1322
		private List<HDProbe> m_RealtimeViewIndependentProbes = new List<HDProbe>();

		// Token: 0x0400052B RID: 1323
		private int m_PlanarProbeCount;

		// Token: 0x0400052C RID: 1324
		private PlanarReflectionProbe[] m_PlanarProbes = new PlanarReflectionProbe[32];

		// Token: 0x0400052D RID: 1325
		private BoundingSphere[] m_PlanarProbeBounds = new BoundingSphere[32];

		// Token: 0x0400052E RID: 1326
		private CullingGroup m_PlanarProbeCullingGroup = new CullingGroup();

		// Token: 0x0400052F RID: 1327
		public ReflectionSystemParameters Parameters;

		// Token: 0x04000530 RID: 1328
		private int[] m_QueryCullResults_Indices;
	}
}
