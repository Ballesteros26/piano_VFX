using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	/// <summary>Represents a cryptographic object identifier. This class cannot be inherited.</summary>
	// Token: 0x0200038E RID: 910
	public sealed class Oid
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class.</summary>
		// Token: 0x06001B8C RID: 7052 RVA: 0x000020EB File Offset: 0x000002EB
		public Oid()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class using a string value of an <see cref="T:System.Security.Cryptography.Oid" /> object.</summary>
		/// <param name="oid">An object identifier.</param>
		// Token: 0x06001B8D RID: 7053 RVA: 0x0006D78C File Offset: 0x0006B98C
		public Oid(string oid)
			: this(oid, OidGroup.All, true)
		{
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x0006D798 File Offset: 0x0006B998
		internal Oid(string oid, OidGroup group, bool lookupFriendlyName)
		{
			if (lookupFriendlyName)
			{
				string text = X509Utils.FindOidInfoWithFallback(2U, oid, group);
				if (text == null)
				{
					text = oid;
				}
				this.Value = text;
			}
			else
			{
				this.Value = oid;
			}
			this.m_group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class using the specified value and friendly name.</summary>
		/// <param name="value">The dotted number of the identifier.</param>
		/// <param name="friendlyName">The friendly name of the identifier.</param>
		// Token: 0x06001B8F RID: 7055 RVA: 0x0006D7D3 File Offset: 0x0006B9D3
		public Oid(string value, string friendlyName)
		{
			this.m_value = value;
			this.m_friendlyName = friendlyName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class using the specified <see cref="T:System.Security.Cryptography.Oid" /> object.</summary>
		/// <param name="oid">The object identifier information to use to create the new object identifier.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oid " />is null.</exception>
		// Token: 0x06001B90 RID: 7056 RVA: 0x0006D7E9 File Offset: 0x0006B9E9
		public Oid(Oid oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			this.m_value = oid.m_value;
			this.m_friendlyName = oid.m_friendlyName;
			this.m_group = oid.m_group;
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0006D823 File Offset: 0x0006BA23
		private Oid(string value, string friendlyName, OidGroup group)
		{
			this.m_value = value;
			this.m_friendlyName = friendlyName;
			this.m_group = group;
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.Oid" /> object from an OID friendly name by searching the specified group.</summary>
		/// <returns>An object that represents the specified OID.</returns>
		/// <param name="friendlyName">The friendly name of the identifier.</param>
		/// <param name="group">The group to search in.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName " /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The OID was not found.</exception>
		// Token: 0x06001B92 RID: 7058 RVA: 0x0006D840 File Offset: 0x0006BA40
		public static Oid FromFriendlyName(string friendlyName, OidGroup group)
		{
			if (friendlyName == null)
			{
				throw new ArgumentNullException("friendlyName");
			}
			string text = X509Utils.FindOidInfo(2U, friendlyName, group);
			if (text == null)
			{
				throw new CryptographicException(global::SR.GetString("The OID value is invalid."));
			}
			return new Oid(text, friendlyName, group);
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.Oid" /> object by using the specified OID value and group.</summary>
		/// <returns>A new instance of an <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		/// <param name="oidValue">The OID value.</param>
		/// <param name="group">The group to search in.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oidValue" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The friendly name for the OID value was not found.</exception>
		// Token: 0x06001B93 RID: 7059 RVA: 0x0006D874 File Offset: 0x0006BA74
		public static Oid FromOidValue(string oidValue, OidGroup group)
		{
			if (oidValue == null)
			{
				throw new ArgumentNullException("oidValue");
			}
			string text = X509Utils.FindOidInfo(1U, oidValue, group);
			if (text == null)
			{
				throw new CryptographicException(global::SR.GetString("The OID value is invalid."));
			}
			return new Oid(oidValue, text, group);
		}

		/// <summary>Gets or sets the dotted number of the identifier.</summary>
		/// <returns>The dotted number of the identifier.</returns>
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x0006D8B3 File Offset: 0x0006BAB3
		// (set) Token: 0x06001B95 RID: 7061 RVA: 0x0006D8BB File Offset: 0x0006BABB
		public string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		/// <summary>Gets or sets the friendly name of the identifier.</summary>
		/// <returns>The friendly name of the identifier.</returns>
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x0006D8C4 File Offset: 0x0006BAC4
		// (set) Token: 0x06001B97 RID: 7063 RVA: 0x0006D8F4 File Offset: 0x0006BAF4
		public string FriendlyName
		{
			get
			{
				if (this.m_friendlyName == null && this.m_value != null)
				{
					this.m_friendlyName = X509Utils.FindOidInfoWithFallback(1U, this.m_value, this.m_group);
				}
				return this.m_friendlyName;
			}
			set
			{
				this.m_friendlyName = value;
				if (this.m_friendlyName != null)
				{
					string text = X509Utils.FindOidInfoWithFallback(2U, this.m_friendlyName, this.m_group);
					if (text != null)
					{
						this.m_value = text;
					}
				}
			}
		}

		// Token: 0x040018E5 RID: 6373
		private string m_value;

		// Token: 0x040018E6 RID: 6374
		private string m_friendlyName;

		// Token: 0x040018E7 RID: 6375
		private OidGroup m_group;
	}
}
