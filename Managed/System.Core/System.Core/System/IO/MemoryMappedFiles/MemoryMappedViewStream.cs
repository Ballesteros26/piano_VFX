using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;
using Unity;

namespace System.IO.MemoryMappedFiles
{
	/// <summary>Represents a view of a memory-mapped file as a sequentially accessed stream.</summary>
	// Token: 0x02000055 RID: 85
	public sealed class MemoryMappedViewStream : UnmanagedMemoryStream
	{
		// Token: 0x0600018E RID: 398 RVA: 0x00004754 File Offset: 0x00002954
		[SecurityCritical]
		internal MemoryMappedViewStream(MemoryMappedView view)
		{
			this.m_view = view;
			base.Initialize(this.m_view.ViewHandle, this.m_view.PointerOffset, this.m_view.Size, MemoryMappedFile.GetFileAccess(this.m_view.Access));
		}

		/// <summary>Gets a handle to the view of a memory-mapped file.</summary>
		/// <returns>A wrapper for the operating system's handle to the view of the file. </returns>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000047A5 File Offset: 0x000029A5
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

		/// <summary>Sets the length of the current stream.</summary>
		/// <param name="value">The desired length of the current stream in bytes.</param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06000190 RID: 400 RVA: 0x000047BC File Offset: 0x000029BC
		public override void SetLength(long value)
		{
			throw new NotSupportedException(global::SR.GetString("MemoryMappedViewStreams are fixed length."));
		}

		/// <summary>Gets the number of bytes by which the starting position of this view is offset from the beginning of the memory-mapped file.</summary>
		/// <returns>The number of bytes between the starting position of this view and the beginning of the memory-mapped file. </returns>
		/// <exception cref="T:System.InvalidOperationException">The object from which this instance was created is null. </exception>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000047CD File Offset: 0x000029CD
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

		// Token: 0x06000192 RID: 402 RVA: 0x000047F4 File Offset: 0x000029F4
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

		/// <summary>Clears all buffers for this stream and causes any buffered data to be written to the underlying file.</summary>
		// Token: 0x06000193 RID: 403 RVA: 0x0000485C File Offset: 0x00002A5C
		[SecurityCritical]
		public override void Flush()
		{
			if (!this.CanSeek)
			{
				__Error.StreamIsClosed();
			}
			if (this.m_view != null)
			{
				this.m_view.Flush((IntPtr)base.Capacity);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000220F File Offset: 0x0000040F
		internal MemoryMappedViewStream()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400025F RID: 607
		private MemoryMappedView m_view;
	}
}
