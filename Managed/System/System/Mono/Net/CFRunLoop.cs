using System;
using System.Runtime.InteropServices;

namespace Mono.Net
{
	// Token: 0x02000057 RID: 87
	internal class CFRunLoop : CFObject
	{
		// Token: 0x0600017B RID: 379
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRunLoopAddSource(IntPtr rl, IntPtr source, IntPtr mode);

		// Token: 0x0600017C RID: 380
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRunLoopRemoveSource(IntPtr rl, IntPtr source, IntPtr mode);

		// Token: 0x0600017D RID: 381
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern int CFRunLoopRunInMode(IntPtr mode, double seconds, bool returnAfterSourceHandled);

		// Token: 0x0600017E RID: 382
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFRunLoopGetCurrent();

		// Token: 0x0600017F RID: 383
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRunLoopStop(IntPtr rl);

		// Token: 0x06000180 RID: 384 RVA: 0x000043D8 File Offset: 0x000025D8
		public CFRunLoop(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00004A54 File Offset: 0x00002C54
		public static CFRunLoop CurrentRunLoop
		{
			get
			{
				return new CFRunLoop(CFRunLoop.CFRunLoopGetCurrent(), false);
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00004A61 File Offset: 0x00002C61
		public void AddSource(IntPtr source, CFString mode)
		{
			CFRunLoop.CFRunLoopAddSource(base.Handle, source, mode.Handle);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00004A75 File Offset: 0x00002C75
		public void RemoveSource(IntPtr source, CFString mode)
		{
			CFRunLoop.CFRunLoopRemoveSource(base.Handle, source, mode.Handle);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00004A89 File Offset: 0x00002C89
		public int RunInMode(CFString mode, double seconds, bool returnAfterSourceHandled)
		{
			return CFRunLoop.CFRunLoopRunInMode(mode.Handle, seconds, returnAfterSourceHandled);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00004A98 File Offset: 0x00002C98
		public void Stop()
		{
			CFRunLoop.CFRunLoopStop(base.Handle);
		}
	}
}
