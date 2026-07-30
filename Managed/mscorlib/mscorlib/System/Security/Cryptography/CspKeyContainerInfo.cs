using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Cryptography
{
	/// <summary>Provides additional information about a cryptographic key pair. This class cannot be inherited.</summary>
	// Token: 0x0200069A RID: 1690
	[ComVisible(true)]
	public sealed class CspKeyContainerInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> class using the specified parameters.</summary>
		/// <param name="parameters">A <see cref="T:System.Security.Cryptography.CspParameters" /> object that provides information about the key.</param>
		// Token: 0x06004855 RID: 18517 RVA: 0x00102DF4 File Offset: 0x00100FF4
		public CspKeyContainerInfo(CspParameters parameters)
		{
			this._params = parameters;
			this._random = true;
		}

		/// <summary>Gets a value indicating whether a key in a key container is accessible.</summary>
		/// <returns>true if the key is accessible; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">The key type is not supported.</exception>
		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06004856 RID: 18518 RVA: 0x00003B29 File Offset: 0x00001D29
		public bool Accessible
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for a container. </summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for a container.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key type is not supported.</exception>
		/// <exception cref="T:System.NotSupportedException">The cryptographic service provider cannot be found.-or-The key container was not found.</exception>
		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06004857 RID: 18519 RVA: 0x0000A42E File Offset: 0x0000862E
		public CryptoKeySecurity CryptoKeySecurity
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a value indicating whether a key can be exported from a key container.</summary>
		/// <returns>true if the key can be exported; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">The key type is not supported.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider cannot be found.-or-The key container was not found.</exception>
		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06004858 RID: 18520 RVA: 0x00003B29 File Offset: 0x00001D29
		public bool Exportable
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether a key is a hardware key.</summary>
		/// <returns>true if the key is a hardware key; otherwise, false.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider cannot be found.</exception>
		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06004859 RID: 18521 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool HardwareDevice
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a key container name.</summary>
		/// <returns>The key container name.</returns>
		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x0600485A RID: 18522 RVA: 0x00102E0A File Offset: 0x0010100A
		public string KeyContainerName
		{
			get
			{
				return this._params.KeyContainerName;
			}
		}

		/// <summary>Gets a value that describes whether an asymmetric key was created as a signature key or an exchange key.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.KeyNumber" /> values that describes whether an asymmetric key was created as a signature key or an exchange key.</returns>
		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x0600485B RID: 18523 RVA: 0x00102E17 File Offset: 0x00101017
		public KeyNumber KeyNumber
		{
			get
			{
				return (KeyNumber)this._params.KeyNumber;
			}
		}

		/// <summary>Gets a value indicating whether a key is from a machine key set.</summary>
		/// <returns>true if the key is from the machine key set; otherwise, false.</returns>
		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x0600485C RID: 18524 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool MachineKeyStore
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether a key pair is protected.</summary>
		/// <returns>true if the key pair is protected; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">The key type is not supported.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider cannot be found.-or-The key container was not found.</exception>
		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x0600485D RID: 18525 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool Protected
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the provider name of a key.</summary>
		/// <returns>The provider name.</returns>
		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x0600485E RID: 18526 RVA: 0x00102E24 File Offset: 0x00101024
		public string ProviderName
		{
			get
			{
				return this._params.ProviderName;
			}
		}

		/// <summary>Gets the provider type of a key.</summary>
		/// <returns>The provider type. The default is 1.</returns>
		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x0600485F RID: 18527 RVA: 0x00102E31 File Offset: 0x00101031
		public int ProviderType
		{
			get
			{
				return this._params.ProviderType;
			}
		}

		/// <summary>Gets a value indicating whether a key container was randomly generated by a managed cryptography class.</summary>
		/// <returns>true if the key container was randomly generated; otherwise, false.</returns>
		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06004860 RID: 18528 RVA: 0x00102E3E File Offset: 0x0010103E
		public bool RandomlyGenerated
		{
			get
			{
				return this._random;
			}
		}

		/// <summary>Gets a value indicating whether a key can be removed from a key container.</summary>
		/// <returns>true if the key is removable; otherwise, false.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) was not found.</exception>
		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06004861 RID: 18529 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool Removable
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a unique key container name.</summary>
		/// <returns>The unique key container name.</returns>
		/// <exception cref="T:System.NotSupportedException">The key type is not supported.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider cannot be found.-or-The key container was not found.</exception>
		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06004862 RID: 18530 RVA: 0x00102E46 File Offset: 0x00101046
		public string UniqueKeyContainerName
		{
			get
			{
				return this._params.ProviderName + "\\" + this._params.KeyContainerName;
			}
		}

		// Token: 0x040025DA RID: 9690
		private CspParameters _params;

		// Token: 0x040025DB RID: 9691
		internal bool _random;
	}
}
