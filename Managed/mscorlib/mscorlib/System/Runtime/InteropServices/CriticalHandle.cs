using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	/// <summary>Represents a wrapper class for handle resources.</summary>
	// Token: 0x020008DA RID: 2266
	[SecurityCritical]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class CriticalHandle : CriticalFinalizerObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.CriticalHandle" /> class with the specified invalid handle value.</summary>
		/// <param name="invalidHandleValue">The value of an invalid handle (usually 0 or -1).</param>
		/// <exception cref="T:System.TypeLoadException">The derived class resides in an assembly without unmanaged code access permission.</exception>
		// Token: 0x0600554D RID: 21837 RVA: 0x00128BC3 File Offset: 0x00126DC3
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandle(IntPtr invalidHandleValue)
		{
			this.handle = invalidHandleValue;
			this._isClosed = false;
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x00128BDC File Offset: 0x00126DDC
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~CriticalHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x00128C0C File Offset: 0x00126E0C
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void Cleanup()
		{
			if (this.IsClosed)
			{
				return;
			}
			this._isClosed = true;
			if (this.IsInvalid)
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!this.ReleaseHandle())
			{
				CriticalHandle.FireCustomerDebugProbe();
			}
			Marshal.SetLastWin32Error(lastWin32Error);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x00002194 File Offset: 0x00000394
		private static void FireCustomerDebugProbe()
		{
		}

		/// <summary>Sets the handle to the specified pre-existing handle.</summary>
		/// <param name="handle">The pre-existing handle to use.</param>
		// Token: 0x06005551 RID: 21841 RVA: 0x00128C44 File Offset: 0x00126E44
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		/// <summary>Gets a value indicating whether the handle is closed.</summary>
		/// <returns>true if the handle is closed; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06005552 RID: 21842 RVA: 0x00128C4D File Offset: 0x00126E4D
		public bool IsClosed
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._isClosed;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the handle value is invalid.</summary>
		/// <returns>true if the handle is valid; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06005553 RID: 21843
		public abstract bool IsInvalid
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get;
		}

		/// <summary>Marks the handle for releasing and freeing resources.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005554 RID: 21844 RVA: 0x00128C55 File Offset: 0x00126E55
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Runtime.InteropServices.CriticalHandle" />. </summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005555 RID: 21845 RVA: 0x00128C55 File Offset: 0x00126E55
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Runtime.InteropServices.CriticalHandle" /> class specifying whether to perform a normal dispose operation.</summary>
		/// <param name="disposing">true for a normal dispose operation; false to finalize the handle.</param>
		// Token: 0x06005556 RID: 21846 RVA: 0x00128C5E File Offset: 0x00126E5E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityCritical]
		protected virtual void Dispose(bool disposing)
		{
			this.Cleanup();
		}

		/// <summary>Marks a handle as invalid.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005557 RID: 21847 RVA: 0x00128C66 File Offset: 0x00126E66
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void SetHandleAsInvalid()
		{
			this._isClosed = true;
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, executes the code required to free the handle.</summary>
		/// <returns>true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant.</returns>
		// Token: 0x06005558 RID: 21848
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected abstract bool ReleaseHandle();

		/// <summary>Specifies the handle to be wrapped.</summary>
		// Token: 0x04002CCA RID: 11466
		protected IntPtr handle;

		// Token: 0x04002CCB RID: 11467
		private bool _isClosed;
	}
}
