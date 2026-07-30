using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Unity
{
	// Token: 0x0200004B RID: 75
	internal class X509ChainImplUnityTls : X509ChainImpl
	{
		// Token: 0x0600011D RID: 285 RVA: 0x000040BC File Offset: 0x000022BC
		internal X509ChainImplUnityTls(UnityTls.unitytls_x509list_ref nativeCertificateChain)
		{
			this.elements = null;
			this.nativeCertificateChain = nativeCertificateChain;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000040DD File Offset: 0x000022DD
		public override bool IsValid
		{
			get
			{
				return this.nativeCertificateChain.handle != UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000040F9 File Offset: 0x000022F9
		public override IntPtr Handle
		{
			get
			{
				return new IntPtr((long)this.nativeCertificateChain.handle);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000410B File Offset: 0x0000230B
		internal UnityTls.unitytls_x509list_ref NativeCertificateChain
		{
			get
			{
				return this.nativeCertificateChain;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004114 File Offset: 0x00002314
		public unsafe override X509ChainElementCollection ChainElements
		{
			get
			{
				base.ThrowIfContextInvalid();
				if (this.elements != null)
				{
					return this.elements;
				}
				this.elements = new X509ChainElementCollection();
				UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
				UnityTls.unitytls_x509_ref unitytls_x509_ref = UnityTls.NativeInterface.unitytls_x509list_get_x509(this.nativeCertificateChain, (IntPtr)0, &unitytls_errorstate);
				int num = 0;
				while (unitytls_x509_ref.handle != UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE)
				{
					IntPtr intPtr = UnityTls.NativeInterface.unitytls_x509_export_der(unitytls_x509_ref, null, (IntPtr)0, &unitytls_errorstate);
					byte[] array = new byte[(int)intPtr];
					byte[] array2;
					byte* ptr;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array2[0];
					}
					UnityTls.NativeInterface.unitytls_x509_export_der(unitytls_x509_ref, ptr, intPtr, &unitytls_errorstate);
					array2 = null;
					this.elements.Add(new X509Certificate2(array));
					unitytls_x509_ref = UnityTls.NativeInterface.unitytls_x509list_get_x509(this.nativeCertificateChain, (IntPtr)num, &unitytls_errorstate);
					num++;
				}
				return this.elements;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004228 File Offset: 0x00002428
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00004230 File Offset: 0x00002430
		public override X509ChainPolicy ChainPolicy
		{
			get
			{
				return this.policy;
			}
			set
			{
				this.policy = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00004239 File Offset: 0x00002439
		public override X509ChainStatus[] ChainStatus
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004240 File Offset: 0x00002440
		public override bool Build(X509Certificate2 certificate)
		{
			return false;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004243 File Offset: 0x00002443
		public override void Reset()
		{
			if (this.elements != null)
			{
				this.nativeCertificateChain.handle = UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE;
				this.elements.Clear();
				this.elements = null;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004274 File Offset: 0x00002474
		protected override void Dispose(bool disposing)
		{
			this.Reset();
			base.Dispose(disposing);
		}

		// Token: 0x0400073F RID: 1855
		private X509ChainElementCollection elements;

		// Token: 0x04000740 RID: 1856
		private UnityTls.unitytls_x509list_ref nativeCertificateChain;

		// Token: 0x04000741 RID: 1857
		private X509ChainPolicy policy = new X509ChainPolicy();
	}
}
