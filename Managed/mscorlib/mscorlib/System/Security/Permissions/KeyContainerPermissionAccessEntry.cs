using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace System.Security.Permissions
{
	/// <summary>Specifies access rights for specific key containers. This class cannot be inherited.</summary>
	// Token: 0x0200059D RID: 1437
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> class, using the specified cryptographic service provider (CSP) parameters and access permissions.</summary>
		/// <param name="parameters">A <see cref="T:System.Security.Cryptography.CspParameters" /> object that contains the cryptographic service provider (CSP) parameters. </param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x0600400B RID: 16395 RVA: 0x000E48AC File Offset: 0x000E2AAC
		public KeyContainerPermissionAccessEntry(CspParameters parameters, KeyContainerPermissionFlags flags)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.ProviderName = parameters.ProviderName;
			this.ProviderType = parameters.ProviderType;
			this.KeyContainerName = parameters.KeyContainerName;
			this.KeySpec = parameters.KeyNumber;
			this.Flags = flags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> class, using the specified key container name and access permissions.</summary>
		/// <param name="keyContainerName">The name of the key container. </param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x0600400C RID: 16396 RVA: 0x000E4904 File Offset: 0x000E2B04
		public KeyContainerPermissionAccessEntry(string keyContainerName, KeyContainerPermissionFlags flags)
		{
			this.KeyContainerName = keyContainerName;
			this.Flags = flags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> class with the specified property values.</summary>
		/// <param name="keyStore">The name of the key store. </param>
		/// <param name="providerName">The name of the provider. </param>
		/// <param name="providerType">The type code for the provider. See the <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntry.ProviderType" /> property for values. </param>
		/// <param name="keyContainerName">The name of the key container. </param>
		/// <param name="keySpec">The key specification. See the <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntry.KeySpec" /> property for values. </param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x0600400D RID: 16397 RVA: 0x000E491A File Offset: 0x000E2B1A
		public KeyContainerPermissionAccessEntry(string keyStore, string providerName, int providerType, string keyContainerName, int keySpec, KeyContainerPermissionFlags flags)
		{
			this.KeyStore = keyStore;
			this.ProviderName = providerName;
			this.ProviderType = providerType;
			this.KeyContainerName = keyContainerName;
			this.KeySpec = keySpec;
			this.Flags = flags;
		}

		/// <summary>Gets or sets the key container permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.KeyContainerPermissionFlags.NoFlags" />.</returns>
		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x000E494F File Offset: 0x000E2B4F
		// (set) Token: 0x0600400F RID: 16399 RVA: 0x000E4957 File Offset: 0x000E2B57
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				if ((value & KeyContainerPermissionFlags.AllFlags) != KeyContainerPermissionFlags.NoFlags)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "KeyContainerPermissionFlags");
				}
				this._flags = value;
			}
		}

		/// <summary>Gets or sets the key container name.</summary>
		/// <returns>The name of the key container.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06004010 RID: 16400 RVA: 0x000E4989 File Offset: 0x000E2B89
		// (set) Token: 0x06004011 RID: 16401 RVA: 0x000E4991 File Offset: 0x000E2B91
		public string KeyContainerName
		{
			get
			{
				return this._containerName;
			}
			set
			{
				this._containerName = value;
			}
		}

		/// <summary>Gets or sets the key specification.</summary>
		/// <returns>One of the AT_ values defined in the Wincrypt.h header file.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06004012 RID: 16402 RVA: 0x000E499A File Offset: 0x000E2B9A
		// (set) Token: 0x06004013 RID: 16403 RVA: 0x000E49A2 File Offset: 0x000E2BA2
		public int KeySpec
		{
			get
			{
				return this._spec;
			}
			set
			{
				this._spec = value;
			}
		}

		/// <summary>Gets or sets the name of the key store.</summary>
		/// <returns>The name of the key store.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x000E49AB File Offset: 0x000E2BAB
		// (set) Token: 0x06004015 RID: 16405 RVA: 0x000E49B3 File Offset: 0x000E2BB3
		public string KeyStore
		{
			get
			{
				return this._store;
			}
			set
			{
				this._store = value;
			}
		}

		/// <summary>Gets or sets the provider name.</summary>
		/// <returns>The name of the provider.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x000E49BC File Offset: 0x000E2BBC
		// (set) Token: 0x06004017 RID: 16407 RVA: 0x000E49C4 File Offset: 0x000E2BC4
		public string ProviderName
		{
			get
			{
				return this._providerName;
			}
			set
			{
				this._providerName = value;
			}
		}

		/// <summary>Gets or sets the provider type.</summary>
		/// <returns>One of the PROV_ values defined in the Wincrypt.h header file.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access. </exception>
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06004018 RID: 16408 RVA: 0x000E49CD File Offset: 0x000E2BCD
		// (set) Token: 0x06004019 RID: 16409 RVA: 0x000E49D5 File Offset: 0x000E2BD5
		public int ProviderType
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object is equal to the current instance.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> is equal to the current <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object to compare with the currentinstance. </param>
		// Token: 0x0600401A RID: 16410 RVA: 0x000E49E0 File Offset: 0x000E2BE0
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = o as KeyContainerPermissionAccessEntry;
			return keyContainerPermissionAccessEntry != null && this._flags == keyContainerPermissionAccessEntry._flags && !(this._containerName != keyContainerPermissionAccessEntry._containerName) && !(this._store != keyContainerPermissionAccessEntry._store) && !(this._providerName != keyContainerPermissionAccessEntry._providerName) && this._type == keyContainerPermissionAccessEntry._type;
		}

		/// <summary>Gets a hash code for the current instance that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object.</returns>
		// Token: 0x0600401B RID: 16411 RVA: 0x000E4A60 File Offset: 0x000E2C60
		public override int GetHashCode()
		{
			int num = this._type ^ this._spec ^ (int)this._flags;
			if (this._containerName != null)
			{
				num ^= this._containerName.GetHashCode();
			}
			if (this._store != null)
			{
				num ^= this._store.GetHashCode();
			}
			if (this._providerName != null)
			{
				num ^= this._providerName.GetHashCode();
			}
			return num;
		}

		// Token: 0x0400207E RID: 8318
		private KeyContainerPermissionFlags _flags;

		// Token: 0x0400207F RID: 8319
		private string _containerName;

		// Token: 0x04002080 RID: 8320
		private int _spec;

		// Token: 0x04002081 RID: 8321
		private string _store;

		// Token: 0x04002082 RID: 8322
		private string _providerName;

		// Token: 0x04002083 RID: 8323
		private int _type;
	}
}
