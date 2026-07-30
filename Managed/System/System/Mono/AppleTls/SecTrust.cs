using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Net;
using ObjCRuntimeInternal;

namespace Mono.AppleTls
{
	// Token: 0x020000CA RID: 202
	internal class SecTrust : INativeObject, IDisposable
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x0000E731 File Offset: 0x0000C931
		internal SecTrust(IntPtr handle, bool owns = false)
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

		// Token: 0x06000489 RID: 1161
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SecStatusCode SecTrustCreateWithCertificates(IntPtr certOrCertArray, IntPtr policies, out IntPtr sectrustref);

		// Token: 0x0600048A RID: 1162 RVA: 0x0000E764 File Offset: 0x0000C964
		public SecTrust(X509CertificateCollection certificates, SecPolicy policy)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			SecCertificate[] array = new SecCertificate[certificates.Count];
			int i = 0;
			foreach (X509Certificate x509Certificate in certificates)
			{
				array[i++] = new SecCertificate(x509Certificate);
			}
			this.Initialize(array, policy);
			for (i = 0; i < array.Length; i++)
			{
				array[i].Dispose();
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000E7FC File Offset: 0x0000C9FC
		private void Initialize(SecCertificate[] array, SecPolicy policy)
		{
			using (CFArray cfarray = CFArray.CreateArray(array))
			{
				this.Initialize(cfarray.Handle, policy);
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000E83C File Offset: 0x0000CA3C
		private void Initialize(IntPtr certHandle, SecPolicy policy)
		{
			SecStatusCode secStatusCode = SecTrust.SecTrustCreateWithCertificates(certHandle, (policy == null) ? IntPtr.Zero : policy.Handle, out this.handle);
			if (secStatusCode != SecStatusCode.Success)
			{
				throw new ArgumentException(secStatusCode.ToString());
			}
		}

		// Token: 0x0600048D RID: 1165
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SecStatusCode SecTrustEvaluate(IntPtr trust, out SecTrustResult result);

		// Token: 0x0600048E RID: 1166 RVA: 0x0000E87C File Offset: 0x0000CA7C
		public SecTrustResult Evaluate()
		{
			if (this.handle == IntPtr.Zero)
			{
				throw new ObjectDisposedException("SecTrust");
			}
			SecTrustResult secTrustResult;
			SecStatusCode secStatusCode = SecTrust.SecTrustEvaluate(this.handle, out secTrustResult);
			if (secStatusCode != SecStatusCode.Success)
			{
				throw new InvalidOperationException(secStatusCode.ToString());
			}
			return secTrustResult;
		}

		// Token: 0x0600048F RID: 1167
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecTrustGetCertificateCount(IntPtr trust);

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000E8CB File Offset: 0x0000CACB
		public int Count
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					return 0;
				}
				return (int)SecTrust.SecTrustGetCertificateCount(this.handle);
			}
		}

		// Token: 0x06000491 RID: 1169
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecTrustGetCertificateAtIndex(IntPtr trust, IntPtr ix);

		// Token: 0x170000EC RID: 236
		public SecCertificate this[IntPtr index]
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					throw new ObjectDisposedException("SecTrust");
				}
				if ((long)index < 0L || (long)index >= (long)this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return new SecCertificate(SecTrust.SecTrustGetCertificateAtIndex(this.handle, index), false);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000E954 File Offset: 0x0000CB54
		internal X509Certificate GetCertificate(int index)
		{
			if (this.handle == IntPtr.Zero)
			{
				throw new ObjectDisposedException("SecTrust");
			}
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return new X509Certificate(SecTrust.SecTrustGetCertificateAtIndex(this.handle, (IntPtr)index));
		}

		// Token: 0x06000494 RID: 1172
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SecStatusCode SecTrustSetAnchorCertificates(IntPtr trust, IntPtr anchorCertificates);

		// Token: 0x06000495 RID: 1173 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		public SecStatusCode SetAnchorCertificates(X509CertificateCollection certificates)
		{
			if (this.handle == IntPtr.Zero)
			{
				throw new ObjectDisposedException("SecTrust");
			}
			if (certificates == null)
			{
				return SecTrust.SecTrustSetAnchorCertificates(this.handle, IntPtr.Zero);
			}
			SecCertificate[] array = new SecCertificate[certificates.Count];
			int num = 0;
			foreach (X509Certificate x509Certificate in certificates)
			{
				array[num++] = new SecCertificate(x509Certificate);
			}
			return this.SetAnchorCertificates(array);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000EA4C File Offset: 0x0000CC4C
		public SecStatusCode SetAnchorCertificates(SecCertificate[] array)
		{
			if (array == null)
			{
				return SecTrust.SecTrustSetAnchorCertificates(this.handle, IntPtr.Zero);
			}
			SecStatusCode secStatusCode;
			using (CFArray cfarray = CFArray.FromNativeObjects(array))
			{
				secStatusCode = SecTrust.SecTrustSetAnchorCertificates(this.handle, cfarray.Handle);
			}
			return secStatusCode;
		}

		// Token: 0x06000497 RID: 1175
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SecStatusCode SecTrustSetAnchorCertificatesOnly(IntPtr trust, bool anchorCertificatesOnly);

		// Token: 0x06000498 RID: 1176 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
		public SecStatusCode SetAnchorCertificatesOnly(bool anchorCertificatesOnly)
		{
			if (this.handle == IntPtr.Zero)
			{
				throw new ObjectDisposedException("SecTrust");
			}
			return SecTrust.SecTrustSetAnchorCertificatesOnly(this.handle, anchorCertificatesOnly);
		}

		// Token: 0x06000499 RID: 1177
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SecStatusCode SecTrustSetVerifyDate(IntPtr trust, IntPtr date);

		// Token: 0x0600049A RID: 1178 RVA: 0x0000EAD0 File Offset: 0x0000CCD0
		public SecStatusCode SetVerifyDate(DateTime date)
		{
			SecStatusCode secStatusCode;
			using (CFDate cfdate = CFDate.Create(date))
			{
				secStatusCode = SecTrust.SecTrustSetVerifyDate(this.handle, cfdate.Handle);
			}
			return secStatusCode;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000EB14 File Offset: 0x0000CD14
		~SecTrust()
		{
			this.Dispose(false);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000EB44 File Offset: 0x0000CD44
		protected virtual void Dispose(bool disposing)
		{
			if (this.handle != IntPtr.Zero)
			{
				CFObject.CFRelease(this.handle);
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000EB6E File Offset: 0x0000CD6E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000EB7D File Offset: 0x0000CD7D
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x04000B84 RID: 2948
		private IntPtr handle;
	}
}
