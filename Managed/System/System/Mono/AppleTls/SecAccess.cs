using System;
using System.Runtime.InteropServices;
using Mono.Net;
using ObjCRuntimeInternal;

namespace Mono.AppleTls
{
	// Token: 0x020000AE RID: 174
	internal class SecAccess : INativeObject, IDisposable
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x0000DBF7 File Offset: 0x0000BDF7
		public SecAccess(IntPtr handle, bool owns = false)
		{
			this.handle = handle;
			if (!owns)
			{
				CFObject.CFRetain(handle);
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000DC10 File Offset: 0x0000BE10
		~SecAccess()
		{
			this.Dispose(false);
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000DC40 File Offset: 0x0000BE40
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000445 RID: 1093
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SecStatusCode SecAccessCreate(IntPtr descriptor, IntPtr trustedList, out IntPtr accessRef);

		// Token: 0x06000446 RID: 1094 RVA: 0x0000DC48 File Offset: 0x0000BE48
		public static SecAccess Create(string descriptor)
		{
			CFString cfstring = CFString.Create(descriptor);
			if (cfstring == null)
			{
				throw new InvalidOperationException();
			}
			SecAccess secAccess;
			try
			{
				IntPtr intPtr;
				SecStatusCode secStatusCode = SecAccess.SecAccessCreate(cfstring.Handle, IntPtr.Zero, out intPtr);
				if (secStatusCode != SecStatusCode.Success)
				{
					throw new InvalidOperationException(secStatusCode.ToString());
				}
				secAccess = new SecAccess(intPtr, true);
			}
			finally
			{
				cfstring.Dispose();
			}
			return secAccess;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000DCB4 File Offset: 0x0000BEB4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000DCC3 File Offset: 0x0000BEC3
		protected virtual void Dispose(bool disposing)
		{
			if (this.handle != IntPtr.Zero)
			{
				CFObject.CFRelease(this.handle);
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x04000935 RID: 2357
		internal IntPtr handle;
	}
}
