using System;
using System.Collections;
using System.Text;
using Mono.Security.X509;
using Mono.Security.X509.Extensions;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020003B6 RID: 950
	internal class X509ChainImplMono : X509ChainImpl
	{
		// Token: 0x06001CF5 RID: 7413 RVA: 0x000725D4 File Offset: 0x000707D4
		public X509ChainImplMono()
			: this(false)
		{
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x000725DD File Offset: 0x000707DD
		public X509ChainImplMono(bool useMachineContext)
		{
			this.location = (useMachineContext ? StoreLocation.LocalMachine : StoreLocation.CurrentUser);
			this.elements = new X509ChainElementCollection();
			this.policy = new X509ChainPolicy();
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x00072608 File Offset: 0x00070808
		[MonoTODO("Mono's X509Chain is fully managed. All handles are invalid.")]
		public X509ChainImplMono(IntPtr chainContext)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x000027E2 File Offset: 0x000009E2
		public override bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x00070DAB File Offset: 0x0006EFAB
		public override IntPtr Handle
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x00072615 File Offset: 0x00070815
		public override X509ChainElementCollection ChainElements
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x0007261D File Offset: 0x0007081D
		// (set) Token: 0x06001CFC RID: 7420 RVA: 0x00072625 File Offset: 0x00070825
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

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x0007262E File Offset: 0x0007082E
		public override X509ChainStatus[] ChainStatus
		{
			get
			{
				if (this.status == null)
				{
					return X509ChainImplMono.Empty;
				}
				return this.status;
			}
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x00072644 File Offset: 0x00070844
		[MonoTODO("Not totally RFC3280 compliant, but neither is MS implementation...")]
		public override bool Build(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentException("certificate");
			}
			this.Reset();
			global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags x509ChainStatusFlags;
			try
			{
				x509ChainStatusFlags = this.BuildChainFrom(certificate);
				this.ValidateChain(x509ChainStatusFlags);
			}
			catch (CryptographicException ex)
			{
				throw new ArgumentException("certificate", ex);
			}
			global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags x509ChainStatusFlags2 = global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError;
			ArrayList arrayList = new ArrayList();
			foreach (X509ChainElement x509ChainElement in this.elements)
			{
				foreach (X509ChainStatus x509ChainStatus in x509ChainElement.ChainElementStatus)
				{
					if ((x509ChainStatusFlags2 & x509ChainStatus.Status) != x509ChainStatus.Status)
					{
						arrayList.Add(x509ChainStatus);
						x509ChainStatusFlags2 |= x509ChainStatus.Status;
					}
				}
			}
			if (x509ChainStatusFlags != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
			{
				arrayList.Insert(0, new X509ChainStatus(x509ChainStatusFlags));
			}
			this.status = (X509ChainStatus[])arrayList.ToArray(typeof(X509ChainStatus));
			if (this.status.Length == 0 || this.ChainPolicy.VerificationFlags == X509VerificationFlags.AllFlags)
			{
				return true;
			}
			bool flag = true;
			X509ChainStatus[] chainElementStatus = this.status;
			int i = 0;
			while (i < chainElementStatus.Length)
			{
				X509ChainStatus x509ChainStatus2 = chainElementStatus[i];
				global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags x509ChainStatusFlags3 = x509ChainStatus2.Status;
				if (x509ChainStatusFlags3 <= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidNameConstraints)
				{
					if (x509ChainStatusFlags3 <= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot)
					{
						if (x509ChainStatusFlags3 != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NotTimeValid)
						{
							if (x509ChainStatusFlags3 != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NotTimeNested)
							{
								if (x509ChainStatusFlags3 != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot)
								{
									goto IL_02E4;
								}
								goto IL_0216;
							}
							else
							{
								flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreNotTimeNested) > X509VerificationFlags.NoFlag;
							}
						}
						else
						{
							flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreNotTimeValid) > X509VerificationFlags.NoFlag;
						}
					}
					else if (x509ChainStatusFlags3 <= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidPolicyConstraints)
					{
						if (x509ChainStatusFlags3 != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidExtension)
						{
							if (x509ChainStatusFlags3 != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidPolicyConstraints)
							{
								goto IL_02E4;
							}
							goto IL_0274;
						}
						else
						{
							flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreWrongUsage) > X509VerificationFlags.NoFlag;
						}
					}
					else if (x509ChainStatusFlags3 != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidBasicConstraints)
					{
						if (x509ChainStatusFlags3 != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidNameConstraints)
						{
							goto IL_02E4;
						}
						goto IL_028D;
					}
					else
					{
						flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreInvalidBasicConstraints) > X509VerificationFlags.NoFlag;
					}
				}
				else if (x509ChainStatusFlags3 <= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.PartialChain)
				{
					if (x509ChainStatusFlags3 <= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.HasNotPermittedNameConstraint)
					{
						if (x509ChainStatusFlags3 != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.HasNotSupportedNameConstraint && x509ChainStatusFlags3 != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.HasNotPermittedNameConstraint)
						{
							goto IL_02E4;
						}
						goto IL_028D;
					}
					else
					{
						if (x509ChainStatusFlags3 == global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.HasExcludedNameConstraint)
						{
							goto IL_028D;
						}
						if (x509ChainStatusFlags3 != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.PartialChain)
						{
							goto IL_02E4;
						}
						goto IL_0216;
					}
				}
				else if (x509ChainStatusFlags3 <= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.CtlNotSignatureValid)
				{
					if (x509ChainStatusFlags3 != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.CtlNotTimeValid)
					{
						if (x509ChainStatusFlags3 != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.CtlNotSignatureValid)
						{
							goto IL_02E4;
						}
					}
					else
					{
						flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreCtlNotTimeValid) > X509VerificationFlags.NoFlag;
					}
				}
				else if (x509ChainStatusFlags3 != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.CtlNotValidForUsage)
				{
					if (x509ChainStatusFlags3 != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoIssuanceChainPolicy)
					{
						goto IL_02E4;
					}
					goto IL_0274;
				}
				else
				{
					flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreWrongUsage) > X509VerificationFlags.NoFlag;
				}
				IL_02E6:
				if (!flag)
				{
					return false;
				}
				i++;
				continue;
				IL_0216:
				flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.AllowUnknownCertificateAuthority) > X509VerificationFlags.NoFlag;
				goto IL_02E6;
				IL_0274:
				flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreInvalidPolicy) > X509VerificationFlags.NoFlag;
				goto IL_02E6;
				IL_028D:
				flag &= (this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreInvalidName) > X509VerificationFlags.NoFlag;
				goto IL_02E6;
				IL_02E4:
				flag = false;
				goto IL_02E6;
			}
			return true;
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00072960 File Offset: 0x00070B60
		public override void Reset()
		{
			if (this.status != null && this.status.Length != 0)
			{
				this.status = null;
			}
			if (this.elements.Count > 0)
			{
				this.elements.Clear();
			}
			if (this.user_root_store != null)
			{
				this.user_root_store.Close();
				this.user_root_store = null;
			}
			if (this.root_store != null)
			{
				this.root_store.Close();
				this.root_store = null;
			}
			if (this.user_ca_store != null)
			{
				this.user_ca_store.Close();
				this.user_ca_store = null;
			}
			if (this.ca_store != null)
			{
				this.ca_store.Close();
				this.ca_store = null;
			}
			this.roots = null;
			this.cas = null;
			this.collection = null;
			this.bce_restriction = null;
			this.working_public_key = null;
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x00072A2C File Offset: 0x00070C2C
		private X509Certificate2Collection Roots
		{
			get
			{
				if (this.roots == null)
				{
					X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
					global::System.Security.Cryptography.X509Certificates.X509Store lmrootStore = this.LMRootStore;
					if (this.location == StoreLocation.CurrentUser)
					{
						x509Certificate2Collection.AddRange(this.UserRootStore.Certificates);
					}
					x509Certificate2Collection.AddRange(lmrootStore.Certificates);
					this.roots = x509Certificate2Collection;
				}
				return this.roots;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x00072A84 File Offset: 0x00070C84
		private X509Certificate2Collection CertificateAuthorities
		{
			get
			{
				if (this.cas == null)
				{
					X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
					global::System.Security.Cryptography.X509Certificates.X509Store lmcastore = this.LMCAStore;
					if (this.location == StoreLocation.CurrentUser)
					{
						x509Certificate2Collection.AddRange(this.UserCAStore.Certificates);
					}
					x509Certificate2Collection.AddRange(lmcastore.Certificates);
					this.cas = x509Certificate2Collection;
				}
				return this.cas;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x00072ADC File Offset: 0x00070CDC
		private global::System.Security.Cryptography.X509Certificates.X509Store LMRootStore
		{
			get
			{
				if (this.root_store == null)
				{
					this.root_store = new global::System.Security.Cryptography.X509Certificates.X509Store(StoreName.Root, StoreLocation.LocalMachine);
					try
					{
						this.root_store.Open(OpenFlags.OpenExistingOnly);
					}
					catch
					{
					}
				}
				return this.root_store;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x00072B28 File Offset: 0x00070D28
		private global::System.Security.Cryptography.X509Certificates.X509Store UserRootStore
		{
			get
			{
				if (this.user_root_store == null)
				{
					this.user_root_store = new global::System.Security.Cryptography.X509Certificates.X509Store(StoreName.Root, StoreLocation.CurrentUser);
					try
					{
						this.user_root_store.Open(OpenFlags.OpenExistingOnly);
					}
					catch
					{
					}
				}
				return this.user_root_store;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x00072B74 File Offset: 0x00070D74
		private global::System.Security.Cryptography.X509Certificates.X509Store LMCAStore
		{
			get
			{
				if (this.ca_store == null)
				{
					this.ca_store = new global::System.Security.Cryptography.X509Certificates.X509Store(StoreName.CertificateAuthority, StoreLocation.LocalMachine);
					try
					{
						this.ca_store.Open(OpenFlags.OpenExistingOnly);
					}
					catch
					{
					}
				}
				return this.ca_store;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x00072BC0 File Offset: 0x00070DC0
		private global::System.Security.Cryptography.X509Certificates.X509Store UserCAStore
		{
			get
			{
				if (this.user_ca_store == null)
				{
					this.user_ca_store = new global::System.Security.Cryptography.X509Certificates.X509Store(StoreName.CertificateAuthority, StoreLocation.CurrentUser);
					try
					{
						this.user_ca_store.Open(OpenFlags.OpenExistingOnly);
					}
					catch
					{
					}
				}
				return this.user_ca_store;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x00072C0C File Offset: 0x00070E0C
		private X509Certificate2Collection CertificateCollection
		{
			get
			{
				if (this.collection == null)
				{
					this.collection = new X509Certificate2Collection(this.ChainPolicy.ExtraStore);
					this.collection.AddRange(this.Roots);
					this.collection.AddRange(this.CertificateAuthorities);
				}
				return this.collection;
			}
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x00072C60 File Offset: 0x00070E60
		private global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags BuildChainFrom(X509Certificate2 certificate)
		{
			this.elements.Add(certificate);
			while (!this.IsChainComplete(certificate))
			{
				certificate = this.FindParent(certificate);
				if (certificate == null)
				{
					return global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.PartialChain;
				}
				if (this.elements.Contains(certificate))
				{
					return global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.Cyclic;
				}
				this.elements.Add(certificate);
			}
			if (!this.Roots.Contains(certificate))
			{
				this.elements[this.elements.Count - 1].StatusFlags |= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot;
			}
			return global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError;
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x00072CEC File Offset: 0x00070EEC
		private X509Certificate2 SelectBestFromCollection(X509Certificate2 child, X509Certificate2Collection c)
		{
			int num = c.Count;
			if (num == 0)
			{
				return null;
			}
			if (num == 1)
			{
				return c[0];
			}
			X509Certificate2Collection x509Certificate2Collection = c.Find(X509FindType.FindByTimeValid, this.ChainPolicy.VerificationTime, false);
			num = x509Certificate2Collection.Count;
			if (num != 0)
			{
				if (num == 1)
				{
					return x509Certificate2Collection[0];
				}
			}
			else
			{
				x509Certificate2Collection = c;
			}
			string authorityKeyIdentifier = X509ChainImplMono.GetAuthorityKeyIdentifier(child);
			if (string.IsNullOrEmpty(authorityKeyIdentifier))
			{
				return x509Certificate2Collection[0];
			}
			foreach (X509Certificate2 x509Certificate in x509Certificate2Collection)
			{
				string subjectKeyIdentifier = this.GetSubjectKeyIdentifier(x509Certificate);
				if (authorityKeyIdentifier == subjectKeyIdentifier)
				{
					return x509Certificate;
				}
			}
			return x509Certificate2Collection[0];
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x00072D98 File Offset: 0x00070F98
		private X509Certificate2 FindParent(X509Certificate2 certificate)
		{
			X509Certificate2Collection x509Certificate2Collection = this.CertificateCollection.Find(X509FindType.FindBySubjectDistinguishedName, certificate.Issuer, false);
			string authorityKeyIdentifier = X509ChainImplMono.GetAuthorityKeyIdentifier(certificate);
			if (authorityKeyIdentifier != null && authorityKeyIdentifier.Length > 0)
			{
				x509Certificate2Collection.AddRange(this.CertificateCollection.Find(X509FindType.FindBySubjectKeyIdentifier, authorityKeyIdentifier, false));
			}
			X509Certificate2 x509Certificate = this.SelectBestFromCollection(certificate, x509Certificate2Collection);
			if (!certificate.Equals(x509Certificate))
			{
				return x509Certificate;
			}
			return null;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x00072DF8 File Offset: 0x00070FF8
		private bool IsChainComplete(X509Certificate2 certificate)
		{
			if (!this.IsSelfIssued(certificate))
			{
				return false;
			}
			if (certificate.Version < 3)
			{
				return true;
			}
			string subjectKeyIdentifier = this.GetSubjectKeyIdentifier(certificate);
			if (string.IsNullOrEmpty(subjectKeyIdentifier))
			{
				return true;
			}
			string authorityKeyIdentifier = X509ChainImplMono.GetAuthorityKeyIdentifier(certificate);
			return string.IsNullOrEmpty(authorityKeyIdentifier) || authorityKeyIdentifier == subjectKeyIdentifier;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x00072E45 File Offset: 0x00071045
		private bool IsSelfIssued(X509Certificate2 certificate)
		{
			return certificate.Issuer == certificate.Subject;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x00072E58 File Offset: 0x00071058
		private void ValidateChain(global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags flag)
		{
			int num = this.elements.Count - 1;
			X509Certificate2 certificate = this.elements[num].Certificate;
			if ((flag & global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.PartialChain) == global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
			{
				this.Process(num);
				if (num == 0)
				{
					this.elements[0].UncompressFlags();
					return;
				}
				num--;
			}
			this.working_public_key = certificate.PublicKey.Key;
			this.working_issuer_name = certificate.IssuerName;
			this.max_path_length = num;
			for (int i = num; i > 0; i--)
			{
				this.Process(i);
				this.PrepareForNextCertificate(i);
			}
			this.Process(0);
			this.CheckRevocationOnChain(flag);
			this.WrapUp();
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x00072F00 File Offset: 0x00071100
		private void Process(int n)
		{
			X509ChainElement x509ChainElement = this.elements[n];
			X509Certificate2 certificate = x509ChainElement.Certificate;
			if (n != this.elements.Count - 1 && certificate.MonoCertificate.KeyAlgorithm == "1.2.840.10040.4.1" && certificate.MonoCertificate.KeyAlgorithmParameters == null)
			{
				X509Certificate2 certificate2 = this.elements[n + 1].Certificate;
				certificate.MonoCertificate.KeyAlgorithmParameters = certificate2.MonoCertificate.KeyAlgorithmParameters;
			}
			bool flag = this.working_public_key == null;
			if (!this.IsSignedWith(certificate, flag ? certificate.PublicKey.Key : this.working_public_key) && (flag || n != this.elements.Count - 1 || this.IsSelfIssued(certificate)))
			{
				x509ChainElement.StatusFlags |= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NotSignatureValid;
			}
			if (this.ChainPolicy.VerificationTime < certificate.NotBefore || this.ChainPolicy.VerificationTime > certificate.NotAfter)
			{
				x509ChainElement.StatusFlags |= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NotTimeValid;
			}
			if (flag)
			{
				return;
			}
			if (!X500DistinguishedName.AreEqual(certificate.IssuerName, this.working_issuer_name))
			{
				x509ChainElement.StatusFlags |= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidNameConstraints;
			}
			if (!this.IsSelfIssued(certificate))
			{
			}
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x00073044 File Offset: 0x00071244
		private void PrepareForNextCertificate(int n)
		{
			X509ChainElement x509ChainElement = this.elements[n];
			X509Certificate2 certificate = x509ChainElement.Certificate;
			this.working_issuer_name = certificate.SubjectName;
			this.working_public_key = certificate.PublicKey.Key;
			X509BasicConstraintsExtension x509BasicConstraintsExtension = certificate.Extensions["2.5.29.19"] as X509BasicConstraintsExtension;
			if (x509BasicConstraintsExtension != null)
			{
				if (!x509BasicConstraintsExtension.CertificateAuthority)
				{
					x509ChainElement.StatusFlags |= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidBasicConstraints;
				}
			}
			else if (certificate.Version >= 3)
			{
				x509ChainElement.StatusFlags |= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidBasicConstraints;
			}
			if (!this.IsSelfIssued(certificate))
			{
				if (this.max_path_length > 0)
				{
					this.max_path_length--;
				}
				else if (this.bce_restriction != null)
				{
					this.bce_restriction.StatusFlags |= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidBasicConstraints;
				}
			}
			if (x509BasicConstraintsExtension != null && x509BasicConstraintsExtension.HasPathLengthConstraint && x509BasicConstraintsExtension.PathLengthConstraint < this.max_path_length)
			{
				this.max_path_length = x509BasicConstraintsExtension.PathLengthConstraint;
				this.bce_restriction = x509ChainElement;
			}
			X509KeyUsageExtension x509KeyUsageExtension = certificate.Extensions["2.5.29.15"] as X509KeyUsageExtension;
			if (x509KeyUsageExtension != null)
			{
				X509KeyUsageFlags x509KeyUsageFlags = X509KeyUsageFlags.KeyCertSign;
				if ((x509KeyUsageExtension.KeyUsages & x509KeyUsageFlags) != x509KeyUsageFlags)
				{
					x509ChainElement.StatusFlags |= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NotValidForUsage;
				}
			}
			this.ProcessCertificateExtensions(x509ChainElement);
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x00073180 File Offset: 0x00071380
		private void WrapUp()
		{
			X509ChainElement x509ChainElement = this.elements[0];
			X509Certificate2 certificate = x509ChainElement.Certificate;
			this.IsSelfIssued(certificate);
			this.ProcessCertificateExtensions(x509ChainElement);
			for (int i = this.elements.Count - 1; i >= 0; i--)
			{
				this.elements[i].UncompressFlags();
			}
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000731DC File Offset: 0x000713DC
		private void ProcessCertificateExtensions(X509ChainElement element)
		{
			foreach (global::System.Security.Cryptography.X509Certificates.X509Extension x509Extension in element.Certificate.Extensions)
			{
				if (x509Extension.Critical)
				{
					string value = x509Extension.Oid.Value;
					if (!(value == "2.5.29.15") && !(value == "2.5.29.19"))
					{
						element.StatusFlags |= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidExtension;
					}
				}
			}
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x0007324B File Offset: 0x0007144B
		private bool IsSignedWith(X509Certificate2 signed, AsymmetricAlgorithm pubkey)
		{
			return pubkey != null && signed.MonoCertificate.VerifySignature(pubkey);
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x00073260 File Offset: 0x00071460
		private string GetSubjectKeyIdentifier(X509Certificate2 certificate)
		{
			X509SubjectKeyIdentifierExtension x509SubjectKeyIdentifierExtension = certificate.Extensions["2.5.29.14"] as X509SubjectKeyIdentifierExtension;
			if (x509SubjectKeyIdentifierExtension != null)
			{
				return x509SubjectKeyIdentifierExtension.SubjectKeyIdentifier;
			}
			return string.Empty;
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x00073292 File Offset: 0x00071492
		private static string GetAuthorityKeyIdentifier(X509Certificate2 certificate)
		{
			return X509ChainImplMono.GetAuthorityKeyIdentifier(certificate.MonoCertificate.Extensions["2.5.29.35"]);
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x000732AE File Offset: 0x000714AE
		private static string GetAuthorityKeyIdentifier(Mono.Security.X509.X509Crl crl)
		{
			return X509ChainImplMono.GetAuthorityKeyIdentifier(crl.Extensions["2.5.29.35"]);
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x000732C8 File Offset: 0x000714C8
		private static string GetAuthorityKeyIdentifier(Mono.Security.X509.X509Extension ext)
		{
			if (ext == null)
			{
				return string.Empty;
			}
			byte[] identifier = new AuthorityKeyIdentifierExtension(ext).Identifier;
			if (identifier == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in identifier)
			{
				stringBuilder.Append(b.ToString("X02"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x00073328 File Offset: 0x00071528
		private void CheckRevocationOnChain(global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags flag)
		{
			bool flag2 = (flag & global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.PartialChain) > global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError;
			bool flag3;
			switch (this.ChainPolicy.RevocationMode)
			{
			case X509RevocationMode.NoCheck:
				return;
			case X509RevocationMode.Online:
				flag3 = true;
				break;
			case X509RevocationMode.Offline:
				flag3 = false;
				break;
			default:
				throw new InvalidOperationException(global::Locale.GetText("Invalid revocation mode."));
			}
			bool flag4 = flag2;
			for (int i = this.elements.Count - 1; i >= 0; i--)
			{
				bool flag5 = true;
				switch (this.ChainPolicy.RevocationFlag)
				{
				case X509RevocationFlag.EndCertificateOnly:
					flag5 = i == 0;
					break;
				case X509RevocationFlag.EntireChain:
					flag5 = true;
					break;
				case X509RevocationFlag.ExcludeRoot:
					flag5 = i != this.elements.Count - 1;
					break;
				}
				X509ChainElement x509ChainElement = this.elements[i];
				if (!flag4)
				{
					flag4 |= (x509ChainElement.StatusFlags & global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NotSignatureValid) > global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError;
				}
				if (flag4)
				{
					x509ChainElement.StatusFlags |= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.RevocationStatusUnknown;
					x509ChainElement.StatusFlags |= global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.OfflineRevocation;
				}
				else if (flag5 && !flag2 && !this.IsSelfIssued(x509ChainElement.Certificate))
				{
					x509ChainElement.StatusFlags |= this.CheckRevocation(x509ChainElement.Certificate, i + 1, flag3);
					flag4 |= (x509ChainElement.StatusFlags & global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.Revoked) > global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError;
				}
			}
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x00073474 File Offset: 0x00071674
		private global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags CheckRevocation(X509Certificate2 certificate, int ca, bool online)
		{
			global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags x509ChainStatusFlags = global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.RevocationStatusUnknown;
			X509Certificate2 x509Certificate = this.elements[ca].Certificate;
			while (this.IsSelfIssued(x509Certificate) && ca < this.elements.Count - 1)
			{
				x509ChainStatusFlags = this.CheckRevocation(certificate, x509Certificate, online);
				if (x509ChainStatusFlags != global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.RevocationStatusUnknown)
				{
					break;
				}
				ca++;
				x509Certificate = this.elements[ca].Certificate;
			}
			if (x509ChainStatusFlags == global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.RevocationStatusUnknown)
			{
				x509ChainStatusFlags = this.CheckRevocation(certificate, x509Certificate, online);
			}
			return x509ChainStatusFlags;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x000734E8 File Offset: 0x000716E8
		private global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags CheckRevocation(X509Certificate2 certificate, X509Certificate2 ca_cert, bool online)
		{
			X509KeyUsageExtension x509KeyUsageExtension = ca_cert.Extensions["2.5.29.15"] as X509KeyUsageExtension;
			if (x509KeyUsageExtension != null)
			{
				X509KeyUsageFlags x509KeyUsageFlags = X509KeyUsageFlags.CrlSign;
				if ((x509KeyUsageExtension.KeyUsages & x509KeyUsageFlags) != x509KeyUsageFlags)
				{
					return global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.RevocationStatusUnknown;
				}
			}
			Mono.Security.X509.X509Crl x509Crl = this.FindCrl(ca_cert);
			bool flag = x509Crl == null && online;
			if (x509Crl == null)
			{
				return global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.RevocationStatusUnknown;
			}
			if (!x509Crl.VerifySignature(ca_cert.PublicKey.Key))
			{
				return global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.RevocationStatusUnknown;
			}
			Mono.Security.X509.X509Crl.X509CrlEntry crlEntry = x509Crl.GetCrlEntry(certificate.MonoCertificate);
			if (crlEntry != null)
			{
				if (!this.ProcessCrlEntryExtensions(crlEntry))
				{
					return global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.Revoked;
				}
				if (crlEntry.RevocationDate <= this.ChainPolicy.VerificationTime)
				{
					return global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.Revoked;
				}
			}
			if (x509Crl.NextUpdate < this.ChainPolicy.VerificationTime)
			{
				return global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.RevocationStatusUnknown | global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.OfflineRevocation;
			}
			if (!this.ProcessCrlExtensions(x509Crl))
			{
				return global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.RevocationStatusUnknown;
			}
			return global::System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError;
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x000735AC File Offset: 0x000717AC
		private static Mono.Security.X509.X509Crl CheckCrls(string subject, string ski, Mono.Security.X509.X509Store store)
		{
			if (store == null)
			{
				return null;
			}
			foreach (object obj in store.Crls)
			{
				Mono.Security.X509.X509Crl x509Crl = (Mono.Security.X509.X509Crl)obj;
				if (x509Crl.IssuerName == subject && (ski.Length == 0 || ski == X509ChainImplMono.GetAuthorityKeyIdentifier(x509Crl)))
				{
					return x509Crl;
				}
			}
			return null;
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x00073630 File Offset: 0x00071830
		private Mono.Security.X509.X509Crl FindCrl(X509Certificate2 caCertificate)
		{
			string text = caCertificate.SubjectName.Decode(X500DistinguishedNameFlags.None);
			string subjectKeyIdentifier = this.GetSubjectKeyIdentifier(caCertificate);
			Mono.Security.X509.X509Crl x509Crl = X509ChainImplMono.CheckCrls(text, subjectKeyIdentifier, this.LMCAStore.Store);
			if (x509Crl != null)
			{
				return x509Crl;
			}
			if (this.location == StoreLocation.CurrentUser)
			{
				x509Crl = X509ChainImplMono.CheckCrls(text, subjectKeyIdentifier, this.UserCAStore.Store);
				if (x509Crl != null)
				{
					return x509Crl;
				}
			}
			x509Crl = X509ChainImplMono.CheckCrls(text, subjectKeyIdentifier, this.LMRootStore.Store);
			if (x509Crl != null)
			{
				return x509Crl;
			}
			if (this.location == StoreLocation.CurrentUser)
			{
				x509Crl = X509ChainImplMono.CheckCrls(text, subjectKeyIdentifier, this.UserRootStore.Store);
				if (x509Crl != null)
				{
					return x509Crl;
				}
			}
			return null;
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x000736C8 File Offset: 0x000718C8
		private bool ProcessCrlExtensions(Mono.Security.X509.X509Crl crl)
		{
			foreach (object obj in crl.Extensions)
			{
				Mono.Security.X509.X509Extension x509Extension = (Mono.Security.X509.X509Extension)obj;
				if (x509Extension.Critical)
				{
					string oid = x509Extension.Oid;
					if (!(oid == "2.5.29.20") && !(oid == "2.5.29.35"))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x00073750 File Offset: 0x00071950
		private bool ProcessCrlEntryExtensions(Mono.Security.X509.X509Crl.X509CrlEntry entry)
		{
			foreach (object obj in entry.Extensions)
			{
				Mono.Security.X509.X509Extension x509Extension = (Mono.Security.X509.X509Extension)obj;
				if (x509Extension.Critical)
				{
					string oid = x509Extension.Oid;
					if (!(oid == "2.5.29.21"))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x040019A9 RID: 6569
		private StoreLocation location;

		// Token: 0x040019AA RID: 6570
		private X509ChainElementCollection elements;

		// Token: 0x040019AB RID: 6571
		private X509ChainPolicy policy;

		// Token: 0x040019AC RID: 6572
		private X509ChainStatus[] status;

		// Token: 0x040019AD RID: 6573
		private static X509ChainStatus[] Empty = new X509ChainStatus[0];

		// Token: 0x040019AE RID: 6574
		private int max_path_length;

		// Token: 0x040019AF RID: 6575
		private X500DistinguishedName working_issuer_name;

		// Token: 0x040019B0 RID: 6576
		private AsymmetricAlgorithm working_public_key;

		// Token: 0x040019B1 RID: 6577
		private X509ChainElement bce_restriction;

		// Token: 0x040019B2 RID: 6578
		private X509Certificate2Collection roots;

		// Token: 0x040019B3 RID: 6579
		private X509Certificate2Collection cas;

		// Token: 0x040019B4 RID: 6580
		private global::System.Security.Cryptography.X509Certificates.X509Store root_store;

		// Token: 0x040019B5 RID: 6581
		private global::System.Security.Cryptography.X509Certificates.X509Store ca_store;

		// Token: 0x040019B6 RID: 6582
		private global::System.Security.Cryptography.X509Certificates.X509Store user_root_store;

		// Token: 0x040019B7 RID: 6583
		private global::System.Security.Cryptography.X509Certificates.X509Store user_ca_store;

		// Token: 0x040019B8 RID: 6584
		private X509Certificate2Collection collection;
	}
}
