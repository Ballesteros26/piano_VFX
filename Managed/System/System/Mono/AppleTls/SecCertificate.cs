using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Net;
using ObjCRuntimeInternal;

namespace Mono.AppleTls
{
	// Token: 0x020000AA RID: 170
	internal class SecCertificate : INativeObject, IDisposable
	{
		// Token: 0x0600041A RID: 1050 RVA: 0x0000D43E File Offset: 0x0000B63E
		internal SecCertificate(IntPtr handle, bool owns = false)
		{
			if (handle == IntPtr.Zero)
			{
				throw new Exception("Invalid handle");
			}
			this.handle = handle;
			if (!owns)
			{
				CFObject.CFRetain(handle);
			}
		}

		// Token: 0x0600041B RID: 1051
		[DllImport("/System/Library/Frameworks/Security.framework/Security", EntryPoint = "SecCertificateGetTypeID")]
		public static extern IntPtr GetTypeID();

		// Token: 0x0600041C RID: 1052
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecCertificateCreateWithData(IntPtr allocator, IntPtr cfData);

		// Token: 0x0600041D RID: 1053 RVA: 0x0000D470 File Offset: 0x0000B670
		public SecCertificate(X509Certificate certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			this.handle = certificate.Impl.GetNativeAppleCertificate();
			if (this.handle != IntPtr.Zero)
			{
				CFObject.CFRetain(this.handle);
				return;
			}
			using (CFData cfdata = CFData.FromData(certificate.GetRawCertData()))
			{
				this.Initialize(cfdata);
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000D4F0 File Offset: 0x0000B6F0
		internal SecCertificate(X509CertificateImpl impl)
		{
			this.handle = impl.GetNativeAppleCertificate();
			if (this.handle != IntPtr.Zero)
			{
				CFObject.CFRetain(this.handle);
				return;
			}
			using (CFData cfdata = CFData.FromData(impl.GetRawCertData()))
			{
				this.Initialize(cfdata);
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000D560 File Offset: 0x0000B760
		private void Initialize(CFData data)
		{
			this.handle = SecCertificate.SecCertificateCreateWithData(IntPtr.Zero, data.Handle);
			if (this.handle == IntPtr.Zero)
			{
				throw new ArgumentException("Not a valid DER-encoded X.509 certificate");
			}
		}

		// Token: 0x06000420 RID: 1056
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecCertificateCopySubjectSummary(IntPtr cert);

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000D598 File Offset: 0x0000B798
		public string SubjectSummary
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					throw new ObjectDisposedException("SecCertificate");
				}
				IntPtr intPtr = IntPtr.Zero;
				string text;
				try
				{
					intPtr = SecCertificate.SecCertificateCopySubjectSummary(this.handle);
					text = CFString.AsString(intPtr);
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						CFObject.CFRelease(intPtr);
					}
				}
				return text;
			}
		}

		// Token: 0x06000422 RID: 1058
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecCertificateCopyData(IntPtr cert);

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000D60C File Offset: 0x0000B80C
		public CFData DerData
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					throw new ObjectDisposedException("SecCertificate");
				}
				IntPtr intPtr = SecCertificate.SecCertificateCopyData(this.handle);
				if (intPtr == IntPtr.Zero)
				{
					throw new ArgumentException("Not a valid certificate");
				}
				return new CFData(intPtr, true);
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000D65F File Offset: 0x0000B85F
		public X509Certificate ToX509Certificate()
		{
			if (this.handle == IntPtr.Zero)
			{
				throw new ObjectDisposedException("SecCertificate");
			}
			return new X509Certificate(this.handle);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000D68C File Offset: 0x0000B88C
		internal static bool Equals(SecCertificate first, SecCertificate second)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			if (first.Handle == second.Handle)
			{
				return true;
			}
			bool flag;
			using (CFData derData = first.DerData)
			{
				using (CFData derData2 = second.DerData)
				{
					if (derData.Handle == derData2.Handle)
					{
						flag = true;
					}
					else if (derData.Length != derData2.Length)
					{
						flag = false;
					}
					else
					{
						IntPtr length = derData.Length;
						for (long num = 0L; num < (long)length; num += 1L)
						{
							if (derData[num] != derData2[num])
							{
								return false;
							}
						}
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000D770 File Offset: 0x0000B970
		~SecCertificate()
		{
			this.Dispose(false);
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000D7A8 File Offset: 0x0000B9A8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000D7B7 File Offset: 0x0000B9B7
		protected virtual void Dispose(bool disposing)
		{
			if (this.handle != IntPtr.Zero)
			{
				CFObject.CFRelease(this.handle);
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x0400092B RID: 2347
		internal IntPtr handle;
	}
}
