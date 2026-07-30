using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) version of the Data Encryption Standard (<see cref="T:System.Security.Cryptography.DES" />) algorithm. This class cannot be inherited.</summary>
	// Token: 0x02000659 RID: 1625
	[ComVisible(true)]
	public sealed class DESCryptoServiceProvider : DES
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DESCryptoServiceProvider" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The Data Encryption Standard (<see cref="T:System.Security.Cryptography.DES" />) cryptographic service provider is not available. </exception>
		// Token: 0x0600461E RID: 17950 RVA: 0x000F6FEC File Offset: 0x000F51EC
		[SecuritySafeCritical]
		public DESCryptoServiceProvider()
		{
			if (!Utils.HasAlgorithm(26113, 0))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptographic service provider (CSP) could not be found for this algorithm."));
			}
			this.FeedbackSizeValue = 8;
		}

		/// <summary>Creates a symmetric Data Encryption Standard (<see cref="T:System.Security.Cryptography.DES" />) encryptor object with the specified key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.DES" /> encryptor object.</returns>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm. </param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> property is <see cref="F:System.Security.Cryptography.CipherMode.OFB" />.-or-The value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> property is <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> and the value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.FeedbackSize" /> property is not 8.-or-An invalid key size was used.-or-The algorithm key size was not available.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600461F RID: 17951 RVA: 0x000F7018 File Offset: 0x000F5218
		[SecuritySafeCritical]
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (DES.IsWeakKey(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Specified key is a known weak key for '{0}' and cannot be used."), "DES");
			}
			if (DES.IsSemiWeakKey(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Specified key is a known semi-weak key for '{0}' and cannot be used."), "DES");
			}
			return new DESTransform(this, true, rgbKey, rgbIV);
		}

		/// <summary>Creates a symmetric Data Encryption Standard (<see cref="T:System.Security.Cryptography.DES" />) decryptor object with the specified key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.DES" /> decryptor object.</returns>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm. </param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> property is <see cref="F:System.Security.Cryptography.CipherMode.OFB" />.-or-The value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> property is <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> and the value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.FeedbackSize" /> property is not 8.-or-An invalid key size was used.-or-The algorithm key size was not available.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004620 RID: 17952 RVA: 0x000F7068 File Offset: 0x000F5268
		[SecuritySafeCritical]
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (DES.IsWeakKey(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Specified key is a known weak key for '{0}' and cannot be used."), "DES");
			}
			if (DES.IsSemiWeakKey(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Specified key is a known semi-weak key for '{0}' and cannot be used."), "DES");
			}
			return new DESTransform(this, false, rgbKey, rgbIV);
		}

		/// <summary>Generates a random key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) to be used for the algorithm.</summary>
		// Token: 0x06004621 RID: 17953 RVA: 0x000F70B8 File Offset: 0x000F52B8
		public override void GenerateKey()
		{
			this.KeyValue = new byte[8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
			while (DES.IsWeakKey(this.KeyValue) || DES.IsSemiWeakKey(this.KeyValue))
			{
				Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
			}
		}

		/// <summary>Generates a random initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) to use for the algorithm.</summary>
		// Token: 0x06004622 RID: 17954 RVA: 0x000F710D File Offset: 0x000F530D
		public override void GenerateIV()
		{
			this.IVValue = new byte[8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.IVValue);
		}
	}
}
