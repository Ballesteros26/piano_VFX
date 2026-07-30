using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents the chain policy to be applied when building an X509 certificate chain. This class cannot be inherited.</summary>
	// Token: 0x020003B7 RID: 951
	public sealed class X509ChainPolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> class. </summary>
		// Token: 0x06001D1E RID: 7454 RVA: 0x000737D5 File Offset: 0x000719D5
		public X509ChainPolicy()
		{
			this.Reset();
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x000737E3 File Offset: 0x000719E3
		internal X509ChainPolicy(X509CertificateCollection store)
		{
			this.store = store;
			this.Reset();
		}

		/// <summary>Gets a collection of object identifiers (OIDs) specifying which application policies or enhanced key usages (EKUs) the certificate supports.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidCollection" />  object.</returns>
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x000737F8 File Offset: 0x000719F8
		public OidCollection ApplicationPolicy
		{
			get
			{
				return this.apps;
			}
		}

		/// <summary>Gets a collection of object identifiers (OIDs) specifying which certificate policies the certificate supports.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidCollection" /> object.</returns>
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x00073800 File Offset: 0x00071A00
		public OidCollection CertificatePolicy
		{
			get
			{
				return this.cert;
			}
		}

		/// <summary>Represents an additional collection of certificates that can be searched by the chaining engine when validating a certificate chain.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x00073808 File Offset: 0x00071A08
		// (set) Token: 0x06001D23 RID: 7459 RVA: 0x00073890 File Offset: 0x00071A90
		public X509Certificate2Collection ExtraStore
		{
			get
			{
				if (this.store2 != null)
				{
					return this.store2;
				}
				this.store2 = new X509Certificate2Collection();
				if (this.store != null)
				{
					foreach (X509Certificate x509Certificate in this.store)
					{
						this.store2.Add(new X509Certificate2(x509Certificate));
					}
				}
				return this.store2;
			}
			internal set
			{
				this.store2 = value;
			}
		}

		/// <summary>Gets or sets values for X509 revocation flags.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationFlag" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationFlag" /> value supplied is not a valid flag. </exception>
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x00073899 File Offset: 0x00071A99
		// (set) Token: 0x06001D25 RID: 7461 RVA: 0x000738A1 File Offset: 0x00071AA1
		public X509RevocationFlag RevocationFlag
		{
			get
			{
				return this.rflag;
			}
			set
			{
				if (value < X509RevocationFlag.EndCertificateOnly || value > X509RevocationFlag.ExcludeRoot)
				{
					throw new ArgumentException("RevocationFlag");
				}
				this.rflag = value;
			}
		}

		/// <summary>Gets or sets values for X509 certificate revocation mode.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationMode" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationMode" /> value supplied is not a valid flag. </exception>
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001D26 RID: 7462 RVA: 0x000738BD File Offset: 0x00071ABD
		// (set) Token: 0x06001D27 RID: 7463 RVA: 0x000738C5 File Offset: 0x00071AC5
		public X509RevocationMode RevocationMode
		{
			get
			{
				return this.mode;
			}
			set
			{
				if (value < X509RevocationMode.NoCheck || value > X509RevocationMode.Offline)
				{
					throw new ArgumentException("RevocationMode");
				}
				this.mode = value;
			}
		}

		/// <summary>Gets the time span that elapsed during online revocation verification or downloading the certificate revocation list (CRL).</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object.</returns>
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001D28 RID: 7464 RVA: 0x000738E1 File Offset: 0x00071AE1
		// (set) Token: 0x06001D29 RID: 7465 RVA: 0x000738E9 File Offset: 0x00071AE9
		public TimeSpan UrlRetrievalTimeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		/// <summary>Gets verification flags for the certificate.</summary>
		/// <returns>A value from the <see cref="T:System.Security.Cryptography.X509Certificates.X509VerificationFlags" /> enumeration.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509VerificationFlags" /> value supplied is not a valid flag. <see cref="F:System.Security.Cryptography.X509Certificates.X509VerificationFlags.NoFlag" /> is the default value. </exception>
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x000738F2 File Offset: 0x00071AF2
		// (set) Token: 0x06001D2B RID: 7467 RVA: 0x000738FA File Offset: 0x00071AFA
		public X509VerificationFlags VerificationFlags
		{
			get
			{
				return this.vflags;
			}
			set
			{
				if ((value | X509VerificationFlags.AllFlags) != X509VerificationFlags.AllFlags)
				{
					throw new ArgumentException("VerificationFlags");
				}
				this.vflags = value;
			}
		}

		/// <summary>The time that the certificate was verified expressed in local time.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001D2C RID: 7468 RVA: 0x0007391C File Offset: 0x00071B1C
		// (set) Token: 0x06001D2D RID: 7469 RVA: 0x00073924 File Offset: 0x00071B24
		public DateTime VerificationTime
		{
			get
			{
				return this.vtime;
			}
			set
			{
				this.vtime = value;
			}
		}

		/// <summary>Resets the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> members to their default values.</summary>
		// Token: 0x06001D2E RID: 7470 RVA: 0x00073930 File Offset: 0x00071B30
		public void Reset()
		{
			this.apps = new OidCollection();
			this.cert = new OidCollection();
			this.store2 = null;
			this.rflag = X509RevocationFlag.ExcludeRoot;
			this.mode = X509RevocationMode.Online;
			this.timeout = TimeSpan.Zero;
			this.vflags = X509VerificationFlags.NoFlag;
			this.vtime = DateTime.Now;
		}

		// Token: 0x040019B9 RID: 6585
		private OidCollection apps;

		// Token: 0x040019BA RID: 6586
		private OidCollection cert;

		// Token: 0x040019BB RID: 6587
		private X509CertificateCollection store;

		// Token: 0x040019BC RID: 6588
		private X509Certificate2Collection store2;

		// Token: 0x040019BD RID: 6589
		private X509RevocationFlag rflag;

		// Token: 0x040019BE RID: 6590
		private X509RevocationMode mode;

		// Token: 0x040019BF RID: 6591
		private TimeSpan timeout;

		// Token: 0x040019C0 RID: 6592
		private X509VerificationFlags vflags;

		// Token: 0x040019C1 RID: 6593
		private DateTime vtime;
	}
}
