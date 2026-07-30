using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Mono.Net;
using Mono.Security.Cryptography;

namespace Mono.AppleTls
{
	// Token: 0x020000B1 RID: 177
	internal class SecImportExport
	{
		// Token: 0x06000449 RID: 1097
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SecStatusCode SecPKCS12Import(IntPtr pkcs12_data, IntPtr options, out IntPtr items);

		// Token: 0x0600044A RID: 1098 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		public static SecStatusCode ImportPkcs12(byte[] buffer, CFDictionary options, out CFDictionary[] array)
		{
			SecStatusCode secStatusCode;
			using (CFData cfdata = CFData.FromData(buffer))
			{
				secStatusCode = SecImportExport.ImportPkcs12(cfdata, options, out array);
			}
			return secStatusCode;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000DD2C File Offset: 0x0000BF2C
		public static SecStatusCode ImportPkcs12(CFData data, CFDictionary options, out CFDictionary[] array)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			IntPtr intPtr;
			SecStatusCode secStatusCode = SecImportExport.SecPKCS12Import(data.Handle, options.Handle, out intPtr);
			array = CFArray.ArrayFromHandle<CFDictionary>(intPtr, (IntPtr h) => new CFDictionary(h, false));
			if (intPtr != IntPtr.Zero)
			{
				CFObject.CFRelease(intPtr);
			}
			return secStatusCode;
		}

		// Token: 0x0600044C RID: 1100
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SecStatusCode SecItemImport(IntPtr importedData, IntPtr fileNameOrExtension, ref SecImportExport.SecExternalFormat inputFormat, ref SecImportExport.SecExternalItemType itemType, SecImportExport.SecItemImportExportFlags flags, IntPtr keyParams, IntPtr importKeychain, out IntPtr outItems);

		// Token: 0x0600044D RID: 1101 RVA: 0x0000DD94 File Offset: 0x0000BF94
		public static CFArray ItemImport(byte[] buffer, string password)
		{
			CFArray cfarray;
			using (CFData cfdata = CFData.FromData(buffer))
			{
				using (CFString cfstring = CFString.Create(password))
				{
					cfarray = SecImportExport.ItemImport(cfdata, SecImportExport.SecExternalFormat.PKCS12, SecImportExport.SecExternalItemType.Aggregate, SecImportExport.SecItemImportExportFlags.None, new SecImportExport.SecItemImportExportKeyParameters?(new SecImportExport.SecItemImportExportKeyParameters
					{
						passphrase = cfstring.Handle
					}));
				}
			}
			return cfarray;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000DE08 File Offset: 0x0000C008
		private static CFArray ItemImport(CFData data, SecImportExport.SecExternalFormat format, SecImportExport.SecExternalItemType itemType, SecImportExport.SecItemImportExportFlags flags = SecImportExport.SecItemImportExportFlags.None, SecImportExport.SecItemImportExportKeyParameters? keyParams = null)
		{
			return SecImportExport.ItemImport(data, ref format, ref itemType, flags, keyParams);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000DE18 File Offset: 0x0000C018
		private static CFArray ItemImport(CFData data, ref SecImportExport.SecExternalFormat format, ref SecImportExport.SecExternalItemType itemType, SecImportExport.SecItemImportExportFlags flags = SecImportExport.SecItemImportExportFlags.None, SecImportExport.SecItemImportExportKeyParameters? keyParams = null)
		{
			IntPtr intPtr = IntPtr.Zero;
			if (keyParams != null)
			{
				intPtr = Marshal.AllocHGlobal(Marshal.SizeOf<SecImportExport.SecItemImportExportKeyParameters>(keyParams.Value));
				if (intPtr == IntPtr.Zero)
				{
					throw new OutOfMemoryException();
				}
				Marshal.StructureToPtr<SecImportExport.SecItemImportExportKeyParameters>(keyParams.Value, intPtr, false);
			}
			IntPtr intPtr2;
			SecStatusCode secStatusCode = SecImportExport.SecItemImport(data.Handle, IntPtr.Zero, ref format, ref itemType, flags, intPtr, IntPtr.Zero, out intPtr2);
			if (intPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr);
			}
			if (secStatusCode != SecStatusCode.Success)
			{
				throw new NotSupportedException(secStatusCode.ToString());
			}
			return new CFArray(intPtr2, true);
		}

		// Token: 0x06000450 RID: 1104
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecIdentityCreate(IntPtr allocator, IntPtr certificate, IntPtr privateKey);

		// Token: 0x06000451 RID: 1105 RVA: 0x0000DEB4 File Offset: 0x0000C0B4
		public static SecIdentity ItemImport(X509Certificate2 certificate)
		{
			if (!certificate.HasPrivateKey)
			{
				throw new NotSupportedException();
			}
			SecIdentity secIdentity;
			using (SecKey secKey = SecImportExport.ImportPrivateKey(certificate))
			{
				using (SecCertificate secCertificate = new SecCertificate(certificate))
				{
					IntPtr intPtr = SecImportExport.SecIdentityCreate(IntPtr.Zero, secCertificate.Handle, secKey.Handle);
					if (CFType.GetTypeID(intPtr) != SecIdentity.GetTypeID())
					{
						throw new InvalidOperationException();
					}
					secIdentity = new SecIdentity(intPtr, true);
				}
			}
			return secIdentity;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000DF48 File Offset: 0x0000C148
		private static byte[] ExportKey(RSA key)
		{
			return Mono.Security.Cryptography.PKCS8.PrivateKeyInfo.Encode(key);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000DF50 File Offset: 0x0000C150
		private static SecKey ImportPrivateKey(X509Certificate2 certificate)
		{
			if (!certificate.HasPrivateKey)
			{
				throw new NotSupportedException();
			}
			CFArray cfarray;
			using (CFData cfdata = CFData.FromData(SecImportExport.ExportKey((RSA)certificate.PrivateKey)))
			{
				cfarray = SecImportExport.ItemImport(cfdata, SecImportExport.SecExternalFormat.OpenSSL, SecImportExport.SecExternalItemType.PrivateKey, SecImportExport.SecItemImportExportFlags.None, null);
			}
			SecKey secKey;
			try
			{
				if (cfarray.Count != 1)
				{
					throw new InvalidOperationException("Private key import failed.");
				}
				IntPtr intPtr = cfarray[0];
				if (CFType.GetTypeID(intPtr) != SecKey.GetTypeID())
				{
					throw new InvalidOperationException("Private key import doesn't return SecKey.");
				}
				secKey = new SecKey(intPtr, cfarray.Handle);
			}
			finally
			{
				cfarray.Dispose();
			}
			return secKey;
		}

		// Token: 0x04000AB5 RID: 2741
		private const int SEC_KEY_IMPORT_EXPORT_PARAMS_VERSION = 0;

		// Token: 0x020000B2 RID: 178
		private enum SecExternalFormat
		{
			// Token: 0x04000AB7 RID: 2743
			Unknown,
			// Token: 0x04000AB8 RID: 2744
			OpenSSL,
			// Token: 0x04000AB9 RID: 2745
			X509Cert = 9,
			// Token: 0x04000ABA RID: 2746
			PEMSequence,
			// Token: 0x04000ABB RID: 2747
			PKCS7,
			// Token: 0x04000ABC RID: 2748
			PKCS12
		}

		// Token: 0x020000B3 RID: 179
		private enum SecExternalItemType
		{
			// Token: 0x04000ABE RID: 2750
			Unknown,
			// Token: 0x04000ABF RID: 2751
			PrivateKey,
			// Token: 0x04000AC0 RID: 2752
			PublicKey,
			// Token: 0x04000AC1 RID: 2753
			SessionKey,
			// Token: 0x04000AC2 RID: 2754
			Certificate,
			// Token: 0x04000AC3 RID: 2755
			Aggregate
		}

		// Token: 0x020000B4 RID: 180
		private enum SecItemImportExportFlags
		{
			// Token: 0x04000AC5 RID: 2757
			None,
			// Token: 0x04000AC6 RID: 2758
			PemArmour
		}

		// Token: 0x020000B5 RID: 181
		private struct SecItemImportExportKeyParameters
		{
			// Token: 0x04000AC7 RID: 2759
			public int version;

			// Token: 0x04000AC8 RID: 2760
			public int flags;

			// Token: 0x04000AC9 RID: 2761
			public IntPtr passphrase;

			// Token: 0x04000ACA RID: 2762
			private IntPtr alertTitle;

			// Token: 0x04000ACB RID: 2763
			private IntPtr alertPrompt;

			// Token: 0x04000ACC RID: 2764
			public IntPtr accessRef;

			// Token: 0x04000ACD RID: 2765
			private IntPtr keyUsage;

			// Token: 0x04000ACE RID: 2766
			private IntPtr keyAttributes;
		}
	}
}
