using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Provides the Authenticode X.509v3 digital signature of a code assembly as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x02000578 RID: 1400
	[ComVisible(true)]
	[Serializable]
	public sealed class Publisher : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Publisher" /> class with the Authenticode X.509v3 certificate containing the publisher's public key.</summary>
		/// <param name="cert">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> that contains the software publisher's public key. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cert" /> parameter is null. </exception>
		// Token: 0x06003EB4 RID: 16052 RVA: 0x000E0F14 File Offset: 0x000DF114
		public Publisher(X509Certificate cert)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			if (cert.GetHashCode() == 0)
			{
				throw new ArgumentException("cert");
			}
			this.m_cert = cert;
		}

		/// <summary>Gets the publisher's Authenticode X.509v3 certificate.</summary>
		/// <returns>The publisher's <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" />.</returns>
		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06003EB5 RID: 16053 RVA: 0x000E0F44 File Offset: 0x000DF144
		public X509Certificate Certificate
		{
			get
			{
				if (this.m_cert.GetHashCode() == 0)
				{
					throw new ArgumentException("m_cert");
				}
				return this.m_cert;
			}
		}

		/// <summary>Creates an equivalent copy of the <see cref="T:System.Security.Policy.Publisher" />.</summary>
		/// <returns>A new, identical copy of the <see cref="T:System.Security.Policy.Publisher" />.</returns>
		// Token: 0x06003EB6 RID: 16054 RVA: 0x000E0F64 File Offset: 0x000DF164
		public object Copy()
		{
			return new Publisher(this.m_cert);
		}

		/// <summary>Creates an identity permission that corresponds to the current instance of the <see cref="T:System.Security.Policy.Publisher" /> class.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.Publisher" />.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> from which to construct the identity permission. </param>
		// Token: 0x06003EB7 RID: 16055 RVA: 0x000E0F71 File Offset: 0x000DF171
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new PublisherIdentityPermission(this.m_cert);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Publisher" /> to the specified object for equivalence.</summary>
		/// <returns>true if the two instances of the <see cref="T:System.Security.Policy.Publisher" /> class are equal; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Policy.Publisher" /> to test for equivalence with the current object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="o" /> parameter is not a <see cref="T:System.Security.Policy.Publisher" /> object. </exception>
		// Token: 0x06003EB8 RID: 16056 RVA: 0x000E0F80 File Offset: 0x000DF180
		public override bool Equals(object o)
		{
			Publisher publisher = o as Publisher;
			if (publisher == null)
			{
				throw new ArgumentException("o", Locale.GetText("not a Publisher instance."));
			}
			return this.m_cert.Equals(publisher.Certificate);
		}

		/// <summary>Gets the hash code of the current <see cref="P:System.Security.Policy.Publisher.Certificate" />.</summary>
		/// <returns>The hash code of the current <see cref="P:System.Security.Policy.Publisher.Certificate" />.</returns>
		// Token: 0x06003EB9 RID: 16057 RVA: 0x000E0FBD File Offset: 0x000DF1BD
		public override int GetHashCode()
		{
			return this.m_cert.GetHashCode();
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Publisher" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Publisher" />.</returns>
		// Token: 0x06003EBA RID: 16058 RVA: 0x000E0FCC File Offset: 0x000DF1CC
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Publisher");
			securityElement.AddAttribute("version", "1");
			SecurityElement securityElement2 = new SecurityElement("X509v3Certificate");
			string rawCertDataString = this.m_cert.GetRawCertDataString();
			if (rawCertDataString != null)
			{
				securityElement2.Text = rawCertDataString;
			}
			securityElement.AddChild(securityElement2);
			return securityElement.ToString();
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x000E1020 File Offset: 0x000DF220
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return (verbose ? 3 : 1) + this.m_cert.GetRawCertData().Length;
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x04001FF9 RID: 8185
		private X509Certificate m_cert;
	}
}
