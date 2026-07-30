using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a safe handle that can be used by Cryptography Next Generation (CNG) objects.</summary>
	// Token: 0x02000006 RID: 6
	public abstract class SafeNCryptHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeNCryptHandle" /> class.</summary>
		// Token: 0x06000016 RID: 22 RVA: 0x00002267 File Offset: 0x00000467
		protected SafeNCryptHandle()
			: base(true)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002270 File Offset: 0x00000470
		protected SafeNCryptHandle(IntPtr handle, SafeHandle parentHandle)
			: base(false)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000227E File Offset: 0x0000047E
		public override bool IsInvalid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Releases a handle used by a Cryptography Next Generation (CNG) object.</summary>
		/// <returns>true if the handle is released successfully; otherwise, false.</returns>
		// Token: 0x06000019 RID: 25 RVA: 0x00002285 File Offset: 0x00000485
		protected override bool ReleaseHandle()
		{
			return false;
		}

		/// <summary>Releases a native handle used by a Cryptography Next Generation (CNG) object.</summary>
		/// <returns>true if the handle is released successfully; otherwise, false.</returns>
		// Token: 0x0600001A RID: 26
		protected abstract bool ReleaseNativeHandle();
	}
}
