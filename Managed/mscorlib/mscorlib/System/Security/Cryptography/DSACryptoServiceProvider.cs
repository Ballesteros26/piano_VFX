using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.DSA" /> algorithm. This class cannot be inherited. </summary>
	// Token: 0x0200069C RID: 1692
	[ComVisible(true)]
	public sealed class DSACryptoServiceProvider : DSA, ICspAsymmetricAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class.</summary>
		// Token: 0x0600486C RID: 18540 RVA: 0x001035E2 File Offset: 0x001017E2
		public DSACryptoServiceProvider()
			: this(1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class with the specified parameters for the cryptographic service provider (CSP).</summary>
		/// <param name="parameters">The parameters for the CSP. </param>
		// Token: 0x0600486D RID: 18541 RVA: 0x001035EF File Offset: 0x001017EF
		public DSACryptoServiceProvider(CspParameters parameters)
			: this(1024, parameters)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class with the specified key size.</summary>
		/// <param name="dwKeySize">The size of the key for the asymmetric algorithm in bits. </param>
		// Token: 0x0600486E RID: 18542 RVA: 0x001035FD File Offset: 0x001017FD
		public DSACryptoServiceProvider(int dwKeySize)
		{
			this.privateKeyExportable = true;
			base..ctor();
			this.Common(dwKeySize, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class with the specified key size and parameters for the cryptographic service provider (CSP).</summary>
		/// <param name="dwKeySize">The size of the key for the cryptographic algorithm in bits. </param>
		/// <param name="parameters">The parameters for the CSP. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The CSP cannot be acquired.-or- The key cannot be created. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dwKeySize" /> is out of range.</exception>
		// Token: 0x0600486F RID: 18543 RVA: 0x00103614 File Offset: 0x00101814
		public DSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
		{
			this.privateKeyExportable = true;
			base..ctor();
			bool flag = parameters != null;
			this.Common(dwKeySize, flag);
			if (flag)
			{
				this.Common(parameters);
			}
		}

		// Token: 0x06004870 RID: 18544 RVA: 0x00103648 File Offset: 0x00101848
		private void Common(int dwKeySize, bool parameters)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(512, 1024, 64);
			this.KeySize = dwKeySize;
			this.dsa = new DSAManaged(dwKeySize);
			this.dsa.KeyGenerated += this.OnKeyGenerated;
			this.persistKey = parameters;
			if (parameters)
			{
				return;
			}
			CspParameters cspParameters = new CspParameters(13);
			if (DSACryptoServiceProvider.useMachineKeyStore)
			{
				cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
			}
			this.store = new KeyPairPersistence(cspParameters);
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x001036D8 File Offset: 0x001018D8
		private void Common(CspParameters parameters)
		{
			this.store = new KeyPairPersistence(parameters);
			this.store.Load();
			if (this.store.KeyValue != null)
			{
				this.persisted = true;
				this.FromXmlString(this.store.KeyValue);
			}
			this.privateKeyExportable = (parameters.Flags & CspProviderFlags.UseNonExportableKey) == CspProviderFlags.NoFlags;
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x00103734 File Offset: 0x00101934
		~DSACryptoServiceProvider()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the name of the key exchange algorithm.</summary>
		/// <returns>The name of the key exchange algorithm.</returns>
		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06004873 RID: 18547 RVA: 0x0000A42E File Offset: 0x0000862E
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the size of the key used by the asymmetric algorithm in bits.</summary>
		/// <returns>The size of the key used by the asymmetric algorithm.</returns>
		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06004874 RID: 18548 RVA: 0x00103764 File Offset: 0x00101964
		public override int KeySize
		{
			get
			{
				return this.dsa.KeySize;
			}
		}

		/// <summary>Gets or sets a value indicating whether the key should be persisted in the cryptographic service provider (CSP).</summary>
		/// <returns>true if the key should be persisted in the CSP; otherwise, false.</returns>
		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06004875 RID: 18549 RVA: 0x00103771 File Offset: 0x00101971
		// (set) Token: 0x06004876 RID: 18550 RVA: 0x00103779 File Offset: 0x00101979
		public bool PersistKeyInCsp
		{
			get
			{
				return this.persistKey;
			}
			set
			{
				this.persistKey = value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object contains only a public key.</summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object contains only a public key; otherwise, false.</returns>
		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06004877 RID: 18551 RVA: 0x00103782 File Offset: 0x00101982
		[ComVisible(false)]
		public bool PublicOnly
		{
			get
			{
				return this.dsa.PublicOnly;
			}
		}

		/// <summary>Gets the name of the signature algorithm.</summary>
		/// <returns>The name of the signature algorithm.</returns>
		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06004878 RID: 18552 RVA: 0x0001B331 File Offset: 0x00019531
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
			}
		}

		/// <summary>Gets or sets a value indicating whether the key should be persisted in the computer's key store instead of the user profile store.</summary>
		/// <returns>true if the key should be persisted in the computer key store; otherwise, false.</returns>
		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06004879 RID: 18553 RVA: 0x0010378F File Offset: 0x0010198F
		// (set) Token: 0x0600487A RID: 18554 RVA: 0x00103796 File Offset: 0x00101996
		public static bool UseMachineKeyStore
		{
			get
			{
				return DSACryptoServiceProvider.useMachineKeyStore;
			}
			set
			{
				DSACryptoServiceProvider.useMachineKeyStore = value;
			}
		}

		/// <summary>Exports the <see cref="T:System.Security.Cryptography.DSAParameters" />.</summary>
		/// <returns>The parameters for <see cref="T:System.Security.Cryptography.DSA" />.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key cannot be exported. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600487B RID: 18555 RVA: 0x0010379E File Offset: 0x0010199E
		public override DSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (includePrivateParameters && !this.privateKeyExportable)
			{
				throw new CryptographicException(Locale.GetText("Cannot export private key"));
			}
			return this.dsa.ExportParameters(includePrivateParameters);
		}

		/// <summary>Imports the specified <see cref="T:System.Security.Cryptography.DSAParameters" />.</summary>
		/// <param name="parameters">The parameters for <see cref="T:System.Security.Cryptography.DSA" />. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.-or- The <paramref name="parameters" /> parameter has missing fields. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600487C RID: 18556 RVA: 0x001037C7 File Offset: 0x001019C7
		public override void ImportParameters(DSAParameters parameters)
		{
			this.dsa.ImportParameters(parameters);
		}

		/// <summary>Creates the <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</summary>
		/// <returns>The digital signature for the specified data.</returns>
		/// <param name="rgbHash">The data to be signed. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600487D RID: 18557 RVA: 0x001037D5 File Offset: 0x001019D5
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			return this.dsa.CreateSignature(rgbHash);
		}

		/// <summary>Computes the hash value of the specified byte array and signs the resulting hash value.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</returns>
		/// <param name="buffer">The input data for which to compute the hash. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600487E RID: 18558 RVA: 0x001037E4 File Offset: 0x001019E4
		public byte[] SignData(byte[] buffer)
		{
			byte[] array = SHA1.Create().ComputeHash(buffer);
			return this.dsa.CreateSignature(array);
		}

		/// <summary>Signs a byte array from the specified start point to the specified end point.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</returns>
		/// <param name="buffer">The input data to sign. </param>
		/// <param name="offset">The offset into the array from which to begin using data. </param>
		/// <param name="count">The number of bytes in the array to use as data. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600487F RID: 18559 RVA: 0x0010380C File Offset: 0x00101A0C
		public byte[] SignData(byte[] buffer, int offset, int count)
		{
			byte[] array = SHA1.Create().ComputeHash(buffer, offset, count);
			return this.dsa.CreateSignature(array);
		}

		/// <summary>Computes the hash value of the specified input stream and signs the resulting hash value.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</returns>
		/// <param name="inputStream">The input data for which to compute the hash. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004880 RID: 18560 RVA: 0x00103834 File Offset: 0x00101A34
		public byte[] SignData(Stream inputStream)
		{
			byte[] array = SHA1.Create().ComputeHash(inputStream);
			return this.dsa.CreateSignature(array);
		}

		/// <summary>Computes the signature for the specified hash value by encrypting it with the private key.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified hash value.</returns>
		/// <param name="rgbHash">The hash value of the data to be signed. </param>
		/// <param name="str">The name of the hash algorithm used to create the hash value of the data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.-or- There is no private key. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004881 RID: 18561 RVA: 0x00103859 File Offset: 0x00101A59
		public byte[] SignHash(byte[] rgbHash, string str)
		{
			if (string.Compare(str, "SHA1", true, CultureInfo.InvariantCulture) != 0)
			{
				throw new CryptographicException(Locale.GetText("Only SHA1 is supported."));
			}
			return this.dsa.CreateSignature(rgbHash);
		}

		/// <summary>Verifies the specified signature data by comparing it to the signature computed for the specified data.</summary>
		/// <returns>true if the signature verifies as valid; otherwise, false.</returns>
		/// <param name="rgbData">The data that was signed. </param>
		/// <param name="rgbSignature">The signature data to be verified. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004882 RID: 18562 RVA: 0x0010388C File Offset: 0x00101A8C
		public bool VerifyData(byte[] rgbData, byte[] rgbSignature)
		{
			byte[] array = SHA1.Create().ComputeHash(rgbData);
			return this.dsa.VerifySignature(array, rgbSignature);
		}

		/// <summary>Verifies the specified signature data by comparing it to the signature computed for the specified hash value.</summary>
		/// <returns>true if the signature verifies as valid; otherwise, false.</returns>
		/// <param name="rgbHash">The hash value of the data to be signed. </param>
		/// <param name="str">The name of the hash algorithm used to create the hash value of the data. </param>
		/// <param name="rgbSignature">The signature data to be verified. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is null.-or- The <paramref name="rgbSignature" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.-or- The signature cannot be verified. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004883 RID: 18563 RVA: 0x001038B2 File Offset: 0x00101AB2
		public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
		{
			if (str == null)
			{
				str = "SHA1";
			}
			if (string.Compare(str, "SHA1", true, CultureInfo.InvariantCulture) != 0)
			{
				throw new CryptographicException(Locale.GetText("Only SHA1 is supported."));
			}
			return this.dsa.VerifySignature(rgbHash, rgbSignature);
		}

		/// <summary>Verifies the <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</summary>
		/// <returns>true if <paramref name="rgbSignature" /> matches the signature computed using the specified hash algorithm and key on <paramref name="rgbHash" />; otherwise, false.</returns>
		/// <param name="rgbHash">The data signed with <paramref name="rgbSignature" />. </param>
		/// <param name="rgbSignature">The signature to be verified for <paramref name="rgbData" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004884 RID: 18564 RVA: 0x001038EE File Offset: 0x00101AEE
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			return this.dsa.VerifySignature(rgbHash, rgbSignature);
		}

		// Token: 0x06004885 RID: 18565 RVA: 0x00103900 File Offset: 0x00101B00
		protected override byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			if (hashAlgorithm != HashAlgorithmName.SHA1)
			{
				throw new CryptographicException(Environment.GetResourceString("'{0}' is not a known hash algorithm.", new object[] { hashAlgorithm.Name }));
			}
			return HashAlgorithm.Create(hashAlgorithm.Name).ComputeHash(data, offset, count);
		}

		// Token: 0x06004886 RID: 18566 RVA: 0x00103950 File Offset: 0x00101B50
		protected override byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			if (hashAlgorithm != HashAlgorithmName.SHA1)
			{
				throw new CryptographicException(Environment.GetResourceString("'{0}' is not a known hash algorithm.", new object[] { hashAlgorithm.Name }));
			}
			return HashAlgorithm.Create(hashAlgorithm.Name).ComputeHash(data);
		}

		// Token: 0x06004887 RID: 18567 RVA: 0x0010399C File Offset: 0x00101B9C
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.persisted && !this.persistKey)
				{
					this.store.Remove();
				}
				if (this.dsa != null)
				{
					this.dsa.Clear();
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x06004888 RID: 18568 RVA: 0x001039DC File Offset: 0x00101BDC
		private void OnKeyGenerated(object sender, EventArgs e)
		{
			if (this.persistKey && !this.persisted)
			{
				this.store.KeyValue = this.ToXmlString(!this.dsa.PublicOnly);
				this.store.Save();
				this.persisted = true;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair.  </summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair.</returns>
		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06004889 RID: 18569 RVA: 0x0000A42E File Offset: 0x0000862E
		[MonoTODO("call into KeyPairPersistence to get details")]
		[ComVisible(false)]
		public CspKeyContainerInfo CspKeyContainerInfo
		{
			[SecuritySafeCritical]
			get
			{
				return null;
			}
		}

		/// <summary>Exports a blob containing the key information associated with a <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object.  </summary>
		/// <returns>A byte array containing the key information associated with a <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object.</returns>
		/// <param name="includePrivateParameters">true to include the private key; otherwise, false.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600488A RID: 18570 RVA: 0x00103A2C File Offset: 0x00101C2C
		[ComVisible(false)]
		[SecuritySafeCritical]
		public byte[] ExportCspBlob(bool includePrivateParameters)
		{
			byte[] array;
			if (includePrivateParameters)
			{
				array = CryptoConvert.ToCapiPrivateKeyBlob(this);
			}
			else
			{
				array = CryptoConvert.ToCapiPublicKeyBlob(this);
			}
			return array;
		}

		/// <summary>Imports a blob that represents DSA key information.</summary>
		/// <param name="keyBlob">A byte array that represents a DSA key blob.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600488B RID: 18571 RVA: 0x00103A50 File Offset: 0x00101C50
		[SecuritySafeCritical]
		[ComVisible(false)]
		public void ImportCspBlob(byte[] keyBlob)
		{
			if (keyBlob == null)
			{
				throw new ArgumentNullException("keyBlob");
			}
			DSA dsa = CryptoConvert.FromCapiKeyBlobDSA(keyBlob);
			if (dsa is DSACryptoServiceProvider)
			{
				DSAParameters dsaparameters = dsa.ExportParameters(!(dsa as DSACryptoServiceProvider).PublicOnly);
				this.ImportParameters(dsaparameters);
				return;
			}
			try
			{
				DSAParameters dsaparameters2 = dsa.ExportParameters(true);
				this.ImportParameters(dsaparameters2);
			}
			catch
			{
				DSAParameters dsaparameters3 = dsa.ExportParameters(false);
				this.ImportParameters(dsaparameters3);
			}
		}

		// Token: 0x040025E9 RID: 9705
		private const int PROV_DSS_DH = 13;

		// Token: 0x040025EA RID: 9706
		private KeyPairPersistence store;

		// Token: 0x040025EB RID: 9707
		private bool persistKey;

		// Token: 0x040025EC RID: 9708
		private bool persisted;

		// Token: 0x040025ED RID: 9709
		private bool privateKeyExportable;

		// Token: 0x040025EE RID: 9710
		private bool m_disposed;

		// Token: 0x040025EF RID: 9711
		private DSAManaged dsa;

		// Token: 0x040025F0 RID: 9712
		private static bool useMachineKeyStore;
	}
}
