using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200034C RID: 844
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/GPUFence.h")]
	public struct GraphicsFence
	{
		// Token: 0x06001B32 RID: 6962 RVA: 0x0002CB50 File Offset: 0x0002AD50
		internal static SynchronisationStageFlags TranslateSynchronizationStageToFlags(SynchronisationStage s)
		{
			return (s == SynchronisationStage.VertexProcessing) ? SynchronisationStageFlags.VertexProcessing : SynchronisationStageFlags.PixelProcessing;
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x0002CB6C File Offset: 0x0002AD6C
		public bool passed
		{
			get
			{
				this.Validate();
				bool flag = !SystemInfo.supportsGraphicsFence || (this.m_FenceType == GraphicsFenceType.AsyncQueueSynchronisation && !SystemInfo.supportsAsyncCompute);
				if (flag)
				{
					throw new NotSupportedException("Cannot determine if this GraphicsFence has passed as this platform has not implemented GraphicsFences.");
				}
				bool flag2 = !this.IsFencePending();
				return flag2 || GraphicsFence.HasFencePassed_Internal(this.m_Ptr);
			}
		}

		// Token: 0x06001B34 RID: 6964
		[FreeFunction("GPUFenceInternals::HasFencePassed_Internal")]
		[MethodImpl(4096)]
		private static extern bool HasFencePassed_Internal(IntPtr fencePtr);

		// Token: 0x06001B35 RID: 6965 RVA: 0x0002CBD0 File Offset: 0x0002ADD0
		internal void InitPostAllocation()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				bool supportsGraphicsFence = SystemInfo.supportsGraphicsFence;
				if (supportsGraphicsFence)
				{
					throw new NullReferenceException("The internal fence ptr is null, this should not be possible for fences that have been correctly constructed using Graphics.CreateGraphicsFence() or CommandBuffer.CreateGraphicsFence()");
				}
				this.m_Version = this.GetPlatformNotSupportedVersion();
			}
			else
			{
				this.m_Version = GraphicsFence.GetVersionNumber(this.m_Ptr);
			}
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0002CC28 File Offset: 0x0002AE28
		internal bool IsFencePending()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			return !flag && this.m_Version == GraphicsFence.GetVersionNumber(this.m_Ptr);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0002CC68 File Offset: 0x0002AE68
		internal void Validate()
		{
			bool flag = this.m_Version == 0 || (SystemInfo.supportsGraphicsFence && this.m_Version == this.GetPlatformNotSupportedVersion());
			if (flag)
			{
				throw new InvalidOperationException("This GraphicsFence object has not been correctly constructed see Graphics.CreateGraphicsFence() or CommandBuffer.CreateGraphicsFence()");
			}
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0002CCA8 File Offset: 0x0002AEA8
		private int GetPlatformNotSupportedVersion()
		{
			return -1;
		}

		// Token: 0x06001B39 RID: 6969
		[FreeFunction("GPUFenceInternals::GetVersionNumber")]
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern int GetVersionNumber(IntPtr fencePtr);

		// Token: 0x040009F8 RID: 2552
		internal IntPtr m_Ptr;

		// Token: 0x040009F9 RID: 2553
		internal int m_Version;

		// Token: 0x040009FA RID: 2554
		internal GraphicsFenceType m_FenceType;
	}
}
