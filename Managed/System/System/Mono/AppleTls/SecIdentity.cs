using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Net;
using ObjCRuntimeInternal;

namespace Mono.AppleTls
{
	// Token: 0x020000AB RID: 171
	internal class SecIdentity : INativeObject, IDisposable
	{
		// Token: 0x0600042A RID: 1066 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
		static SecIdentity()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/Security.framework/Security", 0);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			try
			{
				SecIdentity.ImportExportPassphase = CFObject.GetStringConstant(intPtr, "kSecImportExportPassphrase");
				SecIdentity.ImportItemIdentity = CFObject.GetStringConstant(intPtr, "kSecImportItemIdentity");
				SecIdentity.ImportExportAccess = CFObject.GetStringConstant(intPtr, "kSecImportExportAccess");
				SecIdentity.ImportExportKeychain = CFObject.GetStringConstant(intPtr, "kSecImportExportKeychain");
			}
			finally
			{
				CFObject.dlclose(intPtr);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000D868 File Offset: 0x0000BA68
		internal SecIdentity(IntPtr handle, bool owns = false)
		{
			this.handle = handle;
			if (!owns)
			{
				CFObject.CFRetain(handle);
			}
		}

		// Token: 0x0600042C RID: 1068
		[DllImport("/System/Library/Frameworks/Security.framework/Security", EntryPoint = "SecIdentityGetTypeID")]
		public static extern IntPtr GetTypeID();

		// Token: 0x0600042D RID: 1069
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SecStatusCode SecIdentityCopyCertificate(IntPtr identityRef, out IntPtr certificateRef);

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000D884 File Offset: 0x0000BA84
		public SecCertificate Certificate
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					throw new ObjectDisposedException("SecIdentity");
				}
				IntPtr intPtr;
				SecStatusCode secStatusCode = SecIdentity.SecIdentityCopyCertificate(this.handle, out intPtr);
				if (secStatusCode != SecStatusCode.Success)
				{
					throw new InvalidOperationException(secStatusCode.ToString());
				}
				return new SecCertificate(intPtr, true);
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000D8DC File Offset: 0x0000BADC
		private static CFDictionary CreateImportOptions(CFString password, SecIdentity.ImportOptions options = null)
		{
			if (options == null)
			{
				return CFDictionary.FromObjectAndKey(password.Handle, SecIdentity.ImportExportPassphase.Handle);
			}
			List<Tuple<IntPtr, IntPtr>> list = new List<Tuple<IntPtr, IntPtr>>();
			list.Add(new Tuple<IntPtr, IntPtr>(SecIdentity.ImportExportPassphase.Handle, password.Handle));
			if (options.KeyChain != null)
			{
				list.Add(new Tuple<IntPtr, IntPtr>(SecIdentity.ImportExportKeychain.Handle, options.KeyChain.Handle));
			}
			if (options.Access != null)
			{
				list.Add(new Tuple<IntPtr, IntPtr>(SecIdentity.ImportExportAccess.Handle, options.Access.Handle));
			}
			return CFDictionary.FromKeysAndObjects(list);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000D97C File Offset: 0x0000BB7C
		public static SecIdentity Import(byte[] data, string password, SecIdentity.ImportOptions options = null)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (string.IsNullOrEmpty(password))
			{
				throw new ArgumentException("password");
			}
			SecIdentity secIdentity;
			using (CFString cfstring = CFString.Create(password))
			{
				using (CFDictionary cfdictionary = SecIdentity.CreateImportOptions(cfstring, options))
				{
					CFDictionary[] array;
					SecStatusCode secStatusCode = SecImportExport.ImportPkcs12(data, cfdictionary, out array);
					if (secStatusCode != SecStatusCode.Success)
					{
						throw new InvalidOperationException(secStatusCode.ToString());
					}
					secIdentity = new SecIdentity(array[0].GetValue(SecIdentity.ImportItemIdentity.Handle), false);
				}
			}
			return secIdentity;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000DA28 File Offset: 0x0000BC28
		public static SecIdentity Import(X509Certificate2 certificate, SecIdentity.ImportOptions options = null)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (!certificate.HasPrivateKey)
			{
				throw new InvalidOperationException("Need X509Certificate2 with a private key.");
			}
			string text = Guid.NewGuid().ToString();
			return SecIdentity.Import(certificate.Export(X509ContentType.Pfx, text), text, options);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000DA7C File Offset: 0x0000BC7C
		~SecIdentity()
		{
			this.Dispose(false);
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000DAAC File Offset: 0x0000BCAC
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000DAC3 File Offset: 0x0000BCC3
		protected virtual void Dispose(bool disposing)
		{
			if (this.handle != IntPtr.Zero)
			{
				CFObject.CFRelease(this.handle);
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x0400092C RID: 2348
		private static readonly CFString ImportExportPassphase;

		// Token: 0x0400092D RID: 2349
		private static readonly CFString ImportItemIdentity;

		// Token: 0x0400092E RID: 2350
		private static readonly CFString ImportExportAccess;

		// Token: 0x0400092F RID: 2351
		private static readonly CFString ImportExportKeychain;

		// Token: 0x04000930 RID: 2352
		internal IntPtr handle;

		// Token: 0x020000AC RID: 172
		internal class ImportOptions
		{
			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000DAED File Offset: 0x0000BCED
			// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000DAF5 File Offset: 0x0000BCF5
			public SecAccess Access { get; set; }

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000DAFE File Offset: 0x0000BCFE
			// (set) Token: 0x06000439 RID: 1081 RVA: 0x0000DB06 File Offset: 0x0000BD06
			public SecKeyChain KeyChain { get; set; }
		}
	}
}
