using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides a managed representation of a Win32 OVERLAPPED structure, including methods to transfer information from an <see cref="T:System.Threading.Overlapped" /> instance to a <see cref="T:System.Threading.NativeOverlapped" /> structure.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004AA RID: 1194
	[ComVisible(true)]
	public class Overlapped
	{
		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Threading.Overlapped" /> class.</summary>
		// Token: 0x060037F1 RID: 14321 RVA: 0x00002111 File Offset: 0x00000311
		public Overlapped()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Overlapped" /> class with the specified file position, the 32-bit integer handle to an event that is signaled when the I/O operation is complete, and an interface through which to return the results of the operation.</summary>
		/// <param name="offsetLo">The low word of the file position at which to start the transfer. </param>
		/// <param name="offsetHi">The high word of the file position at which to start the transfer. </param>
		/// <param name="hEvent">The handle to an event that is signaled when the I/O operation is complete. </param>
		/// <param name="ar">An object that implements the <see cref="T:System.IAsyncResult" /> interface and provides status information on the I/O operation. </param>
		// Token: 0x060037F2 RID: 14322 RVA: 0x000CB164 File Offset: 0x000C9364
		[Obsolete("Not 64bit compatible.  Please use the constructor that takes IntPtr for the event handle")]
		public Overlapped(int offsetLo, int offsetHi, int hEvent, IAsyncResult ar)
		{
			this.offsetL = offsetLo;
			this.offsetH = offsetHi;
			this.evt = hEvent;
			this.ares = ar;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Overlapped" /> class with the specified file position, the handle to an event that is signaled when the I/O operation is complete, and an interface through which to return the results of the operation.</summary>
		/// <param name="offsetLo">The low word of the file position at which to start the transfer. </param>
		/// <param name="offsetHi">The high word of the file position at which to start the transfer. </param>
		/// <param name="hEvent">The handle to an event that is signaled when the I/O operation is complete. </param>
		/// <param name="ar">An object that implements the <see cref="T:System.IAsyncResult" /> interface and provides status information on the I/O operation. </param>
		// Token: 0x060037F3 RID: 14323 RVA: 0x000CB189 File Offset: 0x000C9389
		public Overlapped(int offsetLo, int offsetHi, IntPtr hEvent, IAsyncResult ar)
		{
			this.offsetL = offsetLo;
			this.offsetH = offsetHi;
			this.evt_ptr = hEvent;
			this.ares = ar;
		}

		/// <summary>Frees the unmanaged memory associated with a native overlapped structure allocated by the <see cref="Overload:System.Threading.Overlapped.Pack" /> method.</summary>
		/// <param name="nativeOverlappedPtr">A pointer to the <see cref="T:System.Threading.NativeOverlapped" /> structure to be freed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="nativeOverlappedPtr" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060037F4 RID: 14324 RVA: 0x000CB1AE File Offset: 0x000C93AE
		[CLSCompliant(false)]
		public unsafe static void Free(NativeOverlapped* nativeOverlappedPtr)
		{
			if ((IntPtr)((void*)nativeOverlappedPtr) == IntPtr.Zero)
			{
				throw new ArgumentNullException("nativeOverlappedPtr");
			}
			Marshal.FreeHGlobal((IntPtr)((void*)nativeOverlappedPtr));
		}

		/// <summary>Unpacks the specified unmanaged <see cref="T:System.Threading.NativeOverlapped" /> structure into a managed <see cref="T:System.Threading.Overlapped" /> object. </summary>
		/// <returns>An <see cref="T:System.Threading.Overlapped" /> object containing the information unpacked from the native structure.</returns>
		/// <param name="nativeOverlappedPtr">An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="nativeOverlappedPtr" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060037F5 RID: 14325 RVA: 0x000CB1D8 File Offset: 0x000C93D8
		[CLSCompliant(false)]
		public unsafe static Overlapped Unpack(NativeOverlapped* nativeOverlappedPtr)
		{
			if ((IntPtr)((void*)nativeOverlappedPtr) == IntPtr.Zero)
			{
				throw new ArgumentNullException("nativeOverlappedPtr");
			}
			return new Overlapped
			{
				offsetL = nativeOverlappedPtr->OffsetLow,
				offsetH = nativeOverlappedPtr->OffsetHigh,
				evt = (int)nativeOverlappedPtr->EventHandle
			};
		}

		/// <summary>Packs the current instance into a <see cref="T:System.Threading.NativeOverlapped" /> structure, specifying the delegate to be invoked when the asynchronous I/O operation is complete.</summary>
		/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure. </returns>
		/// <param name="iocb">An <see cref="T:System.Threading.IOCompletionCallback" /> delegate that represents the callback method invoked when the asynchronous I/O operation completes.</param>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Threading.Overlapped" /> has already been packed.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060037F6 RID: 14326 RVA: 0x000CB230 File Offset: 0x000C9430
		[CLSCompliant(false)]
		[MonoTODO("Security - we need to propagate the call stack")]
		[Obsolete("Use Pack(iocb, userData) instead")]
		public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb)
		{
			NativeOverlapped* ptr = (NativeOverlapped*)(void*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped)));
			ptr->OffsetLow = this.offsetL;
			ptr->OffsetHigh = this.offsetH;
			ptr->EventHandle = (IntPtr)this.evt;
			return ptr;
		}

		/// <summary>Packs the current instance into a <see cref="T:System.Threading.NativeOverlapped" /> structure, specifying a delegate that is invoked when the asynchronous I/O operation is complete and a managed object that serves as a buffer.</summary>
		/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure. </returns>
		/// <param name="iocb">An <see cref="T:System.Threading.IOCompletionCallback" /> delegate that represents the callback method invoked when the asynchronous I/O operation completes.</param>
		/// <param name="userData">An object or array of objects representing the input or output buffer for the operation. Each object represents a buffer, for example an array of bytes.</param>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Threading.Overlapped" /> has already been packed.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060037F7 RID: 14327 RVA: 0x000CB284 File Offset: 0x000C9484
		[ComVisible(false)]
		[MonoTODO("handle userData")]
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb, object userData)
		{
			NativeOverlapped* ptr = (NativeOverlapped*)(void*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped)));
			ptr->OffsetLow = this.offsetL;
			ptr->OffsetHigh = this.offsetH;
			ptr->EventHandle = this.evt_ptr;
			return ptr;
		}

		/// <summary>Packs the current instance into a <see cref="T:System.Threading.NativeOverlapped" /> structure specifying the delegate to invoke when the asynchronous I/O operation is complete. Does not propagate the calling stack.</summary>
		/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure. </returns>
		/// <param name="iocb">An <see cref="T:System.Threading.IOCompletionCallback" /> delegate that represents the callback method invoked when the asynchronous I/O operation completes.</param>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Threading.Overlapped" /> has already been packed.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x060037F8 RID: 14328 RVA: 0x000CB2D0 File Offset: 0x000C94D0
		[CLSCompliant(false)]
		[Obsolete("Use UnsafePack(iocb, userData) instead")]
		[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
		public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb)
		{
			return this.Pack(iocb);
		}

		/// <summary>Packs the current instance into a <see cref="T:System.Threading.NativeOverlapped" /> structure, specifying the delegate to invoke when the asynchronous I/O operation is complete and the managed object that serves as a buffer. Does not propagate the calling stack.</summary>
		/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure. </returns>
		/// <param name="iocb">An <see cref="T:System.Threading.IOCompletionCallback" /> delegate that represents the callback method invoked when the asynchronous I/O operation completes.</param>
		/// <param name="userData">An object or array of objects representing the input or output buffer for the operation. Each object represents a buffer, for example an array of bytes.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Threading.Overlapped" /> is already packed.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x060037F9 RID: 14329 RVA: 0x000CB2D9 File Offset: 0x000C94D9
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb, object userData)
		{
			return this.Pack(iocb, userData);
		}

		/// <summary>Gets or sets the object that provides status information on the I/O operation.</summary>
		/// <returns>An object that implements the <see cref="T:System.IAsyncResult" /> interface.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x000CB2E3 File Offset: 0x000C94E3
		// (set) Token: 0x060037FB RID: 14331 RVA: 0x000CB2EB File Offset: 0x000C94EB
		public IAsyncResult AsyncResult
		{
			get
			{
				return this.ares;
			}
			set
			{
				this.ares = value;
			}
		}

		/// <summary>Gets or sets the 32-bit integer handle to a synchronization event that is signaled when the I/O operation is complete.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the handle of the synchronization event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x000CB2F4 File Offset: 0x000C94F4
		// (set) Token: 0x060037FD RID: 14333 RVA: 0x000CB2FC File Offset: 0x000C94FC
		[Obsolete("Not 64bit compatible.  Use EventHandleIntPtr instead.")]
		public int EventHandle
		{
			get
			{
				return this.evt;
			}
			set
			{
				this.evt = value;
			}
		}

		/// <summary>Gets or sets the handle to the synchronization event that is signaled when the I/O operation is complete.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> representing the handle of the event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x000CB305 File Offset: 0x000C9505
		// (set) Token: 0x060037FF RID: 14335 RVA: 0x000CB30D File Offset: 0x000C950D
		[ComVisible(false)]
		public IntPtr EventHandleIntPtr
		{
			get
			{
				return this.evt_ptr;
			}
			set
			{
				this.evt_ptr = value;
			}
		}

		/// <summary>Gets or sets the high-order word of the file position at which to start the transfer. The file position is a byte offset from the start of the file.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the high word of the file position.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06003800 RID: 14336 RVA: 0x000CB316 File Offset: 0x000C9516
		// (set) Token: 0x06003801 RID: 14337 RVA: 0x000CB31E File Offset: 0x000C951E
		public int OffsetHigh
		{
			get
			{
				return this.offsetH;
			}
			set
			{
				this.offsetH = value;
			}
		}

		/// <summary>Gets or sets the low-order word of the file position at which to start the transfer. The file position is a byte offset from the start of the file.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the low word of the file position.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06003802 RID: 14338 RVA: 0x000CB327 File Offset: 0x000C9527
		// (set) Token: 0x06003803 RID: 14339 RVA: 0x000CB32F File Offset: 0x000C952F
		public int OffsetLow
		{
			get
			{
				return this.offsetL;
			}
			set
			{
				this.offsetL = value;
			}
		}

		// Token: 0x04001D48 RID: 7496
		private IAsyncResult ares;

		// Token: 0x04001D49 RID: 7497
		private int offsetL;

		// Token: 0x04001D4A RID: 7498
		private int offsetH;

		// Token: 0x04001D4B RID: 7499
		private int evt;

		// Token: 0x04001D4C RID: 7500
		private IntPtr evt_ptr;
	}
}
