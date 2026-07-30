using System;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Provides methods for encrypting and decrypting data. This class cannot be inherited.</summary>
	// Token: 0x02000018 RID: 24
	public sealed class ProtectedData
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002050 File Offset: 0x00000250
		private ProtectedData()
		{
		}

		/// <summary>Encrypts the data in a specified byte array and returns a byte array that contains the encrypted data.</summary>
		/// <returns>A byte array representing the encrypted data.</returns>
		/// <param name="userData">A byte array that contains data to encrypt. </param>
		/// <param name="optionalEntropy">An optional additional byte array used to increase the complexity of the encryption, or null for no additional complexity.</param>
		/// <param name="scope">One of the enumeration values that specifies the scope of encryption. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="userData" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The encryption failed.</exception>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this method. </exception>
		/// <exception cref="T:System.OutOfMemoryException">The system ran out of memory while encrypting the data.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.DataProtectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ProtectData" />
		/// </PermissionSet>
		// Token: 0x06000052 RID: 82 RVA: 0x00003000 File Offset: 0x00001200
		public static byte[] Protect(byte[] userData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			if (userData == null)
			{
				throw new ArgumentNullException("userData");
			}
			ProtectedData.Check(scope);
			ProtectedData.DataProtectionImplementation dataProtectionImplementation = ProtectedData.impl;
			if (dataProtectionImplementation != ProtectedData.DataProtectionImplementation.Win32CryptoProtect)
			{
				if (dataProtectionImplementation != ProtectedData.DataProtectionImplementation.ManagedProtection)
				{
					goto IL_005E;
				}
				try
				{
					return ManagedProtection.Protect(userData, optionalEntropy, scope);
				}
				catch (Exception ex)
				{
					throw new CryptographicException(Locale.GetText("Data protection failed."), ex);
				}
			}
			try
			{
				return NativeDapiProtection.Protect(userData, optionalEntropy, scope);
			}
			catch (Exception ex2)
			{
				throw new CryptographicException(Locale.GetText("Data protection failed."), ex2);
			}
			IL_005E:
			throw new PlatformNotSupportedException();
		}

		/// <summary>Decrypts the data in a specified byte array and returns a byte array that contains the decrypted data.</summary>
		/// <returns>A byte array representing the decrypted data.</returns>
		/// <param name="encryptedData">A byte array containing data encrypted using the <see cref="M:System.Security.Cryptography.ProtectedData.Protect(System.Byte[],System.Byte[],System.Security.Cryptography.DataProtectionScope)" /> method. </param>
		/// <param name="optionalEntropy">An optional additional byte array that was used to encrypt the data, or null if the additional byte array was not used.</param>
		/// <param name="scope">One of the enumeration values that specifies the scope of data protection that was used to encrypt the data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="encryptedData" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The decryption failed.</exception>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this method. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.DataProtectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnprotectData" />
		/// </PermissionSet>
		// Token: 0x06000053 RID: 83 RVA: 0x00003090 File Offset: 0x00001290
		public static byte[] Unprotect(byte[] encryptedData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			ProtectedData.Check(scope);
			ProtectedData.DataProtectionImplementation dataProtectionImplementation = ProtectedData.impl;
			if (dataProtectionImplementation != ProtectedData.DataProtectionImplementation.Win32CryptoProtect)
			{
				if (dataProtectionImplementation != ProtectedData.DataProtectionImplementation.ManagedProtection)
				{
					goto IL_005E;
				}
				try
				{
					return ManagedProtection.Unprotect(encryptedData, optionalEntropy, scope);
				}
				catch (Exception ex)
				{
					throw new CryptographicException(Locale.GetText("Data unprotection failed."), ex);
				}
			}
			try
			{
				return NativeDapiProtection.Unprotect(encryptedData, optionalEntropy, scope);
			}
			catch (Exception ex2)
			{
				throw new CryptographicException(Locale.GetText("Data unprotection failed."), ex2);
			}
			IL_005E:
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003120 File Offset: 0x00001320
		private static void Detect()
		{
			OperatingSystem osversion = Environment.OSVersion;
			PlatformID platform = osversion.Platform;
			if (platform != PlatformID.Win32NT)
			{
				if (platform != PlatformID.Unix)
				{
					ProtectedData.impl = ProtectedData.DataProtectionImplementation.Unsupported;
					return;
				}
				ProtectedData.impl = ProtectedData.DataProtectionImplementation.ManagedProtection;
				return;
			}
			else
			{
				if (osversion.Version.Major < 5)
				{
					ProtectedData.impl = ProtectedData.DataProtectionImplementation.Unsupported;
					return;
				}
				ProtectedData.impl = ProtectedData.DataProtectionImplementation.Win32CryptoProtect;
				return;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003178 File Offset: 0x00001378
		private static void Check(DataProtectionScope scope)
		{
			if (scope < DataProtectionScope.CurrentUser || scope > DataProtectionScope.LocalMachine)
			{
				throw new ArgumentException(Locale.GetText("Invalid enum value '{0}' for '{1}'.", new object[] { scope, "DataProtectionScope" }), "scope");
			}
			ProtectedData.DataProtectionImplementation dataProtectionImplementation = ProtectedData.impl;
			if (dataProtectionImplementation == ProtectedData.DataProtectionImplementation.Unsupported)
			{
				throw new PlatformNotSupportedException();
			}
			if (dataProtectionImplementation == ProtectedData.DataProtectionImplementation.Unknown)
			{
				ProtectedData.Detect();
				return;
			}
		}

		// Token: 0x040000A3 RID: 163
		private static ProtectedData.DataProtectionImplementation impl;

		// Token: 0x02000019 RID: 25
		private enum DataProtectionImplementation
		{
			// Token: 0x040000A5 RID: 165
			Unknown,
			// Token: 0x040000A6 RID: 166
			Win32CryptoProtect,
			// Token: 0x040000A7 RID: 167
			ManagedProtection,
			// Token: 0x040000A8 RID: 168
			Unsupported = -2147483648
		}
	}
}
