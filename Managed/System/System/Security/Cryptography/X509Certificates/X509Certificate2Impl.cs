using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020003AC RID: 940
	internal abstract class X509Certificate2Impl : X509CertificateImpl
	{
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001C56 RID: 7254
		// (set) Token: 0x06001C57 RID: 7255
		public abstract bool Archived { get; set; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001C58 RID: 7256
		public abstract X509ExtensionCollection Extensions { get; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001C59 RID: 7257
		public abstract bool HasPrivateKey { get; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001C5A RID: 7258
		public abstract X500DistinguishedName IssuerName { get; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001C5B RID: 7259
		// (set) Token: 0x06001C5C RID: 7260
		public abstract AsymmetricAlgorithm PrivateKey { get; set; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001C5D RID: 7261
		public abstract PublicKey PublicKey { get; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001C5E RID: 7262
		public abstract Oid SignatureAlgorithm { get; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001C5F RID: 7263
		public abstract X500DistinguishedName SubjectName { get; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001C60 RID: 7264
		public abstract int Version { get; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001C61 RID: 7265
		internal abstract X509CertificateImplCollection IntermediateCertificates { get; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001C62 RID: 7266
		internal abstract X509Certificate2Impl FallbackImpl { get; }

		// Token: 0x06001C63 RID: 7267
		public abstract string GetNameInfo(X509NameType nameType, bool forIssuer);

		// Token: 0x06001C64 RID: 7268
		public abstract void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags);

		// Token: 0x06001C65 RID: 7269
		public abstract byte[] Export(X509ContentType contentType, string password);

		// Token: 0x06001C66 RID: 7270
		public abstract bool Verify(X509Certificate2 thisCertificate);

		// Token: 0x06001C67 RID: 7271
		public abstract void Reset();
	}
}
