using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.InteropServices
{
	/// <summary>Represents a wrapper class for operating system handles. This class must be inherited.</summary>
	// Token: 0x020008EF RID: 2287
	[SecurityCritical]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class SafeHandle : CriticalFinalizerObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SafeHandle" /> class with the specified invalid handle value.</summary>
		/// <param name="invalidHandleValue">The value of an invalid handle (usually 0 or -1).  Your implementation of <see cref="P:System.Runtime.InteropServices.SafeHandle.IsInvalid" /> should return true for this value.</param>
		/// <param name="ownsHandle">true to reliably let <see cref="T:System.Runtime.InteropServices.SafeHandle" /> release the handle during the finalization phase; otherwise, false (not recommended). </param>
		/// <exception cref="T:System.TypeLoadException">The derived class resides in an assembly without unmanaged code access permission. </exception>
		// Token: 0x0600559F RID: 21919 RVA: 0x00129044 File Offset: 0x00127244
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandle(IntPtr invalidHandleValue, bool ownsHandle)
		{
			this.handle = invalidHandleValue;
			this._state = 4;
			this._ownsHandle = ownsHandle;
			if (!ownsHandle)
			{
				GC.SuppressFinalize(this);
			}
			this._fullyInitialized = true;
		}

		// Token: 0x060055A0 RID: 21920 RVA: 0x00129074 File Offset: 0x00127274
		[SecuritySafeCritical]
		~SafeHandle()
		{
			this.Dispose(false);
		}

		/// <summary>Sets the handle to the specified pre-existing handle.</summary>
		/// <param name="handle">The pre-existing handle to use. </param>
		// Token: 0x060055A1 RID: 21921 RVA: 0x001290A4 File Offset: 0x001272A4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		/// <summary>Returns the value of the <see cref="F:System.Runtime.InteropServices.SafeHandle.handle" /> field.</summary>
		/// <returns>An IntPtr representing the value of the <see cref="F:System.Runtime.InteropServices.SafeHandle.handle" /> field. If the handle has been marked invalid with <see cref="M:System.Runtime.InteropServices.SafeHandle.SetHandleAsInvalid" />, this method still returns the original handle value, which can be a stale value.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060055A2 RID: 21922 RVA: 0x001290AD File Offset: 0x001272AD
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public IntPtr DangerousGetHandle()
		{
			return this.handle;
		}

		/// <summary>Gets a value indicating whether the handle is closed.</summary>
		/// <returns>true if the handle is closed; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x060055A3 RID: 21923 RVA: 0x001290B5 File Offset: 0x001272B5
		public bool IsClosed
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return (this._state & 1) == 1;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the handle value is invalid.</summary>
		/// <returns>true if the handle value is invalid; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x060055A4 RID: 21924
		public abstract bool IsInvalid
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get;
		}

		/// <summary>Marks the handle for releasing and freeing resources.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060055A5 RID: 21925 RVA: 0x001290C2 File Offset: 0x001272C2
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Runtime.InteropServices.SafeHandle" /> class.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060055A6 RID: 21926 RVA: 0x001290C2 File Offset: 0x001272C2
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Runtime.InteropServices.SafeHandle" /> class specifying whether to perform a normal dispose operation.</summary>
		/// <param name="disposing">true for a normal dispose operation; false to finalize the handle.</param>
		// Token: 0x060055A7 RID: 21927 RVA: 0x001290CB File Offset: 0x001272CB
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.InternalDispose();
				return;
			}
			this.InternalFinalize();
		}

		/// <summary>When overridden in a derived class, executes the code required to free the handle.</summary>
		/// <returns>true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant.</returns>
		// Token: 0x060055A8 RID: 21928
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected abstract bool ReleaseHandle();

		/// <summary>Marks a handle as no longer used.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060055A9 RID: 21929 RVA: 0x001290E0 File Offset: 0x001272E0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void SetHandleAsInvalid()
		{
			try
			{
			}
			finally
			{
				int state;
				int num;
				do
				{
					state = this._state;
					num = state | 1;
				}
				while (Interlocked.CompareExchange(ref this._state, num, state) != state);
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>Manually increments the reference counter on <see cref="T:System.Runtime.InteropServices.SafeHandle" /> instances.</summary>
		/// <param name="success">true if the reference counter was successfully incremented; otherwise, false.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060055AA RID: 21930 RVA: 0x00129124 File Offset: 0x00127324
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void DangerousAddRef(ref bool success)
		{
			try
			{
			}
			finally
			{
				if (!this._fullyInitialized)
				{
					throw new InvalidOperationException();
				}
				for (;;)
				{
					int state = this._state;
					if ((state & 1) != 0)
					{
						break;
					}
					int num = state + 4;
					if (Interlocked.CompareExchange(ref this._state, num, state) == state)
					{
						goto Block_5;
					}
				}
				throw new ObjectDisposedException(null, "Safe handle has been closed");
				Block_5:
				success = true;
			}
		}

		/// <summary>Manually decrements the reference counter on a <see cref="T:System.Runtime.InteropServices.SafeHandle" /> instance.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060055AB RID: 21931 RVA: 0x00129184 File Offset: 0x00127384
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void DangerousRelease()
		{
			this.DangerousReleaseInternal(false);
		}

		// Token: 0x060055AC RID: 21932 RVA: 0x0012918D File Offset: 0x0012738D
		private void InternalDispose()
		{
			if (!this._fullyInitialized)
			{
				throw new InvalidOperationException();
			}
			this.DangerousReleaseInternal(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060055AD RID: 21933 RVA: 0x001291AA File Offset: 0x001273AA
		private void InternalFinalize()
		{
			if (this._fullyInitialized)
			{
				this.DangerousReleaseInternal(true);
			}
		}

		// Token: 0x060055AE RID: 21934 RVA: 0x001291BC File Offset: 0x001273BC
		private void DangerousReleaseInternal(bool dispose)
		{
			try
			{
			}
			finally
			{
				if (!this._fullyInitialized)
				{
					throw new InvalidOperationException();
				}
				bool flag;
				for (;;)
				{
					int state = this._state;
					if (dispose && (state & 2) != 0)
					{
						break;
					}
					if ((state & 2147483644) == 0)
					{
						goto Block_6;
					}
					flag = (state & 2147483644) == 4 && (state & 1) == 0 && this._ownsHandle && !this.IsInvalid;
					int num = (state & 2147483644) - 4;
					if ((state & 2147483644) == 4)
					{
						num |= 1;
					}
					if (dispose)
					{
						num |= 2;
					}
					if (Interlocked.CompareExchange(ref this._state, num, state) == state)
					{
						goto IL_00A0;
					}
				}
				flag = false;
				goto IL_00A0;
				Block_6:
				throw new ObjectDisposedException(null, "Safe handle has been closed");
				IL_00A0:
				if (flag)
				{
					this.ReleaseHandle();
				}
			}
		}

		/// <summary>Specifies the handle to be wrapped.</summary>
		// Token: 0x04002CDC RID: 11484
		protected IntPtr handle;

		// Token: 0x04002CDD RID: 11485
		private int _state;

		// Token: 0x04002CDE RID: 11486
		private bool _ownsHandle;

		// Token: 0x04002CDF RID: 11487
		private bool _fullyInitialized;

		// Token: 0x04002CE0 RID: 11488
		private const int RefCount_Mask = 2147483644;

		// Token: 0x04002CE1 RID: 11489
		private const int RefCount_One = 4;

		// Token: 0x020008F0 RID: 2288
		private enum State
		{
			// Token: 0x04002CE3 RID: 11491
			Closed = 1,
			// Token: 0x04002CE4 RID: 11492
			Disposed
		}
	}
}
