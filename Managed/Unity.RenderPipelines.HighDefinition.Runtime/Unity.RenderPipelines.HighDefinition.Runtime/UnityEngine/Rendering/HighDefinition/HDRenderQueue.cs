using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000105 RID: 261
	internal static class HDRenderQueue
	{
		// Token: 0x0600085F RID: 2143 RVA: 0x00042525 File Offset: 0x00040725
		public static bool Contains(this RenderQueueRange range, int value)
		{
			return range.lowerBound <= value && value <= range.upperBound;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00042540 File Offset: 0x00040740
		public static int Clamps(this RenderQueueRange range, int value)
		{
			return Math.Max(range.lowerBound, Math.Min(value, range.upperBound));
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0004255B File Offset: 0x0004075B
		public static int ClampsTransparentRangePriority(int value)
		{
			return Math.Max(-100, Math.Min(value, 100));
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0004256C File Offset: 0x0004076C
		public static HDRenderQueue.RenderQueueType GetTypeByRenderQueueValue(int renderQueue)
		{
			if (renderQueue == 1000)
			{
				return HDRenderQueue.RenderQueueType.Background;
			}
			if (HDRenderQueue.k_RenderQueue_AllOpaque.Contains(renderQueue))
			{
				return HDRenderQueue.RenderQueueType.Opaque;
			}
			if (HDRenderQueue.k_RenderQueue_AfterPostProcessOpaque.Contains(renderQueue))
			{
				return HDRenderQueue.RenderQueueType.AfterPostProcessOpaque;
			}
			if (HDRenderQueue.k_RenderQueue_PreRefraction.Contains(renderQueue))
			{
				return HDRenderQueue.RenderQueueType.PreRefraction;
			}
			if (HDRenderQueue.k_RenderQueue_Transparent.Contains(renderQueue))
			{
				return HDRenderQueue.RenderQueueType.Transparent;
			}
			if (HDRenderQueue.k_RenderQueue_LowTransparent.Contains(renderQueue))
			{
				return HDRenderQueue.RenderQueueType.LowTransparent;
			}
			if (HDRenderQueue.k_RenderQueue_AfterPostProcessTransparent.Contains(renderQueue))
			{
				return HDRenderQueue.RenderQueueType.AfterPostprocessTransparent;
			}
			if (renderQueue == 4000)
			{
				return HDRenderQueue.RenderQueueType.Overlay;
			}
			if (renderQueue == 2520)
			{
				return HDRenderQueue.RenderQueueType.RaytracingOpaque;
			}
			if (renderQueue == 3900)
			{
				return HDRenderQueue.RenderQueueType.RaytracingTransparent;
			}
			return HDRenderQueue.RenderQueueType.Unknown;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00042600 File Offset: 0x00040800
		public static int ChangeType(HDRenderQueue.RenderQueueType targetType, int offset = 0, bool alphaTest = false)
		{
			switch (targetType)
			{
			case HDRenderQueue.RenderQueueType.Background:
				return 1000;
			case HDRenderQueue.RenderQueueType.Opaque:
				if (!alphaTest)
				{
					return 2000;
				}
				return 2450;
			case HDRenderQueue.RenderQueueType.AfterPostProcessOpaque:
				if (!alphaTest)
				{
					return 2501;
				}
				return 2510;
			case HDRenderQueue.RenderQueueType.RaytracingOpaque:
				return 2520;
			case HDRenderQueue.RenderQueueType.PreRefraction:
				return 2750 + offset;
			case HDRenderQueue.RenderQueueType.Transparent:
				return 3000 + offset;
			case HDRenderQueue.RenderQueueType.LowTransparent:
				return 3400 + offset;
			case HDRenderQueue.RenderQueueType.AfterPostprocessTransparent:
				return 3700 + offset;
			case HDRenderQueue.RenderQueueType.RaytracingTransparent:
				return 3900;
			case HDRenderQueue.RenderQueueType.Overlay:
				return 4000;
			default:
				throw new ArgumentException("Unknown RenderQueueType, was " + targetType);
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000426A8 File Offset: 0x000408A8
		public static HDRenderQueue.RenderQueueType GetTransparentEquivalent(HDRenderQueue.RenderQueueType type)
		{
			switch (type)
			{
			case HDRenderQueue.RenderQueueType.Background:
			case HDRenderQueue.RenderQueueType.Overlay:
				throw new ArgumentException("Unknow RenderQueueType conversion to transparent equivalent, was " + type);
			case HDRenderQueue.RenderQueueType.Opaque:
				return HDRenderQueue.RenderQueueType.Transparent;
			case HDRenderQueue.RenderQueueType.AfterPostProcessOpaque:
				return HDRenderQueue.RenderQueueType.AfterPostprocessTransparent;
			case HDRenderQueue.RenderQueueType.RaytracingOpaque:
				if ((RenderPipelineManager.currentPipeline as HDRenderPipeline).rayTracingSupported)
				{
					return HDRenderQueue.RenderQueueType.RaytracingTransparent;
				}
				return HDRenderQueue.RenderQueueType.Transparent;
			case HDRenderQueue.RenderQueueType.LowTransparent:
				return HDRenderQueue.RenderQueueType.LowTransparent;
			case HDRenderQueue.RenderQueueType.RaytracingTransparent:
				if (!(RenderPipelineManager.currentPipeline as HDRenderPipeline).rayTracingSupported)
				{
					return HDRenderQueue.RenderQueueType.Transparent;
				}
				return HDRenderQueue.RenderQueueType.RaytracingTransparent;
			}
			return type;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0004272C File Offset: 0x0004092C
		public static HDRenderQueue.RenderQueueType GetOpaqueEquivalent(HDRenderQueue.RenderQueueType type)
		{
			switch (type)
			{
			case HDRenderQueue.RenderQueueType.Background:
			case HDRenderQueue.RenderQueueType.Overlay:
				throw new ArgumentException("Unknow RenderQueueType conversion to opaque equivalent, was " + type);
			case HDRenderQueue.RenderQueueType.RaytracingOpaque:
				if (!(RenderPipelineManager.currentPipeline as HDRenderPipeline).rayTracingSupported)
				{
					return HDRenderQueue.RenderQueueType.Opaque;
				}
				return HDRenderQueue.RenderQueueType.RaytracingOpaque;
			case HDRenderQueue.RenderQueueType.PreRefraction:
			case HDRenderQueue.RenderQueueType.Transparent:
			case HDRenderQueue.RenderQueueType.LowTransparent:
				return HDRenderQueue.RenderQueueType.Opaque;
			case HDRenderQueue.RenderQueueType.AfterPostprocessTransparent:
				return HDRenderQueue.RenderQueueType.AfterPostProcessOpaque;
			case HDRenderQueue.RenderQueueType.RaytracingTransparent:
				if ((RenderPipelineManager.currentPipeline as HDRenderPipeline).rayTracingSupported)
				{
					return HDRenderQueue.RenderQueueType.RaytracingOpaque;
				}
				return HDRenderQueue.RenderQueueType.Opaque;
			}
			return type;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000427AE File Offset: 0x000409AE
		public static HDRenderQueue.OpaqueRenderQueue ConvertToOpaqueRenderQueue(HDRenderQueue.RenderQueueType renderQueue)
		{
			switch (renderQueue)
			{
			case HDRenderQueue.RenderQueueType.Opaque:
				return HDRenderQueue.OpaqueRenderQueue.Default;
			case HDRenderQueue.RenderQueueType.AfterPostProcessOpaque:
				return HDRenderQueue.OpaqueRenderQueue.AfterPostProcessing;
			case HDRenderQueue.RenderQueueType.RaytracingOpaque:
				return HDRenderQueue.OpaqueRenderQueue.Raytracing;
			default:
				throw new ArgumentException("Cannot map to OpaqueRenderQueue, was " + renderQueue);
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000427E1 File Offset: 0x000409E1
		public static HDRenderQueue.RenderQueueType ConvertFromOpaqueRenderQueue(HDRenderQueue.OpaqueRenderQueue opaqueRenderQueue)
		{
			switch (opaqueRenderQueue)
			{
			case HDRenderQueue.OpaqueRenderQueue.Default:
				return HDRenderQueue.RenderQueueType.Opaque;
			case HDRenderQueue.OpaqueRenderQueue.AfterPostProcessing:
				return HDRenderQueue.RenderQueueType.AfterPostProcessOpaque;
			case HDRenderQueue.OpaqueRenderQueue.Raytracing:
				return HDRenderQueue.RenderQueueType.RaytracingOpaque;
			default:
				throw new ArgumentException("Unknown OpaqueRenderQueue, was " + opaqueRenderQueue);
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00042812 File Offset: 0x00040A12
		public static HDRenderQueue.TransparentRenderQueue ConvertToTransparentRenderQueue(HDRenderQueue.RenderQueueType renderQueue)
		{
			switch (renderQueue)
			{
			case HDRenderQueue.RenderQueueType.PreRefraction:
				return HDRenderQueue.TransparentRenderQueue.BeforeRefraction;
			case HDRenderQueue.RenderQueueType.Transparent:
				return HDRenderQueue.TransparentRenderQueue.Default;
			case HDRenderQueue.RenderQueueType.LowTransparent:
				return HDRenderQueue.TransparentRenderQueue.LowResolution;
			case HDRenderQueue.RenderQueueType.AfterPostprocessTransparent:
				return HDRenderQueue.TransparentRenderQueue.AfterPostProcessing;
			case HDRenderQueue.RenderQueueType.RaytracingTransparent:
				return HDRenderQueue.TransparentRenderQueue.Raytracing;
			default:
				throw new ArgumentException("Cannot map to TransparentRenderQueue, was " + renderQueue);
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00042851 File Offset: 0x00040A51
		public static HDRenderQueue.RenderQueueType ConvertFromTransparentRenderQueue(HDRenderQueue.TransparentRenderQueue transparentRenderqueue)
		{
			switch (transparentRenderqueue)
			{
			case HDRenderQueue.TransparentRenderQueue.BeforeRefraction:
				return HDRenderQueue.RenderQueueType.PreRefraction;
			case HDRenderQueue.TransparentRenderQueue.Default:
				return HDRenderQueue.RenderQueueType.Transparent;
			case HDRenderQueue.TransparentRenderQueue.LowResolution:
				return HDRenderQueue.RenderQueueType.LowTransparent;
			case HDRenderQueue.TransparentRenderQueue.AfterPostProcessing:
				return HDRenderQueue.RenderQueueType.AfterPostprocessTransparent;
			case HDRenderQueue.TransparentRenderQueue.Raytracing:
				return HDRenderQueue.RenderQueueType.RaytracingTransparent;
			default:
				throw new ArgumentException("Unknown TransparentRenderQueue, was " + transparentRenderqueue);
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00042890 File Offset: 0x00040A90
		public static string GetShaderTagValue(int index)
		{
			if (HDRenderQueue.k_RenderQueue_AllTransparent.Contains(index) || HDRenderQueue.k_RenderQueue_AfterPostProcessTransparent.Contains(index) || HDRenderQueue.k_RenderQueue_LowTransparent.Contains(index))
			{
				int num = index - 3000;
				return "Transparent" + ((num < 0) ? "" : "+") + num;
			}
			if (index >= 4000)
			{
				return "Overlay+" + (index - 4000);
			}
			if (index >= 2450)
			{
				return "AlphaTest+" + (index - 2450);
			}
			if (index >= 2000)
			{
				return "Geometry+" + (index - 2000);
			}
			int num2 = index - 1000;
			return "Background" + ((num2 < 0) ? "" : "+") + num2;
		}

		// Token: 0x040009D7 RID: 2519
		private const int k_TransparentPriorityQueueRange = 100;

		// Token: 0x040009D8 RID: 2520
		public static readonly RenderQueueRange k_RenderQueue_OpaqueNoAlphaTest = new RenderQueueRange
		{
			lowerBound = 1000,
			upperBound = 2449
		};

		// Token: 0x040009D9 RID: 2521
		public static readonly RenderQueueRange k_RenderQueue_OpaqueAlphaTest = new RenderQueueRange
		{
			lowerBound = 2450,
			upperBound = 2500
		};

		// Token: 0x040009DA RID: 2522
		public static readonly RenderQueueRange k_RenderQueue_AllOpaqueRaytracing = new RenderQueueRange
		{
			lowerBound = 2520,
			upperBound = 2520
		};

		// Token: 0x040009DB RID: 2523
		public static readonly RenderQueueRange k_RenderQueue_AllOpaque = new RenderQueueRange
		{
			lowerBound = 1000,
			upperBound = 2500
		};

		// Token: 0x040009DC RID: 2524
		public static readonly RenderQueueRange k_RenderQueue_AfterPostProcessOpaque = new RenderQueueRange
		{
			lowerBound = 2501,
			upperBound = 2510
		};

		// Token: 0x040009DD RID: 2525
		public static readonly RenderQueueRange k_RenderQueue_PreRefraction = new RenderQueueRange
		{
			lowerBound = 2650,
			upperBound = 2850
		};

		// Token: 0x040009DE RID: 2526
		public static readonly RenderQueueRange k_RenderQueue_Transparent = new RenderQueueRange
		{
			lowerBound = 2900,
			upperBound = 3100
		};

		// Token: 0x040009DF RID: 2527
		public static readonly RenderQueueRange k_RenderQueue_TransparentWithLowRes = new RenderQueueRange
		{
			lowerBound = 2900,
			upperBound = 3500
		};

		// Token: 0x040009E0 RID: 2528
		public static readonly RenderQueueRange k_RenderQueue_LowTransparent = new RenderQueueRange
		{
			lowerBound = 3300,
			upperBound = 3500
		};

		// Token: 0x040009E1 RID: 2529
		public static readonly RenderQueueRange k_RenderQueue_AllTransparent = new RenderQueueRange
		{
			lowerBound = 2650,
			upperBound = 3100
		};

		// Token: 0x040009E2 RID: 2530
		public static readonly RenderQueueRange k_RenderQueue_AllTransparentWithLowRes = new RenderQueueRange
		{
			lowerBound = 2650,
			upperBound = 3500
		};

		// Token: 0x040009E3 RID: 2531
		public static readonly RenderQueueRange k_RenderQueue_AfterPostProcessTransparent = new RenderQueueRange
		{
			lowerBound = 3600,
			upperBound = 3800
		};

		// Token: 0x040009E4 RID: 2532
		public static readonly RenderQueueRange k_RenderQueue_AllTransparentRaytracing = new RenderQueueRange
		{
			lowerBound = 3900,
			upperBound = 3900
		};

		// Token: 0x040009E5 RID: 2533
		public static readonly RenderQueueRange k_RenderQueue_All = new RenderQueueRange
		{
			lowerBound = 0,
			upperBound = 5000
		};

		// Token: 0x0200026B RID: 619
		public enum Priority
		{
			// Token: 0x040015E7 RID: 5607
			Background = 1000,
			// Token: 0x040015E8 RID: 5608
			Opaque = 2000,
			// Token: 0x040015E9 RID: 5609
			OpaqueAlphaTest = 2450,
			// Token: 0x040015EA RID: 5610
			OpaqueLast = 2500,
			// Token: 0x040015EB RID: 5611
			AfterPostprocessOpaque,
			// Token: 0x040015EC RID: 5612
			AfterPostprocessOpaqueAlphaTest = 2510,
			// Token: 0x040015ED RID: 5613
			RaytracingOpaque = 2520,
			// Token: 0x040015EE RID: 5614
			PreRefractionFirst = 2650,
			// Token: 0x040015EF RID: 5615
			PreRefraction = 2750,
			// Token: 0x040015F0 RID: 5616
			PreRefractionLast = 2850,
			// Token: 0x040015F1 RID: 5617
			TransparentFirst = 2900,
			// Token: 0x040015F2 RID: 5618
			Transparent = 3000,
			// Token: 0x040015F3 RID: 5619
			TransparentLast = 3100,
			// Token: 0x040015F4 RID: 5620
			LowTransparentFirst = 3300,
			// Token: 0x040015F5 RID: 5621
			LowTransparent = 3400,
			// Token: 0x040015F6 RID: 5622
			LowTransparentLast = 3500,
			// Token: 0x040015F7 RID: 5623
			AfterPostprocessTransparentFirst = 3600,
			// Token: 0x040015F8 RID: 5624
			AfterPostprocessTransparent = 3700,
			// Token: 0x040015F9 RID: 5625
			AfterPostprocessTransparentLast = 3800,
			// Token: 0x040015FA RID: 5626
			RaytracingTransparent = 3900,
			// Token: 0x040015FB RID: 5627
			Overlay = 4000
		}

		// Token: 0x0200026C RID: 620
		public enum RenderQueueType
		{
			// Token: 0x040015FD RID: 5629
			Background,
			// Token: 0x040015FE RID: 5630
			Opaque,
			// Token: 0x040015FF RID: 5631
			AfterPostProcessOpaque,
			// Token: 0x04001600 RID: 5632
			RaytracingOpaque,
			// Token: 0x04001601 RID: 5633
			PreRefraction,
			// Token: 0x04001602 RID: 5634
			Transparent,
			// Token: 0x04001603 RID: 5635
			LowTransparent,
			// Token: 0x04001604 RID: 5636
			AfterPostprocessTransparent,
			// Token: 0x04001605 RID: 5637
			RaytracingTransparent,
			// Token: 0x04001606 RID: 5638
			Overlay,
			// Token: 0x04001607 RID: 5639
			Unknown
		}

		// Token: 0x0200026D RID: 621
		public enum OpaqueRenderQueue
		{
			// Token: 0x04001609 RID: 5641
			Default,
			// Token: 0x0400160A RID: 5642
			AfterPostProcessing,
			// Token: 0x0400160B RID: 5643
			Raytracing
		}

		// Token: 0x0200026E RID: 622
		public enum TransparentRenderQueue
		{
			// Token: 0x0400160D RID: 5645
			BeforeRefraction,
			// Token: 0x0400160E RID: 5646
			Default,
			// Token: 0x0400160F RID: 5647
			LowResolution,
			// Token: 0x04001610 RID: 5648
			AfterPostProcessing,
			// Token: 0x04001611 RID: 5649
			Raytracing
		}
	}
}
