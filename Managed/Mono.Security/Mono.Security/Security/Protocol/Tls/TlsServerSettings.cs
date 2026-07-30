using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;
using Mono.Security.Protocol.Tls.Handshake;
using Mono.Security.X509;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000051 RID: 81
	internal class TlsServerSettings
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00012DC2 File Offset: 0x00010FC2
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00012DCA File Offset: 0x00010FCA
		public bool ServerKeyExchange
		{
			get
			{
				return this.serverKeyExchange;
			}
			set
			{
				this.serverKeyExchange = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00012DD3 File Offset: 0x00010FD3
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00012DDB File Offset: 0x00010FDB
		public X509CertificateCollection Certificates
		{
			get
			{
				return this.certificates;
			}
			set
			{
				this.certificates = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00012DE4 File Offset: 0x00010FE4
		public RSA CertificateRSA
		{
			get
			{
				return this.certificateRSA;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00012DEC File Offset: 0x00010FEC
		// (set) Token: 0x06000374 RID: 884 RVA: 0x00012DF4 File Offset: 0x00010FF4
		public RSAParameters RsaParameters
		{
			get
			{
				return this.rsaParameters;
			}
			set
			{
				this.rsaParameters = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00012DFD File Offset: 0x00010FFD
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00012E05 File Offset: 0x00011005
		public byte[] SignedParams
		{
			get
			{
				return this.signedParams;
			}
			set
			{
				this.signedParams = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00012E0E File Offset: 0x0001100E
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00012E16 File Offset: 0x00011016
		public bool CertificateRequest
		{
			get
			{
				return this.certificateRequest;
			}
			set
			{
				this.certificateRequest = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00012E1F File Offset: 0x0001101F
		// (set) Token: 0x0600037A RID: 890 RVA: 0x00012E27 File Offset: 0x00011027
		public ClientCertificateType[] CertificateTypes
		{
			get
			{
				return this.certificateTypes;
			}
			set
			{
				this.certificateTypes = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00012E30 File Offset: 0x00011030
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00012E38 File Offset: 0x00011038
		public string[] DistinguisedNames
		{
			get
			{
				return this.distinguisedNames;
			}
			set
			{
				this.distinguisedNames = value;
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00012E4C File Offset: 0x0001104C
		public void UpdateCertificateRSA()
		{
			if (this.certificates == null || this.certificates.Count == 0)
			{
				this.certificateRSA = null;
				return;
			}
			this.certificateRSA = new RSAManaged(this.certificates[0].RSA.KeySize);
			this.certificateRSA.ImportParameters(this.certificates[0].RSA.ExportParameters(false));
		}

		// Token: 0x040001B8 RID: 440
		private X509CertificateCollection certificates;

		// Token: 0x040001B9 RID: 441
		private RSA certificateRSA;

		// Token: 0x040001BA RID: 442
		private RSAParameters rsaParameters;

		// Token: 0x040001BB RID: 443
		private byte[] signedParams;

		// Token: 0x040001BC RID: 444
		private string[] distinguisedNames;

		// Token: 0x040001BD RID: 445
		private bool serverKeyExchange;

		// Token: 0x040001BE RID: 446
		private bool certificateRequest;

		// Token: 0x040001BF RID: 447
		private ClientCertificateType[] certificateTypes;
	}
}
