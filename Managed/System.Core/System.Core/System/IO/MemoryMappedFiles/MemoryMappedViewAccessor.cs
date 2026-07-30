using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;
using Unity;

namespace System.IO.MemoryMappedFiles
{
	/// <summary>Represents a randomly accessed view of a memory-mapped file.</summary>
	// Token: 0x02000054 RID: 84
	public sealed class MemoryMappedViewAccessor : UnmanagedMemoryAccessor
	{
		// Token: 0x06000188 RID: 392 RVA: 0x0000461C File Offset: 0x0000281C
		[SecurityCritical]
		internal MemoryMappedViewAccessor(MemoryMappedView view)
		{
			this.m_view = view;
			base.Initialize(this.m_view.ViewHandle, this.m_view.PointerOffset, this.m_view.Size, MemoryMappedFile.GetFileAccess(this.m_view.Access));
		}

		/// <summary>Gets a handle to the view of a memory-mapped file.</summary>
		/// <returns>A wrapper for the operating system's handle to the view of the file. </returns>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000466D File Offset: 0x0000286D
		public SafeMemoryMappedViewHandle SafeMemoryMappedViewHandle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (this.m_view == null)
				{
					return null;
				}
				return this.m_view.ViewHandle;
			}
		}

		/// <summary>Gets the number of bytes by which the starting position of this view is offset from the beginning of the memory-mapped file. </summary>
		/// <returns>The number of bytes between the starting position of this view and the beginning of the memory-mapped file. </returns>
		/// <exception cref="T:System.InvalidOperationException">The object from which this instance was created is null. </exception>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00004684 File Offset: 0x00002884
		public long PointerOffset
		{
			get
			{
				if (this.m_view == null)
				{
					throw new InvalidOperationException(global::SR.GetString("The underlying MemoryMappedView object is null."));
				}
				return this.m_view.PointerOffset;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000046AC File Offset: 0x000028AC
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.m_view != null && !this.m_view.IsClosed)
				{
					this.Flush();
				}
			}
			finally
			{
				try
				{
					if (this.m_view != null)
					{
						this.m_view.Dispose();
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		/// <summary>Clears all buffers for this view and causes any buffered data to be written to the underlying file.</summary>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the accessor was closed.</exception>
		// Token: 0x0600018C RID: 396 RVA: 0x00004714 File Offset: 0x00002914
		[SecurityCritical]
		public void Flush()
		{
			if (!base.IsOpen)
			{
				throw new ObjectDisposedException("MemoryMappedViewAccessor", global::SR.GetString("Cannot access a closed accessor."));
			}
			if (this.m_view != null)
			{
				this.m_view.Flush((IntPtr)base.Capacity);
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000220F File Offset: 0x0000040F
		internal MemoryMappedViewAccessor()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400025E RID: 606
		private MemoryMappedView m_view;
	}
}
