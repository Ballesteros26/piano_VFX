using System;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x0200016A RID: 362
	internal class SizedReference : IDisposable
	{
		// Token: 0x06000F68 RID: 3944
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateSizedRef(object o);

		// Token: 0x06000F69 RID: 3945
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FreeSizedRef(IntPtr h);

		// Token: 0x06000F6A RID: 3946
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetTargetOfSizedRef(IntPtr h);

		// Token: 0x06000F6B RID: 3947
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetApproximateSizeOfSizedRef(IntPtr h);

		// Token: 0x06000F6C RID: 3948 RVA: 0x0003EE2C File Offset: 0x0003D02C
		[SecuritySafeCritical]
		private void Free()
		{
			IntPtr handle = this._handle;
			if (handle != IntPtr.Zero && Interlocked.CompareExchange(ref this._handle, IntPtr.Zero, handle) == handle)
			{
				SizedReference.FreeSizedRef(handle);
			}
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0003EE70 File Offset: 0x0003D070
		[SecuritySafeCritical]
		public SizedReference(object target)
		{
			IntPtr intPtr = IntPtr.Zero;
			intPtr = SizedReference.CreateSizedRef(target);
			this._handle = intPtr;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0003EE9C File Offset: 0x0003D09C
		~SizedReference()
		{
			this.Free();
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0003EEC8 File Offset: 0x0003D0C8
		public object Target
		{
			[SecuritySafeCritical]
			get
			{
				IntPtr handle = this._handle;
				if (handle == IntPtr.Zero)
				{
					return null;
				}
				object targetOfSizedRef = SizedReference.GetTargetOfSizedRef(handle);
				if (!(this._handle == IntPtr.Zero))
				{
					return targetOfSizedRef;
				}
				return null;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0003EF0C File Offset: 0x0003D10C
		public long ApproximateSize
		{
			[SecuritySafeCritical]
			get
			{
				IntPtr handle = this._handle;
				if (handle == IntPtr.Zero)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Handle is not initialized."));
				}
				long approximateSizeOfSizedRef = SizedReference.GetApproximateSizeOfSizedRef(handle);
				if (this._handle == IntPtr.Zero)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Handle is not initialized."));
				}
				return approximateSizeOfSizedRef;
			}
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0003EF69 File Offset: 0x0003D169
		public void Dispose()
		{
			this.Free();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000931 RID: 2353
		internal volatile IntPtr _handle;
	}
}
