using System;
using System.Security.Permissions;
using Mono.Security.X509.Extensions;

namespace Mono.Security.X509
{
	// Token: 0x0200005F RID: 95
	internal class X509Chain
	{
		// Token: 0x06000326 RID: 806 RVA: 0x00013C45 File Offset: 0x00011E45
		public X509Chain()
		{
			this.certs = new X509CertificateCollection();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00013C58 File Offset: 0x00011E58
		public X509Chain(X509CertificateCollection chain)
			: this()
		{
			this._chain = new X509CertificateCollection();
			this._chain.AddRange(chain);
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00013C77 File Offset: 0x00011E77
		public X509CertificateCollection Chain
		{
			get
			{
				return this._chain;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00013C7F File Offset: 0x00011E7F
		public X509Certificate Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00013C87 File Offset: 0x00011E87
		public X509ChainStatusFlags Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00013C8F File Offset: 0x00011E8F
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00013CC1 File Offset: 0x00011EC1
		public X509CertificateCollection TrustAnchors
		{
			get
			{
				if (this.roots == null)
				{
					this.roots = new X509CertificateCollection();
					this.roots.AddRange(X509StoreManager.TrustedRootCertificates);
					return this.roots;
				}
				return this.roots;
			}
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
			set
			{
				this.roots = value;
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00013CCA File Offset: 0x00011ECA
		public void LoadCertificate(X509Certificate x509)
		{
			this.certs.Add(x509);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00013CD9 File Offset: 0x00011ED9
		public void LoadCertificates(X509CertificateCollection collection)
		{
			this.certs.AddRange(collection);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00013CE8 File Offset: 0x00011EE8
		public X509Certificate FindByIssuerName(string issuerName)
		{
			foreach (X509Certificate x509Certificate in this.certs)
			{
				if (x509Certificate.IssuerName == issuerName)
				{
					return x509Certificate;
				}
			}
			return null;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00013D4C File Offset: 0x00011F4C
		public bool Build(X509Certificate leaf)
		{
			this._status = X509ChainStatusFlags.NoError;
			if (this._chain == null)
			{
				this._chain = new X509CertificateCollection();
				X509Certificate x509Certificate = leaf;
				X509Certificate x509Certificate2 = x509Certificate;
				while (x509Certificate != null && !x509Certificate.IsSelfSigned)
				{
					x509Certificate2 = x509Certificate;
					this._chain.Add(x509Certificate);
					x509Certificate = this.FindCertificateParent(x509Certificate);
				}
				this._root = this.FindCertificateRoot(x509Certificate2);
			}
			else
			{
				int count = this._chain.Count;
				if (count > 0)
				{
					if (this.IsParent(leaf, this._chain[0]))
					{
						int num = 1;
						while (num < count && this.IsParent(this._chain[num - 1], this._chain[num]))
						{
							num++;
						}
						if (num == count)
						{
							this._root = this.FindCertificateRoot(this._chain[count - 1]);
						}
					}
				}
				else
				{
					this._root = this.FindCertificateRoot(leaf);
				}
			}
			if (this._chain != null && this._status == X509ChainStatusFlags.NoError)
			{
				foreach (X509Certificate x509Certificate3 in this._chain)
				{
					if (!this.IsValid(x509Certificate3))
					{
						return false;
					}
				}
				if (!this.IsValid(leaf))
				{
					if (this._status == X509ChainStatusFlags.NotTimeNested)
					{
						this._status = X509ChainStatusFlags.NotTimeValid;
					}
					return false;
				}
				if (this._root != null && !this.IsValid(this._root))
				{
					return false;
				}
			}
			IL_0161:
			return this._status == X509ChainStatusFlags.NoError;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00013ED8 File Offset: 0x000120D8
		public void Reset()
		{
			this._status = X509ChainStatusFlags.NoError;
			this.roots = null;
			this.certs.Clear();
			if (this._chain != null)
			{
				this._chain.Clear();
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00013F06 File Offset: 0x00012106
		private bool IsValid(X509Certificate cert)
		{
			if (!cert.IsCurrent)
			{
				this._status = X509ChainStatusFlags.NotTimeNested;
				return false;
			}
			return true;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00013F1C File Offset: 0x0001211C
		private X509Certificate FindCertificateParent(X509Certificate child)
		{
			foreach (X509Certificate x509Certificate in this.certs)
			{
				if (this.IsParent(child, x509Certificate))
				{
					return x509Certificate;
				}
			}
			return null;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00013F7C File Offset: 0x0001217C
		private X509Certificate FindCertificateRoot(X509Certificate potentialRoot)
		{
			if (potentialRoot == null)
			{
				this._status = X509ChainStatusFlags.PartialChain;
				return null;
			}
			if (this.IsTrusted(potentialRoot))
			{
				return potentialRoot;
			}
			foreach (X509Certificate x509Certificate in this.TrustAnchors)
			{
				if (this.IsParent(potentialRoot, x509Certificate))
				{
					return x509Certificate;
				}
			}
			if (potentialRoot.IsSelfSigned)
			{
				this._status = X509ChainStatusFlags.UntrustedRoot;
				return potentialRoot;
			}
			this._status = X509ChainStatusFlags.PartialChain;
			return null;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00014014 File Offset: 0x00012214
		private bool IsTrusted(X509Certificate potentialTrusted)
		{
			return this.TrustAnchors.Contains(potentialTrusted);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00014024 File Offset: 0x00012224
		private bool IsParent(X509Certificate child, X509Certificate parent)
		{
			if (child.IssuerName != parent.SubjectName)
			{
				return false;
			}
			if (parent.Version > 2 && !this.IsTrusted(parent))
			{
				X509Extension x509Extension = parent.Extensions["2.5.29.19"];
				if (x509Extension != null)
				{
					if (!new BasicConstraintsExtension(x509Extension).CertificateAuthority)
					{
						this._status = X509ChainStatusFlags.InvalidBasicConstraints;
					}
				}
				else
				{
					this._status = X509ChainStatusFlags.InvalidBasicConstraints;
				}
			}
			if (!child.VerifySignature(parent.RSA))
			{
				this._status = X509ChainStatusFlags.NotSignatureValid;
				return false;
			}
			return true;
		}

		// Token: 0x0400050C RID: 1292
		private X509CertificateCollection roots;

		// Token: 0x0400050D RID: 1293
		private X509CertificateCollection certs;

		// Token: 0x0400050E RID: 1294
		private X509Certificate _root;

		// Token: 0x0400050F RID: 1295
		private X509CertificateCollection _chain;

		// Token: 0x04000510 RID: 1296
		private X509ChainStatusFlags _status;
	}
}
