using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Cryptography;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its software publisher's Authenticode X.509v3 certificate. This class cannot be inherited.</summary>
	// Token: 0x02000579 RID: 1401
	[ComVisible(true)]
	[Serializable]
	public sealed class PublisherMembershipCondition : IConstantMembershipCondition, IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x06003EBE RID: 16062 RVA: 0x000E1037 File Offset: 0x000DF237
		internal PublisherMembershipCondition()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PublisherMembershipCondition" /> class with the Authenticode X.509v3 certificate that determines membership.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> that contains the software publisher's public key. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is null. </exception>
		// Token: 0x06003EBF RID: 16063 RVA: 0x000E1046 File Offset: 0x000DF246
		public PublisherMembershipCondition(X509Certificate certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (certificate.GetHashCode() == 0)
			{
				throw new ArgumentException("certificate");
			}
			this.x509 = certificate;
		}

		/// <summary>Gets or sets the Authenticode X.509v3 certificate for which the membership condition tests.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x000E107D File Offset: 0x000DF27D
		// (set) Token: 0x06003EC1 RID: 16065 RVA: 0x000E1085 File Offset: 0x000DF285
		public X509Certificate Certificate
		{
			get
			{
				return this.x509;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.x509 = value;
			}
		}

		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <returns>true if the specified evidence satisfies the membership condition; otherwise, false.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> against which to make the test. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x06003EC2 RID: 16066 RVA: 0x000E109C File Offset: 0x000DF29C
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				if (hostEnumerator.Current is Publisher && this.x509.Equals((hostEnumerator.Current as Publisher).Certificate))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x06003EC3 RID: 16067 RVA: 0x000E10EC File Offset: 0x000DF2EC
		public IMembershipCondition Copy()
		{
			return new PublisherMembershipCondition(this.x509);
		}

		/// <summary>Determines whether the publisher certificate from the specified object is equivalent to the publisher certificate contained in the current <see cref="T:System.Security.Policy.PublisherMembershipCondition" />.</summary>
		/// <returns>true if the publisher certificate from the specified object is equivalent to the publisher certificate contained in the current <see cref="T:System.Security.Policy.PublisherMembershipCondition" />; otherwise, false.</returns>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.PublisherMembershipCondition" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x06003EC4 RID: 16068 RVA: 0x000E10FC File Offset: 0x000DF2FC
		public override bool Equals(object o)
		{
			PublisherMembershipCondition publisherMembershipCondition = o as PublisherMembershipCondition;
			return publisherMembershipCondition != null && this.x509.Equals(publisherMembershipCondition.Certificate);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element. </exception>
		// Token: 0x06003EC5 RID: 16069 RVA: 0x000E1126 File Offset: 0x000DF326
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element. </exception>
		// Token: 0x06003EC6 RID: 16070 RVA: 0x000E1130 File Offset: 0x000DF330
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			string text = e.Attribute("X509Certificate");
			if (text != null)
			{
				byte[] array = CryptoConvert.FromHex(text);
				this.x509 = new X509Certificate(array);
			}
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x06003EC7 RID: 16071 RVA: 0x000E1177 File Offset: 0x000DF377
		public override int GetHashCode()
		{
			return this.x509.GetHashCode();
		}

		/// <summary>Creates and returns a string representation of the <see cref="T:System.Security.Policy.PublisherMembershipCondition" />.</summary>
		/// <returns>A representation of the <see cref="T:System.Security.Policy.PublisherMembershipCondition" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x06003EC8 RID: 16072 RVA: 0x000E1184 File Offset: 0x000DF384
		public override string ToString()
		{
			return "Publisher - " + this.x509.GetPublicKeyString();
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x06003EC9 RID: 16073 RVA: 0x000E119B File Offset: 0x000DF39B
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, which is used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x06003ECA RID: 16074 RVA: 0x000E11A4 File Offset: 0x000DF3A4
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(PublisherMembershipCondition), this.version);
			securityElement.AddAttribute("X509Certificate", this.x509.GetRawCertDataString());
			return securityElement;
		}

		// Token: 0x04001FFA RID: 8186
		private readonly int version = 1;

		// Token: 0x04001FFB RID: 8187
		private X509Certificate x509;
	}
}
