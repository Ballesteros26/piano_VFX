using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x02000079 RID: 121
	internal abstract class MobileTlsContext : IDisposable
	{
		// Token: 0x0600027B RID: 635 RVA: 0x00007FE4 File Offset: 0x000061E4
		public MobileTlsContext(MobileAuthenticatedStream parent, bool serverMode, string targetHost, SslProtocols enabledProtocols, X509Certificate serverCertificate, X509CertificateCollection clientCertificates, bool askForClientCert)
		{
			this.parent = parent;
			this.serverMode = serverMode;
			this.targetHost = targetHost;
			this.enabledProtocols = enabledProtocols;
			this.serverCertificate = serverCertificate;
			this.clientCertificates = clientCertificates;
			this.askForClientCert = askForClientCert;
			this.serverName = targetHost;
			if (!string.IsNullOrEmpty(this.serverName))
			{
				int num = this.serverName.IndexOf(':');
				if (num > 0)
				{
					this.serverName = this.serverName.Substring(0, num);
				}
			}
			this.certificateValidator = CertificateValidationHelper.GetInternalValidator(parent.Settings, parent.Provider);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000807C File Offset: 0x0000627C
		internal MobileAuthenticatedStream Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00008084 File Offset: 0x00006284
		public MonoTlsSettings Settings
		{
			get
			{
				return this.parent.Settings;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00008091 File Offset: 0x00006291
		public MonoTlsProvider Provider
		{
			get
			{
				return this.parent.Provider;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("MONO_TLS_DEBUG")]
		protected void Debug(string message, params object[] args)
		{
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000280 RID: 640
		public abstract bool HasContext { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000281 RID: 641
		public abstract bool IsAuthenticated { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000809E File Offset: 0x0000629E
		public bool IsServer
		{
			get
			{
				return this.serverMode;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000283 RID: 643 RVA: 0x000080A6 File Offset: 0x000062A6
		protected string TargetHost
		{
			get
			{
				return this.targetHost;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000284 RID: 644 RVA: 0x000080AE File Offset: 0x000062AE
		protected string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000285 RID: 645 RVA: 0x000080B6 File Offset: 0x000062B6
		protected bool AskForClientCertificate
		{
			get
			{
				return this.askForClientCert;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000286 RID: 646 RVA: 0x000080BE File Offset: 0x000062BE
		protected SslProtocols EnabledProtocols
		{
			get
			{
				return this.enabledProtocols;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000287 RID: 647 RVA: 0x000080C6 File Offset: 0x000062C6
		protected X509CertificateCollection ClientCertificates
		{
			get
			{
				return this.clientCertificates;
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000080D0 File Offset: 0x000062D0
		protected void GetProtocolVersions(out TlsProtocolCode min, out TlsProtocolCode max)
		{
			if ((this.enabledProtocols & SslProtocols.Tls) != SslProtocols.None)
			{
				min = TlsProtocolCode.Tls10;
			}
			else if ((this.enabledProtocols & SslProtocols.Tls11) != SslProtocols.None)
			{
				min = TlsProtocolCode.Tls11;
			}
			else
			{
				min = TlsProtocolCode.Tls12;
			}
			if ((this.enabledProtocols & SslProtocols.Tls12) != SslProtocols.None)
			{
				max = TlsProtocolCode.Tls12;
				return;
			}
			if ((this.enabledProtocols & SslProtocols.Tls11) != SslProtocols.None)
			{
				max = TlsProtocolCode.Tls11;
				return;
			}
			max = TlsProtocolCode.Tls10;
		}

		// Token: 0x06000289 RID: 649
		public abstract void StartHandshake();

		// Token: 0x0600028A RID: 650
		public abstract bool ProcessHandshake();

		// Token: 0x0600028B RID: 651
		public abstract void FinishHandshake();

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600028C RID: 652
		public abstract MonoTlsConnectionInfo ConnectionInfo { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00008145 File Offset: 0x00006345
		internal X509Certificate LocalServerCertificate
		{
			get
			{
				return this.serverCertificate;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600028E RID: 654
		internal abstract bool IsRemoteCertificateAvailable { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600028F RID: 655
		internal abstract X509Certificate LocalClientCertificate { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000290 RID: 656
		public abstract X509Certificate RemoteCertificate { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000291 RID: 657
		public abstract TlsProtocols NegotiatedProtocol { get; }

		// Token: 0x06000292 RID: 658
		public abstract void Flush();

		// Token: 0x06000293 RID: 659
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		public abstract ValueTuple<int, bool> Read(byte[] buffer, int offset, int count);

		// Token: 0x06000294 RID: 660
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		public abstract ValueTuple<int, bool> Write(byte[] buffer, int offset, int count);

		// Token: 0x06000295 RID: 661
		public abstract void Shutdown();

		// Token: 0x06000296 RID: 662 RVA: 0x00008150 File Offset: 0x00006350
		protected bool ValidateCertificate(X509Certificate leaf, X509Chain chain)
		{
			ValidationResult validationResult = this.certificateValidator.ValidateCertificate(this.TargetHost, this.IsServer, leaf, chain);
			return validationResult != null && validationResult.Trusted && !validationResult.UserDenied;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00008190 File Offset: 0x00006390
		protected bool ValidateCertificate(X509CertificateCollection certificates)
		{
			ValidationResult validationResult = this.certificateValidator.ValidateCertificate(this.TargetHost, this.IsServer, certificates);
			return validationResult != null && validationResult.Trusted && !validationResult.UserDenied;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000081CC File Offset: 0x000063CC
		protected X509Certificate SelectClientCertificate(X509Certificate serverCertificate, string[] acceptableIssuers)
		{
			X509Certificate x509Certificate;
			if (this.certificateValidator.SelectClientCertificate(this.TargetHost, this.ClientCertificates, serverCertificate, acceptableIssuers, out x509Certificate))
			{
				return x509Certificate;
			}
			if (this.clientCertificates == null || this.clientCertificates.Count == 0)
			{
				return null;
			}
			if (this.clientCertificates.Count == 1)
			{
				return this.clientCertificates[0];
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000822F File Offset: 0x0000642F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000027E8 File Offset: 0x000009E8
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00008240 File Offset: 0x00006440
		~MobileTlsContext()
		{
			this.Dispose(false);
		}

		// Token: 0x040007E6 RID: 2022
		private MobileAuthenticatedStream parent;

		// Token: 0x040007E7 RID: 2023
		private bool serverMode;

		// Token: 0x040007E8 RID: 2024
		private string targetHost;

		// Token: 0x040007E9 RID: 2025
		private string serverName;

		// Token: 0x040007EA RID: 2026
		private SslProtocols enabledProtocols;

		// Token: 0x040007EB RID: 2027
		private X509Certificate serverCertificate;

		// Token: 0x040007EC RID: 2028
		private X509CertificateCollection clientCertificates;

		// Token: 0x040007ED RID: 2029
		private bool askForClientCert;

		// Token: 0x040007EE RID: 2030
		private ICertificateValidator2 certificateValidator;
	}
}
