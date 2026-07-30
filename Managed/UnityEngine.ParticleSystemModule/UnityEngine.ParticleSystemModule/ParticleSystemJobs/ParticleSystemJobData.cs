using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x0200005E RID: 94
	public struct ParticleSystemJobData
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x000064D2 File Offset: 0x000046D2
		public int count { get; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x000064DA File Offset: 0x000046DA
		public ParticleSystemNativeArray3 positions { get; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x000064E2 File Offset: 0x000046E2
		public ParticleSystemNativeArray3 velocities { get; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x000064EA File Offset: 0x000046EA
		public ParticleSystemNativeArray3 rotations { get; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x000064F2 File Offset: 0x000046F2
		public ParticleSystemNativeArray3 rotationalSpeeds { get; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x000064FA File Offset: 0x000046FA
		public ParticleSystemNativeArray3 sizes { get; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00006502 File Offset: 0x00004702
		public NativeArray<Color32> startColors { get; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0000650A File Offset: 0x0000470A
		public NativeArray<float> aliveTimePercent { get; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00006512 File Offset: 0x00004712
		public NativeArray<float> inverseStartLifetimes { get; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0000651A File Offset: 0x0000471A
		public NativeArray<uint> randomSeeds { get; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00006522 File Offset: 0x00004722
		public ParticleSystemNativeArray4 customData1 { get; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0000652A File Offset: 0x0000472A
		public ParticleSystemNativeArray4 customData2 { get; }

		// Token: 0x0600071B RID: 1819 RVA: 0x00006534 File Offset: 0x00004734
		internal ParticleSystemJobData(ref NativeParticleData nativeData)
		{
			this = default(ParticleSystemJobData);
			this.count = nativeData.count;
			this.positions = this.CreateNativeArray3(ref nativeData.positions, this.count);
			this.velocities = this.CreateNativeArray3(ref nativeData.velocities, this.count);
			this.rotations = this.CreateNativeArray3(ref nativeData.rotations, this.count);
			this.rotationalSpeeds = this.CreateNativeArray3(ref nativeData.rotationalSpeeds, this.count);
			this.sizes = this.CreateNativeArray3(ref nativeData.sizes, this.count);
			this.startColors = this.CreateNativeArray<Color32>(nativeData.startColors, this.count);
			this.aliveTimePercent = this.CreateNativeArray<float>(nativeData.aliveTimePercent, this.count);
			this.inverseStartLifetimes = this.CreateNativeArray<float>(nativeData.inverseStartLifetimes, this.count);
			this.randomSeeds = this.CreateNativeArray<uint>(nativeData.randomSeeds, this.count);
			this.customData1 = this.CreateNativeArray4(ref nativeData.customData1, this.count);
			this.customData2 = this.CreateNativeArray4(ref nativeData.customData2, this.count);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00006660 File Offset: 0x00004860
		internal unsafe NativeArray<T> CreateNativeArray<T>(void* src, int count) where T : struct
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(src, count, Allocator.Invalid);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0000667C File Offset: 0x0000487C
		internal unsafe ParticleSystemNativeArray3 CreateNativeArray3(ref NativeParticleData.Array3 ptrs, int count)
		{
			return new ParticleSystemNativeArray3
			{
				x = this.CreateNativeArray<float>((void*)ptrs.x, count),
				y = this.CreateNativeArray<float>((void*)ptrs.y, count),
				z = this.CreateNativeArray<float>((void*)ptrs.z, count)
			};
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000066D4 File Offset: 0x000048D4
		internal unsafe ParticleSystemNativeArray4 CreateNativeArray4(ref NativeParticleData.Array4 ptrs, int count)
		{
			return new ParticleSystemNativeArray4
			{
				x = this.CreateNativeArray<float>((void*)ptrs.x, count),
				y = this.CreateNativeArray<float>((void*)ptrs.y, count),
				z = this.CreateNativeArray<float>((void*)ptrs.z, count),
				w = this.CreateNativeArray<float>((void*)ptrs.w, count)
			};
		}
	}
}
