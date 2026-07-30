using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x02000006 RID: 6
	[NativeHeader("Modules/VirtualTexturing/Public/VirtualTextureResolver.h")]
	[StructLayout(0)]
	public class Resolver : IDisposable
	{
		// Token: 0x06000012 RID: 18 RVA: 0x0000205E File Offset: 0x0000025E
		public Resolver()
		{
			this.m_Ptr = Resolver.InitNative();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002084 File Offset: 0x00000284
		~Resolver()
		{
			this.Dispose(false);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000020B8 File Offset: 0x000002B8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000020CC File Offset: 0x000002CC
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Flush_Internal();
				Resolver.ReleaseNative(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000016 RID: 22
		[MethodImpl(4096)]
		private static extern IntPtr InitNative();

		// Token: 0x06000017 RID: 23
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void ReleaseNative(IntPtr ptr);

		// Token: 0x06000018 RID: 24
		[MethodImpl(4096)]
		private extern void Flush_Internal();

		// Token: 0x06000019 RID: 25
		[MethodImpl(4096)]
		private extern void Init_Internal(int width, int height);

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000210E File Offset: 0x0000030E
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002116 File Offset: 0x00000316
		public int CurrentWidth { get; private set; } = 0;

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000211F File Offset: 0x0000031F
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002127 File Offset: 0x00000327
		public int CurrentHeight { get; private set; } = 0;

		// Token: 0x0600001E RID: 30 RVA: 0x00002130 File Offset: 0x00000330
		public void UpdateSize(int width, int height)
		{
			bool flag = this.CurrentWidth != width || this.CurrentHeight != height;
			if (flag)
			{
				this.CurrentWidth = width;
				this.CurrentHeight = height;
				this.Flush_Internal();
				this.Init_Internal(this.CurrentWidth, this.CurrentHeight);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002188 File Offset: 0x00000388
		public void Process(CommandBuffer cmd, RenderTargetIdentifier rt)
		{
			this.Process(cmd, rt, 0, this.CurrentWidth, 0, this.CurrentHeight, 0, 0);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000021B0 File Offset: 0x000003B0
		public void Process(CommandBuffer cmd, RenderTargetIdentifier rt, int x, int width, int y, int height, int mip, int slice)
		{
			bool flag = cmd == null;
			if (flag)
			{
				throw new ArgumentNullException("cmd");
			}
			cmd.ProcessVTFeedback(rt, this.m_Ptr, slice, x, width, y, height, mip);
		}

		// Token: 0x04000007 RID: 7
		internal IntPtr m_Ptr;
	}
}
