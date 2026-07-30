using System;
using System.Net;
using System.Security.Permissions;
using Mono.Security.X509.Extensions;

namespace Mono.Security.X509
{
	// Token: 0x02000016 RID: 22
	public class X509Chain
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00009D34 File Offset: 0x00007F34
		public X509Chain()
		{
			this.certs = new X509CertificateCollection();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00009D47 File Offset: 0x00007F47
		public X509Chain(X509CertificateCollection chain)
			: this()
		{
			this._chain = new X509CertificateCollection();
			this._chain.AddRange(chain);
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00009D66 File Offset: 0x00007F66
		public X509CertificateCollection Chain
		{
			get
			{
				return this._chain;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00009D6E File Offset: 0x00007F6E
		public X509Certificate Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00009D76 File Offset: 0x00007F76
		public X509ChainStatusFlags Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00009D7E File Offset: 0x00007F7E
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00009DB0 File Offset: 0x00007FB0
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

		// Token: 0x06000125 RID: 293 RVA: 0x00009DB9 File Offset: 0x00007FB9
		public void LoadCertificate(X509Certificate x509)
		{
			this.certs.Add(x509);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00009DC8 File Offset: 0x00007FC8
		public void LoadCertificates(X509CertificateCollection collection)
		{
			this.certs.AddRange(collection);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00009DD8 File Offset: 0x00007FD8
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

		// Token: 0x06000128 RID: 296 RVA: 0x00009E3C File Offset: 0x0000803C
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

		// Token: 0x06000129 RID: 297 RVA: 0x00009FC8 File Offset: 0x000081C8
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

		// Token: 0x0600012A RID: 298 RVA: 0x00009FF6 File Offset: 0x000081F6
		private bool IsValid(X509Certificate cert)
		{
			if (!cert.IsCurrent)
			{
				this._status = X509ChainStatusFlags.NotTimeNested;
				return false;
			}
			bool checkCertificateRevocationList = ServicePointManager.CheckCertificateRevocationList;
			return true;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000A010 File Offset: 0x00008210
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

		// Token: 0x0600012C RID: 300 RVA: 0x0000A070 File Offset: 0x00008270
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

		// Token: 0x0600012D RID: 301 RVA: 0x0000A108 File Offset: 0x00008308
		private bool IsTrusted(X509Certificate potentialTrusted)
		{
			return this.TrustAnchors.Contains(potentialTrusted);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000A118 File Offset: 0x00008318
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

		// Token: 0x040000AF RID: 175
		private X509CertificateCollection roots;

		// Token: 0x040000B0 RID: 176
		private X509CertificateCollection certs;

		// Token: 0x040000B1 RID: 177
		private X509Certificate _root;

		// Token: 0x040000B2 RID: 178
		private X509CertificateCollection _chain;

		// Token: 0x040000B3 RID: 179
		private X509ChainStatusFlags _status;
	}
}
