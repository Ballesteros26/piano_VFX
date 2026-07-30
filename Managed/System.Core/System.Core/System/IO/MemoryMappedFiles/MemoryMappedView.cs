using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.IO.MemoryMappedFiles
{
	// Token: 0x02000058 RID: 88
	internal class MemoryMappedView : IDisposable
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00004DEF File Offset: 0x00002FEF
		[SecurityCritical]
		private MemoryMappedView(SafeMemoryMappedViewHandle viewHandle, long pointerOffset, long size, MemoryMappedFileAccess access)
		{
			this.m_viewHandle = viewHandle;
			this.m_pointerOffset = pointerOffset;
			this.m_size = size;
			this.m_access = access;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00004E14 File Offset: 0x00003014
		internal SafeMemoryMappedViewHandle ViewHandle
		{
			[SecurityCritical]
			get
			{
				return this.m_viewHandle;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00004E1C File Offset: 0x0000301C
		internal long PointerOffset
		{
			get
			{
				return this.m_pointerOffset;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00004E24 File Offset: 0x00003024
		internal long Size
		{
			get
			{
				return this.m_size;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00004E2C File Offset: 0x0000302C
		internal MemoryMappedFileAccess Access
		{
			get
			{
				return this.m_access;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00004E34 File Offset: 0x00003034
		internal static MemoryMappedView Create(IntPtr handle, long offset, long size, MemoryMappedFileAccess access)
		{
			IntPtr intPtr;
			IntPtr intPtr2;
			MemoryMapImpl.Map(handle, offset, ref size, access, out intPtr, out intPtr2);
			return new MemoryMappedView(new SafeMemoryMappedViewHandle(intPtr, intPtr2, size), 0L, size, access);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00004E60 File Offset: 0x00003060
		public void Flush(IntPtr capacity)
		{
			this.m_viewHandle.Flush();
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00004E6D File Offset: 0x0000306D
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_viewHandle != null && !this.m_viewHandle.IsClosed)
			{
				this.m_viewHandle.Dispose();
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00004E8F File Offset: 0x0000308F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00004E9E File Offset: 0x0000309E
		internal bool IsClosed
		{
			get
			{
				return this.m_viewHandle == null || this.m_viewHandle.IsClosed;
			}
		}

		// Token: 0x04000263 RID: 611
		private SafeMemoryMappedViewHandle m_viewHandle;

		// Token: 0x04000264 RID: 612
		private long m_pointerOffset;

		// Token: 0x04000265 RID: 613
		private long m_size;

		// Token: 0x04000266 RID: 614
		private MemoryMappedFileAccess m_access;
	}
}
