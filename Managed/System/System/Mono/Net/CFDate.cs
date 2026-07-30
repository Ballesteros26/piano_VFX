using System;
using System.Runtime.InteropServices;
using ObjCRuntimeInternal;

namespace Mono.Net
{
	// Token: 0x02000061 RID: 97
	internal class CFDate : INativeObject, IDisposable
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0000572A File Offset: 0x0000392A
		internal CFDate(IntPtr handle, bool owns)
		{
			this.handle = handle;
			if (!owns)
			{
				CFObject.CFRetain(handle);
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005744 File Offset: 0x00003944
		~CFDate()
		{
			this.Dispose(false);
		}

		// Token: 0x060001C8 RID: 456
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDateCreate(IntPtr allocator, double at);

		// Token: 0x060001C9 RID: 457 RVA: 0x00005774 File Offset: 0x00003974
		public static CFDate Create(DateTime date)
		{
			DateTime dateTime = new DateTime(2001, 1, 1);
			double totalSeconds = (date - dateTime).TotalSeconds;
			IntPtr intPtr = CFDate.CFDateCreate(IntPtr.Zero, totalSeconds);
			if (intPtr == IntPtr.Zero)
			{
				throw new NotSupportedException();
			}
			return new CFDate(intPtr, true);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000057C3 File Offset: 0x000039C3
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000057CB File Offset: 0x000039CB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000057DA File Offset: 0x000039DA
		protected virtual void Dispose(bool disposing)
		{
			if (this.handle != IntPtr.Zero)
			{
				CFObject.CFRelease(this.handle);
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x0400077D RID: 1917
		private IntPtr handle;
	}
}
