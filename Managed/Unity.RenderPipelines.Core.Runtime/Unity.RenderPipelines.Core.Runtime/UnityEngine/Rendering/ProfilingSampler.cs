using System;
using UnityEngine.Profiling;

namespace UnityEngine.Rendering
{
	// Token: 0x0200003B RID: 59
	public class ProfilingSampler
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00007838 File Offset: 0x00005A38
		public static ProfilingSampler Get<TEnum>(TEnum marker) where TEnum : Enum
		{
			TProfilingSampler<TEnum> tprofilingSampler;
			TProfilingSampler<TEnum>.samples.TryGetValue(marker, out tprofilingSampler);
			return tprofilingSampler;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007854 File Offset: 0x00005A54
		public ProfilingSampler(string name)
		{
			this.sampler = CustomSampler.Create("Dummy_" + name, false);
			this.inlineSampler = CustomSampler.Create("Inl_" + name, false);
			this.name = name;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007891 File Offset: 0x00005A91
		internal bool IsValid()
		{
			return this.sampler != null && this.inlineSampler != null;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000078A6 File Offset: 0x00005AA6
		// (set) Token: 0x0600016D RID: 365 RVA: 0x000078AE File Offset: 0x00005AAE
		internal CustomSampler sampler { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000078B7 File Offset: 0x00005AB7
		// (set) Token: 0x0600016F RID: 367 RVA: 0x000078BF File Offset: 0x00005ABF
		internal CustomSampler inlineSampler { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000078C8 File Offset: 0x00005AC8
		// (set) Token: 0x06000171 RID: 369 RVA: 0x000078D0 File Offset: 0x00005AD0
		public string name { get; private set; }

		// Token: 0x1700002E RID: 46
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00002788 File Offset: 0x00000988
		public bool enableRecording
		{
			set
			{
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000078D9 File Offset: 0x00005AD9
		public float gpuElapsedTime
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00005672 File Offset: 0x00003872
		public int gpuSampleCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000078D9 File Offset: 0x00005AD9
		public float cpuElapsedTime
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00005672 File Offset: 0x00003872
		public int cpuSampleCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000078D9 File Offset: 0x00005AD9
		public float inlineCpuElapsedTime
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00005672 File Offset: 0x00003872
		public int inlineCpuSampleCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000268C File Offset: 0x0000088C
		private ProfilingSampler()
		{
		}
	}
}
