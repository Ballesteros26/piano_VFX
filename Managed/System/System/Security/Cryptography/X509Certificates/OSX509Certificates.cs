using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020003A4 RID: 932
	internal static class OSX509Certificates
	{
		// Token: 0x06001BD3 RID: 7123
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecCertificateCreateWithData(IntPtr allocator, IntPtr nsdataRef);

		// Token: 0x06001BD4 RID: 7124
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern int SecTrustCreateWithCertificates(IntPtr certOrCertArray, IntPtr policies, out IntPtr sectrustref);

		// Token: 0x06001BD5 RID: 7125
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern int SecTrustSetAnchorCertificates(IntPtr trust, IntPtr anchorCertificates);

		// Token: 0x06001BD6 RID: 7126
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecPolicyCreateSSL([MarshalAs(UnmanagedType.I1)] bool server, IntPtr cfStringHostname);

		// Token: 0x06001BD7 RID: 7127
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern int SecTrustEvaluate(IntPtr secTrustRef, out OSX509Certificates.SecTrustResult secTrustResultTime);

		// Token: 0x06001BD8 RID: 7128
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CharSet = CharSet.Unicode)]
		private static extern IntPtr CFStringCreateWithCharacters(IntPtr allocator, string str, IntPtr count);

		// Token: 0x06001BD9 RID: 7129
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private unsafe static extern IntPtr CFDataCreate(IntPtr allocator, byte* bytes, IntPtr length);

		// Token: 0x06001BDA RID: 7130
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRetain(IntPtr handle);

		// Token: 0x06001BDB RID: 7131
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRelease(IntPtr handle);

		// Token: 0x06001BDC RID: 7132
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFArrayCreate(IntPtr allocator, IntPtr values, IntPtr numValues, IntPtr callbacks);

		// Token: 0x06001BDD RID: 7133 RVA: 0x0006EE20 File Offset: 0x0006D020
		private unsafe static IntPtr MakeCFData(byte[] data)
		{
			fixed (byte* ptr = &data[0])
			{
				byte* ptr2 = ptr;
				return OSX509Certificates.CFDataCreate(IntPtr.Zero, ptr2, (IntPtr)data.Length);
			}
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x0006EE4C File Offset: 0x0006D04C
		private unsafe static IntPtr FromIntPtrs(IntPtr[] values)
		{
			IntPtr* ptr;
			if (values == null || values.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &values[0];
			}
			return OSX509Certificates.CFArrayCreate(IntPtr.Zero, (IntPtr)((void*)ptr), (IntPtr)values.Length, IntPtr.Zero);
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x0006EE90 File Offset: 0x0006D090
		private static IntPtr GetCertificate(X509Certificate certificate)
		{
			IntPtr intPtr = certificate.Impl.GetNativeAppleCertificate();
			if (intPtr != IntPtr.Zero)
			{
				OSX509Certificates.CFRetain(intPtr);
				return intPtr;
			}
			IntPtr intPtr2 = OSX509Certificates.MakeCFData(certificate.GetRawCertData());
			intPtr = OSX509Certificates.SecCertificateCreateWithData(IntPtr.Zero, intPtr2);
			OSX509Certificates.CFRelease(intPtr2);
			return intPtr;
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x0006EEE0 File Offset: 0x0006D0E0
		public static OSX509Certificates.SecTrustResult TrustEvaluateSsl(X509CertificateCollection certificates, X509CertificateCollection anchors, string host)
		{
			if (certificates == null)
			{
				return OSX509Certificates.SecTrustResult.Deny;
			}
			OSX509Certificates.SecTrustResult secTrustResult;
			try
			{
				secTrustResult = OSX509Certificates._TrustEvaluateSsl(certificates, anchors, host);
			}
			catch
			{
				secTrustResult = OSX509Certificates.SecTrustResult.Deny;
			}
			return secTrustResult;
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x0006EF14 File Offset: 0x0006D114
		private static OSX509Certificates.SecTrustResult _TrustEvaluateSsl(X509CertificateCollection certificates, X509CertificateCollection anchors, string hostName)
		{
			int count = certificates.Count;
			int num = ((anchors != null) ? anchors.Count : 0);
			IntPtr[] array = new IntPtr[count];
			IntPtr[] array2 = new IntPtr[num];
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			IntPtr intPtr4 = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			OSX509Certificates.SecTrustResult secTrustResult = OSX509Certificates.SecTrustResult.Deny;
			OSX509Certificates.SecTrustResult secTrustResult2;
			try
			{
				for (int i = 0; i < count; i++)
				{
					array[i] = OSX509Certificates.GetCertificate(certificates[i]);
					if (array[i] == IntPtr.Zero)
					{
						return OSX509Certificates.SecTrustResult.Deny;
					}
				}
				for (int j = 0; j < num; j++)
				{
					array2[j] = OSX509Certificates.GetCertificate(anchors[j]);
					if (array2[j] == IntPtr.Zero)
					{
						return OSX509Certificates.SecTrustResult.Deny;
					}
				}
				intPtr = OSX509Certificates.FromIntPtrs(array);
				if (hostName != null)
				{
					intPtr4 = OSX509Certificates.CFStringCreateWithCharacters(IntPtr.Zero, hostName, (IntPtr)hostName.Length);
				}
				intPtr3 = OSX509Certificates.SecPolicyCreateSSL(true, intPtr4);
				if (OSX509Certificates.SecTrustCreateWithCertificates(intPtr, intPtr3, out zero) != 0)
				{
					secTrustResult2 = OSX509Certificates.SecTrustResult.Deny;
				}
				else
				{
					if (num > 0)
					{
						intPtr2 = OSX509Certificates.FromIntPtrs(array2);
						OSX509Certificates.SecTrustSetAnchorCertificates(zero, intPtr2);
					}
					OSX509Certificates.SecTrustEvaluate(zero, out secTrustResult);
					secTrustResult2 = secTrustResult;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					OSX509Certificates.CFRelease(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					OSX509Certificates.CFRelease(intPtr2);
				}
				for (int k = 0; k < count; k++)
				{
					if (array[k] != IntPtr.Zero)
					{
						OSX509Certificates.CFRelease(array[k]);
					}
				}
				for (int l = 0; l < num; l++)
				{
					if (array2[l] != IntPtr.Zero)
					{
						OSX509Certificates.CFRelease(array2[l]);
					}
				}
				if (intPtr3 != IntPtr.Zero)
				{
					OSX509Certificates.CFRelease(intPtr3);
				}
				if (intPtr4 != IntPtr.Zero)
				{
					OSX509Certificates.CFRelease(intPtr4);
				}
				if (zero != IntPtr.Zero)
				{
					OSX509Certificates.CFRelease(zero);
				}
			}
			return secTrustResult2;
		}

		// Token: 0x04001975 RID: 6517
		public const string SecurityLibrary = "/System/Library/Frameworks/Security.framework/Security";

		// Token: 0x04001976 RID: 6518
		public const string CoreFoundationLibrary = "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

		// Token: 0x020003A5 RID: 933
		public enum SecTrustResult
		{
			// Token: 0x04001978 RID: 6520
			Invalid,
			// Token: 0x04001979 RID: 6521
			Proceed,
			// Token: 0x0400197A RID: 6522
			Confirm,
			// Token: 0x0400197B RID: 6523
			Deny,
			// Token: 0x0400197C RID: 6524
			Unspecified,
			// Token: 0x0400197D RID: 6525
			RecoverableTrustFailure,
			// Token: 0x0400197E RID: 6526
			FatalTrustFailure,
			// Token: 0x0400197F RID: 6527
			ResultOtherError
		}
	}
}
