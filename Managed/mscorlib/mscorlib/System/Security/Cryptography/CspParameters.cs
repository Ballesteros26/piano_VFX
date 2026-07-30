using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Cryptography
{
	/// <summary>Contains parameters that are passed to the cryptographic service provider (CSP) that performs cryptographic computations. This class cannot be inherited.</summary>
	// Token: 0x02000651 RID: 1617
	[ComVisible(true)]
	public sealed class CspParameters
	{
		/// <summary>Represents the flags for <see cref="T:System.Security.Cryptography.CspParameters" /> that modify the behavior of the cryptographic service provider (CSP).</summary>
		/// <returns>An enumeration value, or a bitwise combination of enumeration values.</returns>
		/// <exception cref="T:System.ArgumentException">Value is not a valid enumeration value.</exception>
		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x060045E1 RID: 17889 RVA: 0x000F536E File Offset: 0x000F356E
		// (set) Token: 0x060045E2 RID: 17890 RVA: 0x000F5378 File Offset: 0x000F3578
		public CspProviderFlags Flags
		{
			get
			{
				return (CspProviderFlags)this.m_flags;
			}
			set
			{
				int num = 255;
				if ((value & (CspProviderFlags)(~(CspProviderFlags)num)) != CspProviderFlags.NoFlags)
				{
					throw new ArgumentException(Environment.GetResourceString("Illegal enum value: {0}.", new object[] { (int)value }), "value");
				}
				this.m_flags = (int)value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for a container. </summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for a container.</returns>
		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x060045E3 RID: 17891 RVA: 0x000F53BE File Offset: 0x000F35BE
		// (set) Token: 0x060045E4 RID: 17892 RVA: 0x000F53C6 File Offset: 0x000F35C6
		public CryptoKeySecurity CryptoKeySecurity
		{
			get
			{
				return this.m_cryptoKeySecurity;
			}
			set
			{
				this.m_cryptoKeySecurity = value;
			}
		}

		/// <summary>Gets or sets a password associated with a smart card key. </summary>
		/// <returns>A password associated with a smart card key.</returns>
		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x060045E5 RID: 17893 RVA: 0x000F53CF File Offset: 0x000F35CF
		// (set) Token: 0x060045E6 RID: 17894 RVA: 0x000F53D7 File Offset: 0x000F35D7
		public SecureString KeyPassword
		{
			get
			{
				return this.m_keyPassword;
			}
			set
			{
				this.m_keyPassword = value;
				this.m_parentWindowHandle = IntPtr.Zero;
			}
		}

		/// <summary>Gets or sets a handle to the unmanaged parent window for a smart card password dialog box.</summary>
		/// <returns>A handle to the parent window for a smart card password dialog box.</returns>
		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x060045E7 RID: 17895 RVA: 0x000F53EB File Offset: 0x000F35EB
		// (set) Token: 0x060045E8 RID: 17896 RVA: 0x000F53F3 File Offset: 0x000F35F3
		public IntPtr ParentWindowHandle
		{
			get
			{
				return this.m_parentWindowHandle;
			}
			set
			{
				this.m_parentWindowHandle = value;
				this.m_keyPassword = null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class.</summary>
		// Token: 0x060045E9 RID: 17897 RVA: 0x000F5403 File Offset: 0x000F3603
		public CspParameters()
			: this(1, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class with the specified provider type code.</summary>
		/// <param name="dwTypeIn">A provider type code that specifies the kind of provider to create. </param>
		// Token: 0x060045EA RID: 17898 RVA: 0x000F540E File Offset: 0x000F360E
		public CspParameters(int dwTypeIn)
			: this(dwTypeIn, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class with the specified provider type code and name.</summary>
		/// <param name="dwTypeIn">A provider type code that specifies the kind of provider to create.</param>
		/// <param name="strProviderNameIn">A provider name. </param>
		// Token: 0x060045EB RID: 17899 RVA: 0x000F5419 File Offset: 0x000F3619
		public CspParameters(int dwTypeIn, string strProviderNameIn)
			: this(dwTypeIn, strProviderNameIn, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class with the specified provider type code and name, and the specified container name.</summary>
		/// <param name="dwTypeIn">The provider type code that specifies the kind of provider to create.</param>
		/// <param name="strProviderNameIn">A provider name. </param>
		/// <param name="strContainerNameIn">A container name. </param>
		// Token: 0x060045EC RID: 17900 RVA: 0x000F5424 File Offset: 0x000F3624
		public CspParameters(int dwTypeIn, string strProviderNameIn, string strContainerNameIn)
			: this(dwTypeIn, strProviderNameIn, strContainerNameIn, CspProviderFlags.NoFlags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class using a provider type, a provider name, a container name, access information, and a password associated with a smart card key.</summary>
		/// <param name="providerType">The provider type code that specifies the kind of provider to create.</param>
		/// <param name="providerName">A provider name. </param>
		/// <param name="keyContainerName">A container name. </param>
		/// <param name="cryptoKeySecurity">An object that represents access rights and audit rules for a container. </param>
		/// <param name="keyPassword">A password associated with a smart card key.</param>
		// Token: 0x060045ED RID: 17901 RVA: 0x000F5430 File Offset: 0x000F3630
		public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, SecureString keyPassword)
			: this(providerType, providerName, keyContainerName)
		{
			this.m_cryptoKeySecurity = cryptoKeySecurity;
			this.m_keyPassword = keyPassword;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class using a provider type, a provider name, a container name, access information, and a handle to an unmanaged smart card password dialog. </summary>
		/// <param name="providerType">The provider type code that specifies the kind of provider to create.</param>
		/// <param name="providerName">A provider name. </param>
		/// <param name="keyContainerName">A container name. </param>
		/// <param name="cryptoKeySecurity">An object that represents access rights and audit rules for the container.</param>
		/// <param name="parentWindowHandle">A handle to the parent window for a smart card password dialog.</param>
		// Token: 0x060045EE RID: 17902 RVA: 0x000F544B File Offset: 0x000F364B
		public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, IntPtr parentWindowHandle)
			: this(providerType, providerName, keyContainerName)
		{
			this.m_cryptoKeySecurity = cryptoKeySecurity;
			this.m_parentWindowHandle = parentWindowHandle;
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x000F5466 File Offset: 0x000F3666
		internal CspParameters(int providerType, string providerName, string keyContainerName, CspProviderFlags flags)
		{
			this.ProviderType = providerType;
			this.ProviderName = providerName;
			this.KeyContainerName = keyContainerName;
			this.KeyNumber = -1;
			this.Flags = flags;
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x000F5494 File Offset: 0x000F3694
		internal CspParameters(CspParameters parameters)
		{
			this.ProviderType = parameters.ProviderType;
			this.ProviderName = parameters.ProviderName;
			this.KeyContainerName = parameters.KeyContainerName;
			this.KeyNumber = parameters.KeyNumber;
			this.Flags = parameters.Flags;
			this.m_cryptoKeySecurity = parameters.m_cryptoKeySecurity;
			this.m_keyPassword = parameters.m_keyPassword;
			this.m_parentWindowHandle = parameters.m_parentWindowHandle;
		}

		/// <summary>Represents the provider type code for <see cref="T:System.Security.Cryptography.CspParameters" />.</summary>
		// Token: 0x040023F2 RID: 9202
		public int ProviderType;

		/// <summary>Represents the provider name for <see cref="T:System.Security.Cryptography.CspParameters" />.</summary>
		// Token: 0x040023F3 RID: 9203
		public string ProviderName;

		/// <summary>Represents the key container name for <see cref="T:System.Security.Cryptography.CspParameters" />.</summary>
		// Token: 0x040023F4 RID: 9204
		public string KeyContainerName;

		/// <summary>Specifies whether an asymmetric key is created as a signature key or an exchange key.</summary>
		// Token: 0x040023F5 RID: 9205
		public int KeyNumber;

		// Token: 0x040023F6 RID: 9206
		private int m_flags;

		// Token: 0x040023F7 RID: 9207
		private CryptoKeySecurity m_cryptoKeySecurity;

		// Token: 0x040023F8 RID: 9208
		private SecureString m_keyPassword;

		// Token: 0x040023F9 RID: 9209
		private IntPtr m_parentWindowHandle;
	}
}
