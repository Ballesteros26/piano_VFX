using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Cryptography;

namespace System.Security.Permissions
{
	/// <summary>Represents the identity of a software publisher. This class cannot be inherited.</summary>
	// Token: 0x020005A7 RID: 1447
	[ComVisible(true)]
	[Serializable]
	public sealed class PublisherIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x0600406C RID: 16492 RVA: 0x000E3C80 File Offset: 0x000E1E80
		public PublisherIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> class with the specified Authenticode X.509v3 certificate.</summary>
		/// <param name="certificate">An X.509 certificate representing the software publisher's identity. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="certificate" /> parameter is not a valid certificate. </exception>
		// Token: 0x0600406D RID: 16493 RVA: 0x000E59A1 File Offset: 0x000E3BA1
		public PublisherIdentityPermission(X509Certificate certificate)
		{
			this.Certificate = certificate;
		}

		/// <summary>Gets or sets an Authenticode X.509v3 certificate that represents the identity of the software publisher.</summary>
		/// <returns>An X.509 certificate representing the identity of the software publisher.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Permissions.PublisherIdentityPermission.Certificate" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Permissions.PublisherIdentityPermission.Certificate" /> is not a valid certificate. </exception>
		/// <exception cref="T:System.NotSupportedException">The property cannot be set because the identity is ambiguous.</exception>
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x0600406E RID: 16494 RVA: 0x000E59B0 File Offset: 0x000E3BB0
		// (set) Token: 0x0600406F RID: 16495 RVA: 0x000E59B8 File Offset: 0x000E3BB8
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
					throw new ArgumentNullException("X509Certificate");
				}
				this.x509 = value;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06004070 RID: 16496 RVA: 0x000E59D0 File Offset: 0x000E3BD0
		public override IPermission Copy()
		{
			PublisherIdentityPermission publisherIdentityPermission = new PublisherIdentityPermission(PermissionState.None);
			if (this.x509 != null)
			{
				publisherIdentityPermission.Certificate = this.x509;
			}
			return publisherIdentityPermission;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x06004071 RID: 16497 RVA: 0x000E59FC File Offset: 0x000E3BFC
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			string text = esd.Attributes["X509v3Certificate"] as string;
			if (text != null)
			{
				byte[] array = CryptoConvert.FromHex(text);
				this.x509 = new X509Certificate(array);
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06004072 RID: 16498 RVA: 0x000E5A44 File Offset: 0x000E3C44
		public override IPermission Intersect(IPermission target)
		{
			PublisherIdentityPermission publisherIdentityPermission = this.Cast(target);
			if (publisherIdentityPermission == null)
			{
				return null;
			}
			if (this.x509 != null && publisherIdentityPermission.x509 != null && this.x509.GetRawCertDataString() == publisherIdentityPermission.x509.GetRawCertDataString())
			{
				return new PublisherIdentityPermission(publisherIdentityPermission.x509);
			}
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06004073 RID: 16499 RVA: 0x000E5A98 File Offset: 0x000E3C98
		public override bool IsSubsetOf(IPermission target)
		{
			PublisherIdentityPermission publisherIdentityPermission = this.Cast(target);
			return publisherIdentityPermission != null && (this.x509 == null || (publisherIdentityPermission.x509 != null && this.x509.GetRawCertDataString() == publisherIdentityPermission.x509.GetRawCertDataString()));
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06004074 RID: 16500 RVA: 0x000E5AE4 File Offset: 0x000E3CE4
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.x509 != null)
			{
				securityElement.AddAttribute("X509v3Certificate", this.x509.GetRawCertDataString());
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. -or-The two permissions are not equal.</exception>
		// Token: 0x06004075 RID: 16501 RVA: 0x000E5B18 File Offset: 0x000E3D18
		public override IPermission Union(IPermission target)
		{
			PublisherIdentityPermission publisherIdentityPermission = this.Cast(target);
			if (publisherIdentityPermission == null)
			{
				return this.Copy();
			}
			if (this.x509 != null && publisherIdentityPermission.x509 != null)
			{
				if (this.x509.GetRawCertDataString() == publisherIdentityPermission.x509.GetRawCertDataString())
				{
					return new PublisherIdentityPermission(this.x509);
				}
			}
			else
			{
				if (this.x509 == null && publisherIdentityPermission.x509 != null)
				{
					return new PublisherIdentityPermission(publisherIdentityPermission.x509);
				}
				if (this.x509 != null && publisherIdentityPermission.x509 == null)
				{
					return new PublisherIdentityPermission(this.x509);
				}
			}
			return null;
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x00058E5B File Offset: 0x0005705B
		int IBuiltInPermission.GetTokenIndex()
		{
			return 10;
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x000E5BA9 File Offset: 0x000E3DA9
		private PublisherIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			PublisherIdentityPermission publisherIdentityPermission = target as PublisherIdentityPermission;
			if (publisherIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(PublisherIdentityPermission));
			}
			return publisherIdentityPermission;
		}

		// Token: 0x040020A8 RID: 8360
		private const int version = 1;

		// Token: 0x040020A9 RID: 8361
		private X509Certificate x509;
	}
}
