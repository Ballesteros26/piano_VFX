using System;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.PlayerLoop
{
	// Token: 0x02000283 RID: 643
	[MovedFrom("UnityEngine.Experimental.PlayerLoop")]
	[RequiredByNativeCode]
	public struct EarlyUpdate
	{
		// Token: 0x02000284 RID: 644
		[RequiredByNativeCode]
		public struct PollPlayerConnection
		{
		}

		// Token: 0x02000285 RID: 645
		[RequiredByNativeCode]
		public struct ProfilerStartFrame
		{
		}

		// Token: 0x02000286 RID: 646
		[RequiredByNativeCode]
		public struct PollHtcsPlayerConnection
		{
		}

		// Token: 0x02000287 RID: 647
		[RequiredByNativeCode]
		public struct GpuTimestamp
		{
		}

		// Token: 0x02000288 RID: 648
		[RequiredByNativeCode]
		public struct AnalyticsCoreStatsUpdate
		{
		}

		// Token: 0x02000289 RID: 649
		[RequiredByNativeCode]
		public struct UnityWebRequestUpdate
		{
		}

		// Token: 0x0200028A RID: 650
		[RequiredByNativeCode]
		public struct UpdateStreamingManager
		{
		}

		// Token: 0x0200028B RID: 651
		[RequiredByNativeCode]
		public struct ExecuteMainThreadJobs
		{
		}

		// Token: 0x0200028C RID: 652
		[RequiredByNativeCode]
		public struct ProcessMouseInWindow
		{
		}

		// Token: 0x0200028D RID: 653
		[RequiredByNativeCode]
		public struct ClearIntermediateRenderers
		{
		}

		// Token: 0x0200028E RID: 654
		[RequiredByNativeCode]
		public struct ClearLines
		{
		}

		// Token: 0x0200028F RID: 655
		[RequiredByNativeCode]
		public struct PresentBeforeUpdate
		{
		}

		// Token: 0x02000290 RID: 656
		[RequiredByNativeCode]
		public struct ResetFrameStatsAfterPresent
		{
		}

		// Token: 0x02000291 RID: 657
		[RequiredByNativeCode]
		public struct UpdateAsyncReadbackManager
		{
		}

		// Token: 0x02000292 RID: 658
		[RequiredByNativeCode]
		public struct UpdateTextureStreamingManager
		{
		}

		// Token: 0x02000293 RID: 659
		[RequiredByNativeCode]
		public struct UpdatePreloading
		{
		}

		// Token: 0x02000294 RID: 660
		[RequiredByNativeCode]
		public struct RendererNotifyInvisible
		{
		}

		// Token: 0x02000295 RID: 661
		[RequiredByNativeCode]
		public struct PlayerCleanupCachedData
		{
		}

		// Token: 0x02000296 RID: 662
		[RequiredByNativeCode]
		public struct UpdateMainGameViewRect
		{
		}

		// Token: 0x02000297 RID: 663
		[RequiredByNativeCode]
		public struct UpdateCanvasRectTransform
		{
		}

		// Token: 0x02000298 RID: 664
		[RequiredByNativeCode]
		public struct UpdateInputManager
		{
		}

		// Token: 0x02000299 RID: 665
		[RequiredByNativeCode]
		public struct ProcessRemoteInput
		{
		}

		// Token: 0x0200029A RID: 666
		[RequiredByNativeCode]
		public struct XRUpdate
		{
		}

		// Token: 0x0200029B RID: 667
		[RequiredByNativeCode]
		public struct ScriptRunDelayedStartupFrame
		{
		}

		// Token: 0x0200029C RID: 668
		[RequiredByNativeCode]
		public struct UpdateKinect
		{
		}

		// Token: 0x0200029D RID: 669
		[RequiredByNativeCode]
		public struct DeliverIosPlatformEvents
		{
		}

		// Token: 0x0200029E RID: 670
		[RequiredByNativeCode]
		public struct DispatchEventQueueEvents
		{
		}

		// Token: 0x0200029F RID: 671
		[RequiredByNativeCode]
		public struct PhysicsResetInterpolatedTransformPosition
		{
		}

		// Token: 0x020002A0 RID: 672
		[RequiredByNativeCode]
		public struct SpriteAtlasManagerUpdate
		{
		}

		// Token: 0x020002A1 RID: 673
		[RequiredByNativeCode]
		[Obsolete("TangoUpdate has been deprecated. Use ARCoreUpdate instead (UnityUpgradable) -> UnityEngine.PlayerLoop.EarlyUpdate/ARCoreUpdate", false)]
		public struct TangoUpdate
		{
		}

		// Token: 0x020002A2 RID: 674
		[RequiredByNativeCode]
		public struct ARCoreUpdate
		{
		}

		// Token: 0x020002A3 RID: 675
		[RequiredByNativeCode]
		public struct PerformanceAnalyticsUpdate
		{
		}
	}
}
